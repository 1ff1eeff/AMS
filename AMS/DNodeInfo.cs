using System;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AMS
{
    /// <summary>
    /// Класс для хранения и управления информацией об узлах сети.
    /// </summary>
    public class DNodeInfo
    {
        private string name = "";
        private string type = "";
        private string standard = "";
        private string protocol = "";
        private string ip = "";
        private string mac = "";
        private string[] services = Array.Empty<string>();
        private bool isSelected = false;

        /// <summary>
        /// Имя узла
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Тип узла
        /// </summary>
        public string Type { get => type; set => type = value; }

        /// <summary>
        /// Стандарт передачи данных
        /// </summary>
        public string Standard { get => standard; set => standard = value; }

        /// <summary>
        /// Протокол передачи данных
        /// </summary>
        public string Protocol { get => protocol; set => protocol = value; }

        /// <summary>
        /// IP-адрес узла
        /// </summary>
        public string Ip { get => ip; set => ip = value; }

        /// <summary>
        /// MAC-адрес узла
        /// </summary>
        public string Mac { get => mac; set => mac = value; }

        /// <summary>
        /// Отслеживаемые сервисы узла
        /// </summary>
        public string[] Services { get => services; set => services = value; }

        /// <summary>
        /// Узел выделен на карте
        /// </summary>
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        /// <summary>
        /// Узел в одной подсети с АСМ.
        /// </summary>
        /// <param name="IP">Проверяемый IP-адрес.</param>
        /// <returns>True, если узел в одной подсети с АСМ.</returns>
        private static bool IsInMyIPv4Subnet(string IP)
        {
            IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress address in addresses)
                if (address.AddressFamily == AddressFamily.InterNetwork && IsMatchMask(IPAddress.Parse(IP), address, 16))
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
        private static bool IsMatchMask(IPAddress ipAddress, IPAddress subnetAsIp, byte subnetLength)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstIpAddress">Начальный IP-адрес диапазона сканирования.</param>
        /// <param name="lastIpAddress">Финальный IP-адрес диапазона сканирования.</param>
        /// <param name="cancelToken">Токен прерывания задачи.</param>
        /// <returns></returns>
        async Task AliveInRange(IPAddress firstIpAddress,
            IPAddress lastIpAddress,
            CancellationToken cancelToken)
        {
            // Создаём класс управления событиями в потоке

            AutoResetEvent waiter = new AutoResetEvent(false);

            // Записываем в массив байт сконвертированную строку для отправки в запросе

            byte[] buffer = Encoding.ASCII.GetBytes("We are ping this IP for testing purposes.");
            
            // Формируем диапазон IP-адресов для сканирования
            List<IPAddress> ips = new List<IPAddress>();

            foreach (IPAddress ip in ips)
            { 
                
            }
            

        }

        /// <summary>
        /// Создание списка всех адресов из заданного диапазона.
        /// </summary>
        /// <param name="firstIPAddress">Начальный IP-адрес.</param>
        /// <param name="lastIPAddress">Финальный IP-адрес.</param>
        /// <returns>Список всех IP-адресов диапазона.</returns>
        private static List<IPAddress> IPAddressesRange(IPAddress firstIPAddress, IPAddress lastIPAddress)
        {
            var firstIPAddressAsBytesArray = firstIPAddress.GetAddressBytes();
            var lastIPAddressAsBytesArray = lastIPAddress.GetAddressBytes();
            Array.Reverse(firstIPAddressAsBytesArray);
            Array.Reverse(lastIPAddressAsBytesArray);
            var firstIPAddressAsInt = BitConverter.ToInt32(firstIPAddressAsBytesArray, 0);
            var lastIPAddressAsInt = BitConverter.ToInt32(lastIPAddressAsBytesArray, 0);
            var ipAddressesInTheRange = new List<IPAddress>();
            for (var i = firstIPAddressAsInt; i <= lastIPAddressAsInt; i++)
            {
                var bytes = BitConverter.GetBytes(i);
                var newIp = new IPAddress(new[] { bytes[3], bytes[2], bytes[1], bytes[0] });
                ipAddressesInTheRange.Add(newIp);
            }
            return ipAddressesInTheRange;
        }
    }
}