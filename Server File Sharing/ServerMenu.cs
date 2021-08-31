using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Server_File_Sharing
{
    public partial class ServerMenu : Form
    {
        /// <summary>
        /// Конфигурация сервера
        /// </summary>
        public static ConfigServer config;
        /// <summary>
        /// Основной поток сервера
        /// </summary>
        public static Thread Thread;
        /// <summary>
        /// Поток для поиска подключений
        /// </summary>
        public static Thread UDPThread;
        /// <summary>
        /// Всплывающий текст "подсказка" списка действий
        /// </summary>
        ToolTip infoAction = new ToolTip();
        /// <summary>
        /// Класс логики сервера
        /// </summary>
        static ServerObject server;
        /// <summary>
        /// Полные пути до выбранных файлов
        /// </summary>
        public List<string> pathFiles = new List<string>();
        /// <summary>
        /// Переменная отображения текста об настройках сервера 
        /// </summary>
        private bool clickButtonInfo = false;
        /// <summary>
        /// Переменная отмены отправки файлов
        /// </summary>
        public bool canselFile = false;
        /// <summary>
        /// Сессии сервера
        /// </summary>
        internal Sessions sessions = new Sessions();
        public ServerMenu()
        {
            config = new ConfigServer();
            InitializeComponent();
            listFiles.Items.Clear();
            listInfoAction.Items.Clear();
            lClientSelectInfo.Text = "";
            lInfoParametr.Text = $"Ip address:{config.getIP()} / Port :{config.getPort()}";
            tPath.Text = @"C:\Users\fura3\OneDrive\Поекты\файлы сервера";
            setToolText(bParameterExit, "Вернуться");
            setToolText(bExit, "Закрыть сессию");
            setToolText(bParameterOpen, "Настройки");
            setToolText(bParameterSave, "Сохранить");
            setToolText(bInfo, "Информация о сессии");
        }
        // Созданные функции
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
                if (count > 50)
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
                while(val.Length > 35)
                {
                    listInfoAction.Items.Add(val.Substring(0,35));
                    val = val.Remove(0, 35);
                }
                if (val.Length > 0)
                    listInfoAction.Items.Add(val);
            }
            else
                listInfoAction.Items.Add(text);
            setToolText(listInfoAction, text , infoAction);
        }
        //Автоматически созданные функции
        private void bExit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Вы хотите закрыть сесиию", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                Close();
        }
        private void bStart_Click(object sender, EventArgs e)
        {
            if (!config.getRun())
            {
                server = new ServerObject(this, config);
                if (tName.Text == null || tName.Text.Replace(" ", "").Length <= 0)
                {
                    MessageBox.Show("Укажите названия сессии","Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                config.Name = tName.Text;
                config.Run = true;
                try
                {
                    UDPThread = new Thread(new ThreadStart(server.ListenUDP));
                    UDPThread.Start();
                    Thread = new Thread(new ThreadStart(server.Listen));
                    Thread.Start();
                }
                catch { MessageBox.Show("Запустить сессию не удалось, проверьте подключение к сети"); }
                this.MaximumSize = new Size(780, 320);
                this.MinimumSize = new Size(780, 320);
                pRun.Location = new Point(0, 0);
                pStart.Visible = false;
                pParameter.Visible = false;
                pRun.Visible = true;
                lInfoName.Text = "Название сессии:" + config.getName();
                lInfoIpPort.Text = "IP:" + config.getIP() + "|Port:" + config.getPort();
            }
        }
        private void bPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            tPath.Text = dialog.SelectedPath;
        }
        private void listUsers_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node.Level == 0)
                {
                    lClientSelect.Text = e.Node.Text;
                    string ip = e.Node.Nodes[0].Text.Remove(0, 3);
                    string port = e.Node.Nodes[1].Text.Remove(0, 5);
                    lClientSelectInfo.Text = $"IP:{ip} | Port:{port}";
                    sessions.setSeleclClient(ip, int.Parse(port));
                }
            }
            catch { lClientSelect.Text = ""; sessions.setSeleclClient(-1); }
        }
        private void ServerMenu_Load(object sender, EventArgs e)
        {
            tIP.Text = config.getIP();
            tPort.Text = config.getPort().ToString();
        }
        private void ServerMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            config.SaveSettings();
            if (server != null)
            {
                bool operation = false;
                foreach (ClientObject cl in server.clients)
                {
                    if(cl.download || cl.load)
                    {
                        operation = true;
                        break;
                    }
                }
                if (operation)
                {
                    if (DialogResult.No == MessageBox.Show("В сессии присходит операция по обмену файлами, хотите прервать её?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                sessions = null;
                server.DisconnectUDP();
                server.Disconnect();
            }
            this.Dispose(true);
        }
        private void bInfo_Click(object sender, EventArgs e)
        {
            if (clickButtonInfo)
                lInfoParametr.Visible = false;
            else
                lInfoParametr.Visible = true;
            clickButtonInfo = !clickButtonInfo;
        }
        private void toolStripMenuClientsDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listUsers.SelectedNode != null)
                {
                    if (DialogResult.Yes == MessageBox.Show("Вы точно хотите удалить выбранного пользователя ?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    {
                        TreeNode clientTree = listUsers.SelectedNode;
                        if (clientTree != null && clientTree.Text.Length > 0)
                        {
                            try
                            {
                                server.clients.Find(delegate (ClientObject cl)
                                {
                                    if (cl.IpAddress == clientTree.Nodes[0].Text.Remove(0, 3)
                                    && cl.Port == int.Parse(clientTree.Nodes[1].Text.Remove(0, 5)))
                                        return true;
                                    else
                                        return false;
                                }).Close();
                                sessions.RemoveClient(clientTree.Nodes[0].Text.Remove(0, 3), int.Parse(clientTree.Nodes[1].Text.Remove(0, 5)));
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }
        }
        //Действия с файлами
        private void bSendFile_Click(object sender, EventArgs e)
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
                if (sessions.getSelectClinet() > -1)
                {
                    string[] dataClient = sessions.getClient(sessions.getSelectClinet());
                    foreach (ClientObject cl in server.clients)
                    {
                        if (cl.IpAddress == dataClient[1] && cl.Port == int.Parse(dataClient[2]))
                        {
                            if (cl.download || cl.load)
                            {
                                MessageBox.Show("У выбранного пользователя происходит операция по обмену файлами, чтобы отправить файлы дождитесь окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    string files = "";
                    foreach (var val in listFiles.Items)
                        files = files + val.ToString() + " ";
                    if (DialogResult.Yes == MessageBox.Show($"Вы точно хотите отправить пользователю {dataClient[0]} \n Файлы: {files}", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        Command cm = new Command();
                        cm.Name = CommandName.FilePreview;
                        cm.Parametr.Add(config.getIP());
                        cm.Parametr.Add(config.getPort());
                        cm.Parametr.Add(config.getName());
                        foreach (var val in listFiles.Items)
                            cm.Parametr.Add(val);
                        server.clients[sessions.getSelectClinet()].SendCommand(cm);
                        sessions.StartSessions(dataClient[1], int.Parse(dataClient[2]));
                        setInfoAction("Ожидание действий от пользователя");
                        pFile.Enabled = false;
                        bFileCancel.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Выбран не существующий пользователь", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lClientSelect.Text = "";
                    lClientSelectInfo.Text = "";
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
            dialog.Title = "Выбирите файлы для отправки";
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
            canselFile = true;
            pFile.Enabled = true;
            bFileCancel.Visible = false;
            string[] dataClient = sessions.getClient(sessions.getSelectClinet());
            if (sessions.getSelectClinet() > -1)
            {
                Command cm = new Command();
                cm.Name = CommandName.FileCansel;
                cm.Parametr.Add(dataClient[1]);
                cm.Parametr.Add(dataClient[2]);
                cm.Parametr.Add(config.getName());
                server.clients[sessions.getSelectClinet()].SendCommand(cm);
            }
            setInfoAction($"Вы отменили отправку файла(ов) пользователю ({dataClient[0]})");
        }
        private void ToolStripMenuFilesDelete_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripItem).Name == "toolStripMenuFilesDeleteAll")
            {
                if (DialogResult.Yes == MessageBox.Show("Вы точно хотите удалить все файлы ?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
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
            catch { MessageBox.Show("Произошла ошибка выбора файла","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error); }
        }
        //Действие с параметрами
        private void bParametrOpen_Click(object sender, EventArgs e)
        {
            pStart.Visible = false;
            pParameter.Location = new Point(0, 0);
            pParameter.Visible = true;
        }
        private void bParameterExit_Click(object sender, EventArgs e)
        {
            if (config.getIP() != tIP.Text || config.getPort() != int.Parse(tPort.Text) || config.getPathSave() != tPath.Text)
            {
                if (DialogResult.Yes == MessageBox.Show("Настройки сервера не были сохранены, сохранить их?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    bParameterSave.PerformClick();
                }
                else
                {
                    tIP.Text = config.getIP();
                    tPort.Text = config.getPort().ToString();
                    tPath.Text = config.getPathSave();
                }
            }
            pParameter.Visible = false;
            pStart.Visible = true;
        }
        private void bParameterSave_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(tPath.Text))
            {
                MessageBox.Show("Папка для сохранения не найдена","Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!ConfigServer.CheckIpv4(tIP.Text))
            {
                MessageBox.Show("Ip address указан не правильно", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!ConfigServer.CheckRange(tPort.Text, 2000, 9999))
            {
                MessageBox.Show("Диапазон порта должен быть от 2000 до 9999", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            config.IP = tIP.Text;
            config.Port = int.Parse(tPort.Text);
            config.PathSave = tPath.Text;
            lInfoParametr.Text = $"Ip address:{config.getIP()} / Port :{config.getPort()}";
            MessageBox.Show("Настройки сервера сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (sender == tPort)
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
        private void ServerMenu_DragDrop(object sender, DragEventArgs e)
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
        private void ServerMenu_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
