namespace Server_File_Sharing
{
    partial class StartMenu
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartMenu));
            this.bServer = new System.Windows.Forms.Button();
            this.bClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bServer
            // 
            resources.ApplyResources(this.bServer, "bServer");
            this.bServer.Name = "bServer";
            this.bServer.UseVisualStyleBackColor = true;
            this.bServer.Click += new System.EventHandler(this.bServer_Click);
            // 
            // bClient
            // 
            resources.ApplyResources(this.bClient, "bClient");
            this.bClient.Name = "bClient";
            this.bClient.UseVisualStyleBackColor = true;
            this.bClient.Click += new System.EventHandler(this.bClient_Click);
            // 
            // StartMenu
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bServer);
            this.Controls.Add(this.bClient);
            this.MaximizeBox = false;
            this.Name = "StartMenu";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bServer;
        private System.Windows.Forms.Button bClient;
    }
}

