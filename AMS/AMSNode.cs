using System;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Drawing;

namespace AMS
{
    /// <summary>
    /// Класс для хранения и управления информацией об узлах сети.
    /// </summary>
    public class AmsNode
    {
        // Имя узла.

        private string name = "";

        // Имя узла на карте.

        private string nameOnMap = "";

        // Тип узла.

        private string type = "";

        // Стандарт передачи данных.

        private string standard = "";

        // Протокол передачи данных.

        private string protocol = "";

        // IP-адрес узла.

        private string ip = "";

        // MAC-адрес узла.

        private string mac = "";

        // Отслеживаемые сервисы узла.

        private string[] services = Array.Empty<string>();

        // Узел выделен на карте.

        private bool isSelected = false;

        // Уникальный идентификатор узла.

        private string id;
        private Point location = new Point();

        /// <summary>
        /// Функция SendARP отправляет запрос протокола разрешения адресов (ARP) 
        /// для получения физического адреса, соответствующего указанному целевому IPv4-адресу.
        /// </summary>
        /// <param name="destIp">Целевой IPv4-адрес в виде структуры IPAddr.</param>
        /// <param name="srcIP">Исходный IPv4-адрес отправителя в виде структуры IPAddr.</param>
        /// <param name="macAddr">Указатель на массив переменных ULONG.</param>
        /// <param name="physicalAddrLen">Указатель на значение ULONG, указывающее максимальный размер 
        /// буфера в байтах, которое приложение выделило для получения физического адреса или MAC-адреса.</param>
        /// <returns></returns>
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);

        /// <summary>
        /// Имя узла.
        /// </summary>
        public string Name { get => name; set => name = value; }
        
        /// <summary>
        /// Имя узла на карте.
        /// </summary>
        public string NameOnMap { get => nameOnMap; set => nameOnMap = value; }
        /// <summary>
        /// Тип узла.
        /// </summary>
        public string Type { get => type; set => type = value; }

        /// <summary>
        /// Стандарт передачи данных.
        /// </summary>
        public string Standard { get => standard; set => standard = value; }

        /// <summary>
        /// Протокол передачи данных.
        /// </summary>
        public string Protocol { get => protocol; set => protocol = value; }

        /// <summary>
        /// IP-адрес узла.
        /// </summary>
        public string Ip { get => ip; set => ip = value; }

        /// <summary>
        /// MAC-адрес узла.
        /// </summary>
        public string Mac { get => mac; set => mac = value; }

        /// <summary>
        /// Отслеживаемые сервисы узла.
        /// </summary>
        public string[] Services { get => services; set => services = value; }

        /// <summary>
        /// Узел выделен на карте.
        /// </summary>
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        /// <summary>
        /// Уникальный идентификатор узла.
        /// </summary>
        public string Id { get => id; set => id = value; }
        public Point Location { get => location; set => location = value; }

        /// <summary>
        /// Узел в одной подсети с АСМ.
        /// </summary>
        /// <param name="ip">Проверяемый IP-адрес.</param>
        /// <returns>True, если узел в одной подсети с АСМ.</returns>
        public bool IsInMyIPv4Subnet(IPAddress ip)
        {
            // Объявляем и инициализируем массив в котором содержатся IP-адреса для локального компьютера.

            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());

            // Анализируем IP-адреса в массиве.

            foreach (IPAddress address in ips)
            {
                // Если адрес узла IPv4 и узлы расположены в одной подсети.

                if (address.AddressFamily == AddressFamily.InterNetwork && IsMaskMatch(ip, address, 16))
                {
                    return true;
                }
            }
            return false;
            
        }

        /// <summary>
        /// Узлы в одной подсети.
        /// </summary>
        /// <param name="ipAddress">Проверяемый IP-адрес.</param>
        /// <param name="subnetAsIp">IP-адрес из подсети для сравнения.</param>
        /// <param name="subnetLength">Длина маски подсети.</param>
        /// <returns>True, если узлы в одной подсети</returns>
        private bool IsMaskMatch(IPAddress ipAddress, IPAddress subnetAsIp, byte subnetLength)
        {
            // Если адрес проверяемого узла не IPv4 или адрес хоста не IPv4 или длина маски подсети больше 32 бит.

            if (ipAddress.AddressFamily != AddressFamily.InterNetwork
                || subnetAsIp.AddressFamily != AddressFamily.InterNetwork || subnetLength >= 32)
            {
                // Покидаем функцию и возвращаем False.

                return false;
            }

            // Объявляем и инициализируем битовый массив содержащий проверяемый IP-адрес.
            // (8 8 8 8 -> [0] = 8, [1] = 8, [2] = 8, [3] = 8)

            byte[] ipBytes = ipAddress.GetAddressBytes();

            // Объявляем и инициализируем битовый массив содержащий маску подсети.
            // (192 168 0 102 -> [0] = 192, [1] = 168, [2] = 0, [3] = 102)

            byte[] maskBytes = subnetAsIp.GetAddressBytes();

            // Формируем IP-адрес – представляем битовый массив как 32-битное число без знака.
            // ({[0]=8 << 24, [1]=8 << 16, [2]=8 << 8, [3]=8} -> 134744072)

            uint ip = (uint)unchecked((ipBytes[0] << 24) | (ipBytes[1] << 16) | (ipBytes[2] << 8) | ipBytes[3]);

            // Формируем маску подсети – представляем битовый массив как 32-битное число без знака.
            // ({[0]=192 << 24, [1]=168 << 16, [2]=0 << 8, [3]=1} -> 3232235622)

            uint mask = (uint)unchecked((maskBytes[0] << 24) | (maskBytes[1] << 16) | (maskBytes[2] << 8) | maskBytes[3]);

            // Определяем старшие биты маски подсети.
            // Результат операции побитового свдига влево значения максимальной величины типа uint на число бит,
            // полученных после вычисления разницы между максимальной длиной маски и длиной полученной во входном параметре.
            // (4294967295 << 16 -> 4294901760)

            uint significantBits = uint.MaxValue << (32 - subnetLength);

            // Если результат логического умножения IP-адреса и старших бит равен 
            // результату логического умножения маски и старших бит, то IP-адрес находится в одной подсети с АСМ.
            // Иначе проверяемы IP-адрес и АСМ находятся в разных подсетях.
            //(134744072 & 4294901760 == 3232235622 & 4294901760 -> 134742016 == 3232235520 -> false

            bool isMaskMatch = (ip & significantBits) == (mask & significantBits);

            // Выходим и возвращаем результат проверки.
            // (false)

            return isMaskMatch;
        }

        /// <summary>
        /// Устанавливает MAC-адрес узла.
        /// </summary>
        /// <param name="ip">IP-адрес узла.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void SetMac(IPAddress ip)
        {
            // Если узел в одной подсети с АСМ.

            if (IsInMyIPv4Subnet(ip))
            {
                // Объявляем и инициализируем битовый массив.

                byte[] macAddress = new byte[6];

                // Задаём длину MAC-адреса
                // (6).

                uint macAddressLength = (uint)macAddress.Length;

                // Получаем физический адрес узла.
                // (172.31.96.1 -> 0 21 93 110 230 50)

                SendARP(BitConverter.ToInt32(ip.GetAddressBytes(), 0), 0, macAddress, ref macAddressLength);

                // Объявляем и инициализируем массив строк с длиной равной длине MAC-адреса.
                // string[6]

                string[] str = new string[(int)macAddressLength];

                // Проходим по всем элементам массива.
                // (от 0 до 6)

                for (int i = 0; i < macAddressLength; i++)
                {
                    // Приводим физический адрес узла к шестнадцатиричному виду.
                    // (0 21 93 110 230 50 -> 00 15 5d 6e e6 32)

                    str[i] = macAddress[i].ToString("x2");
                }

                // Если длина массива равна длине MAC-адреса.

                if (str.Length == macAddressLength)
                {
                    // Формируем MAC-адрес. Объединяем строки из массива. Разделитель – двоеточие.
                    // (00:15:5d:6e:e6:32)

                    Mac = string.Join(":", str);
                }
            }
        }

    }
}