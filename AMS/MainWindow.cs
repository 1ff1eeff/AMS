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
using System.Diagnostics;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.IO;

namespace AMS
{
    public partial class MainWindow : Form
    {
        List<ASMNode> selectedNodes = new List<ASMNode>();
        List<ASMStat> nodesStatus = new List<ASMStat>();
        CancellationTokenSource cts = new CancellationTokenSource();
        int pingTimeout = 1000;

        public MainWindow()
        {
            InitializeComponent();

        }

        // Создать карту
        private void CreateMap_Click(object sender, EventArgs e)
        {
            CreateMap createMap = new CreateMap()
            {
                tc = tabControl1
            };
            createMap.ShowDialog();
        }

        // Добавить устройство
        private void CreateNode_Click(object sender, EventArgs e)
        {
            CreateNode createNode = new CreateNode()
            {
                tc = tabControl1
            };
            createNode.ShowDialog();
        }

        // Редактировать одно или несколько устройств
        private void EditNode_Click(object sender, EventArgs e)
        {
            selectedNodes.Clear();

            // Сохраняем все выделенные узлы на текущей карте

            foreach (DeviceNode node in tabControl1.SelectedTab.Controls.OfType<DeviceNode>())
            {
                if (node.DNode.IsSelected)
                    selectedNodes.Add(node.DNode);
            }

            foreach (var dNode in selectedNodes)
            {
                EditNodeM editNodeM = new EditNodeM()
                {
                    Node = dNode,
                    tc = tabControl1
                };
                editNodeM.Show();
            }
        }

        // Модуль мониторинга
        private void CreateTest_Click(object sender, EventArgs e)
        {
            selectedNodes.Clear();

            // Сохраняем все выделенные узлы на текущей карте

            foreach (DeviceNode node in tabControl1.SelectedTab.Controls.OfType<DeviceNode>())
            {
                if (node.DNode.IsSelected)
                    selectedNodes.Add(node.DNode);
            }

            CreateTest createTest = new CreateTest()
            {
                // Передаём данные о выделенных узлах в модуль мониторинга

                selectedNodes = selectedNodes,

                lvMonitoringNodes = listView1
            };
            createTest.ShowDialog();
        }

        // Модуль анализа
        private void GraphAndStat_Click(object sender, EventArgs e)
        {
            Analysis analysis = new Analysis();            
            analysis.ShowDialog();
        }

        // Индикаторы
        private void Indicators_Click(object sender, EventArgs e)
        {
            Indicators createIndicators = new Indicators();
            createIndicators.Show();
        }

        // Настройки
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

        // Начать или остановить мониторинг узлов
        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                if (button3.Text == "Начать мониторинг")
                {
                    button3.Text = "Прекратить  мониторинг";
                    button2.Enabled = false;

                    // Всего узлов

                    label2.Text = listView1.Items.Count.ToString();

                    if (cts != null)
                        cts.Dispose();                   

                    cts = new CancellationTokenSource();
                    PingLVIs(cts.Token);

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

        

        /// <summary>
        /// Отправляет ping-запрос на каждый элемент ListViewItem.
        /// </summary>
        /// <param name="cancelToken">Токен для отмены операции.</param>
        void PingLVIs(CancellationToken cancelToken)
        {
            var ping = new Ping();
            var failedPings = 0;
            var pingAmount = 4;
            var packetLoss = 0;

            listView1.Invoke(new Action(async () =>
            {
                var lvTesting = new List<ListViewItem>();

                // Получаем информацию об узлах из ListView главной формы

                foreach (ListViewItem item in listView1.Items)                
                    lvTesting.Add(item);           
                                
                // Пока не нажата кнопка "Прекратить мониторинг"
                // опрашиваем узлы в списке

                while (!cancelToken.IsCancellationRequested)
                {
                    uint totalOnline = 0;
                    uint totalOffline = 0;

                    //=====================================================
                    // Получаем информацию о статусе узлов
                    //-----------------------------------------------------
                    // Результат сохраняем в список nodesStatus
                    //=====================================================

                    foreach (ListViewItem item in lvTesting)
                    {
                        // Для хранения статуса текущего узла

                        var nodeStatus = new ASMStat();

                        // Задержка между ping-запросами

                        await Task.Delay(pingTimeout);  

                        
                        // Для осуществления ping-запроса
                        // IP-адрес должен быть указан
                        
                        if (item.SubItems[1].Text.Length > 0)
                        {
                            // Имя узла

                            if (item.SubItems[0].Text.Length > 0)
                                nodeStatus.Name = item.SubItems[0].Text;

                            // IP-адрес узла

                            IPAddress ip = IPAddress.Parse(item.SubItems[1].Text);
                            nodeStatus.Ip = ip.ToString();

                            // ID узла

                            nodeStatus.Id = item.SubItems[6].Text;

                            // Статус доступности узла 

                            bool isOnline = false;

                            //=================================================
                            // Ping-запрос
                            //=================================================

                            try
                            {
                                // Получаем ответ от узла

                                PingReply resp = await ping.SendPingAsync(ip);

                                // Если отслеживаем "Доступность"

                                if (item.SubItems[2].Text.ToString() != " - ")
                                {
                                    // Если статус узла - Online

                                    if (resp.Status == IPStatus.Success)
                                    {
                                        item.SubItems[2].Text = "Online";

                                        // Узел доступен
                                        // Количество успешных ответов на запрос +1

                                        nodeStatus.Succed++;
                                        isOnline = true;

                                    }

                                    // Если статус узла - Offline

                                    else
                                    {
                                        item.SubItems[2].Text = "Offline";

                                        // Узел недоступен
                                        // Количество запросов без ответа +1
                                        nodeStatus.Failed++;
                                        isOnline = false;
                                    } 
                                }

                                // Если отслеживаем "Время отклика"

                                if (item.SubItems[3].Text.ToString() != " - " && resp.Status == IPStatus.Success)
                                    item.SubItems[3].Text = resp.RoundtripTime.ToString() + " мс";

                                // Если отслеживаем "Потери пакетов"

                                if (item.SubItems[4].Text.ToString() != " - ")
                                {
                                    PingReply respPL = await ping.SendPingAsync(ip);
                                    for (int i = 0; i < pingAmount; i++)
                                        if (respPL.Status != IPStatus.Success)
                                            failedPings += 1;
                                    packetLoss = Convert.ToInt32((Convert.ToDouble(failedPings) / Convert.ToDouble(pingAmount)) * 100);
                                    item.SubItems[4].Text = packetLoss.ToString() + "%";
                                    failedPings = 0;
                                    packetLoss = 0;
                                }

                                // Если отслеживаем "Работоспособность сервисов"

                                if (item.SubItems[5].Text.ToString() != " - "
                                    && item.SubItems[5].Text.Length > 0)
                                {
                                    // Службы указанные для мониторинга

                                    string[] mServices = item.SubItems[5].Text.Split(';');

                                    List<string> servicesOnline = new List<string>();
                                    List<string> servicesOffline = new List<string>();

                                    foreach (string mService in mServices)
                                    {
                                        try
                                        {
                                            // Узнаём статус службы на удалённом ПК

                                            Process[] runningProcesses = Process.GetProcessesByName(mService, item.SubItems[0].Text);

                                            if (runningProcesses.Length > 0)
                                                servicesOnline.Add(mService);
                                            else if (mService.Length > 0)
                                            {
                                                servicesOffline.Add(mService);

                                                // Уведомить пользователя о проблеме - "служба не выполняется"

                                                //MessageBox.Show("Служба " + mService + " не найдена на устройстве " + item.SubItems[0].Text);
                                                label5.Text = servicesOffline.Count.ToString();
                                            }
                                        }
                                        catch (Exception) { }
                                    }
                                    item.SubItems[5].Text = "";

                                    foreach (string service in servicesOnline)
                                    {
                                        item.SubItems[5].Text += service + ";";
                                    }
                                }

                            } //try ping.SendPingAsync

                            catch (Exception) { }
                                                                                   
                            // Если в списке статусов уже есть узел с таким же ID

                            if (nodesStatus.Exists(x => x.Id == nodeStatus.Id))
                            {
                                // Находим положение такой записи

                                int idx = nodesStatus.FindIndex(x => x.Id == nodeStatus.Id);
                                
                                if (idx > -1)
                                {
                                    // Обновляем имя и IP-адрес узла в списке

                                    nodesStatus[idx].Name = nodeStatus.Name;
                                    nodesStatus[idx].Ip = nodeStatus.Ip;

                                    // Вычисляем время работы узла в текущей итерации опроса в секундах

                                    uint workTime = (uint)(DateTime.Now - nodeStatus.Time).TotalSeconds;

                                    // Если узел доступен
                                    // Обновляем количество успешных ответов на запрос и общее время в сети

                                    if (isOnline)
                                    {
                                        nodesStatus[idx].Succed += nodeStatus.Succed;

                                        nodesStatus[idx].OnlineTime += workTime;

                                        totalOnline++;
                                    }

                                    // Если узел недоступен
                                    // Обновляем количество запросов без ответа и общее время простоя

                                    else
                                    {
                                        nodesStatus[idx].Failed += nodeStatus.Failed;

                                        nodesStatus[idx].OfflineTime += workTime;

                                        totalOffline++;
                                    }

                                    // Обновляем общее время работы
                                    
                                    nodesStatus[idx].TotalTime += (uint)(DateTime.Now - nodeStatus.Time).TotalSeconds;

                                    // Обновляем процент простоя

                                    nodesStatus[idx].OfflinePercent = nodesStatus[idx].OfflineTime / nodesStatus[idx].TotalTime * 100;

                                    // Обновляем время финального опроса

                                    nodesStatus[idx].FinishTime = DateTime.Now;

                                }

                            }

                            // Если в списке статусов нет узла с таким же ID

                            else
                            {
                                // Добавляем статус текущего узла в список
                                // Имя, IP-адрес, время создания и время финального опроса

                                nodesStatus.Add(nodeStatus);
                            }

                        }

                    }


                    // Сохраняем список в файл

                    if (nodesStatus.Count > 0)
                    {
                        XmlSerializer formatter = new XmlSerializer(typeof(List<ASMStat>));

                        // Сегодняшняя дата для формирования названия файла

                        string today =  DateTime.Now.Date.Day.ToString() 
                                + "-" + DateTime.Now.Date.Month.ToString() 
                                + "-" + DateTime.Now.Date.Year.ToString();

                        using (FileStream fs = new FileStream("Logs\\"+ today + ".xml", FileMode.Create))
                        {
                            formatter.Serialize(fs, nodesStatus);
                        }
                    }

                    label3.Text = totalOnline.ToString();
                    label7.Text = totalOffline.ToString();

                }
            }));
        }
    }
}
