using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMS
{
    public partial class CreateNode : Form
    {
        // Для связи с главной формой
        public TabControl tc;

        public CreateNode()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Получает значения всех элементов списка
        /// </summary>
        private static string[] GetAllListBoxText(ListBox listBox)
        {
            string[] myText = new string[listBox.Items.Count];
            for (int i = 0; i < listBox.Items.Count; i++)
                myText[i] = listBox.Items[i].ToString();
            return myText;
        }

        private void listBox1_DoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text += " (" + listBox1.GetItemText(listBox1.SelectedItem) + ")";
        }

        // Диалог "Добавить сервис в список"
        private void button11_Click(object sender, EventArgs e)
        {
            CreateService createService = new CreateService();
            createService.lbService = listBox4;
            createService.ShowDialog();
        }       

        // ОК
        private void button7_Click(object sender, EventArgs e)
        {
            DNodeInfo node = new DNodeInfo()
            {
                Name = textBox1.Text,
                Type = listBox1.Text,
                Standart = comboBox1.Text,
                Protocol = comboBox2.Text,
                Ip = textBox2.Text,
                Mac = textBox3.Text,
                Services = GetAllListBoxText(listBox4)
            };

            DeviceNode dn = new DeviceNode
            {
                Location = new Point(10, 10),
                dNodeInfo = node
            };

            tc?.TabPages[tc.SelectedIndex].Controls.Add(dn);

            Close();
        }

        // Отмена
        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ТЕСТОВЫЙ РЕЖИМ! Заполняем произвольными данными
        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = "DESKTOP-HTQEQ9D";
            textBox2.Text = "192.168.0.102";
            textBox3.Text = "fe80::288e:fdbd:d37b:a77c";
            listBox4.Items.Clear();
            listBox4.Items.Add("FTP");
            listBox4.Items.Add("notepad");
            comboBox1.SelectedIndex = 4;
            comboBox2.SelectedIndex = 0;
        }

        // Удалить сервис из списка
        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox4.Items.Count > 0 && listBox4.SelectedIndex >= 0)
                listBox4.Items.RemoveAt(listBox4.SelectedIndex);

        }

    }
}
