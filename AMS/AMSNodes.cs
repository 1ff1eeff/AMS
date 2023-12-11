using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AMS
{
    /// <summary>
    /// Класс для хранения и управления информацией об узлах сети.
    /// </summary>
    internal class AmsNodes
    {
        // Для связи с индикатором выполнения операции.

        public System.Windows.Forms.ProgressBar pb;

        // Объявляем и инициализируем список для хранения информации об узлах сети.

        private List<AmsNode> _nodes = new List<AmsNode>();

        // Функции для получения и задания информации об узлах сети.

        public List<AmsNode> Nodes { get => _nodes; set => _nodes = value; }

        // Стандартный конструктор.

        public AmsNodes() 
        { 
        }

        /// <summary>
        /// Поиск активных устройств в диапазоне.
        /// </summary>
        /// <param name="ips">Список IP-адресов диапазона.</param>
        /// <param name="cancelToken">Токен прерывания задачи.</param>
        /// <param name="pingDelay">Таймаут ping-запроса.</param>

        public async Task AliveInRange(List<IPAddress> ips,
            CancellationToken cancelToken,
            int pingDelay = 500)
        {
            // Устанавливаем максимальное значение индикатора выполнения операции равным количеству адресов в списке.

            pb.Maximum = ips.Count;

            // Анализируем IP-адреса в списке.

            foreach (IPAddress ip in ips)
            {
                // Увеличиваем текущую позицию индикатора хода выполнения операции.

                pb.PerformStep();

                // Если не получен запрос на отмену операции.

                if (!cancelToken.IsCancellationRequested)
                {
                    // Объявляем и инициализируем объект класса Ping для осуществления ping-запросов.

                    Ping ping = new Ping();

                    // Посылаем асинхронный ping-запрос с указанным таймаутом.

                    PingReply response = await ping.SendPingAsync(ip, pingDelay);

                    // Если получен результат запроса и его статус "Успешно".

                    if (response != null && response.Status == IPStatus.Success)
                    {
                        // Объявляем и инициализируем объект для хранения информации об узле сети.

                        AmsNode node = new AmsNode();

                        // Задаём IP-адрес активного узла.

                        node.Ip = response.Address.ToString();

                        // Блок кода, в котором может произойти исключение.

                        try
                        {
                            // Объявляем и инициализируем объект для хранения сведений об адресе узла.

                            IPHostEntry entry = Dns.GetHostEntry(ip);

                            // Если объект существует.

                            if (entry != null)
                            {
                                // Задаём имя активного узла.

                                node.Name = entry.HostName;
                            }
                        }

                        // Обрабатываем исключения.

                        catch (SocketException) 
                        { 
                        }

                        // Задаём MAC-адрес узла.

                        node.SetMac(response.Address);

                        // Добавляем текущий узел к списку активных узлов.

                        Nodes.Add(node);

                    }
                }

                // Если получен запрос на отмену операции.

                else
                {
                    // Покидаем функцию.

                    return;
                }
            }
        }
    }
}
