using AMS.Properties;
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
    public partial class Form1 : Form
    {
        // Выбранные узлы
        List<DNodeInfo> selectedNodes = new List<DNodeInfo>();
        int pingTimeout = 1000;

        public Form1()
        {
            InitializeComponent();
        }

        private void CreateMap_Click(object sender, EventArgs e)
        {
            CreateMap createMap = new CreateMap()
            {
                tc = tabControl1
            };
            createMap.ShowDialog();
        }

        private void CreateNode_Click(object sender, EventArgs e)
        {
            CreateNode createNode = new CreateNode()
            {
                tc = tabControl1
            };
            createNode.ShowDialog();
        }

        private void CreateTest_Click(object sender, EventArgs e)
        {
            selectedNodes.Clear();

            // Сохраняем все узлы выделенные на текущей карте
            foreach (var node in tabControl1.SelectedTab.Controls.OfType<DeviceNode>())
            {
                if (node.dNodeInfo.IsSelected)
                    selectedNodes.Add(node.dNodeInfo);
            }

            CreateTest createTest = new CreateTest()
            {
                // Передаём данные о выделенных узлах в модуль мониторинга
                selectedNodes = selectedNodes,
                lvMonitoringNodes = listView1
            };
            createTest.ShowDialog();
        }

        private void GraphAndStat_Click(object sender, EventArgs e)
        {
            GraphAndStat createGraph = new GraphAndStat();
            createGraph.Show();
        }

        private void Indicators_Click(object sender, EventArgs e)
        {
            Indicators createIndicators = new Indicators();
            createIndicators.Show();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        // Удалить карту
        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 0)
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        // Удалить наблюдаемый узел из списка
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
                if (listView1.Items[i].Selected)
                    listView1.Items[i].Remove();
        }

        CancellationTokenSource cts = new CancellationTokenSource();

        // Начать/остановить мониторинг узлов
        private async void button3_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                if (button3.Text == "Начать мониторинг")
                {
                    button3.Text = "Прекратить  мониторинг";
                    button2.Enabled = false;
                    if (cts != null)
                        cts.Dispose();
                    cts = new CancellationTokenSource();
                    await PingItemList(cts.Token);
                }
                else
                {
                    button3.Text = "Начать мониторинг";
                    button2.Enabled = true;
                    if (cts != null)
                        cts.Cancel();
                }
            }
        }

        async Task PingItemList(CancellationToken cancelToken)
        {
            var ping = new Ping();
            var failedPings = 0;
            var pingAmount = 4;
            var packetLoss = 0;

            listView1.Invoke(new Action(async () =>
            {
                var lvTesting = new List<ListViewItem>();

                foreach (ListViewItem item in listView1.Items)
                    lvTesting.Add(item);

                while (!cancelToken.IsCancellationRequested)
                    foreach (ListViewItem item in lvTesting)
                    {
                        await Task.Delay(pingTimeout);  // Тут задержка между пинг-запросами из настроек
                        var ip = IPAddress.Parse(item.SubItems[1].Text.ToString());
                        PingReply resp = await ping.SendPingAsync(ip);
                        if (resp != null)
                        {
                            if (item.SubItems[2].Text.ToString() != " - ")
                                item.SubItems[2].Text = resp.Status == IPStatus.Success ? "Online" : "Offline";

                            if (item.SubItems[3].Text.ToString() != " - " && resp.Status == IPStatus.Success)
                                item.SubItems[3].Text = resp.RoundtripTime.ToString();

                            if (item.SubItems[4].Text.ToString() != " - ")
                            {
                                PingReply respPL = await ping.SendPingAsync(ip);
                                for (int i = 0; i < pingAmount; i++)
                                    if (respPL.Status != IPStatus.Success)
                                        failedPings += 1;
                                packetLoss = Convert.ToInt32((Convert.ToDouble(failedPings) / Convert.ToDouble(pingAmount)) * 100);
                                item.SubItems[4].Text = packetLoss.ToString();
                                failedPings = 0;
                                packetLoss = 0;
                            }

                            //if (item.SubItems[5].Text.ToString() != " - "
                            //    && item.SubItems[5].Text.ToString() != "")
                            //{
                            //    // Службы указанные для мониторинга
                            //    string[] mServices = item.SubItems[5].Text.Split("; ");

                            //    foreach (string mS in mServices)
                            //    {
                            //        try
                            //        {
                            //            // Узнаём статус службы на удалённом ПК
                            //            Process[] runningProcesses = Process.GetProcessesByName(mS, item.SubItems[0].Text);

                            //            if (runningProcesses.Length > 0)
                            //            {
                            //                foreach (Process rP in runningProcesses)
                            //                {
                            //                    // Если служба найдена
                            //                    if (rP.ProcessName != "")
                            //                    {
                            //                        // Уведомляем
                            //                        // Выделяем зелёным цветом 
                            //                        //item.UseItemStyleForSubItems = false;
                            //                        //item.SubItems[5].ForeColor = Color.Green;
                            //                    }
                            //                }
                            //            }
                            //            else
                            //            {
                            //                //item.UseItemStyleForSubItems = false;
                            //                //item.SubItems[5].ForeColor = Color.Red;                                            
                            //            }
                            //        }
                            //        catch (Exception)
                            //        {
                            //            // Уведомляем
                            //            // Выделяем красным цветом


                            //            throw;
                            //        }
                            //    }
                            //}
                        }
                    }
            }));

        }
    }
}
