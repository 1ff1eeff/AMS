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
    public partial class Settings : Form
    {

        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog3.SelectedPath;
            }
        }


        private void textBox4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox4, "Ключи: %A - адрес хоста, %Т - сообщение проверки, \r\n" +
                                            "%S - статус проверки, %С - имя устройства на карте, \r\n" +
                                            "%М - имя карты, %D - дата и время, %DSC - описание проверки, \r\n" +
                                            "%PV - значение параметра мониторинга, %RT - время отклика, \r\n" +
                                            "%RET - перенос строки. \r\n" +
                                            "Значения подставляются автоматически.\r\n");
        }


        private void textBox5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox5, "Ключи: %A - адрес хоста, %Т - сообщение проверки, \r\n" +
                                            "%S - статус проверки, %С - имя устройства на карте, \r\n" +
                                            "%М - имя карты, %D - дата и время, %DSC - описание проверки, \r\n" +
                                            "%PV - значение параметра мониторинга, %RT - время отклика, \r\n" +
                                            "%RET - перенос строки. \r\n" +
                                            "Значения подставляются автоматически.\r\n");
        }

        private void textBox11_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox11, "Ключи: %A - адрес хоста, %Т - сообщение проверки, \r\n" +
                                "%S - статус проверки, %С - имя устройства на карте, \r\n" +
                                "%М - имя карты, %D - дата и время, %DSC - описание проверки, \r\n" +
                                "%PV - значение параметра мониторинга, %RT - время отклика, \r\n" +
                                "%RET - перенос строки. \r\n" +
                                "Значения подставляются автоматически.\r\n");
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel2.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            panel2.Enabled = false;
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Enabled = checkBox13.Checked;
        }

        // ОК
        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Отмена
        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
