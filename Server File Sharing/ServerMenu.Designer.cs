namespace Server_File_Sharing
{
    partial class ServerMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerMenu));
            this.pParameter = new System.Windows.Forms.Panel();
            this.tPort = new System.Windows.Forms.TextBox();
            this.bParameterSave = new System.Windows.Forms.Button();
            this.bParameterExit = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tPath = new System.Windows.Forms.TextBox();
            this.bPath = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tIP = new System.Windows.Forms.TextBox();
            this.bStart = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pRun = new System.Windows.Forms.Panel();
            this.listInfoAction = new System.Windows.Forms.ListBox();
            this.lClientSelectInfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listFiles = new System.Windows.Forms.ListBox();
            this.conMenuFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuFilesDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuFilesDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.bFileCancel = new System.Windows.Forms.Button();
            this.pFile = new System.Windows.Forms.Panel();
            this.bSendFile = new System.Windows.Forms.Button();
            this.bSelectFile = new System.Windows.Forms.Button();
            this.lClientSelect = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lInfoIpPort = new System.Windows.Forms.Label();
            this.lInfoName = new System.Windows.Forms.Label();
            this.bExit = new System.Windows.Forms.Button();
            this.listUsers = new System.Windows.Forms.TreeView();
            this.conMenuClients = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuClientsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.pStart = new System.Windows.Forms.Panel();
            this.lInfoParametr = new System.Windows.Forms.Label();
            this.bInfo = new System.Windows.Forms.Button();
            this.bParameterOpen = new System.Windows.Forms.Button();
            this.tName = new System.Windows.Forms.TextBox();
            this.pParameter.SuspendLayout();
            this.pRun.SuspendLayout();
            this.conMenuFiles.SuspendLayout();
            this.pFile.SuspendLayout();
            this.conMenuClients.SuspendLayout();
            this.pStart.SuspendLayout();
            this.SuspendLayout();
            // 
            // pParameter
            // 
            this.pParameter.Controls.Add(this.tPort);
            this.pParameter.Controls.Add(this.bParameterSave);
            this.pParameter.Controls.Add(this.bParameterExit);
            this.pParameter.Controls.Add(this.label5);
            this.pParameter.Controls.Add(this.tPath);
            this.pParameter.Controls.Add(this.bPath);
            this.pParameter.Controls.Add(this.label4);
            this.pParameter.Controls.Add(this.tIP);
            this.pParameter.Location = new System.Drawing.Point(0, 190);
            this.pParameter.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.pParameter.Name = "pParameter";
            this.pParameter.Size = new System.Drawing.Size(350, 177);
            this.pParameter.TabIndex = 10;
            // 
            // tPort
            // 
            this.tPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Server_File_Sharing.Properties.Settings.Default, "ServerPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tPort.Location = new System.Drawing.Point(174, 44);
            this.tPort.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tPort.Name = "tPort";
            this.tPort.Size = new System.Drawing.Size(54, 30);
            this.tPort.TabIndex = 13;
            this.tPort.Text = global::Server_File_Sharing.Properties.Settings.Default.ServerPort;
            this.tPort.TextChanged += new System.EventHandler(this.tPort_TextChanged);
            this.tPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tIP_KeyPress);
            // 
            // bParameterSave
            // 
            this.bParameterSave.BackgroundImage = global::Server_File_Sharing.Properties.Resources.Значок_сохранения;
            this.bParameterSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bParameterSave.FlatAppearance.BorderSize = 0;
            this.bParameterSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bParameterSave.Location = new System.Drawing.Point(263, 20);
            this.bParameterSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.bParameterSave.Name = "bParameterSave";
            this.bParameterSave.Size = new System.Drawing.Size(33, 36);
            this.bParameterSave.TabIndex = 12;
            this.bParameterSave.UseVisualStyleBackColor = true;
            this.bParameterSave.Click += new System.EventHandler(this.bParameterSave_Click);
            // 
            // bParameterExit
            // 
            this.bParameterExit.BackgroundImage = global::Server_File_Sharing.Properties.Resources.Значок_выхода;
            this.bParameterExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bParameterExit.FlatAppearance.BorderSize = 0;
            this.bParameterExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bParameterExit.Location = new System.Drawing.Point(307, 20);
            this.bParameterExit.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.bParameterExit.Name = "bParameterExit";
            this.bParameterExit.Size = new System.Drawing.Size(33, 36);
            this.bParameterExit.TabIndex = 11;
            this.bParameterExit.UseVisualStyleBackColor = true;
            this.bParameterExit.Click += new System.EventHandler(this.bParameterExit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(4, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(220, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Папку куда сохраняются файлы";
            // 
            // tPath
            // 
            this.tPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Server_File_Sharing.Properties.Settings.Default, "ServerPathSave", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tPath.Location = new System.Drawing.Point(4, 113);
            this.tPath.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tPath.Name = "tPath";
            this.tPath.Size = new System.Drawing.Size(202, 30);
            this.tPath.TabIndex = 8;
            this.tPath.Text = global::Server_File_Sharing.Properties.Settings.Default.ServerPathSave;
            // 
            // bPath
            // 
            this.bPath.Location = new System.Drawing.Point(216, 108);
            this.bPath.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.bPath.Name = "bPath";
            this.bPath.Size = new System.Drawing.Size(124, 40);
            this.bPath.TabIndex = 9;
            this.bPath.Text = "Выбрать";
            this.bPath.UseVisualStyleBackColor = true;
            this.bPath.Click += new System.EventHandler(this.bPath_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(4, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(224, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "ip address            port   ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tIP
            // 
            this.tIP.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Server_File_Sharing.Properties.Settings.Default, "ServerIp", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tIP.Location = new System.Drawing.Point(23, 44);
            this.tIP.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tIP.Name = "tIP";
            this.tIP.Size = new System.Drawing.Size(142, 30);
            this.tIP.TabIndex = 5;
            this.tIP.Text = global::Server_File_Sharing.Properties.Settings.Default.ServerIp;
            this.tIP.TextChanged += new System.EventHandler(this.tIP_TextChanged);
            this.tIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tIP_KeyPress);
            // 
            // bStart
            // 
            this.bStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bStart.Location = new System.Drawing.Point(42, 124);
            this.bStart.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(247, 44);
            this.bStart.TabIndex = 7;
            this.bStart.Text = "Запустить сессию";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(18, 14);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(202, 23);
            this.label6.TabIndex = 8;
            this.label6.Text = "Укажите название сессии";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pRun
            // 
            this.pRun.Controls.Add(this.listInfoAction);
            this.pRun.Controls.Add(this.lClientSelectInfo);
            this.pRun.Controls.Add(this.label2);
            this.pRun.Controls.Add(this.listFiles);
            this.pRun.Controls.Add(this.label1);
            this.pRun.Controls.Add(this.bFileCancel);
            this.pRun.Controls.Add(this.pFile);
            this.pRun.Controls.Add(this.lClientSelect);
            this.pRun.Controls.Add(this.label8);
            this.pRun.Controls.Add(this.lInfoIpPort);
            this.pRun.Controls.Add(this.lInfoName);
            this.pRun.Controls.Add(this.bExit);
            this.pRun.Controls.Add(this.listUsers);
            this.pRun.Location = new System.Drawing.Point(0, 377);
            this.pRun.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.pRun.Name = "pRun";
            this.pRun.Size = new System.Drawing.Size(778, 283);
            this.pRun.TabIndex = 11;
            this.pRun.Visible = false;
            // 
            // listInfoAction
            // 
            this.listInfoAction.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listInfoAction.FormattingEnabled = true;
            this.listInfoAction.ItemHeight = 18;
            this.listInfoAction.Items.AddRange(new object[] {
            "123456789012345678901234567890"});
            this.listInfoAction.Location = new System.Drawing.Point(7, 238);
            this.listInfoAction.Name = "listInfoAction";
            this.listInfoAction.Size = new System.Drawing.Size(266, 40);
            this.listInfoAction.TabIndex = 28;
            // 
            // lClientSelectInfo
            // 
            this.lClientSelectInfo.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClientSelectInfo.Location = new System.Drawing.Point(246, 102);
            this.lClientSelectInfo.Name = "lClientSelectInfo";
            this.lClientSelectInfo.Size = new System.Drawing.Size(230, 23);
            this.lClientSelectInfo.TabIndex = 27;
            this.lClientSelectInfo.Text = "Ip:111.111.111.111|Port:9999";
            this.lClientSelectInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(494, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "Выбранные файлы для отправки";
            // 
            // listFiles
            // 
            this.listFiles.ContextMenuStrip = this.conMenuFiles;
            this.listFiles.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listFiles.FormattingEnabled = true;
            this.listFiles.IntegralHeight = false;
            this.listFiles.ItemHeight = 20;
            this.listFiles.Items.AddRange(new object[] {
            "1234567890123456789012345"});
            this.listFiles.Location = new System.Drawing.Point(494, 79);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(266, 155);
            this.listFiles.TabIndex = 25;
            this.listFiles.SelectedIndexChanged += new System.EventHandler(this.listFiles_SelectedIndexChanged);
            // 
            // conMenuFiles
            // 
            this.conMenuFiles.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.conMenuFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuFilesDelete,
            this.toolStripMenuFilesDeleteAll});
            this.conMenuFiles.Name = "conMenuFiles";
            this.conMenuFiles.Size = new System.Drawing.Size(270, 52);
            this.conMenuFiles.Text = "Выберите способ удаления";
            // 
            // toolStripMenuFilesDelete
            // 
            this.toolStripMenuFilesDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStripMenuFilesDelete.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuFilesDelete.Image = global::Server_File_Sharing.Properties.Resources.Значок_удаления;
            this.toolStripMenuFilesDelete.Name = "toolStripMenuFilesDelete";
            this.toolStripMenuFilesDelete.Size = new System.Drawing.Size(269, 24);
            this.toolStripMenuFilesDelete.Text = "Удалить выбранный файл";
            this.toolStripMenuFilesDelete.Click += new System.EventHandler(this.ToolStripMenuFilesDelete_Click);
            // 
            // toolStripMenuFilesDeleteAll
            // 
            this.toolStripMenuFilesDeleteAll.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuFilesDeleteAll.Image = global::Server_File_Sharing.Properties.Resources.Значок_удаления;
            this.toolStripMenuFilesDeleteAll.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripMenuFilesDeleteAll.Name = "toolStripMenuFilesDeleteAll";
            this.toolStripMenuFilesDeleteAll.Size = new System.Drawing.Size(269, 24);
            this.toolStripMenuFilesDeleteAll.Text = "Удалить все файлы";
            this.toolStripMenuFilesDeleteAll.Click += new System.EventHandler(this.ToolStripMenuFilesDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 24;
            this.label1.Text = "Пользователи сессии";
            // 
            // bFileCancel
            // 
            this.bFileCancel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bFileCancel.Location = new System.Drawing.Point(494, 238);
            this.bFileCancel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.bFileCancel.Name = "bFileCancel";
            this.bFileCancel.Size = new System.Drawing.Size(267, 39);
            this.bFileCancel.TabIndex = 23;
            this.bFileCancel.Text = "Отменить отправку файлы(ов)";
            this.bFileCancel.UseVisualStyleBackColor = true;
            this.bFileCancel.Visible = false;
            this.bFileCancel.Click += new System.EventHandler(this.bFileCancel_Click);
            // 
            // pFile
            // 
            this.pFile.Controls.Add(this.bSendFile);
            this.pFile.Controls.Add(this.bSelectFile);
            this.pFile.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pFile.Location = new System.Drawing.Point(250, 131);
            this.pFile.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.pFile.Name = "pFile";
            this.pFile.Size = new System.Drawing.Size(213, 103);
            this.pFile.TabIndex = 13;
            // 
            // bSendFile
            // 
            this.bSendFile.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bSendFile.Location = new System.Drawing.Point(4, 57);
            this.bSendFile.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.bSendFile.Name = "bSendFile";
            this.bSendFile.Size = new System.Drawing.Size(204, 40);
            this.bSendFile.TabIndex = 15;
            this.bSendFile.Text = "Отправить файл(ы)";
            this.bSendFile.UseVisualStyleBackColor = true;
            this.bSendFile.Click += new System.EventHandler(this.bSendFile_Click);
            // 
            // bSelectFile
            // 
            this.bSelectFile.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bSelectFile.Location = new System.Drawing.Point(14, 6);
            this.bSelectFile.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.bSelectFile.Name = "bSelectFile";
            this.bSelectFile.Size = new System.Drawing.Size(181, 40);
            this.bSelectFile.TabIndex = 11;
            this.bSelectFile.Text = "Выбрать файл(ы)";
            this.bSelectFile.UseVisualStyleBackColor = true;
            this.bSelectFile.Click += new System.EventHandler(this.bSelectFile_Click);
            // 
            // lClientSelect
            // 
            this.lClientSelect.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lClientSelect.Location = new System.Drawing.Point(246, 79);
            this.lClientSelect.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lClientSelect.Name = "lClientSelect";
            this.lClientSelect.Size = new System.Drawing.Size(230, 23);
            this.lClientSelect.TabIndex = 10;
            this.lClientSelect.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(241, 51);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(217, 23);
            this.label8.TabIndex = 9;
            this.label8.Text = "Выбранный пользователь :";
            // 
            // lInfoIpPort
            // 
            this.lInfoIpPort.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lInfoIpPort.Location = new System.Drawing.Point(336, 20);
            this.lInfoIpPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lInfoIpPort.Name = "lInfoIpPort";
            this.lInfoIpPort.Size = new System.Drawing.Size(223, 23);
            this.lInfoIpPort.TabIndex = 8;
            this.lInfoIpPort.Text = "Ip:111.111.111.111|Port:9999";
            this.lInfoIpPort.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lInfoName
            // 
            this.lInfoName.AutoSize = true;
            this.lInfoName.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lInfoName.Location = new System.Drawing.Point(6, 20);
            this.lInfoName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lInfoName.Name = "lInfoName";
            this.lInfoName.Size = new System.Drawing.Size(318, 20);
            this.lInfoName.TabIndex = 7;
            this.lInfoName.Text = "Название сессии: 12345678901234567890";
            // 
            // bExit
            // 
            this.bExit.BackgroundImage = global::Server_File_Sharing.Properties.Resources.Значок_выхода;
            this.bExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bExit.Cursor = System.Windows.Forms.Cursors.Default;
            this.bExit.FlatAppearance.BorderSize = 0;
            this.bExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExit.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bExit.Location = new System.Drawing.Point(723, 8);
            this.bExit.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(38, 40);
            this.bExit.TabIndex = 6;
            this.bExit.UseVisualStyleBackColor = true;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // listUsers
            // 
            this.listUsers.ContextMenuStrip = this.conMenuClients;
            this.listUsers.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listUsers.Location = new System.Drawing.Point(7, 79);
            this.listUsers.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(222, 155);
            this.listUsers.TabIndex = 2;
            this.listUsers.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.listUsers_NodeMouseClick);
            // 
            // conMenuClients
            // 
            this.conMenuClients.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuClientsDelete});
            this.conMenuClients.Name = "conMenuFiles";
            this.conMenuClients.Size = new System.Drawing.Size(295, 26);
            // 
            // toolStripMenuClientsDelete
            // 
            this.toolStripMenuClientsDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStripMenuClientsDelete.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuClientsDelete.Image = global::Server_File_Sharing.Properties.Resources.Значок_удаления;
            this.toolStripMenuClientsDelete.Name = "toolStripMenuClientsDelete";
            this.toolStripMenuClientsDelete.Size = new System.Drawing.Size(294, 22);
            this.toolStripMenuClientsDelete.Text = "Удалить выбранного пользователя";
            this.toolStripMenuClientsDelete.Click += new System.EventHandler(this.toolStripMenuClientsDelete_Click);
            // 
            // pStart
            // 
            this.pStart.Controls.Add(this.lInfoParametr);
            this.pStart.Controls.Add(this.bInfo);
            this.pStart.Controls.Add(this.bParameterOpen);
            this.pStart.Controls.Add(this.tName);
            this.pStart.Controls.Add(this.bStart);
            this.pStart.Controls.Add(this.label6);
            this.pStart.Location = new System.Drawing.Point(0, 0);
            this.pStart.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.pStart.Name = "pStart";
            this.pStart.Size = new System.Drawing.Size(350, 177);
            this.pStart.TabIndex = 12;
            // 
            // lInfoParametr
            // 
            this.lInfoParametr.AutoSize = true;
            this.lInfoParametr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lInfoParametr.Location = new System.Drawing.Point(29, 89);
            this.lInfoParametr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lInfoParametr.Name = "lInfoParametr";
            this.lInfoParametr.Size = new System.Drawing.Size(264, 16);
            this.lInfoParametr.TabIndex = 12;
            this.lInfoParametr.Text = "Ip address:1111.1111.1111.1111 / Port :9999";
            this.lInfoParametr.Visible = false;
            // 
            // bInfo
            // 
            this.bInfo.BackgroundImage = global::Server_File_Sharing.Properties.Resources.Значок_информации;
            this.bInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bInfo.Cursor = System.Windows.Forms.Cursors.Help;
            this.bInfo.FlatAppearance.BorderSize = 0;
            this.bInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bInfo.Location = new System.Drawing.Point(294, 52);
            this.bInfo.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.bInfo.Name = "bInfo";
            this.bInfo.Size = new System.Drawing.Size(27, 28);
            this.bInfo.TabIndex = 11;
            this.bInfo.UseVisualStyleBackColor = true;
            this.bInfo.Click += new System.EventHandler(this.bInfo_Click);
            // 
            // bParameterOpen
            // 
            this.bParameterOpen.AccessibleName = "";
            this.bParameterOpen.BackgroundImage = global::Server_File_Sharing.Properties.Resources.Значок_настройки;
            this.bParameterOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bParameterOpen.Cursor = System.Windows.Forms.Cursors.Help;
            this.bParameterOpen.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.bParameterOpen.FlatAppearance.BorderSize = 0;
            this.bParameterOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bParameterOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bParameterOpen.Location = new System.Drawing.Point(314, 14);
            this.bParameterOpen.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.bParameterOpen.Name = "bParameterOpen";
            this.bParameterOpen.Size = new System.Drawing.Size(26, 26);
            this.bParameterOpen.TabIndex = 10;
            this.bParameterOpen.UseVisualStyleBackColor = true;
            this.bParameterOpen.Click += new System.EventHandler(this.bParametrOpen_Click);
            // 
            // tName
            // 
            this.tName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Server_File_Sharing.Properties.Settings.Default, "ServerName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tName.Location = new System.Drawing.Point(38, 52);
            this.tName.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tName.MaxLength = 20;
            this.tName.Name = "tName";
            this.tName.Size = new System.Drawing.Size(247, 26);
            this.tName.TabIndex = 9;
            this.tName.Text = global::Server_File_Sharing.Properties.Settings.Default.ServerName;
            // 
            // ServerMenu
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 796);
            this.Controls.Add(this.pStart);
            this.Controls.Add(this.pRun);
            this.Controls.Add(this.pParameter);
            this.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.Name = "ServerMenu";
            this.Text = "FS Администратор";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerMenu_FormClosing);
            this.Load += new System.EventHandler(this.ServerMenu_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ServerMenu_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ServerMenu_DragEnter);
            this.pParameter.ResumeLayout(false);
            this.pParameter.PerformLayout();
            this.pRun.ResumeLayout(false);
            this.pRun.PerformLayout();
            this.conMenuFiles.ResumeLayout(false);
            this.pFile.ResumeLayout(false);
            this.conMenuClients.ResumeLayout(false);
            this.pStart.ResumeLayout(false);
            this.pStart.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pParameter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.TextBox tPath;
        private System.Windows.Forms.Button bPath;
        private System.Windows.Forms.TextBox tName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tIP;
        private System.Windows.Forms.Panel pRun;
        private System.Windows.Forms.Button bSendFile;
        private System.Windows.Forms.Button bSelectFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lInfoIpPort;
        private System.Windows.Forms.Label lInfoName;
        private System.Windows.Forms.Button bExit;
        public System.Windows.Forms.TreeView listUsers;
        private System.Windows.Forms.Panel pStart;
        private System.Windows.Forms.Button bInfo;
        private System.Windows.Forms.Button bParameterOpen;
        private System.Windows.Forms.Label lInfoParametr;
        private System.Windows.Forms.Button bParameterExit;
        private System.Windows.Forms.Button bParameterSave;
        private System.Windows.Forms.TextBox tPort;
        public System.Windows.Forms.Panel pFile;
        private System.Windows.Forms.ContextMenuStrip conMenuFiles;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFilesDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFilesDeleteAll;
        public System.Windows.Forms.Button bFileCancel;
        private System.Windows.Forms.ContextMenuStrip conMenuClients;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuClientsDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listInfoAction;
        internal System.Windows.Forms.ListBox listFiles;
        internal System.Windows.Forms.Label lClientSelect;
        internal System.Windows.Forms.Label lClientSelectInfo;
    }
}