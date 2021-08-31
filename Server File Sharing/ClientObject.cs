using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace Server_File_Sharing
{
    /// <summary>
    /// Класс клиента на сервере
    /// </summary>
    class ClientObject
    {
        /// <summary>
        /// ID подключенного клиента
        /// </summary>
        protected internal string Id { get; private set; }
        /// <summary>
        /// Поток между клиентом и сервером
        /// </summary>
        protected internal NetworkStream Stream { get; set; }
        /// <summary>
        /// Клиент сервера
        /// </summary>
        TcpClient client;
        /// <summary>
        /// Сервер
        /// </summary>
        ServerObject server; // объект сервера
        /// <summary>
        /// IP адрес клиента
        /// </summary>
        protected internal string IpAddress;
        /// <summary>
        /// Порт клиента
        /// </summary>
        protected internal int Port;
        /// <summary>
        /// Наименование клиента
        /// </summary>
        protected internal string NameUser;
        /// <summary>
        /// Переменная скачивания файлов
        /// </summary>
        protected internal bool download = false;
        /// <summary>
        /// Переменная загрузки файлов
        /// </summary>
        protected internal bool load = false;
        /// <summary>
        /// Форма окна сервера
        /// </summary>
        ServerMenu form;
        /// <summary>
        /// Итоговый путь загрузки файлов
        /// </summary>
        private string pathFilesSave = "";
        public ClientObject(TcpClient tcpClient, ServerObject serverObject, ServerMenu form)
        {
            NameUser = "";
            this.form = form;
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            client.ReceiveBufferSize = StartMenu.MaxByteStream;
            client.SendBufferSize = StartMenu.MaxByteStream;
            server = serverObject;
            IpAddress = Convert.ToString(((IPEndPoint)client.Client.RemoteEndPoint).Address);
            Port = ((IPEndPoint)client.Client.RemoteEndPoint).Port;
            serverObject.AddConnection(this);
        }
        /// <summary>
        /// Процесс чтения входящих данных
        /// </summary>
        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                while (true)
                {
                    try
                    {
                        if (Stream.DataAvailable)
                        {
                            //если что-то пришло
                            Command cm = new Command();
                            cm.DeSerial(Stream);
                            CheckCommand(cm);
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            catch { }
            finally
            {
                server.RemoveConnection(this.Id);
            }
        }
        /// <summary>
        /// Отправка команды клиенту
        /// </summary>
        /// <param name="command"> Команда для отправки </param>
        public void SendCommand(Command command)
        {
            if (command != null)
            {
                command.Serial(Stream);
            }
        }
        /// <summary>
        /// Проверка команды на совпадение
        /// </summary>
        /// <param name="command">Команда</param>
        public void CheckCommand(Command command)
        {
            switch (command.Name)
            {
                case CommandName.ConnectToServer:
                    {
                        this.NameUser = command.Parametr[0].ToString();
                        int idItem = server.clients.FindIndex(delegate (ClientObject cl) { return cl.Id == this.Id; });
                        if (idItem > -1)
                        {
                            form.listUsers.Invoke(new Action<string>((tx) => form.listUsers.Nodes[idItem].Text = tx),
                                this.NameUser);
                            int id = form.sessions.FindIndexClient(IpAddress, Port);
                            form.sessions.setClient(id, NameUser, IpAddress, Port);
                        }
                        Command cm = new Command();
                        cm.Name = CommandName.Clients;
                        cm.Parametr.Add(server.Config.getName());
                        cm.Parametr.Add(server.Config.getIP());
                        cm.Parametr.Add(server.Config.getPort());
                        foreach (ClientObject client in server.clients)
                        {
                            cm.Parametr.Add(client.NameUser);
                            cm.Parametr.Add(client.IpAddress);
                            cm.Parametr.Add(client.Port);
                        }
                        foreach (ClientObject client in server.clients)
                        {
                            client.SendCommand(cm);
                        }
                        break;
                    }
                case CommandName.ExitClient:
                    {
                        server.RemoveConnection(this.Id);
                        break;
                    }
                case CommandName.File:
                    {
                        if (ServerOrClient(command.Parametr[0], command.Parametr[1], out ClientObject findClient))
                        {
                            bool statusSessions = false;
                            form.Invoke(new MethodInvoker(() =>
                            {
                                if (form.sessions.getStatusSessions(IpAddress, Port))
                                {
                                    form.sessions.StopSessions(IpAddress, Port);
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
                            cm.Parametr.Add(server.Config.Name);
                            SendCommand(cm);
                        }
                        else
                        {
                            if (findClient != null)
                            {
                                findClient.SendCommand(command.Redirection(IpAddress, Port, 2));
                            }
                        }
                        download = false;
                        break;
                    }
                case CommandName.FilePreview:
                    {
                        try
                        {
                            if (ServerOrClient(command.Parametr[0], command.Parametr[1], out ClientObject findClient))
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
                                            dialog.Description = "Выберите папку куда сохранятся файл(ы)";
                                            dialog.ShowNewFolderButton = true;
                                            dialog.SelectedPath = server.Config.getPathSave();
                                            dialog.ShowDialog();
                                            if (Directory.Exists(dialog.SelectedPath))
                                            {
                                                pathFilesSave = dialog.SelectedPath + "\\";
                                                take = true;
                                                download = true;
                                                form.sessions.StartSessions(IpAddress, Port);
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
                                cm.Parametr.Add(server.Config.getIP());
                                cm.Parametr.Add(server.Config.getPort());
                                cm.Parametr.Add(take);
                                SendCommand(cm);
                            }
                            else
                            {
                                if (findClient != null)
                                {
                                    findClient.SendCommand(command.Redirection(IpAddress, Port, 2));
                                }
                            }
                        }
                        catch { }
                        break;
                    }
                case CommandName.FileResult:
                    {
                        try
                        {
                            if (ServerOrClient(command.Parametr[0], command.Parametr[1], out ClientObject findClient))
                            {
                                bool status = false;
                                form.Invoke(new MethodInvoker(() =>
                                {
                                    status = form.sessions.getStatusSessions(IpAddress, Port);
                                    form.sessions.StopSessions(IpAddress, Port);
                                }));
                                if (status)
                                {
                                    if ((bool)command.Parametr[2])
                                    {
                                        if (form.canselFile)
                                        {
                                            Command cm = new Command();
                                            cm.Name = CommandName.FileCansel;
                                            cm.Parametr.Add(command.Parametr[0]);
                                            cm.Parametr.Add(command.Parametr[1]);
                                            cm.Parametr.Add(server.Config.getName());
                                            SendCommand(cm);
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
                                            SendCommand(cm);
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
                            }
                            else
                            {
                                if (findClient != null)
                                {
                                    load = (bool)command.Parametr[2];
                                    findClient.load = (bool)command.Parametr[2];
                                    findClient.SendCommand(command.Redirection(IpAddress, Port, 2));
                                }
                            }
                        }
                        catch { }
                        break;
                    }
                case CommandName.FileCansel:
                    {
                        download = false;
                        if (ServerOrClient(command.Parametr[0], command.Parametr[1], out ClientObject findClient))
                        {
                            form.Invoke(new MethodInvoker(() =>
                            {
                                form.setInfoAction($"Пользователь {command.Parametr[2]} отменил отправку файлов");
                                form.sessions.StopSessions(IpAddress, Port);
                            }));
                        }
                        else
                        {
                            if (findClient != null)
                            {
                                findClient.SendCommand(command.Redirection(IpAddress, Port, 2));
                            }
                        }
                        break;
                    }
                case CommandName.FileDownload:
                    {
                        load = false;
                        if (ServerOrClient(command.Parametr[0], command.Parametr[1], out ClientObject findClient))
                        {
                            form.Invoke(new MethodInvoker(() =>
                            {
                                form.setInfoAction($"Пользователь { command.Parametr[2]} получил файлы");
                            }));
                        }
                        else
                        {
                            if (findClient != null)
                            {
                                findClient.SendCommand(command.Redirection(IpAddress, Port, 2));
                            }
                        }
                        break;
                    }
            }
        }
        /// <summary>
        /// Запись текста в список действий в потоке формы окна сервера 
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
        /// Проверка адресации команды
        /// </summary>
        /// <param name="IP">IP команды </param>
        /// <param name="Port">Порт команды</param>
        /// <param name="client"> Клиент кому адресованная команда </param>
        /// <returns> true - если команда адресована серверу, false - если команда адресована другому клиенту</returns>
        private bool ServerOrClient(object IP, object Port, out ClientObject client)
        {
            client = null;
            if (server.Config.getIP() == IP.ToString() && server.Config.getPort() == int.Parse(Port.ToString()))
                return true;
            else
            {
                foreach (ClientObject val in server.clients)
                {
                    if (val.IpAddress == IP.ToString() && val.Port == int.Parse(Port.ToString()))
                    {
                        client = val;
                        return false;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Закрытие подключения клиента
        /// </summary>
        protected internal void Close()
        {
            try
            {
                Command cm = new Command();
                cm.Name = CommandName.ExitClient;
                cm.Serial(Stream);
            }
            catch { }
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
