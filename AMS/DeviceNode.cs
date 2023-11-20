using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMS
{
    public partial class DeviceNode : UserControl
    {
        // Экземпляр dNodeInfo
        public DNodeInfo dNodeInfo = new DNodeInfo();
        // Точка перемещения
        Point DownPoint;
        // Нажата ли кнопка мыши
        bool IsDragMode;

        public DeviceNode()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                DownPoint = mevent.Location;
                IsDragMode = true;
                base.OnMouseDown(mevent);
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            IsDragMode = false;
            base.OnMouseUp(mevent);
        }

        // Перемещение элемента по форме
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            // Если кнопка мыши нажата
            if (IsDragMode)
            {
                Point p = mevent.Location;
                // Вычисляем разницу в координатах между положением курсора и "нулевой" точкой кнопки
                Point dp = new Point(p.X - DownPoint.X, p.Y - DownPoint.Y);
                Location = new Point(Location.X + dp.X, Location.Y + dp.Y);
            }
            base.OnMouseMove(mevent);
        }

        // Удалить с формы
        private void button1_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }

        private void DeviceNode_Load(object sender, EventArgs e)
        {
            label1.Text = dNodeInfo.Name;

            // Текст всплывающей подсказки

            string tip = "";

            if (!String.IsNullOrEmpty(dNodeInfo.Name))
                tip += "Имя узла: " + dNodeInfo.Name + "\n";

            if (dNodeInfo.Ip.Length != 0)
            {
                tip += "IP-адрес: ";
                tip += dNodeInfo.Ip + " \n";
            }

            if (dNodeInfo.Mac.Length != 0)
            {
                tip += "MAC-адрес: ";
                tip += dNodeInfo.Mac + " \n";
            }

            if (!String.IsNullOrEmpty(dNodeInfo.Type))
                tip += "Тип устройства: " + dNodeInfo.Type + "\n";

            if (!String.IsNullOrEmpty(dNodeInfo.Standart))
                tip += "Стандарт передачи: " + dNodeInfo.Standart + "\n";

            if (!String.IsNullOrEmpty(dNodeInfo.Protocol))
                tip += "Протокол передачи: " + dNodeInfo.Protocol + "\n";

            if (dNodeInfo.Services.Length != 0)
            {
                tip += "Сервисы: ";
                foreach (string s in dNodeInfo.Services)
                    tip += s + " ";
                tip += "\n";
            }

            toolTip1.SetToolTip(this, tip);
        }

        private void DeviceNode_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:

                    if (BorderStyle == BorderStyle.None) BorderStyle = BorderStyle.FixedSingle;
                    else BorderStyle = BorderStyle.None;

                    if (dNodeInfo.IsSelected) dNodeInfo.IsSelected = !dNodeInfo.IsSelected;
                    else dNodeInfo.IsSelected = !dNodeInfo.IsSelected;

                    break;
            }
        }
    }
}
