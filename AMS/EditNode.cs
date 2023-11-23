using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AMS
{
    public partial class EditNode : Form
    {
        public System.Windows.Forms.ListView lv;
        public ListViewItem lvi;

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(int destIp, int srcIP, byte[] macAddr, ref uint physicalAddrLen);

        private List<string> detectedProcesses = new List<string>();


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

        public EditNode()
        {
            InitializeComponent();
        }

        private void EditNode_Load(object sender, EventArgs e)
        {
            // Получаем имя узла

            if (lvi.SubItems.Count > 2)
                textBox1.Text = lvi.SubItems[2].Text;

            if (lvi.Text.Length > 0)
            {
                // Получаем IP-адрес узла

                textBox2.Text = lvi.Text;                                

                if (IsInMyIPv4Subnet(lvi.Text))
                {
                    // Находим MAC-адрес узла

                    IPAddress ip = IPAddress.Parse(lvi.Text); // IP-адрес узла для определения MAC-адреса

                    byte[] macAddr = new byte[6];
                    uint macAddrLen = (uint)macAddr.Length;

                    if (SendARP(BitConverter.ToInt32(ip.GetAddressBytes(), 0), 0, macAddr, ref macAddrLen) != 0)
                        throw new InvalidOperationException("SendARP failed.");

                    string[] str = new string[(int)macAddrLen];
                    for (int i = 0; i < macAddrLen; i++)
                        str[i] = macAddr[i].ToString("x2");
                    if (str.Length > 0)
                        textBox3.Text = string.Join(":", str);

                    // Находим запущенные процессы
                    try
                    {
                        Process[] runningProcesses = Process.GetProcesses(lvi.SubItems[2].Text);

                        if (runningProcesses.Length > 0)
                        {
                            foreach (Process runningProcess in runningProcesses)

                                // Если процесс найден

                                if (runningProcess.ProcessName.Length > 0)
                                    detectedProcesses.Add(runningProcess.ProcessName);
                        }
                    }
                    catch (Exception){}
                }
            }

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text += " (" + listBox1.GetItemText(listBox1.SelectedItem) + ")";
        }

        // Добавить сервис
        private void button11_Click(object sender, EventArgs e)
        {
            CreateService createService = new CreateService
            {
                lbService = listBox4
            };
            createService.ShowDialog();
        }

        // Удалить сервис
        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox4.Items.Count > 0 && listBox4.SelectedIndex >= 0)
                listBox4.Items.RemoveAt(listBox4.SelectedIndex);
        }

        // Обнаружить сервисы
        private void button1_Click(object sender, EventArgs e)
        {                            
            // Выбираем процессы для мониторинга

            SelectProcess selectProcess = new SelectProcess
            {
                detectedProcesses = detectedProcesses,
                lb = listBox4
            };
            selectProcess.ShowDialog();                
        }

        // ОК
        private void button7_Click(object sender, EventArgs e)
        {
            // Собираем полученные данные

            ListViewItem activeNode = new ListViewItem();

            // IP-адрес

            if (textBox2.Text.Length > 0)
                activeNode.Text = textBox2.Text;
            else
                activeNode.Text = " - ";

            // MAC-адрес

            if (textBox3.Text.Length > 0)
                activeNode.SubItems.Add(textBox3.Text);
            else
                activeNode.SubItems.Add(" - ");

            // Имя узла

            if (textBox1.Text.Length > 0)
                activeNode.SubItems.Add(textBox1.Text);
            else
                activeNode.SubItems.Add(" - ");

            // Сервисы

            string services = "";
            foreach (string item in listBox4.Items)
                services += item + ";";

            if (services.Length > 0)
                activeNode.SubItems.Add(services);
            else
                activeNode.SubItems.Add(" - ");

            // Очищаем выбранный элемент в конструкторе карты

            foreach (ListViewItem eachItem in lv.SelectedItems)
                lv.Items.Remove(eachItem);

            // Тип узла

            if (listBox1.SelectedItem != null)
                activeNode.SubItems.Add(listBox1.SelectedItem.ToString());
            else
                activeNode.SubItems.Add(" - ");

            // Стандарт передачи данных

            if (comboBox1.Text.Length > 0)
                activeNode.SubItems.Add(comboBox1.Text);
            else
                activeNode.SubItems.Add(" - ");

            // Протокол передачи данных

            if (comboBox2.Text.Length > 0)
                activeNode.SubItems.Add(comboBox2.Text);
            else
                activeNode.SubItems.Add(" - ");

            // Передаём новые данные узла в конструктор карты
            foreach (ListViewItem eachItem in lv.SelectedItems)
                lv.Items.Remove(eachItem);

            lv.Items.Add(activeNode);

            Close();
        }

        // Отмена
        private void button8_Click(object sender, EventArgs e)
        {

            Close();
        }
    }
}
