using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Net.Sockets;

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

        /// <summary>
        /// Находит свободное место под новый узел на вкладке TabControl.
        /// </summary>
        /// <param name="tc">TabControl для размещения узла.</param>
        /// <returns></returns>

        Point FindSpace(TabControl tc)
        {
            
            // Позиция нового узла
                        
            Point pos = new Point(10, 10);
            
            // Разделитель
            
            int spacer = 10;

            // Получаем список всех узлов размещённых на вкладке карты

            foreach (DeviceNode node in tc.SelectedTab.Controls.OfType<DeviceNode>())
            {
                if (node != null)
                {
                    // Если текущий узел ниже - перемещаем новый узел вниз

                    if (node.Location.Y > pos.Y)                     
                        pos.Y += node.Height + spacer;
                    
                    // Если текущий узел правее - сдвигаем новый узел вправо

                    if (node.Location.X >= pos.X)
                    {
                        pos.X = node.Location.X + node.Size.Width + spacer;

                        // Если на вкладке не хватает места - перемещаем новый узел вниз

                        if (pos.X >= tc.Width - (node.Size.Width + spacer))
                        {
                            pos.X = spacer;
                            pos.Y += node.Height + spacer;
                        }
                    }
                }
            }
            return pos;
        }

        // ОК
        private void button7_Click(object sender, EventArgs e)
        {
            AmsNode node = new AmsNode()
            {
                Id = Guid.NewGuid().ToString(),
                Ip = textBox2.Text,
                Mac = textBox3.Text,
                Name = textBox1.Text,
                Services = GetAllListBoxText(listBox4),
                Type = listBox1.Text,
                Standard = comboBox1.Text,
                Protocol = comboBox2.Text,                
                NameOnMap = textBox4.Text                
            };



            DeviceNode dn = new DeviceNode
            {
                Location = FindSpace(tc),
                DNode = node
            };

            tc.TabPages[tc.SelectedIndex].Controls.Add(dn);

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

            AmsNode node = new AmsNode();                       
            string nodeName = Dns.GetHostName();
            if (nodeName.Length > 0)
            {
                textBox2.Text = Dns.GetHostAddresses(nodeName).First
                                (f => f.AddressFamily == AddressFamily.InterNetwork).ToString(); ;
                
                textBox1.Text = nodeName;

                node.SetMac(Dns.GetHostAddresses(nodeName).First
                                (f => f.AddressFamily == AddressFamily.InterNetwork));
                
                textBox3.Text = node.Mac;
            }
            else
            {
                node.Ip = "192.168.0.102";
                node.Name = "DESKTOP-HTQEQ9D";
                node.Mac = "2a:12:4c:10:0a:21";
            }

            textBox4.Text = "Мой компьютер";

            listBox1.SelectedIndex = 1;

            listBox4.Items.Clear();
            listBox4.Items.Add("AMS");
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
