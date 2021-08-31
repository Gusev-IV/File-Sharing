using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_File_Sharing
{
    /// <summary>
    /// Класс сервера
    /// </summary>
    class ServerObject
    {
        /// <summary>
        /// Сервер для подключения по TCP
        /// </summary>
        public static TcpListener tcpListener;
        /// <summary>
        /// Сервер для подключения по UDP
        /// </summary>
        public UdpClient udpListener;
        /// <summary>
        /// Подключенные клиенты
        /// </summary>
        public List<ClientObject> clients = new List<ClientObject>();
        /// <summary>
        /// Форма окна сервера
        /// </summary>
        ServerMenu form;
        /// <summary>
        /// Конфигурация сервера
        /// </summary>
        public ConfigServer Config;
        /// <summary>
        /// Переменная работы сервера UDP
        /// </summary>
        private bool alive;
        public ServerObject(ServerMenu form, ConfigServer config)
        {
            this.form = form;
            this.Config = config;
        }
        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="clientObject">Клиент</param>
        protected internal void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
            form.Invoke(new MethodInvoker(() =>
            {
                form.listUsers.Nodes.Add(new TreeNode());
                form.listUsers.Nodes[clients.Count - 1].Nodes.Add(new TreeNode().Text = "IP:" + clientObject.IpAddress);
                form.listUsers.Nodes[clients.Count - 1].Nodes.Add(new TreeNode().Text = "Port:" + clientObject.Port.ToString());
                form.sessions.AddClient("", clientObject.IpAddress, int.Parse(clientObject.Port.ToString()));
            }));
        }
        /// <summary>
        /// Удаление клиента по ID
        /// </summary>
        /// <param name="id">ID клиента</param>
        protected internal void RemoveConnection(string id)
        {
            try
            {
                // получаем по id закрытое подключение
                ClientObject client = clients.FirstOrDefault(c => c.Id == id);
                // удаляем его из списка подключений
                if (client != null)
                {
                    for (int i = 0; i < clients.Count; i++)
                    {
                        if (clients[i] == client)
                        {
                            form.Invoke(new MethodInvoker(() =>
                            {
                                if (form.lClientSelect.Text == client.NameUser)
                                {
                                    form.lClientSelectInfo.Text = "";
                                    form.lClientSelect.Text = "";
                                }
                                if (form.listUsers.Nodes.Count > i)
                                    form.listUsers.Nodes.RemoveAt(i);
                                form.sessions.RemoveClient(i);
                            }));
                            client.Close();
                        }
                        else
                        {
                            Command cm = new Command();
                            cm.Name = CommandName.ClientsRemove;
                            cm.Parametr.Add(client.IpAddress);
                            cm.Parametr.Add(client.Port);
                            clients[i].SendCommand(cm);
                        }
                    }
                }
                clients.Remove(client);
            }
            catch { }
        }
        /// <summary>
        /// Прослушивание входящих подключений по TCP
        /// </summary>
        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse(Config.getIP()), Config.getPort());
                tcpListener.Start();
                while (Config.getRun())
                {
                    //if (Token.IsCancellationRequested) return;
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(tcpClient, this, form);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch { }
            finally
            {
                Disconnect();
            }
        }
        /// <summary>
        /// Прослушивание входящих подключений по UDP
        /// </summary>
        protected internal void ListenUDP()
        {
            try
            {
                udpListener = new UdpClient(new IPEndPoint(IPAddress.Parse(Config.getIP()), Config.getPort() - 1));
                udpListener.EnableBroadcast = true;
                Task receiveTask = new Task(ReceiveMessagesUDP);
                receiveTask.Start();
            }
            catch { DisconnectUDP(); }
        }
        /// <summary>
        /// Чтение входящих сообщений по UDP
        /// </summary>
        private void ReceiveMessagesUDP()
        {
            alive = true;
            try
            {
                while (alive)
                {
                    IPEndPoint remoteIp = null;
                    byte[] data = udpListener.Receive(ref remoteIp);
                    string mes = Encoding.Unicode.GetString(data);
                    if (mes == "are you a server?")
                    {
                        byte[] reply = Encoding.Unicode.GetBytes("yes I am a server|" +
                            Config.getIP() + "|" + Config.getName());
                        udpListener.Send(reply, reply.Length, remoteIp);
                    }
                }
            }
            catch { DisconnectUDP(); }
        }
        /// <summary>
        /// Выключение сервера UDP
        /// </summary>
        protected internal void DisconnectUDP()
        {
            alive = false;
            if (udpListener != null)
            {
                udpListener.Close();
            }
        }
        /// <summary>
        /// Выключение сервера TCP
        /// </summary>
        protected internal void Disconnect()
        {
            if (tcpListener != null)
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    clients[i].Close(); //отключение клиента
                }
                tcpListener.Stop(); //остановка сервера
            }
        }
        
    }
}
