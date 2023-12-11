using System;
using System.Drawing;
using System.Windows.Forms;

namespace AMS
{
    /// <summary>
    /// Пользовательский элемент управления, представляющий узел сети на карте.
    /// </summary>
    public partial class DeviceNode : UserControl
    {
        // Объявляем и инициализируем объект класса AmsNode для хранения информации об узле.

        private AmsNode _dNode = new AmsNode();

        // Объявляем объект класса Point для хранения координат узла на карте.

        private Point _dNodeLoc; // 

        // Определяем аксессоры – методы доступа к свойству "_dNode".
        public AmsNode DNode { get => _dNode; set => _dNode = value; }

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public DeviceNode()
        {
            // Инициализация компонентов.

            InitializeComponent();
        }

        // Кнопка "X".
        private void button1_Click(object sender, EventArgs e)
        {
            // Удаляем элемент управления с формы.

            Parent.Controls.Remove(this);
        }

        /// <summary>
        /// Задаёт подпись узла на карте.
        /// </summary>
        /// <param name="name">Имя узла.</param>
        public void LbSetNameOnMap (string name)
        {
            // Задаём подпись узла на карте.

            lbName.Text = name; 
        }

        /// <summary>
        /// Формирует текст содержащий информацию об устройстве.
        /// </summary>
        /// <returns>Текст подсказки.</returns>
        private string DeviceInfoTip()
        {
            // Объявляем и инициализируем строковую переменную, содержащую информацию об устройстве.

            string tip = "";

            // Если поле "Имя узла" содержит текст.

            if (DNode.Name.Length > 0)
            {
                // Формируем строку, содержащую информацию об имени узла.

                tip += "NetBIOS имя узла: " + DNode.Name + "\n";
            }

            // Если поле "Имя узла на карте" содержит текст.

            if (DNode.NameOnMap.Length > 0)
            {
                // Формируем строку, содержащую информацию об имени узла на карте.

                tip += "Имя узла на карте: " + DNode.NameOnMap + "\n";
            }

            // Если поле "IP-адрес" содержит текст.

            if (DNode.Ip.Length > 0)
            {
                // Формируем строку, содержащую информацию об IP-адресе.

                tip += "IP-адрес: " + DNode.Ip + " \n";
            }

            // Если поле "MAC - адрес" содержит текст.

            if (DNode.Mac.Length > 0)
            {
                // Формируем строку, содержащую информацию о MAC-адресе.

                tip += "MAC-адрес: " + DNode.Mac + " \n";
            }

            // Если поле "Тип устройства" содержит текст.

            if (DNode.Type.Length > 0)
            {
                // Формируем строку, содержащую информацию о типе устройства.

                tip += "Тип устройства: " + DNode.Type + "\n";
            }

            // Если поле "Стандарт передачи" содержит текст.

            if (DNode.Standard.Length > 0)
            {
                // Формируем строку, содержащую информацию о стандарте передачи.

                tip += "Стандарт передачи: " + DNode.Standard + "\n";
            }

            // Если поле "Протокол передачи" содержит текст.

            if (DNode.Protocol.Length > 0)
            {
                // Формируем строку, содержащую информацию о протоколе передачи.

                tip += "Протокол передачи: " + DNode.Protocol + "\n";
            }

            // Если поле "Службы" содержит текст.

            if (DNode.Services.Length > 0)
            {
                // Формируем строку, содержащую информацию о службах.

                tip += "Службы: ";

                // Анализируем поле "Службы".

                foreach (string s in DNode.Services)
                {
                    // Добавляем текущую службу к строке.

                    tip += s + " ";
                }

                // Добавляем символ перехода на новую строку.

                tip += "\n";
            }

            // Покидаем функцию и передаём строковую переменную, содержащую информацию об устройстве.

            return tip;
        }

        /// <summary>
        /// Событие – "Форма загружена".
        /// </summary>
        private void DeviceNode_Load(object sender, EventArgs e)
        {
            // Задаём подпись узла на карте.

            LbSetNameOnMap(DNode.NameOnMap);
        }

        /// <summary>
        /// Событие – "Двойное нажатие кнопки мыши на форме".
        /// </summary>
        private void DeviceNode_DoubleClick(object sender, EventArgs e)
        {
            // Отображаем информацию об узле на карте, посредством диалогового окна.

            MessageBox.Show(DeviceInfoTip(), DNode.NameOnMap);
        }

        // Объявляем логическую переменную для хранения состояния режима перемещения.

        private bool _isDragMode;

        // Объявляем логическую переменную для хранения состояния
        // блокирки режима редактирования стиля границ элемента управления.

        private bool _bsLock = false;

        /// <summary>
        /// Переопределение события – "Кнопка мыши нажата".
        /// </summary>
        /// <param name="mevent">Данные для события нажатия кнопки мыши.</param>
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            // Если нажата левая кнопка мыши.

            if (mevent.Button == MouseButtons.Left)
            {
                // Устанавливаем координаты элемента управления,
                // равными положению указателя мыши в момент создания события.

                _dNodeLoc = mevent.Location;

                // Активируем режим перемещения элемента управления.

                _isDragMode = true;

                // Вызываем событие Control.MouseDown базового класса.

                base.OnMouseDown(mevent);
            }
        }

        /// <summary>
        /// Переопределение события – "Изменение положения курсора мыши".
        /// </summary>
        /// <param name="mevent">Данные для события перемещения мыши.</param>
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            // Если активирован режим перемещения элемента управления.

            if (_isDragMode)
            {
                // Блокируем редактирование стиля границ элемента управления.

                _bsLock = true;

                // Объявляем объект класса Point для хранения промежуточных координат. 

                Point p = mevent.Location;

                // Вычисляем разницу в координатах между положением курсора и "нулевой" точкой элемента управления.
                
                Point dp = new Point(p.X - _dNodeLoc.X, p.Y - _dNodeLoc.Y);

                // Добавляем разницу к положению элемента управления.

                Location = new Point(Location.X + dp.X, Location.Y + dp.Y);
            }

            // Вызываем событие Control.MouseMove базового класса.

            base.OnMouseMove(mevent);
        }

        /// <summary>
        /// Переопределение события – "Кнопка мыши отпущена".
        /// </summary>
        /// <param name="mevent">Данные для события отпускания кнопки мыши.</param>
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            // Если нажата левая кнопка мыши и разрешено редактирование стиля границ элемента управления.

            if (mevent.Button == MouseButtons.Left && !_bsLock)
            {
                // Если стиль границы элемента управления – "Нет границы".

                if (BorderStyle == BorderStyle.None)
                {
                    // Устанавливаем стиль границы элемента управления в значение "Одинарная граница"

                    BorderStyle = BorderStyle.FixedSingle;
                }

                // Если стиль границы элемента управления не "Нет границы".

                else
                {
                    // Сохраняем состояние рамки выделения элемента управления.

                    BorderStyle = BorderStyle.None;
                }

                // Если узел выделен на карте.

                if (DNode.IsSelected)
                {
                    // Снимаем выделение с узла.

                    DNode.IsSelected = !DNode.IsSelected;
                }

                // Если узел не выделен на карте.

                else
                {
                    // Выделяем узел.

                    DNode.IsSelected = !DNode.IsSelected;
                }
            }

            // Если нажата правая кнопка мыши и разрешено редактирование стиля границ элемента управления. 

            if (mevent.Button == MouseButtons.Right && !_bsLock)
            {
                // Отображаем информацию об узле на карте, посредством диалогового окна.

                MessageBox.Show(DeviceInfoTip(), DNode.NameOnMap);
            }

            // Деактивируем режим перемещения элемента управления.

            _isDragMode = false;

            // Снимаем блокировку редактирования стиля границ элемента управления.

            _bsLock = false;

            // Вызываем событие Control.MouseUp базового класса.

            base.OnMouseUp(mevent);
        }
    }
}
