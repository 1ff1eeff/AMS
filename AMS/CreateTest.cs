using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMS
{
    public partial class CreateTest : Form
    {
        // Информация о выбранных узлах
        public List<DNodeInfo> selectedNodes = new List<DNodeInfo>();
        public ListView lvMonitoringNodes = new ListView();

        public CreateTest()
        {
            InitializeComponent();
        }

        // ОК
        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedNodes.Count != 0)
                foreach (DNodeInfo selectedNode in selectedNodes)
                {
                    string[] nodeStatus = new string[] { "", "", "", "", "", "" };

                    nodeStatus[0] = selectedNode.Name;

                    nodeStatus[1] = selectedNode.Ip;

                    if (selectedNode.Services != null)
                        foreach (string service in selectedNode.Services)
                        {
                            if (service != "")
                                nodeStatus[5] += service + ";";
                        }

                    if (!checkBox1.Checked) { nodeStatus[2] = " - "; }
                    if (!checkBox2.Checked) { nodeStatus[3] = " - "; }
                    if (!checkBox3.Checked) { nodeStatus[4] = " - "; }
                    if (!checkBox4.Checked) { nodeStatus[5] = " - "; }
                    ListViewItem item = new ListViewItem(nodeStatus);
                    lvMonitoringNodes.Items.Add(item);
                }

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateTest_Load(object sender, EventArgs e)
        {
            if (selectedNodes.Count != 0)
                foreach (DNodeInfo selectedNode in selectedNodes)                
                    listBox1.Items.Add(selectedNode.Name + " " + selectedNode.Ip);                
        }        
    }
}
