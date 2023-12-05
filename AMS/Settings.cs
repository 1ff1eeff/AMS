using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace AMS
{

    public partial class Settings : Form
    {

        public AmsSettings amsSettings = new AmsSettings();

        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tbConfigFile.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fbdMapsFolder.SelectedPath = amsSettings.MapsFolder;

            if (fbdMapsFolder.ShowDialog() == DialogResult.OK)
            {
                tbMapsFolder.Text = fbdMapsFolder.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fbdLogsFolder.SelectedPath = amsSettings.LogsFolder;
            if (fbdLogsFolder.ShowDialog() == DialogResult.OK)
            {
                tbLogsFolder.Text = fbdLogsFolder.SelectedPath;
            }
        }

        /// <summary>
        /// Кнопка указать файл ADB "..."
        /// </summary>
        private void button8_Click(object sender, EventArgs e)
        {

            if(!String.IsNullOrWhiteSpace(amsSettings.AdbFile))
            {
                FileInfo file = new FileInfo(amsSettings.AdbFile);
                openFileDialog2.InitialDirectory = file.DirectoryName;
            }

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                tbAdb.Text = openFileDialog2.FileName;
            }
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

        // ОК
        private void button4_Click(object sender, EventArgs e)
        {
            // Сохраняем настройки

            amsSettings.MailTo = textBox1.Text;
            amsSettings.SmtpSenderName = textBox6.Text;
            amsSettings.SmtpSenderEmail = textBox9.Text;
            amsSettings.SmtpPassword = textBox10.Text;
            amsSettings.SmtpHost = textBox7.Text;
            amsSettings.SmtpPort = Int32.Parse(textBox8.Text);
            amsSettings.Ssl = checkBox1.Checked;
            amsSettings.EmailNotification = checkBox2.Checked;
            amsSettings.PhoneToSMS = textBox12.Text;
            amsSettings.EmailToSMS = textBox13.Text;            
            amsSettings.EmailNeedsTranslit = checkBox4.Checked;
            amsSettings.SmsNeedsTranslit = checkBox5.Checked;
            amsSettings.SmsNotification = checkBox3.Checked;
            if (radioButton3.Checked) amsSettings.WayToSendSms = "adb";
            if (radioButton4.Checked) amsSettings.WayToSendSms = "email";
            amsSettings.AdbFile = tbAdb.Text;

            XmlSerializer formatter = new XmlSerializer(typeof(AmsSettings));

            using (FileStream fs = new FileStream(tbConfigFile.Text, FileMode.Create))
            {
                formatter.Serialize(fs, amsSettings);
            }

            using (FileStream fs = new FileStream(Application.StartupPath + "\\config.xml", FileMode.Create))
            {
                formatter.Serialize(fs, amsSettings);
            }

            Close();
        }

        // Отмена
        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowLicense showLicense = new ShowLicense();
            showLicense.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox10.UseSystemPasswordChar = !textBox10.UseSystemPasswordChar;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //panel3.Enabled = checkBox2.Checked;            

        }

        private void Settings_Load(object sender, EventArgs e)
        {
            // Восстанавливаем настройки

            textBox1.Text = amsSettings.MailTo;
            textBox6.Text = amsSettings.SmtpSenderName;
            textBox9.Text = amsSettings.SmtpSenderEmail;
            textBox10.Text = amsSettings.SmtpPassword;
            textBox7.Text = amsSettings.SmtpHost;
            textBox8.Text = amsSettings.SmtpPort.ToString();
            textBox12.Text = amsSettings.PhoneToSMS;
            textBox13.Text = amsSettings.EmailToSMS;
            tbAdb.Text = amsSettings.AdbFile;

            switch (amsSettings.WayToSendSms)
            {
                case "email":
                    radioButton4.Checked = true;
                    break;
                case "adb":
                    radioButton3.Checked = true;
                    break;
                default:
                    break;
            }

            checkBox1.Checked = amsSettings.Ssl;
            checkBox2.Checked = amsSettings.EmailNotification;
            checkBox3.Checked = amsSettings.SmsNotification;
            checkBox5.Checked = amsSettings.SmsNeedsTranslit;
            checkBox4.Checked = amsSettings.EmailNeedsTranslit;

            tbConfigFile.Text = Application.StartupPath + "\\config.xml";
            tbMapsFolder.Text = Application.StartupPath + "\\Maps";
            tbLogsFolder.Text = Application.StartupPath + "\\Logs";
        }

        
    }
}
