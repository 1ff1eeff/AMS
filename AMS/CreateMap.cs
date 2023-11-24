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
        private static extern int SendARP(int destIp, int srcIP, 
            byte[] macAddr, ref uint physicalAddrLen);
                
        List<DNode> dNodes = new List<DNode>(); // Формируем новые узлы

        CancellationTokenSource cts = new CancellationTokenSource();

        public CreateMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Проверка корректности ввода IP-адреса.
        /// </summary>
        /// <param name="ip">Проверяемый IP-адрес.</param>
        /// <returns></returns>
        public static bool IsIpValid(string ip)
        {
            return ip != null && ip.Count(c => c == '.') == 3 
                && IPAddress.TryParse(ip, out IPAddress address);
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
                        DNode node = new DNode();

                        // Уникальный идентификатор узла

                        node.Id = Guid.NewGuid().ToString();

                        // IP-адрес

                        if (activeNodeItem.Text.Length > 0)
                            node.Ip = activeNodeItem.Text;

                        // МАС-адрес

                        if (activeNodeItem.SubItems.Count > 1 
                            && activeNodeItem.SubItems[1].Text != " - ")
                            node.Mac = activeNodeItem.SubItems[1].Text;

                        // Имя узла

                        if (activeNodeItem.SubItems.Count > 2 
                            && activeNodeItem.SubItems[2].Text != " - ")
                            node.Name = activeNodeItem.SubItems[2].Text;

                        // Сервисы

                        if (activeNodeItem.SubItems.Count > 3 
                            && activeNodeItem.SubItems[3].Text != " - ")
                        {                            
                            node.Services = activeNodeItem.SubItems[3].Text.Split(';');
                        }                            

                        // Тип устройства

                        if (activeNodeItem.SubItems.Count > 4 
                            && activeNodeItem.SubItems[4].Text != " - ")
                            node.Type = activeNodeItem.SubItems[4].Text;

                        // Стандарт передачи данных

                        if (activeNodeItem.SubItems.Count > 5 
                            && activeNodeItem.SubItems[5].Text != " - ")
                            node.Standard = activeNodeItem.SubItems[5].Text;

                        // Протокол передачи данных

                        if (activeNodeItem.SubItems.Count > 6 
                            && activeNodeItem.SubItems[6].Text != " - ")
                            node.Protocol = activeNodeItem.SubItems[6].Text;
                        
                        // Имя узла на карте

                        if (activeNodeItem.SubItems.Count > 7
                            && activeNodeItem.SubItems[7].Text != " - ")
                            node.NameOnMap = activeNodeItem.SubItems[7].Text;

                        dNodes.Add(node);
                    }

                // Добавляем узел на карту

                int x = 10, y = 0, spacer = 10;

                foreach (DNode node in dNodes)
                {
                    // Подготавливаем элемент представляющий узел

                    DeviceNode dn = new DeviceNode
                    {
                        Location = new Point(x, y), // Положение
                        DNode = node, // Узел                        
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

        // Отмена
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
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

        // Сканировать диапазон IP-адресов на наличие активных
        private async void button7_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                if (button7.Text == "Сканировать")
                {                    
                    button7.Text = "Остановить";
                    if (cts != null)
                        cts.Dispose();
                    cts = new CancellationTokenSource();

                    // Начальный и финальный IP-адреса диапазона сканирования

                    IPAddress firstIpAddress;
                    IPAddress lastIpAddress;

                    // Анализируем список диапазонов IP-адресов

                    foreach (ListViewItem lvi in listView1.Items)
                    {
                        progressBar1.Value = 0;                        
                        progressBar1.Visible = true;

                        // Формируем диапазон активных IP-адресов из элементов ListView

                        DNodes nodesList = new DNodes(); // Для хранения полученного диапазона

                        nodesList.pb = progressBar1;

                        // Получаем значение начального адреса из первого столбца

                        firstIpAddress = IPAddress.Parse(lvi.SubItems[0].Text);

                        // Получаем значение финального адреса из третьего столбца

                        lastIpAddress = IPAddress.Parse(lvi.SubItems[2].Text);

                        await nodesList.AliveInRange(IPAddressesRange(firstIpAddress, lastIpAddress), cts.Token);

                        // Анализируем список активных IP-адресов

                        foreach (DNode node in nodesList.Nodes)
                        {
                            ListViewItem nodeLVI = new ListViewItem();

                            if(node.Ip.Length > 0)
                                nodeLVI.Text = node.Ip;         // IP-адрес узла
                            else
                                break;

                            if (node.Mac.Length > 0)
                                nodeLVI.SubItems.Add(node.Mac); // МАС-адрес узла
                            else
                                nodeLVI.SubItems.Add(" - ");

                            if (node.Name.Length > 0)
                                nodeLVI.SubItems.Add(node.Name);    // DNS-имя узла
                            else
                                nodeLVI.SubItems.Add(" - ");

                            nodeLVI.SubItems.Add(" - ");       // Сервисы

                            nodeLVI.SubItems.Add(" - ");       // Тип узла

                            nodeLVI.SubItems.Add(" - ");       // Стандарт передачи данных

                            nodeLVI.SubItems.Add(" - ");       // Протокол передачи данных

                            if (node.Name.Length > 0)
                                nodeLVI.SubItems.Add(node.Name);    // Имя узла на карте
                            else
                                nodeLVI.SubItems.Add(" - ");

                            // Если устройства с текущим IP-адресом 
                            // ещё нет в списке, то добавляем

                            bool ipInList = false;

                            foreach (ListViewItem item  in listView2.Items)
                            {
                                if (item.Text == node.Ip)
                                { 
                                    ipInList = true;
                                    break;
                                }
                            }
                            if (!ipInList)
                            {
                                listView2.Items.Add(nodeLVI);
                            }
                        }                        
                        button7.Text = "Сканировать";
                        progressBar1.Value = 0;
                        progressBar1.Visible = false;
                    }
                }
                else
                {
                    button7.Text = "Сканировать";
                    progressBar1.Visible = false;
                    cts?.Cancel();
                }
            }
        }
 
        // Редактировать узел
        private void button8_Click(object sender, EventArgs e)
        {
            ListViewItem selectedItem = new ListViewItem();
            if (listView2.SelectedItems.Count != 0)
            {
                EditNodeCM editNode = new EditNodeCM()
                {
                    lv = listView2,
                    lvi = listView2.SelectedItems[0]                    
                };

                editNode.ShowDialog();
            }
        }
    }
}
