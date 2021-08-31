using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_File_Sharing
{
    /// <summary>
    /// Команды для взаимодействия
    /// </summary>
    public enum CommandName
    {
        None = -1,
        ConnectToServer = 0,
        File = 1,
        FilePreview = 2,
        FileResult = 3,
        FileCansel = 4,
        FileDownload = 5,
        Rename = 6,
        Clients = 7,
        ExitClient = 8,
        ClientsRemove = 9
    }
    /// <summary>
    /// Класс клиента
    /// </summary>
    public class ClientUse
    {
        /// <summary>
        /// Перечисление статусов подключения
        /// </summary>
        public enum ConnectStatus
        {
            None = 0,
            Yes = 1,
            No = 2
        }
        /// <summary>
        /// Форма окна клиента
        /// </summary>
        ClientMenu form;
        /// <summary>
        /// Поток между клиентом и сервером
        /// </summary>
        NetworkStream stream;
        /// <summary>
        /// Клиента TCP
        /// </summary>
        TcpClient client = new TcpClient();
        /// <summary>
        /// Клиента UDP
        /// </summary>
        UdpClient udpClient;
        /// <summary>
        /// Ip адрес клиента
        /// </summary>
        protected internal string IP = "";
        /// <summary>
        /// Ip адрес сервера
        /// </summary>
        protected internal string IPServer = "";
        /// <summary>
        /// Наименование клиента
        /// </summary>
        protected internal string NameUser = "";
        /// <summary>
        /// Порт сервера
        /// </summary>
        protected internal int Port = 8900;
        /// <summary>
        /// Порт для поиска сервера
        /// </summary>
        protected internal int PortUDP = 8900;
        /// <summary>
        /// Статус подключения к серверу
        /// </summary>
        protected internal ConnectStatus Connecting = ConnectStatus.None;
        /// <summary>
        /// Переменная скачивания файлов
        /// </summary>
        protected internal bool download = false;
        /// <summary>
        /// Переменная загрузки файлов
        /// </summary>
        protected internal bool load = false;
        /// <summary>
        /// Переменная работы подключения к серверу по UDP
        /// </summary>
        private bool alive;
        /// <summary>
        /// Итоговый путь загрузки файлов
        /// </summary>
        private string pathFilesSave = "";
        public ClientUse(ClientMenu form) { this.form = form; }
        /// <summary>
        /// Поиск доступного сервера по UDP
        /// </summary>
        protected internal void CheckServer()
        {
            try
            {
                udpClient = new UdpClient(new IPEndPoint(IPAddress.Parse(IP), PortUDP - 1));
                udpClient.EnableBroadcast = true;
                Task receiveTask = new Task(FindServer);
                receiveTask.Start();
                SendMessageUDP();
            }
            catch { DisconnectUDP(); }
        }
        /// <summary>
        /// Ожидание ответа от серверов
        /// </summary>
        public void FindServer()
        {
            alive = true;
            try
            {
                while (alive)
                {
                    if (udpClient == null)
                        break;
                    IPEndPoint remoteIp = null;
                    byte[] data = udpClient.Receive(ref remoteIp);
                    string mes = Encoding.Unicode.GetString(data);
                    if (mes.Contains("yes I am a server"))
                    {
                        string[] values = mes.Split('|');
                        string ipServer = values[1];
                        string nameServer = values[2];
                        form.cbServer.Invoke(new MethodInvoker(() =>
                        {
                            int id = form.sessions.FindIndexServer(ipServer);
                            if (id == -1)
                            {
                                form.cbServer.Items.Add(nameServer);
                                form.sessions.AddServer(nameServer, ipServer, PortUDP);
                            }
                            if (form.cbServer.Items.Count > 0)
                                form.cbServer.SelectedIndex = 0;
                        }));
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                if (!alive)
                    return;
                throw;
            }
            catch { }
            finally { DisconnectUDP(); }
        }
        /// <summary>
        /// Отправка команды по UDP
        /// </summary>
        public void SendMessageUDP()
        {
            if (udpClient != null && udpClient.Client != null)
            {
                byte[] reply = Encoding.Unicode.GetBytes("are you a server?");
                udpClient.Send(reply, reply.Length, new IPEndPoint(IPAddress.Broadcast, PortUDP - 1));
            }
        }
        /// <summary>
        /// Завершение прослушивания потока
        /// </summary>
        public void DisconnectUDP()
        {
            alive = false;
            if (udpClient != null)
            {
                udpClient.Close();
                udpClient = null;
            }
        }
        /// <summary>
        /// Подключение к серверу по TCP
        /// </summary>
        protected internal void Connect()
        {
            try
            {
                client.Connect(IPAddress.Parse(IPServer), Port);
                client.ReceiveBufferSize = StartMenu.MaxByteStream;
                client.SendBufferSize = StartMenu.MaxByteStream;
                stream = client.GetStream();
                Connecting = ConnectStatus.Yes;
                Start();
            }
            catch { Connecting = ConnectStatus.No; }
        }
        /// <summary>
        /// Отправка команды об подключении серверу и запуск потока прослушивания по TCP
        /// </summary>
        public void Start()
        {
            try
            {
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
                Command cm = new Command();
                cm.Name = CommandName.ConnectToServer;
                cm.Parametr.Add(NameUser);
                SendMessage(cm);
            }
            catch { Connecting = ConnectStatus.No; }
        }
        /// <summary>
        /// Отправка команды серверу по TCP
        /// </summary>
        /// <param name="command"> Команда для отправки</param>
        public void SendMessage(Command command)
        {
            try
            {
                command.Serial(stream);
            }
            catch { }
        }
        /// <summary>
        /// Ожидание входящих команд
        /// </summary>
        public void ReceiveMessage()
        {
            try
            {
                while (true)
                {
                    Command command = new Command();
                    command.DeSerial(stream);
                    CheckCommand(command);
                }
            }
            catch { }
            finally { Disconnect(); }
        }
        /// <summary>
        /// Проверка команды на совпадение
        /// </summary>
        /// <param name="command">Команда</param>
        public void CheckCommand(Command command)
        {
            switch (command.Name)
            {
                case CommandName.ExitClient:
                    {
                        form.Invoke(new MethodInvoker(() =>
                        {
                            MessageBox.Show("Сессия была завершена хостом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            form.Close();
                        }));
                        break;
                    }
                case CommandName.File:
                    {
                        bool statusSessions = false;
                        form.Invoke(new MethodInvoker(() =>
                        {
                            if (form.sessions.getStatusSessions(command.Parametr[0].ToString(), int.Parse(command.Parametr[1].ToString())))
                            {
                                form.sessions.StopSessions(command.Parametr[0].ToString(), int.Parse(command.Parametr[1].ToString()));
                                statusSessions = true;
                            }
                        }));
                        if (statusSessions)
                        {
                            FormInvokAction("Идет скачивание файлов");
                            FormInvokAction("Скачивание 10%");
                            string errorFile = command.ReadFile(pathFilesSave, 2);
                            FormInvokAction("Скачивание 90%");
                            form.Invoke(new MethodInvoker(() =>
                            {
                                if (errorFile != null && errorFile.Length > 0)
                                {
                                    MessageBox.Show("Файлы, которые не удалось принять" + errorFile.Substring(0, errorFile.Length - 2)
                                        , "Ошибка принятия файла(ов)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                form.setInfoAction("Скачивание файлов завершилось");
                            }));
                        }
                        Command cm = new Command();
                        cm.Name = CommandName.FileDownload;
                        cm.Parametr.Add(command.Parametr[0]);
                        cm.Parametr.Add(command.Parametr[1]);
                        cm.Parametr.Add(NameUser);
                        SendMessage(cm);
                        download = false;
                        break;
                    }
                case CommandName.Clients:
                    {
                        IPEndPoint local = (client.Client.LocalEndPoint as IPEndPoint);
                        form.Invoke(new MethodInvoker(() =>
                        {
                            form.listUsers.Nodes.Clear();
                            form.sessions.RemoveAllClient();
                            for (int i = 0; i < command.Parametr.Count; i = i + 3)
                            {
                                if (local.Address.ToString() == command.Parametr[i + 1].ToString() && local.Port == int.Parse(command.Parametr[i + 2].ToString()))
                                { }
                                else
                                {
                                    TreeNode node = new TreeNode();
                                    node.Text = command.Parametr[i].ToString();
                                    node.Nodes.Add(new TreeNode() { Text = "IP:" + command.Parametr[i + 1].ToString() });
                                    node.Nodes.Add(new TreeNode() { Text = "Port:" + command.Parametr[i + 2].ToString() });
                                    form.listUsers.Nodes.Add(node);
                                    form.sessions.AddClient(
                                        command.Parametr[i].ToString(),
                                        command.Parametr[i + 1].ToString(),
                                        int.Parse(command.Parametr[i + 2].ToString())
                                        );
                                }
                            }
                        }));
                        break;
                    }
                case CommandName.ClientsRemove:
                    {
                        string ip = command.Parametr[0].ToString();
                        int port = int.Parse(command.Parametr[1].ToString());
                        form.Invoke(new MethodInvoker(() =>
                        {
                            foreach (TreeNode node in form.listUsers.Nodes)
                            {
                                if (node.Nodes[0].Text == "IP:" + ip && node.Nodes[1].Text == "Port:" + port.ToString())
                                {
                                    if (form.lUserSelect.Text == node.Text)
                                    {
                                        form.lUserSelectInfo.Text = "";
                                        form.lUserSelect.Text = "";
                                    }
                                    form.listUsers.Nodes.Remove(node);
                                    form.sessions.RemoveClient(ip, port);
                                    break;
                                }
                            }
                        }));
                        break;
                    }
                case CommandName.FilePreview:
                    {
                        try
                        {
                            bool take = false;
                            string nameFiles = "";
                            pathFilesSave = Application.StartupPath + "\\";
                            for (int i = 3; i < command.Parametr.Count; i++)
                            {
                                nameFiles = nameFiles + command.Parametr[i] + " ";
                            }
                            form.Invoke(new MethodInvoker(() =>
                            {
                                while (true)
                                {
                                    if (DialogResult.Yes ==
                                        MessageBox.Show("Пользователь , " + command.Parametr[2].ToString() + " , отправил вам файл(ы), хотите их принять? Файлы:" + nameFiles,
                                        "Получение файла(ов)", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                                    {
                                        FolderBrowserDialog dialog = new FolderBrowserDialog();
                                        dialog.Description = "Выберите папку куда сохранятся файлы";
                                        dialog.ShowNewFolderButton = true;
                                        dialog.ShowDialog();
                                        if (Directory.Exists(dialog.SelectedPath))
                                        {
                                            pathFilesSave = dialog.SelectedPath + "\\";
                                            take = true;
                                            download = true;
                                            form.sessions.StartSessions(command.Parametr[0].ToString(), int.Parse(command.Parametr[1].ToString()));
                                            break;
                                        }
                                        else
                                            MessageBox.Show("Вы не выбрали каталог", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                        break;
                                }
                                form.setInfoAction("Вам отправили файлы, выберите действие");
                            }));
                            Command cm = new Command();
                            cm.Name = CommandName.FileResult;
                            cm.Parametr.Add(command.Parametr[0]);
                            cm.Parametr.Add(command.Parametr[1]);
                            cm.Parametr.Add(take);
                            SendMessage(cm);
                        }
                        catch { }
                        break;
                    }
                case CommandName.FileResult:
                    {
                        bool status = false;
                        form.Invoke(new MethodInvoker(() =>
                        {
                            status = form.sessions.getStatusSessions(command.Parametr[0].ToString(), int.Parse(command.Parametr[1].ToString()));
                            form.sessions.StopSessions(command.Parametr[0].ToString(), int.Parse(command.Parametr[1].ToString()));
                        }));
                        if (status)
                        {
                            try
                            {
                                if ((bool)command.Parametr[2])
                                {
                                    if (form.canselFile)
                                    {
                                        Command cm = new Command();
                                        cm.Name = CommandName.FileCansel;
                                        cm.Parametr.Add(command.Parametr[0]);
                                        cm.Parametr.Add(command.Parametr[1]);
                                        cm.Parametr.Add(NameUser);
                                        SendMessage(cm);
                                    }
                                    else
                                    {
                                        load = true;
                                        Command cm = new Command();
                                        cm.Name = CommandName.File;
                                        cm.Parametr.Add(command.Parametr[0]);
                                        cm.Parametr.Add(command.Parametr[1]);
                                        FormInvokAction("Пользователь принял отправленые файлы");
                                        FormInvokAction("Идет отправка файлов");
                                        FormInvokAction("Отправка 10%");
                                        string errorFile = cm.WriteFile(form.pathFiles);
                                        FormInvokAction("Отправка 50%");
                                        form.Invoke(new MethodInvoker(() =>
                                        {
                                            if (errorFile != null && errorFile.Length > 0)
                                            {
                                                MessageBox.Show("Файлы, которые не удалось отправить" + errorFile.Substring(0, errorFile.Length - 2)
                                                    , "Ошибка отправки файла(ов)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                            form.pathFiles.Clear();
                                            form.listFiles.Items.Clear();
                                            form.bFileCancel.Visible = false;
                                            form.pFile.Enabled = true;
                                        }));
                                        SendMessage(cm);
                                        FormInvokAction("Отправка 90%");
                                        cm = null;
                                        FormInvokAction("Отправка файлов завершилась, ожидание ответа");
                                    }
                                }
                                else
                                {
                                    form.Invoke(new MethodInvoker(() =>
                                    {
                                        form.setInfoAction("Пользователь отказался принимать отправленые файлы");
                                        form.bFileCancel.Visible = false;
                                        form.pFile.Enabled = true;
                                    }));
                                }
                            }
                            catch { }
                        }
                        break;
                    }
                case CommandName.FileCansel:
                    {
                        download = false;
                        form.Invoke(new MethodInvoker(() =>
                        {
                            form.setInfoAction($"Пользователь {command.Parametr[2]} отменил отправку файлов");
                            form.sessions.StopSessions(command.Parametr[0].ToString(), int.Parse(command.Parametr[1].ToString()));
                        }));
                        break;
                    }
                case CommandName.FileDownload:
                    {
                        load = false;
                        form.Invoke(new MethodInvoker(() =>
                        {
                            form.setInfoAction($"Пользователь {command.Parametr[2]} получил файлы");
                        }));
                        break;
                    }
            }
        }
        /// <summary>
        /// Запись текста в список действий в потоке формы окна клиента
        /// </summary>
        /// <param name="text">Текст для записи</param>
        private void FormInvokAction(string text)
        {
            form.Invoke(new MethodInvoker(() =>
            {
                form.setInfoAction(text);
            }));
        }
        /// <summary>
        /// Завершение подключения клиента к серверу
        /// </summary>
        public void Disconnect()
        {
            try
            {
                Command cm = new Command();
                cm.Name = CommandName.ExitClient;
                SendMessage(cm);
            }
            catch { }
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
        }
    }
    /// <summary>
    /// Класс команды
    /// </summary>
    [Serializable]
    public class Command
    {
        /// <summary>
        /// Тип команды
        /// </summary>
        public CommandName Name;
        /// <summary>
        /// Описание команды
        /// </summary>
        public string Description;
        /// <summary>
        /// Параметры команды
        /// </summary>
        public List<object> Parametr;
        /// <summary>
        /// Секретный ключ шифровки данных
        /// </summary>
        private byte[] SecretKey = { 117, 122, 124, 16, 207 };
        public Command()
        {
            Name = CommandName.None;
            Description = "";
            Parametr = new List<object>();
        }
        /// <summary>
        /// Сериализация команды в поток
        /// </summary>
        /// <param name="stream"> Поток, куда надо сериализовать команду</param>
        public void Serial(NetworkStream stream)
        {
            if (stream == null)
                return;
            try
            {
                MemoryStream memory = new MemoryStream();
                BinaryFormatter binary = new BinaryFormatter();
                binary.Serialize(memory, this);
                var nonce = new byte[4];
                using (var randomGenerator = new RNGCryptoServiceProvider())
                    randomGenerator.GetBytes(nonce);
                var cryptText = nonce.Concat(EncryptDecrypt(SecretKey, nonce, memory.ToArray()));
                memory.Close();
                memory.Dispose();
                binary.Serialize(stream, cryptText.ToArray());
            }
            catch { stream.Close(); }
        }
        /// <summary>
        /// Десериализация команды из потока
        /// </summary>
        /// <param name="stream">Поток, откуда надо десериализовать команду </param>
        public void DeSerial(NetworkStream stream)
        {
            if (stream == null)
                return;
            while (stream.DataAvailable)
            {
                try
                {
                    BinaryFormatter binary = new BinaryFormatter();
                    byte[] cryptText = (byte[])binary.Deserialize(stream);
                    var decripted = EncryptDecrypt(keyBytes: SecretKey, nonceBytes: cryptText.Take(4).ToArray(), data: cryptText.Skip(4));
                    MemoryStream memory = new MemoryStream(decripted.ToArray());
                    Command command = (Command)binary.Deserialize(memory);
                    this.Name = command.Name;
                    this.Parametr = command.Parametr;
                    this.Description = command.Description;
                    memory.Close();
                    memory.Dispose();
                }
                catch { stream.Close(); }
            }
        }
        /// <summary>
        /// Записывает в параметры команды файлы
        /// </summary>
        /// <param name="arrayPathFile">Список путей до файлов</param>
        /// <returns> Возвращает текст с названием файлов, которые не удалось записать в команду</returns>
        public string WriteFile(List<string> arrayPathFile)
        {
            try
            {
                string errorFile = "";
                foreach (string file in arrayPathFile)
                {
                    if (File.Exists(file))
                    {
                        try
                        {
                            byte[] fileByte = File.ReadAllBytes(file);
                            Parametr.Add(getNameFile(file));
                            Parametr.Add(fileByte);
                        }
                        catch { errorFile = errorFile + getNameFile(file) + " , "; }
                    }
                    else errorFile = errorFile + getNameFile(file) + " , ";
                }
                return errorFile;
            }
            catch { return null; }
        }
        /// <summary>
        /// Создает файлы из данных команды
        /// </summary>
        /// <param name="pathDirectory"> Путь создания файлов</param>
        /// <param name="startPosition"> Позиция для чтения файлов из команды</param>
        /// <returns> Возвращает текст с названием файлов, которые не удалось создать из команды </returns>
        public string ReadFile(string pathDirectory, int startPosition)
        {
            try
            {
                string errorFiles = "";
                for (int i = startPosition; i < Parametr.Count; i = i + 2)
                {
                    try
                    {
                        string path = pathDirectory + "\\" + Parametr[i].ToString();
                        File.WriteAllBytes(path, (byte[])Parametr[i + 1]);
                    }
                    catch { errorFiles = errorFiles + Parametr[i].ToString() + " , "; }
                }
                return errorFiles;
            }
            catch { return null; }
        }
        /// <summary>
        /// Возвращает название файла с расширением
        /// </summary>
        /// <param name="pathFile"> Полный путь до файла</param>
        /// <returns> Название файла с расширением</returns>
        private string getNameFile(string pathFile)
        {
            return pathFile.Remove(0, pathFile.LastIndexOf(@"\")).Replace(@"\", "");
        }
        /// <summary>
        /// Переадресует команду изменяя ip адрес и порт 
        /// </summary>
        /// <param name="ipClient">Ip адрес пользователя</param>
        /// <param name="Port">Порт пользователя</param>
        /// <param name="startPosition"> Позиция для чтения файлов из команды</param>
        /// <returns> Возвращает измененную команду</returns>
        public Command Redirection(string ipClient, int Port, int startPosition)
        {
            try
            {
                Command command = new Command();
                command.Name = this.Name;
                command.Parametr.Add(ipClient);
                command.Parametr.Add(Port);
                for (int i = startPosition; i < this.Parametr.Count; i++)
                    command.Parametr.Add(this.Parametr[i]);
                return command;
            }
            catch { return null; }
        }
        /// <summary>
        /// Функция шифровки данных
        /// </summary>
        /// <param name="keyBytes"> Ключ для шифрования</param>
        /// <param name="nonceBytes"> Nonce</param>
        /// <param name="data"> Данные</param>
        /// <returns> Возвращает зашифрованные данные </returns>
        private static IEnumerable<byte> EncryptDecrypt(byte[] keyBytes, byte[] nonceBytes, IEnumerable<byte> data)
        {
            if (keyBytes == null) throw new ArgumentNullException(nameof(keyBytes));
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (nonceBytes == null) throw new ArgumentNullException(nameof(nonceBytes));
            if (nonceBytes.Length < 4) throw new ArgumentOutOfRangeException(nameof(nonceBytes));
            int roundIndex = 0;
            byte[] roundGamma = null;
            int gammaIndex = 0;
            foreach (var d in data)
            {
                if (gammaIndex == 0)
                {
                    var counterBlock = nonceBytes.Concat(BitConverter.GetBytes(roundIndex)).ToArray();
                    using (var hmacSHA = new HMACSHA512(keyBytes))
                        roundGamma = hmacSHA.ComputeHash(counterBlock);
                }
                yield return (byte)(d ^ roundGamma[gammaIndex]);
                if (gammaIndex < roundGamma.Length - 1)
                    gammaIndex++;
                else
                {
                    gammaIndex = 0;
                    roundIndex++;
                }
            }
        }
    }
    /// <summary>
    /// Класс сессий
    /// </summary>
    public class Sessions
    {
        /// <summary>
        /// Данные клиента сессии
        /// </summary>
        private protected struct ClientServers
        {
            /// <summary>
            /// Наименование
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Ip адрес
            /// </summary>
            public string Ip { get; set; }
            /// <summary>
            /// Порт
            /// </summary>
            public int Port { get; set; }
            /// <summary>
            /// Статус работы сессии
            /// </summary>
            public bool StatusSession { get; set; }
        }
        /// <summary>
        /// Пользователи сессии
        /// </summary>
        List<ClientServers> clients;
        /// <summary>
        /// Сервера сессии
        /// </summary>
        List<ClientServers> servers;
        /// <summary>
        /// Выбранный пользователь
        /// </summary>
        int SelectClient { get; set; }
        public Sessions()
        {
            clients = new List<ClientServers>();
            servers = new List<ClientServers>();
            SelectClient = -1;
        }
        /// <summary>
        /// Добавление сервера в список серверов сессии
        /// </summary>
        /// <param name="name">Название сервера</param>
        /// <param name="ipAddress">Ip адрес сервера</param>
        /// <param name="port">Порт сервера</param>
        public void AddServer(string name, string ipAddress, int port)
        {
            servers.Add(new ClientServers() { Name = name, Ip = ipAddress, Port = port });
        }
        /// <summary>
        /// Удаление сервера по порядковому номеру из списка серверов сессии 
        /// </summary>
        /// <param name="position">Порядковый номер сервера </param>
        public void RemoveServer(int position)
        {
            try
            {
                servers.RemoveAt(position);
            }
            catch { }
        }
        /// <summary>
        /// Удаление всех серверов из списка серверов сессии 
        /// </summary>
        public void RemoveAllServer()
        {
            servers.Clear();
        }
        /// <summary>
        /// Возвращает массив данных об указанном сервере  
        /// </summary>
        /// <param name="position"> Порядковый номер сервера</param>
        /// <returns> Массив данных сервера</returns>
        public string[] getServer(int position)
        {
            try
            {
                string[] serverData = new string[3];
                serverData[0] = servers[position].Name;
                serverData[1] = servers[position].Ip;
                serverData[2] = servers[position].Port.ToString();
                return serverData;
            }
            catch { return null; }
        }
        /// <summary>
        /// Поиск сервера по Ip адресу
        /// </summary>
        /// <param name="ipAddress"> Ip адрес</param>
        /// <returns> Порядковый номер сервера </returns>
        public int FindIndexServer(string ipAddress)
        {
            int id = servers.FindIndex(delegate (ClientServers cl)
            {
                if (cl.Ip == ipAddress)
                    return true;
                else
                    return false;
            });
            return id;
        }
        /// <summary>
        /// Добавление пользователя в список пользователей сессии
        /// </summary>
        /// <param name="name">Название пользователя</param>
        /// <param name="ipAddress">Ip адрес пользователя</param>
        /// <param name="port">Порт пользователя</param>
        public void AddClient(string name, string ipAddress, int port)
        {
            clients.Add(new ClientServers() { Name = name, Ip = ipAddress, Port = port, StatusSession = false });
        }
        /// <summary>
        /// Удаление пользователя по Ip адресу и порту из списка пользователя сессии 
        /// </summary>
        /// <param name="ipAddress">Ip адрес пользователя </param>
        /// <param name="port">Порт пользователя</param>
        /// <returns> true - удаление прошло успешно, false - не удалось удалить пользователя </returns>
        public bool RemoveClient(string ipAddress, int port)
        {
            int id = FindIndexClient(ipAddress, port);
            if (id > -1)
            {
                if (RemoveClient(id))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        /// Удаление пользователя по порядковому номеру из списка пользователя сессии 
        /// </summary>
        /// <param name="position">Порядковый номер пользователя</param>
        /// <returns>true - удаление прошло успешно, false - не удалось удалить пользователя</returns>
        public bool RemoveClient(int position)
        {
            try
            {
                if (SelectClient == position)
                    SelectClient = -1;
                clients.RemoveAt(position);
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// Удаление всех пользователей из списка пользователей сессии 
        /// </summary>
        public void RemoveAllClient()
        {
            SelectClient = -1;
            clients.Clear();
        }
        /// <summary>
        /// Установка выбранного пользователя при помощи Ip адреса и порта
        /// </summary>
        /// <param name="ipAddress">Ip адрес пользователя </param>
        /// <param name="port">Порт пользователя</param>
        public void setSeleclClient(string ipAddress, int port)
        {
            setSeleclClient(FindIndexClient(ipAddress, port));
        }
        /// <summary>
        /// Установка выбранного пользователя при помощи порядковый номера
        /// </summary>
        /// <param name="value">Порядковый номер пользователя</param>
        public void setSeleclClient(int value)
        {
            SelectClient = value;
        }
        /// <summary>
        /// Возврат порядкового номера выбранноого пользователя
        /// </summary>
        /// <returns> Порядкового номера выбранноого пользователя</returns>
        public int getSelectClinet()
        {
            return SelectClient;
        }
        /// <summary>
        /// Возвращает массив данных об указанном пользователе
        /// </summary>
        /// <param name="position"> Порядковый номер пользователя</param>
        /// <returns> Массив данных пользователя</returns>
        public string[] getClient(int position)
        {
            try
            {
                string[] clientData = new string[3];
                clientData[0] = clients[position].Name;
                clientData[1] = clients[position].Ip;
                clientData[2] = clients[position].Port.ToString();
                return clientData;
            }
            catch { return null; }
        }
        /// <summary>
        /// Установка новых данных пользователя по порядковому номеру
        /// </summary>
        /// <param name="position"> Порядковый номер пользователя</param>
        /// <param name="name">Наименование пользователя</param>
        /// <param name="ipAddress">Ip адрес пользователя</param>
        /// <param name="port">Порт пользователя</param>
        public void setClient(int position, string name, string ipAddress, int port)
        {
            setClient(position, name, ipAddress, port, false);
        }
        /// <summary>
        /// Установка новых данных пользователя по порядковому номеру
        /// </summary>
        /// <param name="position"> Порядковый номер пользователя</param>
        /// <param name="name">Наименование пользователя</param>
        /// <param name="ipAddress">Ip адрес пользователя</param>
        /// <param name="port">Порт пользователя</param>
        /// <param name="status">Статус работы сессии</param>
        public void setClient(int position, string name, string ipAddress, int port, bool status)
        {
            try
            {
                ClientServers client = clients[position];
                client.Name = name;
                client.Ip = ipAddress;
                client.Port = port;
                client.StatusSession = status;
                clients[position] = client;
            }
            catch { }
        }
        /// <summary>
        /// Возвращает статус сессии по Ip адресу и порту пользователя
        /// </summary>
        /// <param name="ipAddress">>Ip адрес пользователя</param>
        /// <param name="port">Порт пользователя</param>
        /// <returns>Статус сессии</returns>
        public bool getStatusSessions(string ipAddress, int port)
        {
            int id = FindIndexClient(ipAddress, port);
            if (id > -1)
                return clients[id].StatusSession;
            else
                return false;
        }
        /// <summary>
        /// Запускает сессию по Ip адресу и порту пользователя
        /// </summary>
        /// <param name="ipAddress">Ip адрес пользователя</param>
        /// <param name="port">Порт пользователя</param>
        /// <returns>true - если удалось запустить сессию, false - если не удалось запустить сессию </returns>
        public bool StartSessions(string ipAddress, int port)
        {
            int id = FindIndexClient(ipAddress, port);
            if (id > -1)
            {
                if (StartSession(id))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        ///  Запускает сессию по порядковому номеру пользователя
        /// </summary>
        /// <param name="position">Порядковый номер пользователя</param>
        /// <returns> >true - если удалось запустить сессию, false - если не удалось запустить сессию </returns>
        public bool StartSession(int position)
        {
            try
            {
                ClientServers client = clients[position];
                if (client.StatusSession == false)
                {
                    client.StatusSession = true;
                    clients[position] = client;
                    return true;
                }
                else
                    return false;
            }
            catch { return false; }
        }
        /// <summary>
        /// Останавливает сессию по Ip адресу и порту пользователя
        /// </summary>
        /// <param name="ipAddress">Ip адрес пользователя</param>
        /// <param name="port">Порт пользователя</param>
        /// <returns>true - если удалось остановить сессию, false - если не удалось остановить сессию </returns>
        public bool StopSessions(string ipAddress, int port)
        {
            int id = FindIndexClient(ipAddress, port);
            if (id > -1)
            {
                if (StopSession(id))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        /// <summary>
        /// Останавливает сессию по порядковому номеру пользователя
        /// </summary>
        /// <param name="position">Порядковый номер пользователя</param>
        /// <returns>true - если удалось остановить сессию, false - если не удалось остановить сессию</returns>
        public bool StopSession(int position)
        {
            try
            {
                ClientServers client = clients[position];
                if (client.StatusSession == true)
                {
                    client.StatusSession = false;
                    clients[position] = client;
                    return true;
                }
                else
                    return false;
            }
            catch { return false; }
        }
        /// <summary>
        /// Поиск порядкового номера по Ip адресу и порту пользователя
        /// </summary>
        /// <param name="ipAddress">Ip адрес пользователя</param>
        /// <param name="port">Порт пользователя</param>
        /// <returns>Порядковый номер пользователя, -1 - если пользователь не найден</returns>
        public int FindIndexClient(string ipAddress, int port)
        {
            int id = clients.FindIndex(delegate (ClientServers cl)
            {
                if (cl.Ip == ipAddress && cl.Port == port)
                    return true;
                else
                    return false;
            });
            return id;
        }
    }
}
