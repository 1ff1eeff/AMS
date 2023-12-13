using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace AMS
{
    /// <summary>
    /// Форма "Редактирование узла" в режиме создания карты.
    /// </summary>
    public partial class EditNodeCM : Form
    {
        // Для связи с компонентом ListView.

        public ListView lv;

        // Для связи с элементом ListView.

        public ListViewItem lvi;
        
        // Список для хранения информации о запущенных службах.

        private readonly List<string> _detectedProcesses = new List<string>();

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public EditNodeCM()
        {
            // Инициализация компонентов.

            InitializeComponent();
        }

        // Событие – "Форма загружена".
        // Получаем информацию об узле.
        private void EditNode_Load(object sender, EventArgs e)
        {
            // Если полученный ListView содержит хотя бы один элемент.

            if (lvi.Text.Length > 0)
            {
                // Получаем IP-адрес узла.

                tbIp.Text = lvi.Text;
            }

            // Если полученный ListView содержит два элемента.

            if (lvi.SubItems.Count > 1)
            {
                // Получаем MAC-адрес узла.

                tbMac.Text = lvi.SubItems[1].Text;
            }

            // Если полученный ListView содержит три элемента.

            if (lvi.SubItems.Count > 2)
            {
                // Получаем имя узла.

                tbName.Text = lvi.SubItems[2].Text;
            }

            // Если полученный ListView содержит восемь элементов.

            if (lvi.SubItems.Count > 7)
            {
                // Получаем имя узла на карте.

                tbNameOnMap.Text = lvi.SubItems[7].Text;
            }

            // Объявляем и инициализируем объект класса AmsNode.

            AmsNode node = new AmsNode();

            // Если узел расположен в одной сети с АСМ.

            if (node.IsInMyIPv4Subnet(IPAddress.Parse(lvi.Text)))
            {
                // Блок кода, в котором может произойти исключение.

                try
                {
                    // Помещаем найденные службы в массив объектов класса Process.

                    Process[] runningProcesses = Process.GetProcesses(lvi.SubItems[2].Text);

                    // Если найдена хотя бы одна служба.

                    if (runningProcesses.Length > 0)
                    {
                        // Анализируем найденные службы.

                        foreach (Process runningProcess in runningProcesses)
                        {
                            // Если обнаружено имя службы.

                            if (runningProcess.ProcessName.Length > 0)
                            {
                                // Добавляем в список предназначенный для хранения запущенных служб.

                                _detectedProcesses.Add(runningProcess.ProcessName);
                            }
                        }
                    }
                }

                // Обрабатываем исключения.

                catch (Exception) 
                { 
                }
            }
        }

        /// <summary>
        /// Кнопка "Добавить".
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Создаём экземпляр формы "CreateService".

            CreateService createService = new CreateService
            {
                // Передаём управление компонентом, отображающим запущенные службы.

                lbService = lbRunServices
            };

            // Открываем форму добавления новой службы.

            createService.ShowDialog();
        }

        /// <summary>
        /// Кнопка "Удалить".
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Если в компоненте, отображающем запущенные службы
            // добавлен и выделен хотя бы один элемент.

            if (lbRunServices.Items.Count > 0 && lbRunServices.SelectedIndex >= 0)
            {
                // Удаляем выделенный элемент.

                lbRunServices.Items.RemoveAt(lbRunServices.SelectedIndex);
            }
        }

        /// <summary>
        /// Кнопка "Обнаружить".
        /// </summary>
        private void btnDetect_Click(object sender, EventArgs e)
        {
            // Создаём экземпляр формы "Выбор процесса".

            SelectProcess selectProcess = new SelectProcess
            {
                // Передаём управление списком содержащем информацию о запущенных службах.

                detectedProcesses = _detectedProcesses,

                // Передаём управление компонентом, отображающим запущенные службы.

                lb = lbRunServices
            };

            // Открываем форму "Выбор процесса" в формате диалогового окна.

            selectProcess.ShowDialog();                
        }

        /// <summary>
        /// Кнопка "ОК".
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Объявляем и инициализируем список, содержащий элементы компонента ListView.

            ListViewItem activeNode = new ListViewItem();

            // Если поле для ввода IP-адреса содержит текст.

            if (!string.IsNullOrEmpty(tbIp.Text))
            {
                // Первое поле элемента – IP-адрес узла.

                activeNode.Text = tbIp.Text;
            }

            // Если поле для ввода IP-адреса не содержит текст.

            else
            {
                // В первое поле элемента устанавливаем значение " - ". 

                activeNode.Text = " - ";
            }

            // Если поле для ввода MAC-адреса содержит текст.

            if (!string.IsNullOrEmpty(tbMac.Text))
            {
                // Второе поле элемента – MAC-адрес узла.

                activeNode.SubItems.Add(tbMac.Text);
            }

            // Если поле для ввода MAC-адреса не содержит текст.

            else
            {
                // Во второе поле элемента устанавливаем значение " - ".

                activeNode.SubItems.Add(" - ");
            }

            // Если поле для ввода имени узла содержит текст.

            if (!string.IsNullOrEmpty(tbNameOnMap.Text))
            {
                // Третье поле элемента – имя узла.

                activeNode.SubItems.Add(tbNameOnMap.Text);
            }

            // Если поле для ввода имени узла не содержит текст.

            else
            {
                // В третье поле элемента устанавливаем значение " - ".

                activeNode.SubItems.Add(" - ");
            }

            // Объявляем и инициализируем текстовую переменную для хранения имени служб.

            string services = "";

            // Если компонент ListBox содержит информацию о службах – анализируем.

            foreach (string item in lbRunServices.Items)
            {
                // Добавляем значение текущего элемента в текстовую переменную, определённую для хранения имени служб.
                // Разделитель между службами – точка с запятой.

                services += item + ";";
            }

            // Если переменная, определённая для хранения имени служб, содержит текст.

            if (!string.IsNullOrEmpty(services))
            {
                // Четвёртое поле элемента – запущеные службы.

                activeNode.SubItems.Add(services);
            }
            else
            {
                // В четвёртое поле элемента устанавливаем значение " - ". 

                activeNode.SubItems.Add(" - ");
            }

            // Компонент ListBox, представляющий информацию о типе узла, содержит элементы.

            if (lbDeviceType.SelectedItem != null)
            {
                // Пятое поле элемента – тип узла.

                activeNode.SubItems.Add(lbDeviceType.SelectedItem.ToString());
            }

            // Компонент ListBox, представляющий информацию о типе узла, не содержит элементы.

            else
            {
                // В пятое поле элемента устанавливаем значение " - ". 

                activeNode.SubItems.Add(" - ");
            }

            // Компонент СomboBox, представляющий информацию о стандарте передачи данных узла, содержит тескст.

            if (!string.IsNullOrEmpty(cbStandard.Text))
            {
                // Шестое поле элемента – стандарт передачи данных узла.

                activeNode.SubItems.Add(cbStandard.Text);
            }

            // Компонент СomboBox, представляющий информацию о стандарт передачи данных узла, не содержит тескст.

            else
            {
                // В шестое поле элемента устанавливаем значение " - ".

                activeNode.SubItems.Add(" - ");
            }

            // Компонент СomboBox, представляющий информацию о протоколе передачи данных данных узла, содержит тескст.

            if (!string.IsNullOrEmpty(cbProtocol.Text))
            {
                // Седьмое поле элемента – протокол передачи данных данных узла.

                activeNode.SubItems.Add(cbProtocol.Text);
            }

            // Компонент СomboBox, представляющий информацию о протоколе передачи данных данных узла, содержит тескст.

            else
            {
                // В седьмое поле элемента устанавливаем значение " - ".

                activeNode.SubItems.Add(" - ");
            }

            // Если поле для ввода имени узла на карте содержит текст.

            if (!string.IsNullOrEmpty(tbName.Text))
            {
                // Восьмое поле элемента имя узла на карте.

                activeNode.SubItems.Add(tbName.Text);
            }

            // Если поле для ввода имени узла на карте не содержит текст.

            else
            {
                // В восьмое поле элемента устанавливаем значение " - ". 

                activeNode.SubItems.Add(" - ");
            }
                  
            // Анализируем выбранные элементы компонента ListView.

            foreach (ListViewItem item in lv.SelectedItems)
            {
                // Удаляем текущий элемент.

                lv.Items.Remove(item);
            }

            // Передаём полученные данные узла в конструктор карты.

            lv.Items.Add(activeNode);
            
            // Закрываем форму.

            Close();
        }

        /// <summary>
        /// Кнопка "Отмена".
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Закрываем форму.

            Close();
        }
    }
}
