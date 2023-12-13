using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMS
{
    public partial class EditNodeM : Form
    {
        // Для связи с главной формой

        private AmsNode node;
        
        public TabControl tc;

        private List<string> detectedProcesses = new List<string>();        

        public AmsNode Node { get => node; set => node = value; }

        public EditNodeM()
        {
            InitializeComponent();
        }

        // ОК
        private void button7_Click(object sender, EventArgs e)
        {
            foreach (DeviceNode nodeOnMap in tc.SelectedTab.Controls.OfType<DeviceNode>())
            {
                if (nodeOnMap.DNode.IsSelected 
                    && nodeOnMap.DNode.Id == node.Id)
                {
                    // Задаём имя узла

                    if (textBox1.Text.Length > 0)
                        nodeOnMap.DNode.Name = textBox1.Text;

                    // Задаём имя узла на карте

                    if (textBox4.Text.Length > 0)
                    {
                        nodeOnMap.DNode.NameOnMap = textBox4.Text;
                        nodeOnMap.LbSetNameOnMap(textBox4.Text);
                    }

                    // Задаём IP-адрес

                    if (textBox2.Text.Length > 0)
                    {
                        nodeOnMap.DNode.Ip = textBox2.Text;
                    }

                    // Задаём MAC-адрес узла

                    if (textBox3.Text.Length > 0)
                    {
                        nodeOnMap.DNode.Mac = textBox3.Text;
                    }

                    // Задаём список процессов

                    if (listBox4.Items.Count > 0)
                    {                        
                        nodeOnMap.DNode.Services = new string[listBox4.Items.Count];
                        for (int i = 0; i < listBox4.Items.Count; i++)                        
                            nodeOnMap.DNode.Services[i] = listBox4.Items[i].ToString();
                    }
                    
                    // Задаём тип узла

                    if (comboBox3.Items.Count > 0)
                    {
                        nodeOnMap.DNode.Type = comboBox3.Text;
                    }

                    // Задаём стандарт передачи данных

                    if (comboBox1.Items.Count > 0)
                    {
                        nodeOnMap.DNode.Standard = comboBox1.Text;
                    }
                    // Задаём протокол передачи данных

                    if (comboBox2.Items.Count > 0)
                    {
                        nodeOnMap.DNode.Protocol = comboBox2.Text;
                    }

                }
            }

            Close();
        }

        // Отмена
        private void button8_Click(object sender, EventArgs e)
        {
            Close();
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
            try
            {
                // Формируем список запущенных процессов по DNS-имени узла

                Process[] runningProcesses = Process.GetProcesses(node.Name);

                if (runningProcesses.Length > 0)

                    foreach (Process runningProcess in runningProcesses)

                        // Если процесс найден добавляем в список

                        if (runningProcess.ProcessName.Length > 0)
                            detectedProcesses.Add(runningProcess.ProcessName);
            }
            catch (Exception) { }

            // Выбираем процессы для мониторинга

            SelectProcess selectProcess = new SelectProcess
            {
                detectedProcesses = detectedProcesses,
                lb = listBox4
            };
            selectProcess.ShowDialog();
        }

        // Получаем информацию об узле
        private void EditNodeM_Load(object sender, EventArgs e)
        {
            this.Text += " (" + node.Id + ")";

            // Получаем имя узла

            if (node.Name.Length > 0 ) 
                textBox1.Text = node.Name;

            // Получаем имя узла на карте

            if (node.NameOnMap.Length > 0)
                textBox4.Text = node.NameOnMap;
            
            // Получаем IP-адрес узла

            if (node.Ip.Length > 0)
                textBox2.Text = node.Ip;

            // Получаем MAC-адрес узла

            if (node.Mac.Length > 0)
                textBox3.Text = node.Mac;

            // Получаем список процессов

            foreach (string service in node.Services)
            {
                if (service.Length > 0)
                    listBox4.Items.Add(service);
            }

            // Получаем тип узла

            if (node.Type.Length > 0)
                comboBox3.Text = node.Type;

            // Получаем стандарт передачи данных

            if (node.Type.Length > 0)
                comboBox1.Text = node.Standard;

            // Получаем протокол передачи данных

            if (node.Type.Length > 0)
                comboBox2.Text = node.Protocol;
        }
    }
}
