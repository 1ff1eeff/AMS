using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;

namespace AMS
{
    public partial class CreateMap : Form
    {
        // Для связи с элементом карты главной формы

        public TabControl tc;

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);

        // Формируем новые узлы

        List<DNodeInfo> dNodesInfo = new List<DNodeInfo>();

        public CreateMap()
        {
            InitializeComponent();
        }


        // ОК
        private void button6_Click(object sender, EventArgs e)
        {
            // Создаём пустую карту

            if (radioButton1.Checked)
            {
                // Создаём новую вкладку на карте

                TabPage tp = new TabPage(textBox9.Text);
                tc.TabPages.Add(tp);
                tc.SelectTab(tp);
            }

            // Создаём карту на основании сканирования IP диапазонов

            if (radioButton2.Checked)
            {
                // Создаём новую карту

                if (!checkBox1.Checked)
                {
                    // Создаём новую вкладку на карте

                    TabPage tp = new TabPage(textBox9.Text);
                    tc.TabPages.Add(tp);
                    tc.SelectTab(tp);
                }

                // Заполняем данные нового узла

                if (listView2.Items.Count > 0)
                    foreach (ListViewItem activeNodeItem in listView2.Items)
                    {
                        DNodeInfo node = new DNodeInfo();

                        // IP-адрес
                        
                        if (activeNodeItem.Text.Length > 0)
                            node.Ip = activeNodeItem.Text;

                        // МАС-адрес

                        if (activeNodeItem.SubItems.Count > 1 && activeNodeItem.SubItems[1].Text != " - ")
                            node.Mac = activeNodeItem.SubItems[1].Text;

                        // Имя узла

                        if (activeNodeItem.SubItems.Count > 2 && activeNodeItem.SubItems[2].Text != " - ")
                            node.Name = activeNodeItem.SubItems[2].Text;

                        // Сервисы

                        if (activeNodeItem.SubItems.Count > 3 && activeNodeItem.SubItems[3].Text != " - ")
                        {                            
                            node.Services = activeNodeItem.SubItems[3].Text.Split(';');
                        }                            

                        // Тип устройства

                        if (activeNodeItem.SubItems.Count > 4 && activeNodeItem.SubItems[4].Text != " - ")
                            node.Type = activeNodeItem.SubItems[4].Text;

                        // Стандарт передачи данных

                        if (activeNodeItem.SubItems.Count > 5 && activeNodeItem.SubItems[5].Text != " - ")
                            node.Standard = activeNodeItem.SubItems[5].Text;

                        // Протокол передачи данных

                        if (activeNodeItem.SubItems.Count > 6 && activeNodeItem.SubItems[6].Text != " - ")
                            node.Protocol = activeNodeItem.SubItems[6].Text;

                        dNodesInfo.Add(node);
                    }

                // Добавляем узел на карту

                int x = 10, y = 0, spacer = 10;
                foreach (DNodeInfo node in dNodesInfo)
                {
                    // Подготавливаем элемент представляющий узел

                    DeviceNode dn = new DeviceNode
                    {
                        Location = new Point(x, y),
                        dNodeInfo = node,
                    };

                    // Располагаем каждый последующий элемент
                    // на расстоянии ширины элемента и разделителя
                    // Если элемент слишком близко к краю формы,
                    // то переносим на следующую строку

                    if (x < tc.Width - (dn.Size.Width * 2 + spacer))
                        x += dn.Size.Width + spacer;
                    else
                    {
                        x = 10;
                        y += dn.Size.Height + spacer;
                    }
                    // Добавляем новый элемент на карту
                    
                        tc.TabPages[tc.SelectedIndex].Controls.Add(dn);
                }
            }
            Close();
        }

        //private void Tp_Click(object? sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        // Отмена
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            // Очищаем поле для ввода имени карты

            textBox9.Text = "";
        }

        // Добавить диапазон в список
        private void button2_Click(object sender, EventArgs e)
        {
            if (IsIpValid(textBox1.Text) && IsIpValid(textBox1.Text))
            {
                ListViewItem lvi = new ListViewItem(textBox1.Text);
                lvi.SubItems.Add("–");
                lvi.SubItems.Add(textBox2.Text);
                listView1.Items.Add(lvi);
            }
        }

        // Удалить IP из списка активных
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in listView2.SelectedItems)
                listView2.Items.Remove(eachItem);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = radioButton2.Checked;
            checkBox1.Enabled = radioButton2.Checked;
        }

        // Проверить корректность ввода IP-адреса в поле "Начальный IP-адрес"
        private void textBox1_Leave(object sender, EventArgs e)
        {

            if (!IsIpValid(textBox1.Text)) MessageBox.Show("Некорректный начальный IP-адрес!");
        }

        // Скопировать значение начального IP-адреса в поле "Финальный IP-адрес"
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
        }

        // Проверить корректность ввода IP-адреса в поле "Финальный IP-адрес"
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!IsIpValid(textBox2.Text)) MessageBox.Show("Некорректный финальный IP-адрес!");
        }

        // Удалить выделенную строку из списка диапазонов сканирования
        private void button5_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in listView1.SelectedItems)
                listView1.Items.Remove(eachItem);
        }

        CancellationTokenSource cts = new CancellationTokenSource();

        // Сканировать диапазон IP-адресов на наличие активных
        private async void button7_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                if (button7.Text == "Сканировать")
                {
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;
                    button7.Text = "Остановить";
                    if (cts != null)
                        cts.Dispose();
                    cts = new CancellationTokenSource();
                    await PingItemList(cts.Token);
                }
                else
                {
                    button7.Text = "Сканировать";
                    progressBar1.Visible = false;
                    if (cts != null)
                        cts.Cancel();
                }
            }
        }

        async Task PingItemList(CancellationToken cancelToken)
        {
            var ping = new Ping();


            try
            {
                // Создаём класс управления событиями в потоке

                AutoResetEvent waiter = new AutoResetEvent(false);

                // Записываем в массив байт сконвертированную строку для отправки в запросе

                byte[] buffer = Encoding.ASCII.GetBytes("We are ping this IP for testing purposes.");

                // Начальный и финальный IP-адреса диапазона сканирования

                IPAddress firstIpAddress;
                IPAddress lastIpAddress;

                // По данным элемента формы listView1 формируем диапазон IP-адресов для сканирования

                foreach (ListViewItem lvi in listView1.Items)
                {
                    // Получаем значение начального адреса из первого столбца

                    firstIpAddress = IPAddress.Parse(lvi.SubItems[0].Text);

                    // Получаем значение финального адреса из третьего столбца

                    lastIpAddress = IPAddress.Parse(lvi.SubItems[2].Text);

                    // Составляем список всех адресов расположенных от начального до финального IP

                    var range = IPAddressesRange(firstIpAddress, lastIpAddress);

                    progressBar1.Maximum = range.Count;


                    foreach (var ipAddress in range)
                    {
                        if (cancelToken.IsCancellationRequested)
                            return; 

                        progressBar1.PerformStep();

                        // Посылаем асинхронный ping запрос с таймаутом 500 мс

                        PingReply resp = await ping.SendPingAsync(ipAddress, 500);

                        if (resp != null && resp.Status == IPStatus.Success)
                        {
                            string activeHostIP = resp.Address.ToString();

                            string activeHostName = GetHostName(resp.Address.ToString());

                            ListViewItem lvi2 = new ListViewItem();

                            // IP-адрес

                            lvi2.Text = activeHostIP;

                            // Находим MAC-адрес узла

                            if (IsInMyIPv4Subnet(lvi.Text))
                            {
                                
                                IPAddress ip = IPAddress.Parse(lvi.Text); // IP-адрес узла для определения MAC-адреса

                                byte[] macAddr = new byte[6];
                                uint macAddrLen = (uint)macAddr.Length;

                                if (SendARP(BitConverter.ToInt32(ip.GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) != 0)
                                    throw new InvalidOperationException("SendARP failed.");

                                string[] str = new string[(int)macAddrLen];
                                for (int i = 0; i < macAddrLen; i++)
                                    str[i] = macAddr[i].ToString("x2");
                                if (str.Length > 0)
                                    lvi2.SubItems.Add(string.Join(":", str));
                            }

                            if (activeHostName != null && activeHostName.Length > 0)
                            {
                                // Имя узла
                                lvi2.SubItems.Add(activeHostName);                                
                            }

                            listView2.Items.Add(lvi2);
                        }
                    }
                    progressBar1.Value = 0;
                    button7.Text = "Сканировать";
                    progressBar1.Visible = false;

                }

            }
            catch (Exception) { }

        }

        public static string GetHostName(string ipAddress)
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry(ipAddress);
                if (entry != null)
                {
                    return entry.HostName;
                }
            }
            catch (SocketException) { }

            return null;
        }

        /// <summary>
        /// Проверка корректности ввода IP-адреса.
        /// </summary>
        /// <param name="ip">Проверяемый IP-адрес.</param>
        /// <returns></returns>
        public static bool IsIpValid(string ip)
        {
            return ip != null && ip.Count(c => c == '.') == 3 && IPAddress.TryParse(ip, out IPAddress address);
        }

        /// <summary>
        /// Создание списка всех адресов из заданного диапазона.
        /// </summary>
        /// <param name="firstIPAddress">Начальный IP-адрес.</param>
        /// <param name="lastIPAddress">Финальный IP-адрес.</param>
        /// <returns></returns>
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

        private void button8_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = new ListViewItem();
            if (listView2.SelectedItems.Count != 0)
            {
                //selectedItem = listView2.SelectedItems[0];

                EditNode editNode = new EditNode()
                {
                    lv = listView2,
                    lvi = listView2.SelectedItems[0]                    
                };

                editNode.ShowDialog();
            }

        }

        // Находится ли узел в одной подсети с АСМ
        private static bool IsInMyIPv4Subnet(string IP)
        {
            IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress address in addresses)
                if (address.AddressFamily == AddressFamily.InterNetwork && IsMatchMask(IPAddress.Parse(IP), address, 16))
                    return true;
            return false;
        }

        // Находятся ли два узла в одной подсети
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
    }
}
