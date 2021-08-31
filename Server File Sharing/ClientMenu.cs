using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_File_Sharing
{
    public partial class ClientMenu : Form
    {
        /// <summary>
        /// Класс логики клиента
        /// </summary>
        public static ClientUse clientUse;
        /// <summary>
        /// Поток взаимодейтвия между клиентом и сервером
        /// </summary>
        public static Thread ClientThread;
        /// <summary>
        /// Поток поиска серверов
        /// </summary>
        public static Thread ClientUDPThread;
        /// <summary>
        /// Настройки приложения
        /// </summary>
        Properties.Settings Config;
        /// <summary>
        /// Всплывающий текст "подсказка" выбранного сервера
        /// </summary>
        ToolTip infoSelectServer = new ToolTip();
        /// <summary>
        /// Всплывающий текст "подсказка" кнопки подключения к серверу
        /// </summary>
        ToolTip infoConnectServer = new ToolTip();
        /// <summary>
        /// Всплывающий текст "подсказка" списка действий
        /// </summary>
        ToolTip infoAction = new ToolTip();
        /// <summary>
        /// Сессии клиента
        /// </summary>
        internal Sessions sessions = new Sessions();
        /// <summary>
        /// Полные пути до выбранных файлов
        /// </summary>
        public List<string> pathFiles = new List<string>();
        /// <summary>
        /// Переменная отмены отправки файлов
        /// </summary>
        public bool canselFile = false;
        /// <summary>
        /// Переменная таймера попытки подключения к серверу
        /// </summary>
        private int timeConnect = 0;
        public ClientMenu()
        {
            Sessions s = new Sessions();
            InitializeComponent();
            Config = new Properties.Settings();
            if (!CheckMac())
            {
                Config.ServerIp = "";
                Config.ServerName = "";
                Config.ServerPathSave = "";
                Config.ServerPort = "";
                Config.ClientIP = "";
                var ipList = ConfigServer.ListIpv4();
                if(ipList.Count > 0)
                    Config.ClientIP = ipList[0];
                ConfigServer.ListIpv4();
                Config.ClientIpServer = "";
                Config.ClientName = "";
                Config.ClientPort = "";
                Config.ClientFavorite = null;
            }
            if (Config.ClientFavorite == null)
                Config.ClientFavorite = new System.Collections.Specialized.StringCollection();
            clientUse = new ClientUse(this);
            cbServer.DropDownStyle = ComboBoxStyle.DropDownList;
            listFiles.Items.Clear();
            listInfoAction.Items.Clear();
            lUserSelectInfo.Text = "";
            conMenuServer.Visible = false;
            setToolText(bParameterExit, "Вернуться");
            setToolText(bExit, "Закрыть сессию");
            setToolText(bParameterOpen, "Настройки");
            setToolText(picInfo, "Выберите сессию", infoSelectServer);
            setToolText(bUpdateServers, "Обновить");
            setToolText(bSaveFavorite, "Добавить сессию в список сохраненых");
            setToolText(bConnectServer, "Статус подключения: нет подключения",infoConnectServer);
        }
        // Созданные функции
        /// <summary>
        /// Проверка на совпадение MAC адреса с настройками приложения
        /// </summary>
        /// <returns></returns>
        private bool CheckMac()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string mac = nic.GetPhysicalAddress().ToString();
                if (mac == Config.MAC)
                    return true;
                else
                {
                    Config.MAC = mac;
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// Установка текста "подсказки" на указанный объект
        /// </summary>
        /// <param name="objectVal">Объект, на который необходимо установить текст "подсказку"</param>
        /// <param name="text">Текст "подсказка"</param>
        private void setToolText(Control objectVal, string text)
        {
            ToolTip tool = new ToolTip();
            tool.SetToolTip(objectVal, text);
        }
        /// <summary>
        ///  Установка нового текста "подсказки" на ToolTip и на указанный объект  
        /// </summary>
        /// <param name="objectVal">>Объект, на который необходимо установить текст "подсказку"</param>
        /// <param name="text">Текст "подсказка"</param>
        /// <param name="tool">Объект ToolTip</param>
        private void setToolText(Control objectVal, string text, ToolTip tool)
        {
            tool.SetToolTip(objectVal, text);
        }
        /// <summary>
        /// Проверка списка файлов на наличие с указанного файла
        /// </summary>
        /// <param name="name">НАзвание файла</param>
        /// <returns>true - такой файл уже есть в списке файлов, false - такого файла нет в списке файлов</returns>
        private bool CheckFileExist(string name)
        {
            foreach (string file in pathFiles)
            {
                if (file == name)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Установка текста в "подсказку" файлов
        /// </summary>
        /// <param name="text">Текст, который необходимо установить</param>
        private void setFileInfo(string text)
        {
            string result = "";
            int count = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (count > 30)
                {
                    count = 0;
                    result = result + text[i] + "\n";
                }
                else
                {
                    result = result + text[i];
                    count++;
                }
            }
            setToolText(listFiles, result);
        }
        /// <summary>
        /// Установка текста в "подсказку" списка действий
        /// </summary>
        /// <param name="text">Текст, который необходимо установить</param>
        public void setInfoAction(string text)
        {
            text = DateTime.Now.ToLongTimeString() + " | " + text;
            if (text.Length > 35)
            {
                string val = text;
                while (val.Length > 35)
                {
                    listInfoAction.Items.Add(val.Substring(0, 35));
                    val = val.Remove(0, 35);
                }
                if (val.Length > 0)
                    listInfoAction.Items.Add(val);
            }
            else
                listInfoAction.Items.Add(text);
            setToolText(listInfoAction, text, infoAction);
        }
        /// <summary>
        /// Асинхронный метод проверки подключения к серверу
        /// </summary>
        private async void CheckConnectingAsync()
        {
            await Task.Run(CheckConnecting);
        }
        /// <summary>
        /// Проверка подключения к серверу
        /// </summary>
        private void CheckConnecting()
        {
            try
            {
                timerConnect.Start();
                while (clientUse.Connecting == ClientUse.ConnectStatus.None)
                {
                    if (timeConnect == 59)
                    {
                        timerConnect.Stop();
                        this.Invoke(new MethodInvoker(() => {
                            setToolText(bConnectServer, "Статус подключения: не удалось подключится к сессии", infoConnectServer);
                        }));
                        timeConnect = 0;
                        return;
                    }
                }
                timerConnect.Stop();
                if (clientUse.Connecting == ClientUse.ConnectStatus.No)
                {
                    this.Invoke(new MethodInvoker(() => {
                        setToolText(bConnectServer, "Статус подключения: Disconnect", infoConnectServer);
                    }));
                }
                else
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        sessions.RemoveAllServer();
                        pConnect.Visible = true;
                        pStart.Visible = false;
                        pParameter.Visible = false;
                        pIpPort.Visible = false;
                        this.MaximumSize = new Size(700, 280);
                        this.MinimumSize = new Size(700, 280);
                        pConnect.Location = new Point(0, 0);
                        lName.Text = clientUse.NameUser;
                    }));
                }
                timeConnect = 0;
            }
            catch { }
        }
        // Автоматически созданные функции
        private void pConnectServer_Click(object sender, EventArgs e)
        {
            try
            {
                if (tName.Text == null || tName.Text.Replace(" ", "").Length <= 0)
                {
                    MessageBox.Show("Введите своё имя", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (!chSelectServer.Checked)
                {
                    int id = cbServer.SelectedIndex;
                    if (id < 0)
                    {
                        MessageBox.Show("Выберите сессию", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string[] dataServer = sessions.getServer(id);
                    if (dataServer == null)
                    {
                        MessageBox.Show("Ошибка при подключении к сессии", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    Config.ClientIpServer = dataServer[1];
                    Config.ClientPort = dataServer[2];
                }
                else
                {
                    if (!ConfigServer.CheckRange(tPort.Text, 2000, 9999))
                    {
                        MessageBox.Show("Диапазон порта должен быть от 2000 до 9999", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!ConfigServer.CheckIpv4(tIPServer.Text))
                    {
                        MessageBox.Show("Ip адрес указан неверно", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Config.ClientIpServer = tIPServer.Text;
                    Config.ClientPort = tPort.Text;
                }
                Config.ClientName = tName.Text;
                clientUse.IPServer = Config.ClientIpServer;
                try
                {
                    clientUse.Port = int.Parse(Config.ClientPort);
                }
                catch
                {
                    MessageBox.Show("Отсутствуют данные для подключения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                clientUse.NameUser = Config.ClientName;
                if (clientUse.IPServer == "" || clientUse.NameUser == "")
                {
                    MessageBox.Show("Отсутствуют данные для подключения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                ClientThread = new Thread(new ThreadStart(clientUse.Connect));
                ClientThread.Start();
                CheckConnectingAsync();
                timerConnect.Enabled = true;
            }
            catch { MessageBox.Show("Ошибка при подключении к сессии", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void bExit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Вы хотите закрыть сесиию", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                Close();
            }
        }
        private void bSaveFavorite_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Вы точно хотите добавить сессию в список сохраненых ?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                foreach (TreeNode val in listUsers.Nodes)
                {
                    if (val.Nodes[0].Text.Remove(0,3) == clientUse.IPServer && val.Nodes[1].Text.Remove(0, 5) == clientUse.Port.ToString())
                    {
                        foreach (string servers in Config.ClientFavorite)
                        {
                            string[] data = servers.Split('|');
                            if (data[0] == val.Text && data[1] == clientUse.IPServer && data[2] == clientUse.Port.ToString())
                            {
                                MessageBox.Show("Эта сессия уже сохранена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        if (val.Text != null || val.Text != "")
                            Config.ClientFavorite.Add(val.Text + "|" + clientUse.IPServer + "|" + clientUse.Port);
                        break;
                    }
                }
            }
        }
        private void timerConnect_Tick(object sender, EventArgs e)
        {
            timeConnect++;
        }
        private void chSelectServer_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                setToolText(picInfo, "Введите Ip (максимальное число 255 минимальное 0)\n" +
                    "Пример 255.255.255.255 или 1.1.1.1 .\n" +
                    "Введите порт от 2000 до 9999", infoSelectServer);
                lSelectServer.Text = "Введите ip и port:";
                pInfo.Visible = false;
                pIpPort.Visible = true;
                pIpPort.Location = new Point(45, 50);
                if (chSelectFavorite.Checked)
                    chSelectFavorite.Checked = false;
            }
            else
            {
                setToolText(picInfo, "Выберите сессию", infoSelectServer);
                lSelectServer.Text = "Доступные сеcсии";
                cbServer.Sorted = true;
                pInfo.Visible = true;
                pIpPort.Visible = false;
            }
        }
        private void bUpdateServers_Click(object sender, EventArgs e)
        {
            if (bParametrUpdate.Text == "Изменить")
            {
                if (int.TryParse(tParametrPort.Text, out int result))
                {
                    if (clientUse.PortUDP != result)
                        clientUse.PortUDP = result;
                }
            }
            else
            {
                bParametrUpdate.Text = "Изменить";
                tParametrPort.Text = clientUse.PortUDP.ToString();
                tParametrPort.Enabled = false;
            }
            bUpdateServers.Enabled = false;
            cbServer.Items.Clear();
            sessions.RemoveAllServer();
            clientUse.DisconnectUDP();
            clientUse.IP = tParametrIp.Text;
            Config.ClientIP = tParametrIp.Text;
            ClientUDPThread = new Thread(new ThreadStart(clientUse.CheckServer));
            ClientUDPThread.Start();
            timerFindServer.Start();
        }
        private void timerFindServer_Tick(object sender, EventArgs e)
        {
            timerFindServer.Stop();
            bUpdateServers.Enabled = true;
        }
        private void cbServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = cbServer.SelectedIndex;
            if (id > -1)
            {
                string[] dataServer = sessions.getServer(id);
                setToolText(picInfo, "IP: " + dataServer[1] + "|Port: " + dataServer[2], infoSelectServer);
            }
            else
                setToolText(picInfo, "Выберите сессию", infoSelectServer);
            clientUse.Connecting = ClientUse.ConnectStatus.None;
            setToolText(bConnectServer, "Статус подключения: нет подключения", infoConnectServer);
        }
        private void listUsers_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node.Level == 0)
                {
                    lUserSelect.Text = e.Node.Text;
                    string ip = e.Node.Nodes[0].Text.Remove(0, 3);
                    string port = e.Node.Nodes[1].Text.Remove(0, 5);
                    lUserSelectInfo.Text = $"IP:{ip} | Port:{port}";
                    sessions.setSeleclClient(ip, int.Parse(port));
                }
            }
            catch { lUserSelect.Text = ""; sessions.setSeleclClient(-1); }
        }
        private void chSelectFavorite_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                sessions.RemoveAllServer();
                cbServer.Items.Clear();
                cbServer.Sorted = false;
                if ((sender as CheckBox).Checked)
                {
                    foreach (string server in Config.ClientFavorite)
                    {
                        string[] val = server.Split('|');
                        sessions.AddServer(val[0], val[1], int.Parse(val[2]));
                        cbServer.Items.Add(val[0]);
                    }
                    if (cbServer.Items.Count > 0)
                        cbServer.SelectedIndex = 0;
                    else
                        setToolText(picInfo, "Выберите сессию", infoSelectServer);
                    bUpdateServers.Visible = false;
                    cbServer.ContextMenuStrip = conMenuServer;
                    if (chSelectServer.Checked)
                        chSelectServer.Checked = false;
                }
                else
                {
                    cbServer.Sorted = true;
                    bUpdateServers.Visible = true;
                    cbServer.ContextMenuStrip = null;
                }
                lSelectServer.Text = "Доступные сеcсии";
            }
            catch { }
        }
        private void toolStripMenuServerDelete_Click(object sender, EventArgs e)
        {
            int id = cbServer.SelectedIndex;
            if (id > -1)
            {
                if (DialogResult.Yes == MessageBox.Show("Вы точно хотите удалить выбранную сессию из сохраненных ?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    sessions.RemoveServer(id);
                    cbServer.Items.RemoveAt(id);
                    Config.ClientFavorite.RemoveAt(id);
                }
            }
        }
        private void ClientMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(tIPServer.Text.Length > 0)
                Config.ClientIpServer = tIPServer.Text;
            Config.ClientPort = tParametrPort.Text;
            Config.ClientName = tName.Text;
            Config.ClientIP = tParametrIp.Text;
            Config.Save();
            if (clientUse != null)
            {
                if (clientUse.download || clientUse.load)
                {
                    if (DialogResult.No == MessageBox.Show("Присходит операция по обмену файлами, хотите прервать её?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                clientUse.DisconnectUDP();
                clientUse.Disconnect();
            }
            this.Dispose(true);
        }
        //Действия с файлами
        private void bFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (pathFiles == null || pathFiles.Count == 0)
                {
                    MessageBox.Show("Выбирете файлы", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    try
                    {
                        int countByte = 0;
                        foreach (string path in pathFiles)
                        {
                            countByte = countByte + (int)File.OpenRead(path).Length;
                        }
                        if ((StartMenu.MaxByteStream - 52428800) < countByte)
                        {
                            MessageBox.Show("Общий вес файлов превышает максимально допустимый, 250 МБ ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Общий вес файлов превышает максимально допустимый, 250 МБ ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (clientUse.load || clientUse.download)
                {
                    MessageBox.Show("У вас уже происходит операция по обмену файлами, чтобы отправить файлы дождитесь окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (sessions.getSelectClinet() > -1)
                {
                    string[] dataClient = sessions.getClient(sessions.getSelectClinet());
                    string files = "";
                    foreach (var val in listFiles.Items)
                    {
                        files = files + val.ToString() + " ";
                    }
                    if (DialogResult.Yes == MessageBox.Show($"Вы точно хотите отправить пользователю {dataClient[0]} \n Файлы: {files}", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        Command cm = new Command();
                        cm.Name = CommandName.FilePreview;
                        cm.Parametr.Add(dataClient[1]);
                        cm.Parametr.Add(dataClient[2]);
                        cm.Parametr.Add(clientUse.NameUser);
                        foreach (var val in listFiles.Items)
                            cm.Parametr.Add(val);
                        clientUse.SendMessage(cm);
                        sessions.StartSessions(dataClient[1], int.Parse(dataClient[2]));
                        setInfoAction($"Ожидание действий от пользователя ({dataClient[0]})");
                        pFile.Enabled = false;
                        bFileCancel.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Выбран не существующий пользователь", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lUserSelect.Text = "";
                    lUserSelectInfo.Text = "";
                }
            }
            catch 
            {
                MessageBox.Show("При попытке отправки файла(ов) произошла ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void bSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Выберите файлы для отправки";
            dialog.CheckFileExists = true;
            dialog.Multiselect = true;
            dialog.ShowDialog();
            for (int i = 0; i < dialog.FileNames.Length; i++)
            {
                if (!CheckFileExist(dialog.FileNames[i]))
                {
                    pathFiles.Add(dialog.FileNames[i]);
                    listFiles.Items.Add(dialog.SafeFileNames[i]);
                }
            }
        }
        private void bFileCancel_Click(object sender, EventArgs e)
        {
            string[] dataClient = sessions.getClient(sessions.getSelectClinet());
            canselFile = true;
            pFile.Enabled = true;
            bFileCancel.Visible = false;
            Command cm = new Command();
            cm.Name = CommandName.FileCansel;
            cm.Parametr.Add(dataClient[1]);
            cm.Parametr.Add(dataClient[2]);
            cm.Parametr.Add(clientUse.NameUser);
            clientUse.SendMessage(cm);
            setInfoAction($"Вы отменили отправку файла(ов) пользователю {dataClient[0]}");
        }
        private void ToolStripMenuFilesDelete_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripItem).Name == "toolStripMenuFilesDeleteAll")
            {
                if (DialogResult.Yes == MessageBox.Show("Вы точно хотите удалить все выбранные файлы ?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    pathFiles.Clear();
                    listFiles.Items.Clear();
                }
            }
            else
            {
                if (listFiles.SelectedIndex >= 0)
                {
                    int idSelect = listFiles.SelectedIndex;
                    pathFiles.RemoveAt(idSelect);
                    listFiles.Items.RemoveAt(idSelect);
                    if (listFiles.Items.Count > 0)
                        listFiles.SelectedIndex = 0;
                }
            }
        }
        private void listFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = listFiles.SelectedIndex;
                if (id > -1)
                {
                    setFileInfo(pathFiles[id]);
                }
            }
            catch { MessageBox.Show("Произошла ошибка выбора файла", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        //Действия с параметрами
        private void bParameterOpen_Click(object sender, EventArgs e)
        {
            pIpPort.Visible = false;
            pStart.Visible = false;
            pParameter.Location = new Point(0, 0);
            pParameter.Visible = true;
        }
        private void bParameterExit_Click(object sender, EventArgs e)
        {
            if (chSelectServer.Checked)
                pIpPort.Visible = true;
            pStart.Visible = true;
            pParameter.Visible = false;
            bParametrUpdate.Text = "Изменить";
            tParametrIp.Enabled = false;
            tParametrPort.Enabled = false;
        }
        private void bParametrUpdate_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            if (bt.Text == "Изменить")
            {
                bt.Text = "Сохранить";
                tParametrIp.Enabled = true;
                tParametrPort.Enabled = true;
            }
            else
            {
                tParametrIp.Enabled = false;
                tParametrPort.Enabled = false;
                bt.Text = "Изменить";
            }
        }
        //Дополнительные проверки
        private void tIP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox val = (TextBox)sender;
                int position = val.SelectionStart;
                string text = val.Text;
                if (text.Length > 0)
                {
                    if (text.Length > 15)
                        text = text.Remove(text.Length - 1, 1);
                    else
                        text = ConfigServer.Mask(text);
                    val.Text = text;
                    if (text.Length > 0)
                    {
                        if (text[text.Length - 1] == '.' && position == text.Length - 2)
                            val.SelectionStart = text.Length + 1;
                        else
                            val.SelectionStart = position;
                    }
                }
            }
            catch { }
        }
        private void tIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (sender == tPort || sender == tParametrPort)
                {
                    if (ConfigServer.CheckRange(e.KeyChar.ToString(), 9) || e.KeyChar == 8)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                else
                {
                    if (ConfigServer.CheckRange(e.KeyChar.ToString(), 9) || e.KeyChar == '.' || e.KeyChar == 8 || e.KeyChar == 22)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
            catch { e.Handled = true; }
        }
        private void tPort_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox box = (TextBox)sender;
                if (!ConfigServer.CheckRange(box.Text, 2000, 9999) && box.Text.Length == 4)
                {
                    box.Text = box.Text.Remove(box.Text.Length - 1, 1);
                }
            }
            catch { }
        }
        private void ClientMenu_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop) && e.Effect == DragDropEffects.Move)
                {
                    string[] s = (string[])e.Data.GetData("FileDrop", false);
                    foreach (string path in s)
                    {
                        if (!CheckFileExist(path))
                        {
                            pathFiles.Add(path);
                            listFiles.Items.Add(path.Remove(0, path.LastIndexOf("\\") + 1));
                        }
                    }
                    if (listFiles.Items.Count > 0)
                        listFiles.SelectedIndex = 0;
                }
            }
            catch { }
        }
        private void ClientMenu_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
