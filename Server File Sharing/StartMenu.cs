using System;
using System.Drawing;
using System.Windows.Forms;

namespace Server_File_Sharing
{
    public partial class StartMenu : Form
    {
        /// <summary>
        /// Максимальное число байт допустимых на принятие и отправку
        /// </summary>
        static protected internal int MaxByteStream = 314572800;
        public StartMenu()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void bClient_Click(object sender, EventArgs e)
        {
            ClientMenu client = new ClientMenu();
            this.Hide();
            client.MaximumSize = new Size(350, 245);
            client.MinimumSize = new Size(350, 245);
            client.StartPosition = FormStartPosition.CenterScreen;
           // client.Location = this.Location;
            client.ShowDialog();
            this.Location = client.Location;
            this.Show();
            GC.Collect();
        }
        private void bServer_Click(object sender, EventArgs e)
        {
            ServerMenu server = new ServerMenu();
            this.Hide();
            server.MaximumSize = new Size(370, 210);
            server.MinimumSize = new Size(370, 210);
            server.StartPosition = FormStartPosition.CenterScreen;
            server.ShowDialog();
            this.Location = server.Location;
            this.Show();
            GC.Collect();
        }
    }
}
