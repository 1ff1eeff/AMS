using System;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;

namespace AMS
{
    /// <summary>
    /// Класс для хранения и управления информацией об узлах сети.
    /// </summary>
    public class DNode
    {
        private string name = "";
        private string nameOnMap = "";
        private string type = "";
        private string standard = "";
        private string protocol = "";
        private string ip = "";
        private string mac = "";
        private string[] services = Array.Empty<string>();
        private bool isSelected = false;
        private string id;

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
        /// MAC-адрес узла.
        /// </summary>
        /// <param name="ip">IP-адрес узла.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void SetMac(IPAddress ip) 
        {            
            if (IsInMyIPv4Subnet(ip))
            {
                byte[] macAddress = new byte[6];
                uint macAddressLength = (uint)macAddress.Length;
                if (SendARP(BitConverter.ToInt32(ip.GetAddressBytes(), 0), 0, macAddress, ref macAddressLength) != 0)
                    throw new InvalidOperationException("SendARP failed.");
                string[] str = new string[(int)macAddressLength];
                for (int i = 0; i < macAddressLength; i++)
                    str[i] = macAddress[i].ToString("x2");
                if (str.Length > 0)
                    Mac = string.Join(":", str);
            }
        }

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


        /// <summary>
        /// Узел в одной подсети с АСМ.
        /// </summary>
        /// <param name="ip">Проверяемый IP-адрес.</param>
        /// <returns>True, если узел в одной подсети с АСМ.</returns>
        public bool IsInMyIPv4Subnet(IPAddress ip)
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress address in ips)
                if (address.AddressFamily == AddressFamily.InterNetwork && IsMatchMask(ip, address, 16))
                    return true;
            return false;
        }

        /// <summary>
        /// Узлы в одной подсети.
        /// </summary>
        /// <param name="ipAddress">Проверяемый IP-адрес.</param>
        /// <param name="subnetAsIp">IP-адрес из подсети для сравнения.</param>
        /// <param name="subnetLength">Длина маски подсети.</param>
        /// <returns>True, если узлы в одной подсети</returns>
        private bool IsMatchMask(IPAddress ipAddress, IPAddress subnetAsIp, byte subnetLength)
        {
            if (ipAddress.AddressFamily != AddressFamily.InterNetwork
                || subnetAsIp.AddressFamily != AddressFamily.InterNetwork
                || subnetLength >= 32)
                return false;

            byte[] ipBytes = ipAddress.GetAddressBytes();
            byte[] maskBytes = subnetAsIp.GetAddressBytes();

            uint ip = (uint)unchecked((ipBytes[0] << 24) | (ipBytes[1] << 16) | (ipBytes[2] << 8) | ipBytes[3]);
            uint mask = (uint)unchecked((maskBytes[0] << 24) | (maskBytes[1] << 16) | (maskBytes[2] << 8) | maskBytes[3]);
            uint significantBits = uint.MaxValue << (32 - subnetLength);

            return (ip & significantBits) == (mask & significantBits);
        }
    }
}