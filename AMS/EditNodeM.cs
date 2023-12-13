using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AMS
{
    /// <summary>
    /// Форма "Редактирование узла" на карте.
    /// </summary>
    public partial class EditNodeM : Form
    {
        // Для связи с компонентом TabControl.

        public TabControl tc;

        // Объявляем объект класса AmsNode для хранения информации об узле.

        private AmsNode _node;

        // Список для хранения информации о запущенных службах.

        private readonly List<string> _detectedProcesses = new List<string>();

        // Определяем аксессоры – методы доступа к свойству "_node". 
        public AmsNode Node { get => _node; set => _node = value; }
        
        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public EditNodeM()
        {
            // Инициализация компонентов.

            InitializeComponent();
        }

        // Событие – "Форма загружена".
        // Анализируем объект "_node". Получаем информацию об узле.
        private void EditNodeM_Load(object sender, EventArgs e)
        {
            // Добавляем IP-адрес к тексту заголовка формы.

            Text += _node.Ip;

            // Если поле "Имя узла" содержит текст.

            if (_node.Name.Length > 0)
            {
                // Получаем имя узла.

                tbName.Text = _node.Name;
            }

            // Если поле "Имя узла на карте" содержит текст.

            if (_node.NameOnMap.Length > 0)
            {
                // Получаем имя узла на карте.

                tbNameOnMap.Text = _node.NameOnMap;
            }

            // Если поле "IP-адрес" содержит текст.

            if (_node.Ip.Length > 0)
            {
                // Получаем IP-адрес узла.

                tbIp.Text = _node.Ip;
            }

            // Если поле "MAC-адрес" содержит текст.

            if (_node.Mac.Length > 0)
            {
                // Получаем MAC-адрес узла.

                tbMac.Text = _node.Mac;
            }

            // Если поле "Службы" содержит элементы – анализируем список служб.

            foreach (string service in _node.Services)
            {
                // Если имя службы содержит текст.

                if (service.Length > 0)
                {
                    // Заполняем список запущенных служб. Компонент формы – ListBox.
                    // Добавляем имя текущей службы в качестве нового элемента.

                    lbServices.Items.Add(service);
                }
            }

            // Если поле "Тип узла" содержит текст.

            if (_node.Type.Length > 0)
            {
                // Получаем тип узла.

                cbDeviceType.Text = _node.Type;
            }

            // Если поле "Стандарт передачи данных" содержит текст.

            if (_node.Standard.Length > 0)
            {
                // Получаем стандарт передачи данных.

                cbStandard.Text = _node.Standard;
            }

            // Если поле "Протокол передачи данных" содержит текст.

            if (_node.Type.Length > 0)
            {
                // Получаем протокол передачи данных.

                cbProtocol.Text = _node.Protocol;
            }
        }

        // Кнопка "ОК".
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Анализируем информацию о выделенных узлах на компоненте TabControl.

            foreach (DeviceNode nodeOnMap in tc.SelectedTab.Controls.OfType<DeviceNode>())
            {
                // Если узел выделен и его универсальный идентификатор совпадает с идентификатором объекта. 

                if (nodeOnMap.DNode.IsSelected && nodeOnMap.DNode.Id == _node.Id)
                {
                    // Если поле для ввода имени узла содержит текст.

                    if (tbName.Text.Length > 0)
                    {

                        // Задаём имя узла.

                        nodeOnMap.DNode.Name = tbName.Text;
                    }

                    // Если поле для ввода имени узла узла на карте содержит текст.

                    if (tbNameOnMap.Text.Length > 0)
                    {
                        // Задаём имя узла на карте.

                        nodeOnMap.DNode.NameOnMap = tbNameOnMap.Text;

                        // Задаём подпись узла на карте.

                        nodeOnMap.LbSetNameOnMap(tbNameOnMap.Text);
                    }

                    // Если поле для ввода IP-адреса содержит текст.

                    if (tbIp.Text.Length > 0)
                    {
                        // Задаём IP-адрес.

                        nodeOnMap.DNode.Ip = tbIp.Text;
                    }

                    // Если поле для ввода MAC-адреса содержит текст.

                    if (tbMac.Text.Length > 0)
                    {
                        // Задаём MAC-адрес узла.

                        nodeOnMap.DNode.Mac = tbMac.Text;
                    }

                    // Если компонент ListBox содержит информацию о службах.

                    if (lbServices.Items.Count > 0)
                    {
                        // Объявляем массив строк, для хранения имён процессов.

                        nodeOnMap.DNode.Services = new string[lbServices.Items.Count];

                        // Анализируем компонент ListBox, содержащий информацию о службах. 

                        for (int i = 0; i < lbServices.Items.Count; i++)
                        {
                            // Заполняем поле "Отслеживаемые службы" компонента, представляющего узел на карте.

                            nodeOnMap.DNode.Services[i] = lbServices.Items[i].ToString();
                        }
                    }

                    // Если компонент СomboBox, представляющий информацию о типе узла, содержит тескст.

                    if (cbDeviceType.Items.Count > 0)
                    {
                        // Задаём тип узла.

                        nodeOnMap.DNode.Type = cbDeviceType.Text;
                    }

                    // Если компонент СomboBox,
                    // представляющий информацию о стандарте передачи данных узла, содержит тескст.

                    if (cbStandard.Items.Count > 0)
                    {
                        // Задаём стандарт передачи данных.

                        nodeOnMap.DNode.Standard = cbStandard.Text;
                    }

                    // Если компонент СomboBox,
                    // представляющий информацию о протоколе передачи данных данных узла, содержит тескст.

                    if (cbProtocol.Items.Count > 0)
                    {
                        // Задаём протокол передачи данных.

                        nodeOnMap.DNode.Protocol = cbProtocol.Text;
                    }

                }
            }
            // Закрываем форму.

            Close();
        }

        // Кнопка "Отмена".
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Закрываем форму.

            Close();
        }

        // Добавить сервис
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Создаём экземпляр формы "CreateService".

            CreateService createService = new CreateService
            {
                // Передаём управление компонентом ListBox, предназначенным для хранения списка служб.

                lbService = lbServices
            };

            // Открываем форму добавления новой службы.

            createService.ShowDialog();
        }

        /// <summary>
        /// Кнопка "Удалить".
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Если компонент ListBox содержит элементы и один из них выбран.

            if (lbServices.Items.Count > 0 && lbServices.SelectedIndex >= 0)
            {
                // Удаляем элемент с выбранным индексом.

                lbServices.Items.RemoveAt(lbServices.SelectedIndex);
            }
        }

        /// <summary>
        /// Кнопка "Обнаружить".
        /// </summary>
        private void btnDetect_Click(object sender, EventArgs e)
        {
            // Блок кода, в котором может произойти исключение.

            try
            {
                // Формируем массив для хранения запущенных процессов.

                Process[] runningProcesses = Process.GetProcesses(_node.Name);

                // Если в массиве есть записи.

                if (runningProcesses.Length > 0)
                {
                    // Анализируем элементы массива.

                    foreach (Process runningProcess in runningProcesses)
                    {
                        // Если поле элемента "Имя процесса" содержит текст.

                        if (runningProcess.ProcessName.Length > 0)
                        {
                            // Добавляем имя процесса в список для хранения информации о запущенных службах.

                            _detectedProcesses.Add(runningProcess.ProcessName);
                        }
                    }
                }
            }

            // Обрабатываем исключения.

            catch (Exception) 
            { 
            }

            // Создаём экземпляр формы "Выбор процесса".

            SelectProcess selectProcess = new SelectProcess
            {
                // Передаём управление списком содержащем информацию о запущенных службах.

                detectedProcesses = _detectedProcesses,

                // Передаём управление компонентом, отображающим запущенные службы.

                lb = lbServices
            };

            // Открываем форму "Выбор процесса" в формате диалогового окна.

            selectProcess.ShowDialog();
        }

        
    }
}
