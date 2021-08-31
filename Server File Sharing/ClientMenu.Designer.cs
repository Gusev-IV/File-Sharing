namespace Server_File_Sharing
{
    partial class ClientMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientMenu));
            this.pConnect = new System.Windows.Forms.Panel();
            this.bSaveFavorite = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lUserSelectInfo = new System.Windows.Forms.Label();
            this.listInfoAction = new System.Windows.Forms.ListBox();
            this.listFiles = new System.Windows.Forms.ListBox();
            this.conMenuFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuFilesDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuFilesDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.bFileCancel = new System.Windows.Forms.Button();
            this.pFile = new System.Windows.Forms.Panel();
            this.bFileSelect = new System.Windows.Forms.Button();
            this.bFile = new System.Windows.Forms.Button();
            this.lUserSelect = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.bExit = new System.Windows.Forms.Button();
            this.listUsers = new System.Windows.Forms.TreeView();
            this.pIpPort = new System.Windows.Forms.Panel();
            this.tPort = new System.Windows.Forms.TextBox();
            this.tIPServer = new System.Windows.Forms.TextBox();
            this.pParameter = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tParametrIp = new System.Windows.Forms.TextBox();
            this.bParameterExit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.bParametrUpdate = new System.Windows.Forms.Button();
            this.tParametrPort = new System.Windows.Forms.TextBox();
            this.chSelectServer = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.lSelectServer = new System.Windows.Forms.Label();
            this.bConnectServer = new System.Windows.Forms.Button();
            this.timerConnect = new System.Windows.Forms.Timer(this.components);
            this.timerFindServer = new System.Windows.Forms.Timer(this.components);
            this.pStart = new System.Windows.Forms.Panel();
            this.chSelectFavorite = new System.Windows.Forms.CheckBox();
            this.picInfo = new System.Windows.Forms.PictureBox();
            this.pInfo = new System.Windows.Forms.Panel();
            this.bUpdateServers = new System.Windows.Forms.Button();
            this.bParameterOpen = new System.Windows.Forms.Button();
            this.tName = new System.Windows.Forms.TextBox();
            this.conMenuServer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuServerDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.pConnect.SuspendLayout();
            this.conMenuFiles.SuspendLayout();
            this.pFile.SuspendLayout();
            this.pIpPort.SuspendLayout();
            this.pParameter.SuspendLayout();
            this.pStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfo)).BeginInit();
            this.pInfo.SuspendLayout();
            this.conMenuServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pConnect
            // 
            this.pConnect.Controls.Add(this.bSaveFavorite);
            this.pConnect.Controls.Add(this.label5);
            this.pConnect.Controls.Add(this.lUserSelectInfo);
            this.pConnect.Controls.Add(this.listInfoAction);
            this.pConnect.Controls.Add(this.listFiles);
            this.pConnect.Controls.Add(this.label4);
            this.pConnect.Controls.Add(this.bFileCancel);
            this.pConnect.Controls.Add(this.pFile);
            this.pConnect.Controls.Add(this.lUserSelect);
            this.pConnect.Controls.Add(this.label10);
            this.pConnect.Controls.Add(this.label9);
            this.pConnect.Controls.Add(this.lName);
            this.pConnect.Controls.Add(this.bExit);
            this.pConnect.Controls.Add(this.listUsers);
            this.pConnect.Location = new System.Drawing.Point(355, 125);
            this.pConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pConnect.Name = "pConnect";
            this.pConnect.Size = new System.Drawing.Size(675, 245);
            this.pConnect.TabIndex = 8;
            this.pConnect.Visible = false;
            // 
            // bSaveFavorite
            // 
            this.bSaveFavorite.BackgroundImage = global::Server_File_Sharing.Properties.Resources.Значок_добаления;
            this.bSaveFavorite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bSaveFavorite.FlatAppearance.BorderSize = 0;
            this.bSaveFavorite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSaveFavorite.Location = new System.Drawing.Point(599, 4);
            this.bSaveFavorite.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSaveFavorite.Name = "bSaveFavorite";
            this.bSaveFavorite.Size = new System.Drawing.Size(31, 34);
            this.bSaveFavorite.TabIndex = 24;
            this.bSaveFavorite.UseVisualStyleBackColor = true;
            this.bSaveFavorite.Click += new System.EventHandler(this.bSaveFavorite_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(428, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "Выбранные файлы для отправки";
            // 
            // lUserSelectInfo
            // 
            this.lUserSelectInfo.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUserSelectInfo.Location = new System.Drawing.Point(240, 84);
            this.lUserSelectInfo.Name = "lUserSelectInfo";
            this.lUserSelectInfo.Size = new System.Drawing.Size(166, 23);
            this.lUserSelectInfo.TabIndex = 28;
            this.lUserSelectInfo.Text = "Ip:111.111.111.111|Port:9999";
            this.lUserSelectInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // listInfoAction
            // 
            this.listInfoAction.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listInfoAction.FormattingEnabled = true;
            this.listInfoAction.ItemHeight = 18;
            this.listInfoAction.Items.AddRange(new object[] {
            "123456789012345678901234567890"});
            this.listInfoAction.Location = new System.Drawing.Point(4, 200);
            this.listInfoAction.Name = "listInfoAction";
            this.listInfoAction.Size = new System.Drawing.Size(272, 40);
            this.listInfoAction.TabIndex = 27;
            // 
            // listFiles
            // 
            this.listFiles.ContextMenuStrip = this.conMenuFiles;
            this.listFiles.FormattingEnabled = true;
            this.listFiles.IntegralHeight = false;
            this.listFiles.ItemHeight = 23;
            this.listFiles.Location = new System.Drawing.Point(431, 50);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(240, 145);
            this.listFiles.TabIndex = 26;
            this.listFiles.SelectedIndexChanged += new System.EventHandler(this.listFiles_SelectedIndexChanged);
            // 
            // conMenuFiles
            // 
            this.conMenuFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuFilesDelete,
            this.toolStripMenuFilesDeleteAll});
            this.conMenuFiles.Name = "conMenuFiles";
            this.conMenuFiles.Size = new System.Drawing.Size(239, 48);
            // 
            // toolStripMenuFilesDelete
            // 
            this.toolStripMenuFilesDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStripMenuFilesDelete.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuFilesDelete.Image = global::Server_File_Sharing.Properties.Resources.Значок_удаления;
            this.toolStripMenuFilesDelete.Name = "toolStripMenuFilesDelete";
            this.toolStripMenuFilesDelete.Size = new System.Drawing.Size(238, 22);
            this.toolStripMenuFilesDelete.Text = "Удалить выбранный файл";
            this.toolStripMenuFilesDelete.Click += new System.EventHandler(this.ToolStripMenuFilesDelete_Click);
            // 
            // toolStripMenuFilesDeleteAll
            // 
            this.toolStripMenuFilesDeleteAll.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuFilesDeleteAll.Image = global::Server_File_Sharing.Properties.Resources.Значок_удаления;
            this.toolStripMenuFilesDeleteAll.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripMenuFilesDeleteAll.Name = "toolStripMenuFilesDeleteAll";
            this.toolStripMenuFilesDeleteAll.Size = new System.Drawing.Size(238, 22);
            this.toolStripMenuFilesDeleteAll.Text = "Удалить все файлы";
            this.toolStripMenuFilesDeleteAll.Click += new System.EventHandler(this.ToolStripMenuFilesDelete_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(4, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 19);
            this.label4.TabIndex = 25;
            this.label4.Text = "Пользователи сессии";
            // 
            // bFileCancel
            // 
            this.bFileCancel.Location = new System.Drawing.Point(431, 203);
            this.bFileCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bFileCancel.Name = "bFileCancel";
            this.bFileCancel.Size = new System.Drawing.Size(240, 30);
            this.bFileCancel.TabIndex = 22;
            this.bFileCancel.Text = "Отменить отправку файлов";
            this.bFileCancel.UseVisualStyleBackColor = true;
            this.bFileCancel.Visible = false;
            this.bFileCancel.Click += new System.EventHandler(this.bFileCancel_Click);
            // 
            // pFile
            // 
            this.pFile.Controls.Add(this.bFileSelect);
            this.pFile.Controls.Add(this.bFile);
            this.pFile.Location = new System.Drawing.Point(224, 121);
            this.pFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pFile.Name = "pFile";
            this.pFile.Size = new System.Drawing.Size(200, 74);
            this.pFile.TabIndex = 22;
            // 
            // bFileSelect
            // 
            this.bFileSelect.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bFileSelect.Location = new System.Drawing.Point(19, 5);
            this.bFileSelect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bFileSelect.Name = "bFileSelect";
            this.bFileSelect.Size = new System.Drawing.Size(155, 30);
            this.bFileSelect.TabIndex = 13;
            this.bFileSelect.Text = "Выбрать файл(ы)";
            this.bFileSelect.UseVisualStyleBackColor = true;
            this.bFileSelect.Click += new System.EventHandler(this.bSelectFile_Click);
            // 
            // bFile
            // 
            this.bFile.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bFile.Location = new System.Drawing.Point(8, 38);
            this.bFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bFile.Name = "bFile";
            this.bFile.Size = new System.Drawing.Size(177, 32);
            this.bFile.TabIndex = 11;
            this.bFile.Text = "Отправить файл(ы)";
            this.bFile.UseVisualStyleBackColor = true;
            this.bFile.Click += new System.EventHandler(this.bFile_Click);
            // 
            // lUserSelect
            // 
            this.lUserSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lUserSelect.Location = new System.Drawing.Point(237, 57);
            this.lUserSelect.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lUserSelect.Name = "lUserSelect";
            this.lUserSelect.Size = new System.Drawing.Size(169, 27);
            this.lUserSelect.TabIndex = 20;
            this.lUserSelect.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(220, 27);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(207, 23);
            this.label10.TabIndex = 19;
            this.label10.Text = "Выбранный пользователь";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(4, 6);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "Ваше имя:";
            // 
            // lName
            // 
            this.lName.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lName.Location = new System.Drawing.Point(89, 3);
            this.lName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(236, 28);
            this.lName.TabIndex = 17;
            this.lName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bExit
            // 
            this.bExit.BackgroundImage = global::Server_File_Sharing.Properties.Resources.Значок_выхода;
            this.bExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bExit.FlatAppearance.BorderSize = 0;
            this.bExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bExit.Location = new System.Drawing.Point(638, 4);
            this.bExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(33, 35);
            this.bExit.TabIndex = 8;
            this.bExit.UseVisualStyleBackColor = true;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // listUsers
            // 
            this.listUsers.Location = new System.Drawing.Point(4, 50);
            this.listUsers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listUsers.Name = "listUsers";
            this.listUsers.Size = new System.Drawing.Size(220, 145);
            this.listUsers.TabIndex = 16;
            this.listUsers.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.listUsers_NodeMouseClick);
            // 
            // pIpPort
            // 
            this.pIpPort.Controls.Add(this.tPort);
            this.pIpPort.Controls.Add(this.tIPServer);
            this.pIpPort.Location = new System.Drawing.Point(439, 48);
            this.pIpPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pIpPort.Name = "pIpPort";
            this.pIpPort.Size = new System.Drawing.Size(241, 44);
            this.pIpPort.TabIndex = 9;
            this.pIpPort.Visible = false;
            // 
            // tPort
            // 
            this.tPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Server_File_Sharing.Properties.Settings.Default, "ClientPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tPort.Location = new System.Drawing.Point(177, 5);
            this.tPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tPort.Name = "tPort";
            this.tPort.Size = new System.Drawing.Size(54, 30);
            this.tPort.TabIndex = 20;
            this.tPort.Text = global::Server_File_Sharing.Properties.Settings.Default.ClientPort;
            this.tPort.TextChanged += new System.EventHandler(this.tPort_TextChanged);
            this.tPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tIP_KeyPress);
            // 
            // tIPServer
            // 
            this.tIPServer.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Server_File_Sharing.Properties.Settings.Default, "ClientIpServer", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tIPServer.Location = new System.Drawing.Point(4, 5);
            this.tIPServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tIPServer.Name = "tIPServer";
            this.tIPServer.Size = new System.Drawing.Size(163, 30);
            this.tIPServer.TabIndex = 7;
            this.tIPServer.Text = global::Server_File_Sharing.Properties.Settings.Default.ClientIpServer;
            this.tIPServer.TextChanged += new System.EventHandler(this.tIP_TextChanged);
            this.tIPServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tIP_KeyPress);
            // 
            // pParameter
            // 
            this.pParameter.Controls.Add(this.label2);
            this.pParameter.Controls.Add(this.tParametrIp);
            this.pParameter.Controls.Add(this.bParameterExit);
            this.pParameter.Controls.Add(this.label3);
            this.pParameter.Controls.Add(this.bParametrUpdate);
            this.pParameter.Controls.Add(this.tParametrPort);
            this.pParameter.Location = new System.Drawing.Point(4, 302);
            this.pParameter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pParameter.Name = "pParameter";
            this.pParameter.Size = new System.Drawing.Size(331, 125);
            this.pParameter.TabIndex = 19;
            this.pParameter.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 23);
            this.label2.TabIndex = 22;
            this.label2.Text = "Поиск от моего IP";
            // 
            // tParametrIp
            // 
            this.tParametrIp.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Server_File_Sharing.Properties.Settings.Default, "ClientIP", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tParametrIp.Enabled = false;
            this.tParametrIp.Location = new System.Drawing.Point(172, 5);
            this.tParametrIp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tParametrIp.Name = "tParametrIp";
            this.tParametrIp.Size = new System.Drawing.Size(147, 30);
            this.tParametrIp.TabIndex = 21;
            this.tParametrIp.Text = global::Server_File_Sharing.Properties.Settings.Default.ClientIP;
            this.tParametrIp.TextChanged += new System.EventHandler(this.tIP_TextChanged);
            // 
            // bParameterExit
            // 
            this.bParameterExit.BackgroundImage = global::Server_File_Sharing.Properties.Resources.Значок_выхода;
            this.bParameterExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bParameterExit.FlatAppearance.BorderSize = 0;
            this.bParameterExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bParameterExit.Location = new System.Drawing.Point(263, 81);
            this.bParameterExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bParameterExit.Name = "bParameterExit";
            this.bParameterExit.Size = new System.Drawing.Size(33, 30);
            this.bParameterExit.TabIndex = 17;
            this.bParameterExit.UseVisualStyleBackColor = true;
            this.bParameterExit.Click += new System.EventHandler(this.bParameterExit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(9, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 23);
            this.label3.TabIndex = 16;
            this.label3.Text = "Поиск по PORT";
            // 
            // bParametrUpdate
            // 
            this.bParametrUpdate.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bParametrUpdate.Location = new System.Drawing.Point(107, 78);
            this.bParametrUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bParametrUpdate.Name = "bParametrUpdate";
            this.bParametrUpdate.Size = new System.Drawing.Size(123, 37);
            this.bParametrUpdate.TabIndex = 18;
            this.bParametrUpdate.Text = "Изменить";
            this.bParametrUpdate.UseVisualStyleBackColor = true;
            this.bParametrUpdate.Click += new System.EventHandler(this.bParametrUpdate_Click);
            // 
            // tParametrPort
            // 
            this.tParametrPort.Enabled = false;
            this.tParametrPort.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tParametrPort.Location = new System.Drawing.Point(172, 38);
            this.tParametrPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tParametrPort.MaxLength = 4;
            this.tParametrPort.Name = "tParametrPort";
            this.tParametrPort.Size = new System.Drawing.Size(58, 30);
            this.tParametrPort.TabIndex = 17;
            this.tParametrPort.Text = "8900";
            this.tParametrPort.TextChanged += new System.EventHandler(this.tPort_TextChanged);
            this.tParametrPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tIP_KeyPress);
            // 
            // chSelectServer
            // 
            this.chSelectServer.AutoSize = true;
            this.chSelectServer.Font = new System.Drawing.Font("Comic Sans MS", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chSelectServer.Location = new System.Drawing.Point(198, 182);
            this.chSelectServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chSelectServer.Name = "chSelectServer";
            this.chSelectServer.Size = new System.Drawing.Size(125, 22);
            this.chSelectServer.TabIndex = 13;
            this.chSelectServer.Text = "Указать cессию";
            this.chSelectServer.UseVisualStyleBackColor = true;
            this.chSelectServer.CheckedChanged += new System.EventHandler(this.chSelectServer_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 100);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 23);
            this.label1.TabIndex = 14;
            this.label1.Text = "Введите своё имя :";
            // 
            // cbServer
            // 
            this.cbServer.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbServer.FormattingEnabled = true;
            this.cbServer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbServer.Location = new System.Drawing.Point(4, 8);
            this.cbServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(231, 31);
            this.cbServer.Sorted = true;
            this.cbServer.TabIndex = 12;
            this.cbServer.SelectedIndexChanged += new System.EventHandler(this.cbServer_SelectedIndexChanged);
            // 
            // lSelectServer
            // 
            this.lSelectServer.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lSelectServer.Location = new System.Drawing.Point(57, 12);
            this.lSelectServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lSelectServer.Name = "lSelectServer";
            this.lSelectServer.Size = new System.Drawing.Size(214, 26);
            this.lSelectServer.TabIndex = 11;
            this.lSelectServer.Text = "Доступные сессии";
            this.lSelectServer.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // bConnectServer
            // 
            this.bConnectServer.Location = new System.Drawing.Point(48, 138);
            this.bConnectServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bConnectServer.Name = "bConnectServer";
            this.bConnectServer.Size = new System.Drawing.Size(241, 41);
            this.bConnectServer.TabIndex = 0;
            this.bConnectServer.Text = "Подключится к сессии";
            this.bConnectServer.UseVisualStyleBackColor = true;
            this.bConnectServer.Click += new System.EventHandler(this.pConnectServer_Click);
            // 
            // timerConnect
            // 
            this.timerConnect.Interval = 1000;
            this.timerConnect.Tick += new System.EventHandler(this.timerConnect_Tick);
            // 
            // timerFindServer
            // 
            this.timerFindServer.Interval = 2000;
            this.timerFindServer.Tick += new System.EventHandler(this.timerFindServer_Tick);
            // 
            // pStart
            // 
            this.pStart.Controls.Add(this.chSelectFavorite);
            this.pStart.Controls.Add(this.picInfo);
            this.pStart.Controls.Add(this.pInfo);
            this.pStart.Controls.Add(this.bParameterOpen);
            this.pStart.Controls.Add(this.lSelectServer);
            this.pStart.Controls.Add(this.chSelectServer);
            this.pStart.Controls.Add(this.label1);
            this.pStart.Controls.Add(this.tName);
            this.pStart.Controls.Add(this.bConnectServer);
            this.pStart.Location = new System.Drawing.Point(0, 0);
            this.pStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pStart.Name = "pStart";
            this.pStart.Size = new System.Drawing.Size(335, 215);
            this.pStart.TabIndex = 10;
            // 
            // chSelectFavorite
            // 
            this.chSelectFavorite.AutoSize = true;
            this.chSelectFavorite.Font = new System.Drawing.Font("Comic Sans MS", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chSelectFavorite.Location = new System.Drawing.Point(8, 182);
            this.chSelectFavorite.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chSelectFavorite.Name = "chSelectFavorite";
            this.chSelectFavorite.Size = new System.Drawing.Size(183, 22);
            this.chSelectFavorite.TabIndex = 17;
            this.chSelectFavorite.Text = "Выбрать из сохраненных";
            this.chSelectFavorite.UseVisualStyleBackColor = true;
            this.chSelectFavorite.CheckedChanged += new System.EventHandler(this.chSelectFavorite_CheckedChanged);
            // 
            // picInfo
            // 
            this.picInfo.BackgroundImage = global::Server_File_Sharing.Properties.Resources.Значок_информации;
            this.picInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picInfo.Location = new System.Drawing.Point(11, 57);
            this.picInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picInfo.Name = "picInfo";
            this.picInfo.Size = new System.Drawing.Size(26, 26);
            this.picInfo.TabIndex = 11;
            this.picInfo.TabStop = false;
            // 
            // pInfo
            // 
            this.pInfo.Controls.Add(this.cbServer);
            this.pInfo.Controls.Add(this.bUpdateServers);
            this.pInfo.Location = new System.Drawing.Point(44, 48);
            this.pInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pInfo.Name = "pInfo";
            this.pInfo.Size = new System.Drawing.Size(287, 46);
            this.pInfo.TabIndex = 11;
            // 
            // bUpdateServers
            // 
            this.bUpdateServers.BackgroundImage = global::Server_File_Sharing.Properties.Resources.Значок_обновления;
            this.bUpdateServers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bUpdateServers.FlatAppearance.BorderSize = 0;
            this.bUpdateServers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bUpdateServers.Location = new System.Drawing.Point(246, 8);
            this.bUpdateServers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bUpdateServers.Name = "bUpdateServers";
            this.bUpdateServers.Size = new System.Drawing.Size(30, 26);
            this.bUpdateServers.TabIndex = 15;
            this.bUpdateServers.UseVisualStyleBackColor = true;
            this.bUpdateServers.Click += new System.EventHandler(this.bUpdateServers_Click);
            // 
            // bParameterOpen
            // 
            this.bParameterOpen.BackgroundImage = global::Server_File_Sharing.Properties.Resources.Значок_настройки;
            this.bParameterOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bParameterOpen.FlatAppearance.BorderSize = 0;
            this.bParameterOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bParameterOpen.Location = new System.Drawing.Point(305, 12);
            this.bParameterOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bParameterOpen.Name = "bParameterOpen";
            this.bParameterOpen.Size = new System.Drawing.Size(26, 26);
            this.bParameterOpen.TabIndex = 16;
            this.bParameterOpen.UseVisualStyleBackColor = true;
            this.bParameterOpen.Click += new System.EventHandler(this.bParameterOpen_Click);
            // 
            // tName
            // 
            this.tName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Server_File_Sharing.Properties.Settings.Default, "ClientName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.tName.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tName.Location = new System.Drawing.Point(163, 95);
            this.tName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tName.MaxLength = 20;
            this.tName.Name = "tName";
            this.tName.Size = new System.Drawing.Size(160, 30);
            this.tName.TabIndex = 10;
            this.tName.Text = global::Server_File_Sharing.Properties.Settings.Default.ClientName;
            // 
            // conMenuServer
            // 
            this.conMenuServer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuServerDelete});
            this.conMenuServer.Name = "conMenuFiles";
            this.conMenuServer.Size = new System.Drawing.Size(254, 26);
            // 
            // toolStripMenuServerDelete
            // 
            this.toolStripMenuServerDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStripMenuServerDelete.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuServerDelete.Image = global::Server_File_Sharing.Properties.Resources.Значок_удаления;
            this.toolStripMenuServerDelete.Name = "toolStripMenuServerDelete";
            this.toolStripMenuServerDelete.Size = new System.Drawing.Size(253, 22);
            this.toolStripMenuServerDelete.Text = "Удалить выбранную сессию";
            this.toolStripMenuServerDelete.Click += new System.EventHandler(this.toolStripMenuServerDelete_Click);
            // 
            // ClientMenu
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 607);
            this.Controls.Add(this.pParameter);
            this.Controls.Add(this.pIpPort);
            this.Controls.Add(this.pStart);
            this.Controls.Add(this.pConnect);
            this.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ClientMenu";
            this.Text = "FS Пользователь";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientMenu_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ClientMenu_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ClientMenu_DragEnter);
            this.pConnect.ResumeLayout(false);
            this.pConnect.PerformLayout();
            this.conMenuFiles.ResumeLayout(false);
            this.pFile.ResumeLayout(false);
            this.pIpPort.ResumeLayout(false);
            this.pIpPort.PerformLayout();
            this.pParameter.ResumeLayout(false);
            this.pParameter.PerformLayout();
            this.pStart.ResumeLayout(false);
            this.pStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInfo)).EndInit();
            this.pInfo.ResumeLayout(false);
            this.conMenuServer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pConnect;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lName;
        public System.Windows.Forms.TreeView listUsers;
        private System.Windows.Forms.Button bFileSelect;
        private System.Windows.Forms.Button bFile;
        public System.Windows.Forms.Button bExit;
        private System.Windows.Forms.Panel pIpPort;
        private System.Windows.Forms.TextBox tIPServer;
        private System.Windows.Forms.Panel pParameter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bParametrUpdate;
        private System.Windows.Forms.TextBox tParametrPort;
        private System.Windows.Forms.CheckBox chSelectServer;
        private System.Windows.Forms.Button bUpdateServers;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbServer;
        private System.Windows.Forms.Label lSelectServer;
        private System.Windows.Forms.Button bConnectServer;
        private System.Windows.Forms.TextBox tName;
        private System.Windows.Forms.Timer timerConnect;
        private System.Windows.Forms.Timer timerFindServer;
        private System.Windows.Forms.Panel pStart;
        private System.Windows.Forms.PictureBox picInfo;
        private System.Windows.Forms.Button bParameterOpen;
        private System.Windows.Forms.Panel pInfo;
        private System.Windows.Forms.Button bParameterExit;
        private System.Windows.Forms.TextBox tPort;
        private System.Windows.Forms.TextBox tParametrIp;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Panel pFile;
        private System.Windows.Forms.ContextMenuStrip conMenuFiles;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFilesDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFilesDeleteAll;
        public System.Windows.Forms.Button bFileCancel;
        private System.Windows.Forms.Button bSaveFavorite;
        private System.Windows.Forms.CheckBox chSelectFavorite;
        private System.Windows.Forms.ContextMenuStrip conMenuServer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuServerDelete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listInfoAction;
        internal System.Windows.Forms.ListBox listFiles;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label lUserSelect;
        internal System.Windows.Forms.Label lUserSelectInfo;
    }
}