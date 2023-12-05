using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace AMS
{
    public partial class CreateTest : Form
    {
        // Информация о выбранных узлах
        public List<AmsNode> selectedNodes = new List<AmsNode>();
        public System.Windows.Forms.ListView lvMonitoringNodes = new System.Windows.Forms.ListView();

        public CreateTest()
        {
            InitializeComponent();
        }

        // ОК
        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedNodes.Count > 0)
                foreach (AmsNode selectedNode in selectedNodes)
                {
                    string[] nodeStatus = new string[] { "", "", "", "", "", "", "" };

                    nodeStatus[0] = selectedNode.Name;

                    nodeStatus[1] = selectedNode.Ip;

                    if (selectedNode.Services != null)
                        foreach (string service in selectedNode.Services)
                        {
                            if (service.Length > 0)
                                nodeStatus[5] += service + ";";
                        }

                    if (selectedNode.Id.Length > 0)
                        nodeStatus[6] = selectedNode.Id;

                    if (!checkBox1.Checked) { nodeStatus[2] = " - "; }
                    if (!checkBox2.Checked) { nodeStatus[3] = " - "; }
                    if (!checkBox3.Checked) { nodeStatus[4] = " - "; }
                    if (!checkBox4.Checked) { nodeStatus[5] = " - "; }
                    ListViewItem item = new ListViewItem(nodeStatus);

                    // Если устройства с текущим ID 
                    // ещё нет в списке, то добавляем

                    bool idInList = false;

                    foreach (ListViewItem lvi in lvMonitoringNodes.Items)
                    {
                        if (lvi.SubItems[6].Text.Length > 0 && lvi.SubItems[6].Text == selectedNode.Id)
                        {
                            idInList = true;
                            break;
                        }
                    }
                    if (!idInList)                    
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
                foreach (AmsNode selectedNode in selectedNodes)                
                    listBox1.Items.Add(selectedNode.Name + " " + selectedNode.Ip);                
        }        
    }
}
