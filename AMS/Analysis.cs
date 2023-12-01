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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace AMS
{
    public partial class Analysis : Form
    {
        public List<ASMStat> nodesStatus = new List<ASMStat>();

        public Analysis()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Analysis_Load(object sender, EventArgs e)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<ASMStat>));

            // Сегодняшняя дата для формирования названия файла

            string today = DateTime.Now.Date.Day.ToString()
                                + "-" + DateTime.Now.Date.Month.ToString()
                                + "-" + DateTime.Now.Date.Year.ToString();

            // Открываем файл и заполняем список

            using (FileStream fs = new FileStream("Logs\\" + today + ".xml", FileMode.Open)) 
            { 
                List<ASMStat> stats = formatter.Deserialize(fs) as List<ASMStat>;

                if (stats != null && stats.Count > 0)
                {
                    foreach (ASMStat stat in stats)
                    {
                        ListViewItem item = new ListViewItem();

                        item.Text = stat.Name;
                        item.SubItems.Add(stat.Ip);
                        item.SubItems.Add(stat.Succed.ToString());
                        item.SubItems.Add(stat.Failed.ToString());
                        item.SubItems.Add(stat.OfflineTime.ToString());
                        item.SubItems.Add(stat.OnlineTime.ToString());
                        item.SubItems.Add(stat.OfflinePercent.ToString());
                        item.SubItems.Add(stat.Time.ToString());
                        item.SubItems.Add(stat.FinishTime.ToString());

                        lvStatistics.Items.Add(item);
                    }
                }
            }
        }
    }
}
