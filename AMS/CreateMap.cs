using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AMS
{
    /// <summary>
    /// Форма "Создание новой карты".
    /// </summary>
    public partial class CreateMap : Form
    {
        // Для связи с элементом главной формы, содержащим карты.

        public TabControl tc;

        // Импорт из "Win32API/iphlpapi.dll" функции "SendARP"
        // для получения MAC-адреса, соответствующего указанному целевому IPv4-адресу.        

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(int destIp, int srcIP, 
            byte[] macAddr, ref uint physicalAddrLen);

        // Список для хранения информации об узлах сети.

        private readonly List<AmsNode> _nodes = new List<AmsNode>();

        // Объект управления токеном для отмены операции. 

        private CancellationTokenSource _cts = new CancellationTokenSource();

        // Пустая строка.

        private const string _strEmpty = "";

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>        
        public CreateMap()
        {
            // Инициализация компонентов.

            InitializeComponent();
        }

        /// <summary>
        /// Проверка корректности ввода IP-адреса.
        /// </summary>
        /// <param name="ip">Проверяемый IP-адрес.</param>
        /// <returns>Возвращает true, если полученный IP-адрес корректен.</returns>
        public static bool IsIpValid(string ip)
        {
            // Возвращам true, если строка задана, количество символов разделителей
            // "точка" равно трём и строка проанализирована как IP-адрес.

            return ip != null && ip.Count(c => c == '.') == 3 
                && IPAddress.TryParse(ip, out IPAddress address);
        }

        /// <summary>
        /// Создание списка всех адресов из заданного диапазона.
        /// </summary>
        /// <param name="firstIPAddress">Начальный IP-адрес.</param>
        /// <param name="lastIPAddress">Финальный IP-адрес.</param>
        /// <returns>Список всех IP-адресов диапазона.</returns>
        private static List<IPAddress> IPAddressesRange(IPAddress firstIPAddress, IPAddress lastIPAddress)
        {
            // Представляем начальный IP-адрес диапазона в виде массива байтов.

            byte[] firstIPAddressAsBytesArray = firstIPAddress.GetAddressBytes();

            // Представляем финальный IP-адрес диапазона в виде массива байтов.

            byte[] lastIPAddressAsBytesArray = lastIPAddress.GetAddressBytes();

            // Инвертируем порядок элементов в массиве байтов, представляющем начальный IP-адрес.

            Array.Reverse(firstIPAddressAsBytesArray);

            // Инвертируем порядок элементов в массиве байтов, представляющем финальный IP-адрес.

            Array.Reverse(lastIPAddressAsBytesArray);

            // Преобразуем массив байтов в 32-битовое число со знаком, образованное четырьмя байтами.

            int firstIPAddressAsInt = BitConverter.ToInt32(firstIPAddressAsBytesArray, 0);

            // Преобразуем массив байтов в 32-битовое число со знаком, образованное четырьмя байтами.

            int lastIPAddressAsInt = BitConverter.ToInt32(lastIPAddressAsBytesArray, 0);

            // Создаём список для хранения диапазона IP-адресов.

            List<IPAddress> ipAddressesInTheRange = new List<IPAddress>();

            // Проходим по всем IP-адресам от начального до финального.

            for (int i = firstIPAddressAsInt; i <= lastIPAddressAsInt; i++)
            {
                // Преобразуем IP-адрес представленный в виде 32-битного числа в массив из четырёх байтов.

                byte[] bytes = BitConverter.GetBytes(i);

                // Объявляем и инициализируем объект, представляющий IP-адрес, массивом из четырёх байтов.

                IPAddress newIp = new IPAddress(new[] { bytes[3], bytes[2], bytes[1], bytes[0] });

                // Добавляем созданный объект, представляющий IP-адрес, в список для хранения диапазона IP-адресов.

                ipAddressesInTheRange.Add(newIp);
            }

            // Возвращаем список содержащий все IP-адреса указанного диапазона.

            return ipAddressesInTheRange;
        }

        /// <summary>
        /// Кнопка "ОК"
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            // Если выбран вариант "Создать пустую карту".

            if (rbNewMap.Checked)
            {
                // Создаём и иницализируем новую вкладку.
                // Текст ярлычка вкладки получаем из поля ввода "tbMapName" – "Имя новой карты"

                TabPage tp = new TabPage(tbMapName.Text);

                // Добавляем новую вкладку на карту.

                tc.TabPages.Add(tp);

                // Делаем вкладку активной.

                tc.SelectTab(tp);
            }

            // Если выбран вариант "Карта на основе сканирования".
            // Создаём карту на основании сканирования IP диапазонов.

            if (rbMapOnScan.Checked)
            {
                // Если параметр "Не создать новую карту" не отмечен.

                if (!cbNewMap.Checked)
                {
                    // Создаём и иницализируем новую вкладку.
                    // Текст ярлычка вкладки получаем из поля ввода "Имя новой карты".

                    TabPage tp = new TabPage(tbMapName.Text);

                    // Добавляем новую вкладку на карту.

                    tc.TabPages.Add(tp);

                    // Делаем вкладку активной.

                    tc.SelectTab(tp);
                }

                // Если в списке "Активные IP-адреса" есть элементы.

                if (lvScanResult.Items.Count > 0)
                {
                    // На основании элементов компонента формы ListView "Активные IP-адреса".                    

                    foreach (ListViewItem activeNodeItem in lvScanResult.Items)
                    {
                        // Объявляем и инициализируем объект, представляющий информацию о новом узле.

                        AmsNode node = new AmsNode
                        {
                            // Уникальный идентификатор.

                            Id = Guid.NewGuid().ToString(),

                            // IP-адрес.

                            Ip = activeNodeItem.Text,

                            // МАС-адрес.

                            Mac = activeNodeItem.SubItems[1].Text,

                            // NetBIOS-имя.

                            Name = activeNodeItem.SubItems[2].Text,

                            // Службы.

                            Services = activeNodeItem.SubItems[3].Text.Split(';'),

                            // Тип.

                            Type = activeNodeItem.SubItems[4].Text,

                            // Стандарт передачи данных.

                            Standard = activeNodeItem.SubItems[5].Text,

                            // Протокол передачи данных.

                            Protocol = activeNodeItem.SubItems[6].Text,

                            // Имя узла на карте.

                            NameOnMap = activeNodeItem.SubItems[7].Text
                        };

                        // Добавляем информацию о текущем узле в список.

                        _nodes.Add(node);
                    }
                }

                // Объявляем и инициализируем переменные для позиционирования узла на карте.
                // Положение по X, Y и разделитель.

                int x = 10, y = 0, spacer = 10;

                // Добавляем узлы на карту.

                foreach (AmsNode node in _nodes)
                {
                    // Подготавливаем пользовательский компонент, представляющий узел.

                    DeviceNode dn = new DeviceNode
                    {
                        // Положение по X и Y.

                        Location = new Point(x, y),

                        // Передаём информацию об узле в пользовательский компонент, представляющий узел.

                        DNode = node,                      
                    };

                    // Если для создания пользовательского компонента DeviceNode хватает места на карте.                

                    if (x < tc.Width - (dn.Size.Width * 2 + spacer))
                    {
                        // Располагаем каждый последующий компонент на расстоянии ширины компонента и разделителя.    

                        x += dn.Size.Width + spacer;
                    }

                    // Если элемент слишком близко к краю формы, то переносим на следующую строку.

                    else
                    {
                        // Положение по горизонтали равно значению разделителя.
                        
                        x = spacer;

                        // Увеличиваем положение по вертикали на значение высоты компонента плюс разделитель.

                        y += dn.Size.Height + spacer;
                    }

                    // Если на компоненте формы, представляющему карту, есть вкладки.

                    if (tc.TabPages.Count > 0)
                    {
                        // Добавляем новый элемент на карту.

                        tc.TabPages[tc.SelectedIndex].Controls.Add(dn);
                    }
                }
            }

            // Закрываем форму.

            Close();
        }

        /// <summary>
        /// Кнопка "Отмена".
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            // Закрываем форму.

            Close();
        }

        // Кнопка "Добавить диапазон в список"
        private void button2_Click(object sender, EventArgs e)
        {
            // Если введённые IP-адреса диапазона корректны.

            if (IsIpValid(tbFirstIp.Text) && IsIpValid(tbLastIp.Text))
            {
                // Объявляем и инициализируем элемент ListViewItem
                // текстом из поля для ввода "Начальный адрес".

                ListViewItem lvi = new ListViewItem(tbFirstIp.Text);

                // Второе поле элемента ListView – разделитель (для упрощения восприятия).

                lvi.SubItems.Add("–");

                // Третье поле элемента ListView – текст поля для ввода "Финальный адрес".

                lvi.SubItems.Add(tbLastIp.Text);

                // Добавляем созданный элемент ListViewItem в ListView "Диапазоны сканирования".

                lvScanRange.Items.Add(lvi);
            }
        }

        /// <summary>
        /// Кнопка "Удалить"
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            // Удаляем выделенные элементы из списка активных IP-адресов.

            foreach (ListViewItem selectedItem in lvScanResult.SelectedItems)
            {
                // Удаляем выделенный элемент из списка активных IP-адресов..

                lvScanResult.Items.Remove(selectedItem);
            }
        }

        /// <summary>
        /// Кнопка "Карта на основе сканирования".
        /// Событие "Состояние флаговой кнопки изменилось".
        /// </summary>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Активируем панель с компонентами, отвечающими за сканирование диапазона IP-адресов,
            // соответственно состоянию флаговой кнопки "Карта на основе сканирования".

            panScanRange.Enabled = rbMapOnScan.Checked;

            // Если на главной форме добавлены карты.

            if (tc.TabPages.Count > 0)
            {
                // Делаем флаговую кнопку "Не создать новую карту" доступной для взаимодействия.

                cbNewMap.Enabled = true;
            }

            // Если ни одной карты нет, то доступно только создание новой карты.

            else
            {
                // Отключаем взаимодействие с флаговой кнопкой "Не создать новую карту". 

                cbNewMap.Enabled = false;

                // Переводим флаговую кнопку в выключенное состояние.

                cbNewMap.Checked = false;
            }
        }

        /// <summary>
        /// Поле для ввода "Начальный адрес".
        /// Событие "Элемент перестал быть активным элементом управления".
        /// </summary>
        private void tbFirstIp_Leave(object sender, EventArgs e)
        {
            // Проверить корректность ввода IP-адреса в поле "Начальный IP-адрес"

            if (!IsIpValid(tbFirstIp.Text))
            {
                // Уведомляем пользователя посредством диалогового окна.

                MessageBox.Show("Некорректный начальный IP-адрес!");

                // Очищаем поле ввода "Начальный адрес"

                tbFirstIp.Text = _strEmpty;
            } 
        }

        /// <summary>
        /// Поле для ввода "Начальный адрес".
        /// Событие "Элемент перестал быть активным элементом управления".
        /// </summary>        
        private void tbLastIp_Leave(object sender, EventArgs e)
        {
            // Проверяем корректность ввода IP-адреса в поле "Финальный IP-адрес"
            // Ели адрес некорректен.

            if (!IsIpValid(tbLastIp.Text)) 
            {
                // Уведомляем пользователя посредством диалогового окна.

                MessageBox.Show("Некорректный финальный IP-адрес!");

                // Очищаем поле ввода "Финальный адрес"

                tbLastIp.Text = _strEmpty;
            }   
        }

        /// <summary>
        /// Кнопка ">".
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            // Копируем текст поля для ввода "Начальный IP-адрес" в поле "Финальный IP-адрес".

            tbLastIp.Text = tbFirstIp.Text;
        }

        /// <summary>
        /// Кнопка "Удалить".
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Удаляем выделенные строки из списка диапазонов сканирования.

            foreach (ListViewItem eachItem in lvScanRange.SelectedItems)
            {
                // Удаляем выделенную строку из списка диапазонов сканирования.

                lvScanRange.Items.Remove(eachItem);
            }
        }

        /// <summary>
        /// Кнопка "Сканировать".
        /// </summary>
        private async void btnScan_Click(object sender, EventArgs e)
        {
            // Если в ListView "Диапазоны сканирования" добавлены элементы.

            if (lvScanRange.Items.Count > 0)
            {
                // Если текст кнопки "Сканировать".

                if (btnScan.Text == "Сканировать")
                {
                    // Устанавливаем текст кнопки в значение "Остановить".

                    btnScan.Text = "Остановить";

                    // Если объект управления токеном существует в данном контексте. 

                    if (_cts != null)

                        // Удаляем текущий объект управления токеном.

                        _cts.Dispose();

                    // Инициализируем новый объект управления токеном.

                    _cts = new CancellationTokenSource();

                    // Объявляем переменную для хранения начального IP-адреса диапазона сканирования.

                    IPAddress firstIpAddress;

                    // Объявляем переменную для хранения финального IP-адреса диапазона сканирования.

                    IPAddress lastIpAddress;

                    // Анализируем список диапазонов IP-адресов

                    foreach (ListViewItem lvi in lvScanRange.Items)
                    {
                        // Сбрасываем значение индикатора выполнения операции.

                        progressBar1.Value = 0;

                        // Делаем индикатор выполнения операции видимым.

                        progressBar1.Visible = true;

                        // Объявляем и инициализируем экземпляр класса AmsNodes
                        // для хранения информации об активных узлах сети.

                        AmsNodes nodesList = new AmsNodes();

                        // Передаём управление индикатором выполнения операции.

                        nodesList.pb = progressBar1;

                        // Получаем значение начального адреса из первого столбца ListView.

                        firstIpAddress = IPAddress.Parse(lvi.SubItems[0].Text);

                        // Получаем значение финального адреса из третьего столбца ListView.

                        lastIpAddress = IPAddress.Parse(lvi.SubItems[2].Text);

                        // Ищем активные устройства в диапазоне и сохраняем их в список внутри объекта AmsNodes.

                        await nodesList.AliveInRange(IPAddressesRange(firstIpAddress, lastIpAddress), _cts.Token);

                        // Анализируем список активных устройств.

                        foreach (AmsNode node in nodesList.Nodes)
                        {
                            // Объявляем и инициализируем элемент ListViewItem.

                            ListViewItem nodeLVI = new ListViewItem();

                            // Передаём в поле "IP-адрес" элемента ListViewItem
                            // значение IP-адреса активного устройства.

                            nodeLVI.Text = node.Ip;

                            // Если был обнаружен МАС-адрес устройства.

                            if (node.Mac.Length > 0)
                            {
                                // Передаём в поле "МАС-адрес" элемента ListViewItem
                                // значение МАС-адреса активного устройства.

                                nodeLVI.SubItems.Add(node.Mac);
                            }

                            // Если МАС-адрес устройства обнаружен не был.

                            else
                            {
                                // Передаём в поле "МАС-адрес" элемента ListViewItem пустую строку.

                                nodeLVI.SubItems.Add(_strEmpty);
                            }

                            // Если было обнаружено NetBIOS-имя узла

                            if (node.Name.Length > 0)
                            {
                                // Передаём в поле "Имя узла" элемента ListViewItem
                                // значение "Имя" активного устройства.

                                nodeLVI.SubItems.Add(node.Name);
                            }

                            // Если NetBIOS-имя узла обнаружено не было.

                            else
                            {
                                // Передаём в поле "Имя узла" элемента ListViewItem пустую строку.

                                nodeLVI.SubItems.Add(_strEmpty);
                            }

                            // Передаём в поле "Сервисы" элемента ListViewItem пустую строку.

                            nodeLVI.SubItems.Add(_strEmpty);

                            // Передаём в поле "Тип узла" элемента ListViewItem пустую строку.

                            nodeLVI.SubItems.Add(_strEmpty);

                            // Передаём в поле "Стандарт передачи данных" элемента ListViewItem пустую строку.

                            nodeLVI.SubItems.Add(_strEmpty);       

                            // Передаём в поле "Протокол передачи данных" элемента ListViewItem пустую строку.

                            nodeLVI.SubItems.Add(_strEmpty);

                            // Если было обнаружено NetBIOS-имя узла

                            if (node.Name.Length > 0)
                            {
                                // Передаём в поле "Имя узла на карте" элемента ListViewItem
                                // значение "Имя" активного устройства.

                                nodeLVI.SubItems.Add(node.Name);
                            }

                            // Если NetBIOS-имя узла обнаружено не было.

                            else
                            {
                                // Передаём в поле "Имя узла на карте" элемента ListViewItem
                                // значение IP-адреса активного устройства.

                                nodeLVI.SubItems.Add(node.Ip);
                            }

                            // Если устройства с текущим IP-адресом ещё нет в списке, то добавляем.

                            // Переменная отображающая статус наличия IP-адреса в списке .

                            bool ipInList = false;

                            // Анализируем элементы ListViewItem копонента формы ListView.

                            foreach (ListViewItem item in lvScanResult.Items)
                            {
                                // Если поле "IP-адрес" совпадает с IP-адресом текущего активного устройства.

                                if (item.Text == node.Ip)
                                {

                                    // IP-адрес уже в списке.

                                    ipInList = true;

                                    // Переходим к следующему.

                                    break;
                                }
                            }

                            // IP-адрес не в списке.

                            if (!ipInList)
                            {
                                lvScanResult.Items.Add(nodeLVI);
                            }

                        }

                        // Устанавливаем текст кнопки в значение "Сканировать".

                        btnScan.Text = "Сканировать";

                        // Сбрасываем значение индикатора выполнения операции.

                        progressBar1.Value = 0;

                        // Скрываем индикатор выполнения операции.

                        progressBar1.Visible = false;
                    }
                }

                // Если текст кнопки отличается от "Сканировать".

                else
                {
                    // Устанавливаем текст кнопки в значение "Сканировать".

                    btnScan.Text = "Сканировать";

                    // Скрываем индикатор выполнения операции.

                    progressBar1.Visible = false;

                    // Передаём запрос на отмену операции.

                    _cts.Cancel();
                }
            }
        }
 
        /// <summary>
        /// Кнопка "Редактировать узел"
        /// </summary>
        private void button8_Click(object sender, EventArgs e)
        {
            // Если в списке "Активные IP-адреса" есть выделенные узлы.

            if (lvScanResult.SelectedItems.Count != 0)
            {
                // Создаём экземпляр формы "Редактирование узла".

                EditNodeCM editNode = new EditNodeCM()
                {
                    // Передаём управление компонентом ListView "Активные IP-адреса". 

                    lv = lvScanResult,

                    // Передаём управление выбранным элементом на этом компоненте формы.

                    lvi = lvScanResult.SelectedItems[0]                    
                };

                // Открываем форму в формате диалогового окна.

                editNode.ShowDialog();
            }
        }
    }
}
