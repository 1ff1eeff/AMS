using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace AMS
{
    

    public partial class Analysis : Form
    {
        public List<AmsStat> nodesStatus = new List<AmsStat>();

        public AmsSettings amsSettings = new AmsSettings();

        public Analysis()
        {
            InitializeComponent();            
        }

        // Кнопка "Закрыть"
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Analysis_Load(object sender, EventArgs e)
        {           

            // Сегодняшняя дата для формирования названия файла

            string today =  DateTime.Now.Date.Day.ToString()
                    + "-" + DateTime.Now.Date.Month.ToString()
                    + "-" + DateTime.Now.Date.Year.ToString();

            string logsDir = Directory.CreateDirectory(amsSettings.LogsFolder + "\\").FullName;

            string todayLogFile = logsDir + today + ".xml";

            // Открываем файл и заполняем список

            if (File.Exists(todayLogFile))
            {
                UpdateNodesStatistics(todayLogFile);
            }

            // Ищем архивные log-файлы

            string[] files = Directory.GetFiles(logsDir);

            foreach (string file in files)
            {
                comboBox1.Items.Add(file);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filename = comboBox1.Text;
            lvStatistics.Items.Clear();
            UpdateNodesStatistics(filename);
        }

        /// <summary>
        /// Обновить список статистики узлов.
        /// </summary>
        /// <param name="filename">Расположение XML-файла.</param>

        private void UpdateNodesStatistics(string filename)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<AmsStat>));

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {

                try
                {
                    List<AmsStat> stats = formatter.Deserialize(fs) as List<AmsStat>;
                    
                    comboBox1.Text = filename;

                    if (stats != null && stats.Count > 0)
                    {
                        foreach (AmsStat stat in stats)
                        {
                            ListViewItem item = new ListViewItem();

                            // Имя узла

                            item.Text = stat.Name;

                            // Адрес узла

                            item.SubItems.Add(stat.Ip);

                            // Успешно

                            item.SubItems.Add(stat.Succed.ToString());

                            // Отказов

                            item.SubItems.Add(stat.Failed.ToString());

                            // Время простоя

                            item.SubItems.Add(GenTimeFromSeconds(stat.OfflineTime));

                            // Время работы

                            item.SubItems.Add(GenTimeFromSeconds(stat.OnlineTime));

                            // Процент простоя

                            item.SubItems.Add(stat.OfflinePercent.ToString() + "%");

                            // Начало опроса

                            item.SubItems.Add(stat.Time.ToString());

                            // Финал опроса

                            item.SubItems.Add(stat.FinishTime.ToString());
                            
                            lvStatistics.Items.Add(item);
                        }
                    }
                }
                catch (Exception e) 
                {
                    comboBox1.Text = e.Message;
                }
            }
        }


        /// <summary>
        /// Преобразовать секунды в форматированную строку.
        /// </summary>
        /// <param name="seconds">Время в секундах.</param>
        /// <returns></returns>
        static string GenTimeFromSeconds(double seconds)
        {
            string timeInterval = TimeSpan.FromSeconds(seconds).ToString();

            int pIndex = timeInterval.IndexOf(':');
            pIndex = timeInterval.IndexOf('.', pIndex);
            if (pIndex < 0) timeInterval += "        ";

            return timeInterval;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(amsSettings.LogsFolder);
            openFileDialog1.InitialDirectory = amsSettings.LogsFolder;
            openFileDialog1.Filter = "Файлы логирования (*.xml)|*.xml|Все файлы (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            if (!comboBox1.Items.Contains(openFileDialog1.FileName))
            {
                comboBox1.Items.Add(openFileDialog1.FileName);
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
            else
            {
                comboBox1.Text = openFileDialog1.FileName;
            }

        }
    }
}
