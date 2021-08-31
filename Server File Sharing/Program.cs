using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace Server_File_Sharing
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartMenu());
        }

    }
    /// <summary>
    /// Класс конфигурации серверной части приложение
    /// </summary>
    public class ConfigServer
    {
        /// <summary>
        /// Ip адрес сервера
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// Наименование сервера
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Путь до стандартной папки сохранения
        /// </summary>
        public string PathSave { get; set; }
        /// <summary>
        /// Порт сервера
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Статус работы сервера
        /// </summary>
        public bool Run { get; set; }
        /// <summary>
        /// Список ip адресов компьютера
        /// </summary>
        public List<string> HostIP { get; set; }
        /// <summary>
        /// Настройки приложения
        /// </summary>
        Properties.Settings Settings { get; set; }
        /// <summary>
        /// Создает новый экземпляр класса и загружает настройки приложения
        /// </summary>
        public ConfigServer()
        {
            Settings = new Properties.Settings();
            if (!CheckMac())
            {
                Settings.ServerIp = "";
                Settings.ServerName = "";
                Settings.ServerPathSave = "";
                Settings.ServerPort = "";
                Settings.ClientIP = "";
                Settings.ClientIpServer = "";
                Settings.ClientName = "";
                Settings.ClientPort = "";
            }
            IP = Settings.ServerIp;
            Name = Settings.ServerName;
            try
            {
                Port = int.Parse(Settings.ServerPort);
            }
            catch { Port = 8900; }
            PathSave = Settings.ServerPathSave;
            HostIP = ListIpv4();
            setIP();
        }
        /// <summary>
        /// Возвращает установленный Ip адрес сервера
        /// </summary>
        /// <returns>Ip адрес</returns>
        public string getIP()
        {
            return IP;
        }
        /// <summary>
        ///  Возвращает указанный Ip адрес компьютера
        /// </summary>
        /// <param name="position"> Номер ip адреса</param>
        /// <returns>Ip адрес</returns>
        public string getIPHost(int position)
        {
            if (HostIP == null || HostIP.Count == 0)
                return "";
            if (HostIP.Count <= position)
                return "";
            else
                return HostIP[position];
        }
        /// <summary>
        /// Возвращает установленный порт сервера
        /// </summary>
        /// <returns>Порт сервера</returns>
        public int getPort()
        {
            return Port;
        }
        /// <summary>
        /// Возвращает статус работы сервера
        /// </summary>
        /// <returns>Статус работы сервера</returns>
        public bool getRun()
        {
            return Run;
        }
        /// <summary>
        /// Возвращает установленное наименование сервера 
        /// </summary>
        /// <returns>Установленное наименование сервера </returns>
        public string getName()
        {
            return Name;
        }
        /// <summary>
        /// Возвращает установленный стандартный путь сохранения файлов
        /// </summary>
        /// <returns>Путь сохранения файлов</returns>
        public string getPathSave()
        {
            return PathSave;
        }
        /// <summary>
        /// Сохраняет текущие настройки сервера в настройки приложения
        /// </summary>
        public void SaveSettings()
        {
            Settings.ServerIp = IP;
            Settings.ServerName = Name;
            Settings.ServerPathSave = PathSave;
            Settings.ServerPort = Port.ToString();
            Settings.Save();
        }
        /// <summary>
        /// Возвращает список Ip адресов компьютера
        /// </summary>
        /// <returns>Список Ip адресов компьютера</returns>
        public static List<string> ListIpv4()
        {
            List<string> list = new List<string>();
            foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (CheckIpv4(ip.ToString()))
                    list.Add(ip.ToString());
            }
            return list;
        }
        /// <summary>
        /// Проверяет строку на совпадение с Ip адреса версии 4
        /// </summary>
        /// <param name="ip">Входная строка</param>
        /// <returns>Являетмя ли строка ip адресом версии 4</returns>
        public static bool CheckIpv4(string ip)
        {
            bool check = false;
            if (3 == CountChar(ip, '.'))
            {
                try
                {
                    ip = ip + ".";
                    for (int i = 0; i < 4; i++)
                    {
                        string value = ip.Substring(0, ip.IndexOf("."));
                        if (!CheckRange(value, 255))
                            return false;
                        ip = ip.Remove(0, value.Length + 1);
                    }
                    check = true;
                }
                catch { check = false; }
            }
            return check;
        }
        /// <summary>
        /// Проверяет текст по маске и возвращает измененный текст
        /// </summary>
        /// <param name="text">Текст для проверки/param>
        /// <returns>Измененный текст</returns>
        public static string Mask(string text)
        {
            string returnText = text;
            try
            {
                while (true)
                {
                    int countPoint = CountChar(returnText, '.');
                    if (countPoint > 0 && countPoint < 4)
                    {
                        string valStr = returnText;
                        int position = 0;
                        for (int i = 0; i < countPoint; i++)
                        {
                            string val = valStr.Substring(0, valStr.IndexOf("."));
                            if (val.Length > 0 && val.Length < 4)
                            {
                                if (CheckRange(val, 255))
                                {
                                    position = position + val.Length + 1;
                                    valStr = valStr.Remove(0, val.Length + 1);
                                }
                                else
                                {
                                    position = position + val.Length - 1;
                                    return returnText.Remove(position, 1);
                                }
                            }
                            else
                            {
                                returnText = returnText.Remove(position, 1);
                                continue;
                            }
                        }
                        if(valStr.Length > 0)
                        {
                            if (CheckRange(valStr, 255))
                            {
                                if(countPoint < 3 && valStr.Length == 3)
                                    return returnText + ".";
                                else
                                    return returnText;
                            }
                            else
                                return returnText.Remove(returnText.Length - 1, 1);
                        }
                        else
                            return returnText;
                    }
                    else
                    {
                        if (CheckRange(returnText, 255))
                        {
                            if (returnText.Length == 3)
                                return returnText + ".";
                            else
                                return returnText;
                        }
                        else
                        {
                            if (returnText.Length > 0)
                                return returnText.Remove(returnText.Length - 1, 1);
                            else
                                return returnText;
                        }
                    }
                }
            }
            catch { return text; }
        }
        /// <summary>
        /// Возвращает количество совпадений заданного символа в заданной строке
        /// </summary>
        /// <param name="text">Строка</param>
        /// <param name="character">Символ</param>
        /// <returns>Количество повторений заданного символа в заданной строке</returns>
        private static int CountChar(string text, char character)
        {
            int count = 0;
            foreach (char value in text)
            {
                if (value == character)
                    count++;
            }
            return count;
        }
        /// <summary>
        /// Проверяет соответствует ли заданный текст промежутку между 0 и заданным числом
        /// </summary>
        /// <param name="text">Текст</param>
        /// <param name="range">Число</param>
        /// <returns>true - если соответствует, false - если не соответствует</returns>
        public static bool CheckRange(string text, int range)
        {
            return CheckRange(text, 0 , range);
        }
        /// <summary>
        /// Проверяет соответствует ли заданный текст заданному промежутку
        /// </summary>
        /// <param name="text">Заданный текст</param>
        /// <param name="rangeMin">Минимальное значение</param>
        /// <param name="rangeMax">Максимальное значение</param>
        /// <returns>true - если соответствует, false - если не соответствует</returns>
        public static bool CheckRange(string text, int rangeMin, int rangeMax)
        {
            bool check = false;
            try
            {
                int value = int.Parse(text);
                if (value >= rangeMin && value <= rangeMax)
                    check = true;
            }
            catch { check = false; }
            return check;
        }
        /// <summary>
        /// Проверяет совпадает ли MAC адрес компьютера с настройками приложения 
        /// </summary>
        /// <returns></returns>
        private bool CheckMac()
        {
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string mac = nic.GetPhysicalAddress().ToString();
                if (mac == Settings.MAC)
                    return true;
                else
                {
                    Settings.MAC = mac;
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// Устанавливает первый Ip адрес в списке Ip адресов компьютера
        /// </summary>
        private void setIP()
        {
            if (IP == null || IP == "")
            {
                IP = getIPHost(0);
            }
        }
    }
}
