using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AMS
{
    /// <summary>
    /// Форма "Создание нового узла".
    /// </summary>
    public partial class CreateNode : Form
    {
        // Для связи с главной формой
        public TabControl tc;

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public CreateNode()
        {
            // Инициализация компонентов.

            InitializeComponent();
        }

        /// <summary>
        /// Получает имена запущенных служб.
        /// </summary>
        /// <param name="listBox">Содержит имена запущенных служб.</param>
        /// <returns>Массив строк, содержащий имена запущенных служб.</returns>
        private static string[] GetSrvices(ListBox listBox)
        {
            // Объявляем и инициализируем массив строк.

            string[] myText = new string[listBox.Items.Count];

            // Анализируем список запущенных служб.

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                // Добавляем службу в массив строк.

                myText[i] = listBox.Items[i].ToString();
            }

            // Покидаем функцию и возвращаем массив строк, содержащий имена запущенных служб.

            return myText;
        }

        /// <summary>
        /// Список элементов.
        /// Событие "Двойное нажатие кнопки мыши".
        /// </summary>
        private void listBox1_DoubleClick(object sender, MouseEventArgs e)
        {
            // Добавляем к полю для ввода "NetBIOS имя узла"
            // выбранный тип узла в скобках.

            tbName.Text += " (" + lbType.GetItemText(lbType.SelectedItem) + ")";
        }

        /// <summary>
        /// Кнопка "Добавить".
        /// </summary>
        private void button11_Click(object sender, EventArgs e)
        {
            // Создаём экземпляр формы "Добавить службу".

            CreateService createService = new CreateService();

            // Передаём управление элементом ListBox.

            createService.lbService = lbServices;

            // Открываем форму в формате диалогового окна.

            createService.ShowDialog();
        }

        /// <summary>
        /// Находит свободное место под новый узел на вкладке TabControl.
        /// </summary>
        /// <param name="tc">TabControl для размещения узла.</param>

        Point FindSpace(TabControl tc)
        {

            // Объявляем и инициализируем объект класса Point для хранения позиции создаваемого узла.

            Point pos = new Point(10, 10);

            // Объявляем и инициализируем переменную для хранения значения размера разделителя.

            int spacer = 10;

            // Получаем список всех узлов размещённых на вкладке карты.

            foreach (DeviceNode node in tc.SelectedTab.Controls.OfType<DeviceNode>())
            {
                // Если узел существует.

                if (node != null)
                {
                    // Если текущий узел ниже создаваемого.

                    if (node.Location.Y > pos.Y)
                    {
                        // Перемещаем новый узел вниз на значение высоты узла плюс значение разделителя.

                        pos.Y += node.Height + spacer;
                    }

                    // Если текущий узел правее создаваемого.

                    if (node.Location.X >= pos.X)
                    {
                        // Сдвигаем новый узел вправо на значение ширины узла плюс значение разделителя.

                        pos.X = node.Location.X + node.Size.Width + spacer;

                        // Если на вкладке не хватает места для нового узла.

                        if (pos.X >= tc.Width - (node.Size.Width + spacer))
                        {
                            // Сбрасываем положение по ширине.

                            pos.X = spacer;

                            // Перемещаем новый узел вниз на значение высоты узла плюс значение разделителя.

                            pos.Y += node.Height + spacer;
                        }
                    }
                }
            }

            // Покидаем функцию и возвращаем значение 

            return pos;
        }

        // Кнопка "ОК".
        private void button7_Click(object sender, EventArgs e)
        {
            // Объявляем и инициализируем объект класса AmsNode,
            // содержащий информацию об узле сети.

            AmsNode node = new AmsNode()
            {
                // Определяем поле "Уникальный идентификатор узла" как новый экземпляр структуры Guid.

                Id = Guid.NewGuid().ToString(),

                // Определяем поле "IP-адрес узла" значением поля для ввода "IP-адрес".

                Ip = tbIp.Text,

                // Определяем поле "MAC-адрес узла" значением поля для ввода "MAC-адрес".

                Mac = tbMac.Text,

                // Определяем поле "Имя узла" значением поля для ввода "NetBIOS имя узла".

                Name = tbName.Text,

                // Определяем поле "Службы" значением поля для ввода "Службы".

                Services = GetSrvices(lbServices),

                // Определяем поле "Тип узла" значением поля для ввода "Тип устройства".

                Type = lbType.Text,

                // Определяем поле "Стандарт передачи данных" значением поля для ввода "Стандарт передачи данных".

                Standard = cbStandard.Text,

                // Определяем поле "Протокол передачи данных" значением поля для ввода "Протокол передачи данных".

                Protocol = cbProtocol.Text,

                // Определяем поле "Имя узла на карте" значением поля для ввода "Имя узла на карте".

                NameOnMap = tbNameOnMap.Text                
            };


            // Подготавливаем пользовательский компонент, представляющий узел.

            DeviceNode dn = new DeviceNode
            {
                // Определяем положение на карте.

                Location = FindSpace(tc),

                // Передаём информацию об узле.

                DNode = node
            };

            // Добавляем новый элемент на карту.

            tc.TabPages[tc.SelectedIndex].Controls.Add(dn);
            
            // Закрываем форму.

            Close();
        }

        // Кнопка "Отмена".
        private void button8_Click(object sender, EventArgs e)
        {
            // Закрываем форму.

            Close();
        }

        // Удалить сервис из списка
        private void button10_Click(object sender, EventArgs e)
        {
            // Если в компоненте, отображающем запущенные службы
            // добавлен и выделен хотя бы один элемент. 

            if (lbServices.Items.Count > 0 && lbServices.SelectedIndex >= 0)
            {
                // Удаляем выделенный элемент.

                lbServices.Items.RemoveAt(lbServices.SelectedIndex);
            }
        }
    }
}
