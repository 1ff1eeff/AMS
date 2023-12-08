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
        // Настройки приложения.

        internal AmsSettings amsSettings = new AmsSettings();

        // Узлы, выбранные на карте.

        internal List<AmsNode> selectedNodes = new List<AmsNode>();

        // Состояния узлов.

        internal List<AmsNodeStat> nodesStatus = new List<AmsNodeStat>();

        // Объект управления токеном для отмены операции.

        private CancellationTokenSource _cts = new CancellationTokenSource();

        // Задержка между ping-запросами.

        private readonly int _pingTimeout = 1000;

        // Расположение файла конфигурации, загружаемый при запуске приложения.

        private readonly string _defaulConfigFile = Application.StartupPath + "\\config.xml";

        // Символ "кавычки" (для формирования строки аргументов запуска внешнего приложения).

        public const string quote = "\"";

        /// <summary>
        /// Конструктор главного окна приложения. 
        /// Инициализация компонентов.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Ожидается конфигурационный файл в корневой папке приложения.

            if (File.Exists(_defaulConfigFile))
            {

                // Открываем файл и настраиваем приложение.
                // Тип данных для десериализации – <AmsSettings>.

                XmlSerializer formatter = new XmlSerializer(typeof(AmsSettings));

                try
                {
                    // Открываем конфигурационный файл настроек приложения, указанный по умолчанию.

                    using (FileStream fs = new FileStream(_defaulConfigFile, FileMode.Open))
                    {
                        // Если файл был успешно десериализован.

                        if (formatter.Deserialize(fs) is AmsSettings settings)
                        {
                            // Переносим настройки из файла конфигурации в приложение.

                            amsSettings = settings;
                        }
                    }
                }
                catch (Exception e)
                {
                    // Если при открытии файла возникли проблемы –
                    // выводим текст полученного исключения в диалоговом окне.

                    MessageBox.Show(e.Message);
                }
            }

            // При отсутствии конфигурационного файла.

            else
            {
                // Создаём новый файл и заполняем значениями по умолчанию.
                // Тип данных для сериализации – <AmsSettings>.

                XmlSerializer formatter = new XmlSerializer(typeof(AmsSettings));

                // Сохраняем данные настроек приложения в новый файл конфигурации.

                using (FileStream fs = new FileStream(_defaulConfigFile, FileMode.Create))
                {
                    // Переносим настройки приложения в файл конфигурации, в XML формате.

                    formatter.Serialize(fs, amsSettings);
                }
            }                        
        }

        /// <summary>
        /// Кнопка "Создать карту".
        /// </summary>
        private void CreateMap_Click(object sender, EventArgs e)
        {
            // Создаём экземпляр формы "Создать карту".

            CreateMap createMap = new CreateMap()
            {
                // Передаём управление элементом TabControl главной формы, представляющим карту сети.

                tc = tabControl1
            };

            // Открываем форму в формате диалогового окна.

            createMap.ShowDialog();
        }

        /// <summary>
        /// Кнопка "Добавить устройство".
        /// </summary>
        private void CreateNode_Click(object sender, EventArgs e)
        {
            // Создаём экземпляр формы "Добавить устройство".

            CreateNode createNode = new CreateNode()
            {
                // Передаём управление элементом TabControl главной формы, представляющим карту сети.

                tc = tabControl1
            };

            // Открываем форму в формате диалогового окна.

            createNode.ShowDialog();
        }

        /// <summary>
        /// Кнопка "Редактировать одно или несколько устройств".
        /// </summary>
        private void EditNode_Click(object sender, EventArgs e)
        {
            // Подготавливаем список для хранения выделенных узлов.

            selectedNodes.Clear();

            // Помещаем все выделенные на текущей карте узлы в список.

            foreach (DeviceNode node in tabControl1.SelectedTab.Controls.OfType<DeviceNode>())
            {
                // Если узел выделен.

                if (node.DNode.IsSelected)

                    // Добавляем его в список.

                    selectedNodes.Add(node.DNode);
            }

            // Открываем окно редактирования для кажого выделенного узла.

            foreach (var dNode in selectedNodes)
            {
                // Создаём экземпляр формы "Редактировать устройство".

                EditNodeM editNodeM = new EditNodeM()
                {
                    // Передаём управление элементом TabControl главной формы, представляющим карту сети
                    // и выделенным экземпляром пользовательского элемента, представляющего узел на карте.

                    tc = tabControl1,
                    Node = dNode
                };

                // Открываем окно редактирования узла.

                editNodeM.Show();
            }
        }

        /// <summary>
        /// Кнопка "Модуль мониторинга".
        /// </summary>
        private void CreateTest_Click(object sender, EventArgs e)
        {
            // Если на форме расположена активная карта

            if (tabControl1.TabPages.Count > 0)
            {
                // Подготавливаем список для хранения выделенных узлов.

                selectedNodes.Clear();

                // Сохраняем все выделенные на текущей карте узлы.

                foreach (DeviceNode node in tabControl1.SelectedTab.Controls.OfType<DeviceNode>())
                {
                    // Если узел выделен.

                    if (node.DNode.IsSelected)

                        // Добавляем в список.

                        selectedNodes.Add(node.DNode);
                }

                // Создаём экземпляр формы "Модуль мониторинга".

                CreateTest createTest = new CreateTest()
                {
                    // Передаём управление выделенными узлами и элементом ListView главной формы

                    selectedNodes = selectedNodes,

                    lvMonitoringNodes = listView1
                };

                // Открываем окно модуля мониторинга.

                createTest.ShowDialog();
            }
        }

        /// <summary>
        /// Кнопка "Модуль анализа".
        /// </summary>
        private void Analysis_Click(object sender, EventArgs e)
        {
            // Создаём экземпляр формы "Модуль анализа".

            Analysis analysis = new Analysis()
            {
                // Передаём управление настройками приложения.

                amsSettings = amsSettings
            };

            // Открываем окно модуля анализа.

            analysis.ShowDialog();
        }

        /// <summary>
        /// Кнопка "Настройки".
        /// </summary>
        private void Settings_Click(object sender, EventArgs e)
        {
            // Создаём экземпляр формы "Настройки".

            Settings settings = new Settings();

            // Передаём управление настройками приложения.

            settings.amsSettings = amsSettings;

            // Открываем окно модуля анализа.

            settings.ShowDialog();
        }

        /// <summary>
        /// Кнопка "Закрыть карту".
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            // Перед закрытием карты выводим диалоговое окно предупреждения с тремя кнопками: "Да", "Нет", "Отмена".

            DialogResult dialogResult = MessageBox.Show("Сохранить карту перед закрытием?", "Закрываем карту.", MessageBoxButtons.YesNoCancel);

            // Проверяем ответ на диалог.

            switch (dialogResult)
            {
                // Если ответ: "Отмена".

                case DialogResult.Cancel:
                    break;

                // Если ответ: "Да".

                case DialogResult.Yes:

                    // Сохраняем карту в XML файл.

                    SaveMap();

                    // Если элемент TabControl содержит вкладки.

                    if (tabControl1.TabPages.Count > 0)

                        // Удаляем выбранную вкладку.

                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    break;

                // Если ответ: "Нет".

                case DialogResult.No:

                    // Если элемент TabControl содержит вкладки.

                    if (tabControl1.TabPages.Count > 0)

                        // Удаляем выбранную вкладку.

                        tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Открывает карту из XML-файла.
        /// </summary>
        private void OpenMap()
        {
            // Проверяем наличие директории для хранения карт.
            // При её отсутствии – создаём новую папку.

            Directory.CreateDirectory(amsSettings.MapsFolder);

            // Открывать по умолчанию папку карт.

            openMapDialog.InitialDirectory = amsSettings.MapsFolder;

            // Отображаем диалог выбора файла карты.
            // Если пользователь отменил открытие карты.

            if (openMapDialog.ShowDialog() == DialogResult.Cancel)
            {
                // Покидаем функцию.

                return;
            }

            // Создаём новую вкладку карты. Имя – название файла.

            TabPage tp = new TabPage(Path.GetFileNameWithoutExtension(openMapDialog.FileName));

            // Добавляем вкладку на элемент TabPages

            tabControl1.TabPages.Add(tp);

            // Переходим на вновь созданную вкладку.

            tabControl1.SelectTab(tp);

            // Получаем информацию об узлах из XML файла.
            // Тип данных для сериализации – List<AmsNode>.

            XmlSerializer formatter = new XmlSerializer(typeof(List<AmsNode>));

            // Открываем сохранённую ранее карту узлов по имени, полученному из окна OpenFileDialog.

            using (FileStream fs = new FileStream(openMapDialog.FileName, FileMode.Open))
            {
                try
                {
                    // Заполняем список узлов данными из файла.
                    // Если файл был успешно десериализован содержит информацию об узлах.

                    if (formatter.Deserialize(fs) is List<AmsNode> nodes && nodes.Count > 0)
                    {
                        // Добавляем узлы на карту.

                        foreach (AmsNode node in nodes)
                        {
                            // Подготавливаем элемент представляющий узел.

                            DeviceNode dn = new DeviceNode
                            {
                                // Координаты элемента, представляющего узел на карте.

                                Location = node.Location,

                                // Экземпляр пользовательского элемента, представляющего узел на карте.

                                DNode = node
                            };

                            // Снимаем выделение с узла.

                            dn.DNode.IsSelected = false;

                            // Добавляем новый элемент на карту.

                            tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(dn);
                        }
                    }

                    // Закрываем файл.

                    fs.Close();
                }



                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Сохраняет карту в XML файл.
        /// </summary>
        private void SaveMap()
        {
            // Проверяем наличие директории для хранения карт.
            // При её отсутствии – создаём новую папку.

            Directory.CreateDirectory(amsSettings.MapsFolder);

            // Открывать по умолчанию папку карт.

            saveMapDialog.InitialDirectory = amsSettings.MapsFolder;

            // Отображаем диалог выбора файла.
            // Если пользователь отменил открытие карты.

            if (saveMapDialog.ShowDialog() == DialogResult.Cancel)
            {
                // Покидаем функцию.

                return;
            }

            // Создаём новый список узлов.

            List<AmsNode> nodes = new List<AmsNode>();

            // Добавляем в список все узлы расположенные на текущей вкладке карты.

            foreach (DeviceNode dNode in tabControl1.SelectedTab.Controls.OfType<DeviceNode>())
            {

                // Сохраняем текущую позицию узла.

                dNode.DNode.Location = dNode.Location;

                // Добавляем узел в список.

                nodes.Add(dNode.DNode);
            }
                        
            // Тип данных для сериализации – List<AmsNode>.

            XmlSerializer formatter = new XmlSerializer(typeof(List<AmsNode>));

            // Сохраняем список узлов в файл.

            using (FileStream fs = new FileStream(saveMapDialog.FileName, FileMode.Create))
            {
                // Информация об узлах сохраняется в XML формате.
                
                formatter.Serialize(fs, nodes);

                // Закрываем файл.

                fs.Close();
            }
        }

        /// <summary>
        /// Кнопка "Удалить наблюдаемый узел из списка".
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            // Удаляем все выбранные элементы из ListView.

            foreach (ListViewItem item in listView1.SelectedItems)
            {
                // Удаляем элемент.

                listView1.Items.Remove(item);
            }
        }

        /// <summary>
        /// Кнопка "Начать/остановить мониторинг".
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            // Если в ListView добавлен хотя бы один элемент.

            if (listView1.Items.Count > 0)
            {
                // Если текст кнопки в текущий момент "Начать мониторинг".

                if (button3.Text == "Начать мониторинг")
                {
                    // Устанавливаем текст кнопки в значение "Прекратить  мониторинг".

                    button3.Text = "Прекратить  мониторинг";

                    // На время работы отключаем кнопку "Удалить из списка".

                    button2.Enabled = false;

                    // Задаем значение индикатору "Всего узлов"

                    label2.Text = listView1.Items.Count.ToString();

                    // Если объект управления токеном существует в данном контексте. 

                    if (_cts != null)

                        // Удаляем текущий объект управления токеном.

                        _cts.Dispose();                   

                    // Инициализируем новый объект управления токеном.

                    _cts = new CancellationTokenSource();

                    // Запускаем мониторинг узлов на выполнение.
                    // Токен для прерывания операции получаем из созданного объекта управления токеном.

                    StartMonitoring(_cts.Token);
                }

                // Если текст кнопки в текущий момент не "Начать мониторинг".

                else
                {

                    // Устанавливаем текст кнопки в значение "Начать мониторинг".

                    button3.Text = "Начать мониторинг";

                    // Делаем кнопку "Удалить из списка" доступной.

                    button2.Enabled = true;

                    // Если в данный момент задан объект управления токеном.

                    if (_cts != null)
                    {
                        // Передаём запрос на отмену операции.

                        _cts.Cancel();
                    }
                }
            }
        }

        /// <summary>
        /// Кнопка "Сохранить карту".
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            // Сохраняем карту в формат XML.

            SaveMap();
        }

        /// <summary>
        /// Кнопка "Открыть карту".
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            // Открываем карту из XML-файла.

            OpenMap();
        }

        /// <summary>
        /// Меняет пиктограмму узла с указанным ID.
        /// </summary>
        /// <param name="tc">TabControl содержащий вкладку с узлом.</param>
        /// <param name="id">ID узла.</param>
        /// <param name="res">Новое фоновое изображение узла.</param>
        private void SelectNodeImg(TabControl tc, string id, Bitmap res)
        {
            // Создаем список для хранения узлов расположенных на карте.

            List<DeviceNode> nodesOnMap = new List<DeviceNode>();

            // Добавляем узлы с выбранной вкладки карты в список. 

            foreach (DeviceNode node in tc.SelectedTab.Controls.OfType<DeviceNode>())

                // Добавляем узел в список.

                nodesOnMap.Add(node);

            // Если на карте есть узлы и ID узла на карте совпадает с заданным ID 

            if (nodesOnMap.Count > 0 && nodesOnMap.Exists(x => x.DNode.Id == id))
            {
                // Определяем индекс узла в списке, сравнивая по ID.

                int idx = nodesOnMap.FindIndex(x => x.DNode.Id == id);

                // Если индекс найден

                if (idx != -1)
                { 
                    // Заменяем фоновое изображение элемента, представляющего узел на карте
                    // на изображение, указанное во входных параметрах функции.
                    
                    nodesOnMap[idx].BackgroundImage = res;
                }
            }
        }

        /// <summary>
        /// Отправляет e-mail.
        /// </summary>
        /// <param name="mailSubject">Тема письма.</param>
        /// <param name="mailBody">Сообщение.</param>
        /// <param name="settings">Настройки e-mail в приложении.</param>
        private async Task SendEmailAsync(string mailSubject, string mailBody, AmsSettings settings)
        {
            // Создаём новый экземпляр SMTP клиента.

            SmtpClient smtp = new SmtpClient();

            // Настраиваем SMTP клиент.

            // Не будем использовать данные по умолчанию.
            // Воспользуемся данными, полученными из настроек приложения.

            smtp.UseDefaultCredentials = false;

            // Порт для SMTP.

            smtp.Port = settings.SmtpPort;

            // Используем протокол SSL для шифрования соединения.

            smtp.EnableSsl = settings.Ssl;

            // Формат доставки – International.
            // Символы, отличные от ASCII, в полях конверта и заголовка, используемых протоколом
            // SMTP для сообщений электронной почты, кодируются с помощью символов UTF-8.

            smtp.DeliveryFormat = SmtpDeliveryFormat.International;

            // Способ доставки сообщений – Network.
            // Электронная почта отправляется по сети на сервер SMTP.

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            // Имя хоста, используемого для SMTP отправления сообщений.

            smtp.Host = settings.SmtpHost;

            // Максимальное время за которое сообщение должно быть отправлено.

            smtp.Timeout = settings.SmtpTimeout;

            // Учётные данные для проверки подлинности отправителя.

            smtp.Credentials = new NetworkCredential(settings.SmtpSenderEmail, settings.SmtpPassword);

            // Формируем новое электронное сообщения для передачи SMTP-серверу.
            
            // Создаём и инициализируем новый экземпляр объекта, отвечающего за отправление сообщений.

            MailMessage mailMessage = new MailMessage();

            // E-mail адрес и имя отправителя сообщения электронной почты.

            mailMessage.From = new MailAddress(settings.SmtpSenderEmail, settings.SmtpSenderName);

            // E-mail получателя задаём согласно информации, указанной в настройках приложения.

            mailMessage.To.Add(settings.MailTo);

            // Тема сообщения, полученная из входного параметра функции "mailSubject".

            mailMessage.Subject = mailSubject;

            // Текст сообщения, полученный из входного параметра функции "mailBody".

            mailMessage.Body = mailBody;

            // Кодировка темы сообщения – "UTF8".

            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            // Кодировка текста сообщения – "UTF8".

            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

            // Кодировка передачи текста сообщения – "Base64".

            mailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            
            // Текст почтового сообщения имеет формат HTML.
            
            mailMessage.IsBodyHtml = true;

            // Приоритет данного сообщения электронной почты: "Normal" – "Обычный".

            mailMessage.Priority = MailPriority.Normal;

            try
            {
                // Отправляем сформированное электронное сообщение SMTP-серверу.

                await smtp.SendMailAsync(mailMessage);                
            }

            // Если в процессе отправления электронного сообщения возникли проблемы.

            catch (Exception e)
            {
                // Уведомляем пользователя посредством диалогового окна.

                MessageBox.Show(e.Message);
                throw;
            } 
            
            // Завершаем отправление электронного сообщения. Освобождаем ресурсы.

            smtp.Dispose();
        }

        /// <summary>
        /// Отправляет SMS через e-mail шлюз.
        /// </summary>
        /// <param name="mailBody">Сообщение.</param>
        /// <param name="settings">Настройки e-mail в приложении.</param>
        private async Task SendEmailToSmsAsync(string mailBody, AmsSettings settings)
        {
            // Создаём новый экземпляр SMTP клиента.

            SmtpClient smtp = new SmtpClient();

            // Настраиваем SMTP клиент.

            // Не будем использовать данные по умолчанию.
            // Воспользуемся данными, полученными из настроек приложения.

            smtp.UseDefaultCredentials = false;

            // Порт для SMTP-транзакций.

            smtp.Port = settings.SmtpPort;

            // Используем протокол SSL для шифрования соединения.

            smtp.EnableSsl = settings.Ssl;

            // Формат доставки – International.
            // Символы, отличные от ASCII, в полях конверта и заголовка, используемых протоколом
            // SMTP для сообщений электронной почты, кодируются с помощью символов UTF-8.

            smtp.DeliveryFormat = SmtpDeliveryFormat.International;

            // Способ доставки сообщений – Network.
            // Электронная почта отправляется по сети на сервер SMTP.

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            // Имя хоста, используемого для SMTP отправления сообщений.

            smtp.Host = settings.SmtpHost;

            // Максимальное время за которое сообщение должно быть отправлено.

            smtp.Timeout = settings.SmtpTimeout;

            // Учётные данные для проверки подлинности отправителя.

            smtp.Credentials = new NetworkCredential(settings.SmtpSenderEmail, settings.SmtpPassword);

            // Формируем новое электронное сообщения для передачи SMTP-серверу.
            // Создаём и инициализируем новый экземпляр объекта, отвечающего за отправление сообщений.

            MailMessage mailMessage = new MailMessage();

            // E-mail адрес и имя отправителя сообщения электронной почты.

            mailMessage.From = new MailAddress(settings.SmtpSenderEmail, settings.SmtpSenderName);

            // E-mail получателя задаём согласно информации, указанной в настройках приложения.

            mailMessage.To.Add(settings.EmailToSMS);

            // Тема сообщения, полученная из входного параметра функции "mailSubject".

            mailMessage.Body = mailBody;

            // Кодировка темы сообщения – "UTF8".

            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            // Кодировка текста сообщения – "UTF8".

            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

            // Кодировка передачи текста сообщения – "Base64".

            mailMessage.BodyTransferEncoding = System.Net.Mime.TransferEncoding.Base64;

            // Текст почтового сообщения имеет формат HTML.

            mailMessage.IsBodyHtml = true;

            // Приоритет данного сообщения электронной почты: "Normal" – "Обычный".

            mailMessage.Priority = MailPriority.Normal;
            
            try
            {
                // Отправляем сформированное электронное сообщение SMTP-серверу.

                await smtp.SendMailAsync(mailMessage);
            }
            catch (Exception e)
            {
                // Если в процессе отправления электронного сообщения возникли
                // проблемы – уведомляем пользователя посредством диалогового окна.

                MessageBox.Show(e.Message);
                throw;
            }

            // Завершаем отправление электронного сообщения. Освобождаем ресурсы.

            smtp.Dispose();
        }

        /// <summary>
        /// Отправляет SMS способом, указанным в настройках приложения.
        /// </summary>
        /// <param name="text">Текст SMS сообщения.</param>
        /// <param name="settings">Настройки приложения.</param>
        /// <returns></returns>

        async Task SendSmsAsync(string text, AmsSettings settings)
        {
            switch (settings.WayToSendSms)
            {
                // Способ отправки SMS: при помощи email-to-sms шлюза.

                case "email":

                    // Если в настройках указано: "Транслитерировать сообщение".

                    if (settings.EmailNeedsTranslit)
                    {
                        // Экземпляр класса Translit для осуществления конвертации текста.

                        Translit translit = new Translit(false);

                        // Транслитерируем текст сообщения.

                        text = translit.TranslitString(text);
                    }

                    // Если e-mail шлюз определён в настройках.

                    if (!String.IsNullOrWhiteSpace(settings.EmailToSMS))

                        // Отправляем SMS с заданным текстом и указанным в настройках параметрам.

                        await SendEmailToSmsAsync(text, settings);

                    // Если пользователь забыл указать e-mail шлюз в настройках.

                    else

                        // Уведомляем посредством диалогового окна.

                        MessageBox.Show("Не указан e-mail шлюз в настройках приложения.", "Отправка СМС не удалась");

                    break;

                // Способ отправки SMS: при помощи физического устройства на базе Android 10+ через Android Debug Bridge.

                case "adb":

                    // Если файл adb.exe указан в настройках.

                    if (!String.IsNullOrWhiteSpace(settings.AdbFile))
                    {
                        // Экземпляр класса Process для запуска внешнего приложения.

                        Process process = new Process();

                        // Указывем полный путь для запуска приложения, указанный в настройках.

                        process.StartInfo.FileName = settings.AdbFile;

                        // Экземпляр класса FileInfo для запуска файла adb.exe.

                        FileInfo file = new FileInfo(settings.AdbFile);

                        // Рабочая папка adb.exe.

                        string workingDirectory = file.DirectoryName;

                        // Передаём рабочую папку в экземпляр процесса.

                        process.StartInfo.WorkingDirectory = workingDirectory;

                        // Если в настройках указано: "Транслитерировать сообщение".

                        if (settings.SmsNeedsTranslit)
                        {
                            // Экземпляр класса Translit для осуществления конвертации текста.

                            Translit translit = new Translit(false);

                            // Транслитерируем текст сообщения.

                            text = translit.TranslitString(text);
                        }

                        // Аргументы для запуска внешнего приложения.
                        // Для Android версии 10 и выше:
                        // adb shell service call isms 5 i32 1 s16 "com.android.mms" s16 "null" s16 "номер получателя" s16 "null" s16 "текст сообщения" s16 "null" s16 "null" i32 0 i64 0
                        // Для более ранних версий следует изменить на:
                        // adb shell service call isms 7 i32 1 s16 "com.android.mms" s16 "номер получателя" s16 "null" s16 "текст сообщения" s16 "null" s16 "null"

                        process.StartInfo.Arguments = "shell service call isms 5 i32 1 s16 " + quote + "com.android.mms" + quote + " s16 " + quote + "null" + quote + " s16 " + quote + amsSettings.PhoneToSMS + quote + " s16 " + quote + "null" + quote + " s16 " + quote + text + quote + " s16 " + quote + "null" + quote + " s16 " + quote + "null" + quote + " i32 0 i64 0";

                        // Запуск внешнего приложения.

                        process.Start();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Запускает мониторинг узлов, отмеченных для наблюдения.
        /// </summary>
        /// <param name="cancelToken">Токен для отмены операции.</param>
        void StartMonitoring(CancellationToken cancelToken)
        {
            // Экземпляр класса Ping для организации процесса опроса узлов.

            Ping ping = new Ping();

            // Количество запросов без ответа.

            int failedPings = 0;

            // Количество запросов для определения процента потери пакетов
            // (чем больше, тем точнее, но медленнее).

            int pingAmount = 4;

            // Процент потери пакетов

            int packetLoss = 0;

            // Вызываем делегат элемента ListView для организации работы в параллельном режиме
            // между отображением графического интерфейса приложения и процессом мониторинга узлов.

            listView1.Invoke(new Action(async () =>
            {
                // Список узлов отмеченных для мониторинга.

                List<ListViewItem> lvTesting = new List<ListViewItem>();

                // Передаём информацию о наблюдаемых узлах, полученную из элемента
                // ListView главного окна приложения в созданный выше список.

                foreach (ListViewItem item in listView1.Items)
                    lvTesting.Add(item);

                // Пока не нажата кнопка "Прекратить мониторинг"
                // опрашиваем узлы в списке.

                while (!cancelToken.IsCancellationRequested)
                {
                    // Количество узлов "в сети".

                    uint totalOnline = 0;

                    // Количество узлов "не в сети".

                    uint totalOffline = 0;

                    // Получаем информацию о статусе узлов,                  
                    // результат сохраняем в список nodesStatus.

                    foreach (ListViewItem item in lvTesting)
                    {
                        // Статус текущего узла.

                        AmsNodeStat nodeStatus = new AmsNodeStat();

                        // Задержка между ping-запросами.

                        await Task.Delay(_pingTimeout);

                        // Для осуществления ping-запроса
                        // IP-адрес должен быть указан.

                        if (item.SubItems[1].Text.Length > 0)
                        {
                            // Имя узла.

                            if (item.SubItems[0].Text.Length > 0)
                                nodeStatus.Name = item.SubItems[0].Text;

                            // IP-адрес узла.

                            IPAddress ip = IPAddress.Parse(item.SubItems[1].Text);
                            nodeStatus.Ip = ip.ToString();

                            // ID узла.

                            nodeStatus.Id = item.SubItems[6].Text;

                            // Статус доступности узла.

                            bool isOnline = false;
                                                        
                            // Ping-запрос.                            

                            try
                            {
                                // Получаем ответ от узла.

                                PingReply resp = await ping.SendPingAsync(ip);

                                // Если отслеживаем "Доступность".

                                if (item.SubItems[2].Text != " - ")
                                {                                   
                                    // Если статус узла - Online.

                                    if (resp.Status == IPStatus.Success)
                                    {
                                        // Меняем пиктограмму узла на "Online".

                                        SelectNodeImg(tabControl1, nodeStatus.Id, Resources.PC_Online);

                                        // А до этого был Offline.

                                        if (item.SubItems[2].Text == "Offline")
                                        {
                                            // Если оповещения по e-mail включены в настройках приложения – отправляем письмо.

                                            if (amsSettings.EmailNotification)
                                            {
                                                // Тема письма.

                                                string subject = "Cоединение с узлом " + item.SubItems[1].Text + " (" + item.Text + ") восстановлено";

                                                // Текст письма.

                                                string body = DateTime.Now.ToString() + ": Узел "
                                                    + item.SubItems[1].Text + " (" + item.Text + ") " + "появился в сети.";

                                                // Оповещаем по e-mail с параметрами указанными в настройках приложения.

                                                await SendEmailAsync(subject, body, amsSettings);
                                            }

                                            // Если оповещения по SMS включены в настройках приложения – отправляем SMS.

                                            if (amsSettings.SmsNotification)
                                            {
                                                // Текст сообщения.

                                                string smsText = "Восстановлено соединение с " + item.SubItems[1].Text + " " + item.Text;

                                                // Оповещаем посредством SMS способом указанным в настройках приложения.

                                                await SendSmsAsync(smsText, amsSettings);

                                            }

                                            // Звуковое оповещение о восстановлении соединения.

                                            new System.Media.SoundPlayer(@"C:\WINDOWS\Media\Windows Unlock.wav").Play();
                                        }

                                        // Устанавливаем статус доступности "в сети".

                                        item.SubItems[2].Text = "Online";

                                        // Узел доступен.

                                        isOnline = true;

                                        // Количество успешных ответов на запрос +1.

                                        nodeStatus.Succed++;
                                    }

                                    // Если статус узла - Offline.

                                    else
                                    {
                                        // Меняем пиктограмму узла на "Offline".

                                        SelectNodeImg(tabControl1, nodeStatus.Id, Resources.PC_Offline);

                                        // А до этого был Online.

                                        if (item.SubItems[2].Text == "Online")
                                        {
                                            // Отправляем e-mail оповещение.

                                            if (amsSettings.EmailNotification)
                                            {
                                                // Тема письма.

                                                string subject = "Потеряно соединение с узлом " + item.SubItems[1].Text + " (" + item.Text + ")";

                                                // Текст письма.

                                                string body = DateTime.Now.ToString() + ": Узел "
                                                    + item.SubItems[1].Text + " (" + item.Text + ") " + "перестал отвечать на запросы.";

                                                // Отправление письма по указанным в настройках параметрам.

                                                await SendEmailAsync(subject, body, amsSettings);
                                            }

                                            // Отправляем SMS оповещение.

                                            if (amsSettings.SmsNotification)
                                            {
                                                // Текст сообщения.

                                                string smsText = "Потеряно\\ соединение\\ с\\ " + item.SubItems[1].Text + "\\ " + item.Text;

                                                // Оповещаем посредством SMS способом указанным в настройках приложения.

                                                await SendSmsAsync(smsText, amsSettings);

                                            }

                                            // Звуковое оповещение о потери соединения.

                                            new System.Media.SoundPlayer(@"C:\WINDOWS\Media\Windows Notify System Generic.wav").Play();
                                        }

                                        // Меняем доступность узла.

                                        item.SubItems[2].Text = "Offline";

                                        // Узел недоступен.

                                        isOnline = false;

                                        // Количество запросов без ответа +1.

                                        nodeStatus.Failed++;
                                    }
                                }

                                // Если отслеживаем "Время отклика".

                                if (item.SubItems[3].Text.ToString() != " - " && resp.Status == IPStatus.Success)
                                {
                                    item.SubItems[3].Text = resp.RoundtripTime.ToString() + " мс";     /*------------------------------------------------------------БББББББББББББББББББББББ---------*/
                                }

                                // Если отслеживаем "Потери пакетов".

                                if (item.SubItems[4].Text.ToString() != " - ")
                                {
                                    // Оправляем ping-запрос на указанный IP-адрес.

                                    PingReply respPL = await ping.SendPingAsync(ip);

                                    // Повторяем указанное в начале функции количество раз.

                                    for (int i = 0; i < pingAmount; i++)
                                        if (respPL.Status != IPStatus.Success)
                                            failedPings += 1;

                                    packetLoss = Convert.ToInt32((Convert.ToDouble(failedPings) / Convert.ToDouble(pingAmount)) * 100);
                                    item.SubItems[4].Text = packetLoss.ToString() + "%";
                                    failedPings = 0;
                                    packetLoss = 0;
                                }

                                // Если отслеживаем "Работоспособность сервисов".

                                if (item.SubItems[5].Text.ToString() != " - "
                                    && item.SubItems[5].Text.Length > 0)
                                {
                                    // Службы указанные для мониторинга.

                                    string[] mServices = item.SubItems[5].Text.Split(';');

                                    // Список досупных служб.

                                    List<string> servicesOnline = new List<string>();

                                    // Список служб не ответевших на запрос.

                                    List<string> servicesOffline = new List<string>();

                                    // Анализируем указанные для мониторинга службы.

                                    foreach (string mService in mServices)
                                    {
                                        // Если указана хотя бы одна служба для мониторинга.

                                        if (item.SubItems[0].Text.Length > 0)
                                            try
                                            {
                                                // Узнаём статус службы на удалённом ПК (имя службы, доменное имя удаленного компьютера).

                                                Process[] runningProcesses = Process.GetProcessesByName(mService, item.SubItems[0].Text);

                                                // Если найдена хотя бы одна служба.

                                                if (runningProcesses.Length > 0)

                                                    // Добавляем в список досупных служб.

                                                    servicesOnline.Add(mService);

                                                // Если ни одна служба не обнаружена.

                                                else if (mService.Length > 0)
                                                {
                                                    // Добавляем в список недосупных служб.

                                                    servicesOffline.Add(mService);

                                                    // Уведомить пользователя о проблеме - "служба не выполняется".

                                                    // Отправляем e-mail оповещение.

                                                    if (amsSettings.EmailNotification)
                                                    {
                                                        // Тема письма.

                                                        string subject = "Служба " + mService + " на устройстве " + item.SubItems[1].Text + " (" + item.Text + ") не выполняется!";

                                                        // Текст письма.

                                                        string body = DateTime.Now.ToString() + ": Служба  " + mService + " на устройстве " 
                                                            + item.SubItems[1].Text + " (" + item.Text + ") " + "перестала отвечать на запросы.";

                                                        // Отправление письма по указанным в настройках параметрам.

                                                        await SendEmailAsync(subject, body, amsSettings);
                                                    }

                                                    // Отправляем SMS оповещение.

                                                    if (amsSettings.SmsNotification)
                                                    {
                                                        // Текст сообщения.

                                                        string smsText = "Служба\\ " + mService + "не\\ отвечает\\ "+ item.Text;

                                                        // Оповещаем посредством SMS, способом указанным в настройках приложения.

                                                        await SendSmsAsync(smsText, amsSettings);

                                                    }

                                                    // Изменяем индикатор "Обнаружена проблема"

                                                    label5.Text = servicesOffline.Count.ToString();
                                                }
                                            }
                                            catch (Exception) { }
                                    }

                                    // Заполняем поле "службы" в списке мониторинга узлов.
                                    // Очищаем поле.

                                    item.SubItems[5].Text = "";

                                    // Если обнаружены работающие службы.

                                    if (servicesOnline.Count > 0)

                                        // Добавляем каждую службу в список.

                                        foreach (string service in servicesOnline)

                                            // Значение поля - имя службы, разделитель – точка с запятой.

                                            item.SubItems[5].Text += service + ";";
                                }
                            } 

                            catch (Exception) { }

                            // Если в списке статусов есть узел с таким же ID.

                            if (nodesStatus.Exists(x => x.Id == nodeStatus.Id))
                            {
                                // Находим положение такой записи.

                                int idx = nodesStatus.FindIndex(x => x.Id == nodeStatus.Id);

                                if (idx > -1)
                                {
                                    // Обновляем имя узла в списке.

                                    nodesStatus[idx].Name = nodeStatus.Name;

                                    // Обновляем IP - адрес узла в списке. 

                                    nodesStatus[idx].Ip = nodeStatus.Ip;

                                    // Вычисляем время работы узла в текущей итерации опроса в секундах.

                                    uint workTime = (uint)(DateTime.Now - nodeStatus.Time).TotalSeconds;

                                    // Обновляем статистику работы узлов.
                                    // Если узел доступен.

                                    if (isOnline)
                                    {
                                        // Количество успешных ответов на запрос.

                                        nodesStatus[idx].Succed += nodeStatus.Succed;

                                        // Общее время в сети.

                                        nodesStatus[idx].OnlineTime += workTime;

                                        // Увеличиваем количество доступных узлов на 1.

                                        totalOnline++;
                                    }
                                                                        
                                    // Если узел не отвечает.

                                    else
                                    {
                                        // Количество запросов без ответа.

                                        nodesStatus[idx].Failed += nodeStatus.Failed;

                                        // Общее время простоя.

                                        nodesStatus[idx].OfflineTime += workTime;

                                        // Увеличиваем количество недоступных узлов на 1

                                        totalOffline++;
                                    }

                                    // Обновляем общее время работы.

                                    nodesStatus[idx].TotalTime += (uint)(DateTime.Now - nodeStatus.Time).TotalSeconds;

                                    // Обновляем процент простоя.

                                    nodesStatus[idx].OfflinePercent = Math.Round(nodesStatus[idx].OfflineTime / nodesStatus[idx].TotalTime * 100, 0);

                                    // Обновляем время финального опроса.

                                    nodesStatus[idx].FinishTime = DateTime.Now;
                                }
                            }

                            // Если в списке статусов нет узла с таким же ID.

                            else
                            {
                                // Добавляем имя, IP-адрес, время создания и время финального опроса
                                // текущего узла в список, хранящий информацию о статусах узлов.

                                nodesStatus.Add(nodeStatus);
                            }
                        }
                    }

                    // Логирование: сохраняем текущие показатели узлов в файл.

                    if (nodesStatus.Count > 0)
                    {
                        // Cохраняем список статусов в файл.
                        // Тип данных для сериализации – List<AmsNodeStat>.

                        XmlSerializer formatter = new XmlSerializer(typeof(List<AmsNodeStat>));

                        // Сегодняшняя дата для формирования названия файла.

                        string today = DateTime.Now.Date.Day.ToString()
                                + "-" + DateTime.Now.Date.Month.ToString()
                                + "-" + DateTime.Now.Date.Year.ToString();

                        // Проверяем наличие директории для хранения логов.
                        // При её отсутствии – создаём новую папку.

                        Directory.CreateDirectory(amsSettings.LogsFolder);

                        // Сохраняем данные логирования в файл.

                        using (FileStream fs = new FileStream(amsSettings.LogsFolder + "\\" + today + ".xml", FileMode.Create))
                        {
                            formatter.Serialize(fs, nodesStatus);
                        }
                    }

                    // Оповещение о количестве узлов "в сети" внизу формы.

                    label3.Text = totalOnline.ToString();

                    // Оповещение о количестве узлов "не в сети" внизу формы.

                    label7.Text = totalOffline.ToString();
                }
            }));
        }
    }
}
