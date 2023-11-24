using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace AMS
{
    public partial class EditNodeCM : Form
    {
        public System.Windows.Forms.ListView lv;
        public ListViewItem lvi;
        
        private List<string> detectedProcesses = new List<string>();

        public EditNodeCM()
        {
            InitializeComponent();
        }

        // Получаем информацию об узле
        private void EditNode_Load(object sender, EventArgs e)
        {
            // Получаем имя узла

            if (lvi.SubItems.Count > 2)
                textBox1.Text = lvi.SubItems[2].Text;

            // Получаем имя узла на карте
            if (lvi.SubItems.Count > 2)
                textBox4.Text = lvi.SubItems[7].Text;

            // Получаем IP-адрес узла

            if (lvi.Text.Length > 0)
                textBox2.Text = lvi.Text;    
                                             
            // Получаем MAC-адрес узла
            
            if (lvi.SubItems.Count > 1)
                textBox3.Text = lvi.SubItems[1].Text; 

            // Формируем список запущенных процессов по DNS-имени узла
            DNode node = new DNode();
            if (node.IsInMyIPv4Subnet(IPAddress.Parse(lvi.Text)))
            {
                try
                {
                    Process[] runningProcesses = Process.GetProcesses(lvi.SubItems[2].Text);

                    if (runningProcesses.Length > 0)

                        foreach (Process runningProcess in runningProcesses)

                            // Если процесс найден добавляем в список

                            if (runningProcess.ProcessName.Length > 0)
                                detectedProcesses.Add(runningProcess.ProcessName);
                }
                catch (Exception) { }
            }
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

            // Имя узла на карте

            if (textBox4.Text.Length > 0)
                activeNode.SubItems.Add(textBox4.Text);
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
