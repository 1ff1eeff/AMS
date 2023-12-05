using AMS.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using System.Net.Mail;

namespace AMS
{
    public partial class MainWindow : Form
    {
        List<AmsNode> selectedNodes = new List<AmsNode>();
        List<AmsStat> nodesStatus = new List<AmsStat>();
        AmsSettings amsSettings = new AmsSettings();
        CancellationTokenSource cts = new CancellationTokenSource();
        int pingTimeout = 1000;
        string defaulConfigFile = Application.StartupPath + "\\config.xml";
        const string quote = "\"";
        public MainWindow()
        {
            InitializeComponent();

            // Ожидается конфигурационный файл в корневой папке приложения

            if (File.Exists(defaulConfigFile))
            {

                // Открываем и настраиваем приложение согласно данным файла

                XmlSerializer formatter = new XmlSerializer(typeof(AmsSettings));

                try
                {
                    using (FileStream fs = new FileStream(defaulConfigFile, FileMode.Open))
                    {
                        amsSettings = formatter.Deserialize(fs) as AmsSettings;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            // При отсутствии конфигурационного файла

            else
            {

                // Создаём новый файл и заполняем значениями по умолчанию

                XmlSerializer formatter = new XmlSerializer(typeof(AmsSettings));

                using (FileStream fs = new FileStream(defaulConfigFile, FileMode.Create))
                {
                    formatter.Serialize(fs, amsSettings);
                }
            }

                        
        }

        /// <summary>
        /// Кнопка "Создать карту"
        /// </summary>
        private void CreateMap_Click(object sender, EventArgs e)
        {
            CreateMap createMap = new CreateMap()
            {
                tc = tabControl1
            };
            createMap.ShowDialog();
        }

        /// <summary>
        /// Кнопка "Добавить устройство"
        /// </summary>
        private void CreateNode_Click(object sender, EventArgs e)
        {
            CreateNode createNode = new CreateNode()
            {
                tc = tabControl1
            };
            createNode.ShowDialog();
        }

        /// <summary>
        /// Кнопка "Редактировать одно или несколько устройств"
        /// </summary>
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

        /// <summary>
        /// Кнопка "Модуль мониторинга"
        /// </summary>
        private void CreateTest_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 0)
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
        }

        /// <summary>
        /// Кнопка "Модуль анализа"
        /// </summary>
        private void Analysis_Click(object sender, EventArgs e)
        {
            Analysis analysis = new Analysis()
            {
                amsSettings = amsSettings
            };            
            analysis.ShowDialog();
        }

        /// <summary>
        /// Кнопка "Настройки"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.amsSettings = this.amsSettings;
            settings.ShowDialog();
        }

        /// <summary>
        /// Кнопка "Удалить карту"
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Сохранить карту перед закрытием?", "", MessageBoxButtons.YesNoCancel);

            switch (dialogResult)
            {
                case DialogResult.Cancel:
                    break;

                case DialogResult.Yes:
                    SaveMap();
                    if (tabControl1.TabPages.Count > 0)
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    break;

                case DialogResult.No:

                    if (tabControl1.TabPages.Count > 0)
                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Кнопка "Удалить наблюдаемый узел из списка"
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
                if (listView1.Items[i].Selected)
                    listView1.Items[i].Remove();
        }

        /// <summary>
        /// Кнопка "Начать/остановить мониторинг"
        /// </summary>
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
                    StartMonitoring(cts.Token);
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
        /// Кнопка "Сохранить карту"
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            SaveMap();
        }

        /// <summary>
        /// Кнопка "Открыть карту"
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            OpenMap();
        }

        /// <summary>
        /// Запускает мониторинг узлов, отмеченных для наблюдения.
        /// </summary>
        /// <param name="cancelToken">Токен для отмены операции.</param>
        void StartMonitoring(CancellationToken cancelToken)
        {
            var ping = new Ping();
            var failedPings = 0;
            var pingAmount = 4;
            var packetLoss = 0;

            listView1.Invoke(new Action(async () =>
            {
                var lvTesting = new List<ListViewItem>();

                // Получаем информацию о наблюдаемых узлах из ListView

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

                        var nodeStatus = new AmsStat();

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

                                if (item.SubItems[2].Text != " - ")
                                {
                                    // Если статус узла - Online

                                    if (resp.Status == IPStatus.Success)
                                    {
                                        
                                        NodeImg(tabControl1, nodeStatus.Id, Resources.PC_Online);

                                        // А до этого был Offline

                                        if (item.SubItems[2].Text == "Offline")
                                        {
                                            // Отправляем e-mail оповещение

                                            if (amsSettings.EmailNotification)
                                            {
                                                string subject = "Cоединение с узлом " + item.SubItems[1].Text + " (" + item.Text + ") восстановлено";
                                                string body = DateTime.Now.ToString() + ": Узел "
                                                    + item.SubItems[1].Text + " (" + item.Text + ") " + "появился в сети.";

                                                await SendEmailAsync(subject, body, amsSettings);
                                            }

                                            if (amsSettings.SmsNotification)
                                            {
                                                switch (amsSettings.WayToSendSms)
                                                {
                                                    case "email":

                                                        if (!String.IsNullOrWhiteSpace(amsSettings.AdbFile))
                                                        {
                                                            string smsText = "Восстановлено соединение с " + item.SubItems[1].Text + " " + item.Text;
                                                            if (amsSettings.EmailNeedsTranslit)
                                                            {
                                                                Translit translit = new Translit(false);
                                                                smsText = translit.TranslitString(smsText);
                                                            }
                                                            if (!String.IsNullOrWhiteSpace(amsSettings.EmailToSMS)) await SendEmailToSmsAsync(smsText, amsSettings);
                                                            else MessageBox.Show("Не указан e-mail шлюз в настройках приложения.", "Отправка СМС не удалась");
                                                        }

                                                        break;

                                                    case "adb":

                                                        if (!String.IsNullOrWhiteSpace(amsSettings.AdbFile))
                                                        {
                                                            Process process = new Process();
                                                            process.StartInfo.FileName = amsSettings.AdbFile;
                                                            FileInfo file = new FileInfo(amsSettings.AdbFile);
                                                            string workingDirectory = file.DirectoryName;
                                                            process.StartInfo.WorkingDirectory = workingDirectory;
                                                            string smsText = "Восстановлено\\ соединение\\ с\\ " + item.SubItems[1].Text + "\\ " + item.Text;
                                                            if (amsSettings.SmsNeedsTranslit)
                                                            {
                                                                Translit translit = new Translit(false);
                                                                smsText = translit.TranslitString(smsText);
                                                            }
                                                            process.StartInfo.Arguments = "shell service call isms 5 i32 1 s16 " + quote + "com.android.mms" + quote + " s16 " + quote + "null" + quote + " s16 " + quote + amsSettings.PhoneToSMS + quote + " s16 " + quote + "null" + quote + " s16 " + quote + smsText + quote + " s16 " + quote + "null" + quote + " s16 " + quote + "null" + quote + " i32 0 i64 0";
                                                            process.Start();
                                                        }

                                                        break;

                                                    default:
                                                        break;
                                                }
                                            }

                                            // Звуковое оповещение о восстановлении соединения

                                            new System.Media.SoundPlayer(@"C:\WINDOWS\Media\Windows Unlock.wav").Play();
                                        }

                                        item.SubItems[2].Text = "Online";

                                        // Узел доступен
                                        // Количество успешных ответов на запрос +1

                                        nodeStatus.Succed++;
                                        isOnline = true;

                                    }

                                    // Если статус узла - Offline

                                    else
                                    {
                                        // А до этого был Online

                                        if(item.SubItems[2].Text == "Online")
                                        {
                                            // Отправляем e-mail оповещение

                                            if(amsSettings.EmailNotification)
                                            {
                                                string subject = "Потеряно соединение с узлом " + item.SubItems[1].Text + " (" + item.Text + ")";
                                                string body = DateTime.Now.ToString() + ": Узел " 
                                                    + item.SubItems[1].Text + " (" + item.Text + ") " + "перестал отвечать на запросы.";

                                                await SendEmailAsync(subject, body, amsSettings);
                                            }

                                            // Отправляем SMS оповещение

                                            if (amsSettings.SmsNotification)
                                            {
                                                switch (amsSettings.WayToSendSms)
                                                {
                                                    case "email":
                                                        if (!String.IsNullOrWhiteSpace(amsSettings.AdbFile))
                                                        {
                                                            string smsText = "Потеряно соединение с " + item.SubItems[1].Text + " " + item.Text;
                                                            if (amsSettings.EmailNeedsTranslit)
                                                            {
                                                                Translit translit = new Translit(false);
                                                                smsText = translit.TranslitString(smsText);
                                                            }
                                                            if (!String.IsNullOrWhiteSpace(amsSettings.EmailToSMS)) await SendEmailToSmsAsync(smsText, amsSettings);
                                                            else MessageBox.Show("Не указан e-mail шлюз в настройках приложения.", "Отправка СМС не удалась");
                                                        }

                                                        break;

                                                    case "adb":

                                                        if (!String.IsNullOrWhiteSpace(amsSettings.AdbFile))
                                                        {
                                                            Process process = new Process();
                                                            process.StartInfo.FileName = amsSettings.AdbFile;
                                                            FileInfo file = new FileInfo(amsSettings.AdbFile);
                                                            string workingDirectory = file.DirectoryName;
                                                            process.StartInfo.WorkingDirectory = workingDirectory;
                                                            string smsText = "Потеряно\\ соединение\\ с\\ " + item.SubItems[1].Text + "\\ " + item.Text;
                                                            if (amsSettings.SmsNeedsTranslit)
                                                            {
                                                                Translit translit = new Translit(false);
                                                                smsText = translit.TranslitString(smsText);
                                                            }
                                                            process.StartInfo.Arguments = "shell service call isms 5 i32 1 s16 " + quote + "com.android.mms" + quote + " s16 " + quote + "null" + quote + " s16 " + quote + amsSettings.PhoneToSMS + quote + " s16 " + quote + "null" + quote + " s16 " + quote + smsText + quote + " s16 " + quote + "null" + quote + " s16 " + quote + "null" + quote + " i32 0 i64 0";
                                                            process.Start();
                                                        }

                                                        break;

                                                    default:
                                                        break;
                                                }
                                            }

                                            // Звуковое оповещение о потери соединения

                                            new System.Media.SoundPlayer(@"C:\WINDOWS\Media\Windows Notify System Generic.wav").Play();
                                        }

                                        // Меняем пиктограмму узла на "Offline"

                                        NodeImg(tabControl1, nodeStatus.Id, Resources.PC_Offline);

                                        // Меняем доступность узла

                                        item.SubItems[2].Text = "Offline";
                                                                                
                                        // Количество запросов без ответа +1

                                        nodeStatus.Failed++;

                                        // Узел недоступен

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
                                        if (item.SubItems[0].Text.Length > 0)
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

                                                    label5.Text = servicesOffline.Count.ToString();
                                                }
                                            }
                                            catch (Exception) { }
                                    }
                                    item.SubItems[5].Text = "";
                                    if (servicesOnline.Count > 0)  
                                        foreach (string service in servicesOnline)
                                            item.SubItems[5].Text += service + ";";
                                    
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

                                    nodesStatus[idx].OfflinePercent = Math.Round(nodesStatus[idx].OfflineTime / nodesStatus[idx].TotalTime * 100, 0);

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
                        XmlSerializer formatter = new XmlSerializer(typeof(List<AmsStat>));

                        // Сегодняшняя дата для формирования названия файла

                        string today =  DateTime.Now.Date.Day.ToString() 
                                + "-" + DateTime.Now.Date.Month.ToString() 
                                + "-" + DateTime.Now.Date.Year.ToString();
                        
                        Directory.CreateDirectory(amsSettings.LogsFolder);

                        using (FileStream fs = new FileStream(amsSettings.LogsFolder + "\\" + today + ".xml", FileMode.Create))
                        {
                            formatter.Serialize(fs, nodesStatus);
                        }
                    }

                    label3.Text = totalOnline.ToString();
                    label7.Text = totalOffline.ToString();
                }
            }));
        }

        /// <summary>
        /// Меняет пиктограмму узла с указанным ID.
        /// </summary>
        /// <param name="tc">TabControl содержащий вкладку с узлом.</param>
        /// <param name="id">ID узла.</param>
        /// <param name="res">Новое фоновое изображение узла.</param>
        private void NodeImg(TabControl tc, string id, Bitmap res)
        {
            List<DeviceNode> nodesOnMap = new List<DeviceNode>();
            foreach (DeviceNode node in tc.SelectedTab.Controls.OfType<DeviceNode>())
                nodesOnMap.Add(node);

            if (nodesOnMap.Count > 0 && nodesOnMap.Exists(x => x.DNode.Id == id))
            {
                int idx = nodesOnMap.FindIndex(x => x.DNode.Id == id);
                if (idx != -1)
                    nodesOnMap[idx].BackgroundImage = res;
            }
        }

        /// <summary>
        /// Открывает карту, в формате XML (List<AMSNode>).
        /// </summary>
        private void OpenMap()
        {

            Directory.CreateDirectory(amsSettings.MapsFolder);
            openMapDialog.InitialDirectory = amsSettings.MapsFolder;
            openMapDialog.Filter = "Карты АСМ (*.xml)|*.xml|Все файлы (*.*)|*.*";

            if (openMapDialog.ShowDialog() == DialogResult.Cancel)
                return;
                        
            TabPage tp = new TabPage(Path.GetFileNameWithoutExtension(openMapDialog.FileName));
            tabControl1.TabPages.Add(tp);
            tabControl1.SelectTab(tp);

            XmlSerializer formatter = new XmlSerializer(typeof(List<AmsNode>));

            using (FileStream fs = new FileStream(openMapDialog.FileName, FileMode.Open))
            {
                try
                {
                    List<AmsNode> nodes = formatter.Deserialize(fs) as List<AmsNode>;

                    if (nodes != null && nodes.Count > 0)
                    {
                        // Добавляем узлы на карту

                        foreach (AmsNode node in nodes)
                        {
                            // Подготавливаем элемент представляющий узел

                            DeviceNode dn = new DeviceNode
                            {
                                Location = node.Location, // Положение                                
                                DNode = node, // Узел                        
                            };

                            dn.DNode.IsSelected = false;

                            // Добавляем новый элемент на карту

                            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(dn);
                        }
                    }
                    fs.Close();
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Сохраняет карту в файл XML (List<AMSNode>).
        /// </summary>
        private void SaveMap()
        {
            Directory.CreateDirectory(amsSettings.MapsFolder);
            saveMapDialog.InitialDirectory = amsSettings.MapsFolder;
            saveMapDialog.Filter = "Карты АСМ (*.xml)|*.xml|Все файлы (*.*)|*.*";

            if (saveMapDialog.ShowDialog() == DialogResult.Cancel)
                return;

            List<AmsNode> nodes = new List<AmsNode>();

            foreach (DeviceNode dNode in tabControl1.SelectedTab.Controls.OfType<DeviceNode>())
            {
                dNode.DNode.Location = dNode.Location;
                nodes.Add(dNode.DNode);
            }

            XmlSerializer formatter = new XmlSerializer(typeof(List<AmsNode>));

            using (FileStream fs = new FileStream(saveMapDialog.FileName, FileMode.Create))
            {
                formatter.Serialize(fs, nodes);
                fs.Close();
            }
        }

        /// <summary>
        /// Отправляет e-mail.
        /// </summary>
        /// <param name="mailSubject">Тема письма.</param>
        /// <param name="mailBody">Сообщение.</param>
        /// <param name="settings">Настройки e-mail в приложении.</param>
        /// <returns></returns>
        private async Task SendEmailAsync(string mailSubject, string mailBody, AmsSettings settings)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Port = settings.SmtpPort;
            smtp.EnableSsl = settings.Ssl;
            smtp.DeliveryFormat = SmtpDeliveryFormat.International;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = settings.SmtpHost;
            smtp.Timeout = settings.SmtpTimeout;
            smtp.Credentials = new NetworkCredential(settings.SmtpSenderEmail, settings.SmtpPassword);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(settings.SmtpSenderEmail, settings.SmtpSenderName);
            mailMessage.To.Add(settings.MailTo);
            mailMessage.Subject = mailSubject;
            mailMessage.Body = mailBody;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            try
            {
                await smtp.SendMailAsync(mailMessage);                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            } 
            
            smtp.Dispose();
        }

        /// <summary>
        /// Отправляет SMS через e-mail шлюз.
        /// </summary>
        /// <param name="mailBody">Сообщение.</param>
        /// <param name="settings">Настройки e-mail в приложении.</param>
        /// <returns></returns>
        private async Task SendEmailToSmsAsync(string mailBody, AmsSettings settings)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Port = settings.SmtpPort;
            smtp.EnableSsl = settings.Ssl;
            smtp.DeliveryFormat = SmtpDeliveryFormat.International;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = settings.SmtpHost;
            smtp.Timeout = settings.SmtpTimeout;
            smtp.Credentials = new NetworkCredential(settings.SmtpSenderEmail, settings.SmtpPassword);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(settings.SmtpSenderEmail, settings.SmtpSenderName);
            mailMessage.To.Add(settings.EmailToSMS);
            mailMessage.Body = mailBody;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            try
            {
                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }

            smtp.Dispose();
        }

    }
}
