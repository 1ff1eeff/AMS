using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AMS
{
    internal class DNodes
    {
        public System.Windows.Forms.ProgressBar pb;

        //private DNode node;
        private List<DNode> nodes = new List<DNode>();

        public List<DNode> Nodes { get => nodes; set => nodes = value; }


        /// <summary>
        /// Поиск активных устройств в диапазоне.
        /// </summary>
        /// <param name="ips">Список IP-адресов диапазона.</param>
        /// <param name="cancelToken">Токен прерывания задачи.</param>
        /// <param name="pingDelay">Таймаут ping-запроса.</param>
        /// <returns></returns>
        /// 
        public async Task AliveInRange(List<IPAddress> ips,
            CancellationToken cancelToken,
            int pingDelay = 500)
        {
            // Сконвертированная в массив байт строка для отправки в запросе

            byte[] buffer = Encoding.ASCII.GetBytes("We are ping this IP for testing purposes.");

            pb.Maximum = ips.Count;

            foreach (IPAddress ip in ips)
            {
                pb.PerformStep();

                if (cancelToken.IsCancellationRequested)
                    return;

                Ping ping = new Ping();

                // Посылаем асинхронный ping запрос с таймаутом 500 мс

                PingReply response = await ping.SendPingAsync(ip, 500);

                if (response != null && response.Status == IPStatus.Success)
                {
                    DNode node = new DNode();

                    // IP-адрес активного узла

                    node.Ip = response.Address.ToString();

                    // DNS-имя активного узла

                    try
                    {
                        IPHostEntry entry = Dns.GetHostEntry(ip);
                        if (entry != null)
                            node.Name = entry.HostName;
                    }
                    catch (SocketException) { }

                    // MAC-адрес узла

                    node.SetMac(response.Address);

                    // Добавляем активный узел к списку узлов

                    Nodes.Add(node);

                }
            }
        }
    }
}
