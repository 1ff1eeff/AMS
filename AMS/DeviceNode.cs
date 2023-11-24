using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMS
{
    public partial class DeviceNode : UserControl
    {

        private DNode dNode = new DNode(); // Экземпляр DNode

        Point DownPoint; // Точка перемещения

        bool IsDragMode; // Нажата ли кнопка мыши

        public DNode DNode { get => dNode; set => dNode = value; }

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

        public string DeviceInfoTip()
        {
            // Текст всплывающей подсказки

            string tip = "";

            //// ID

            //tip += "ID: " + DNode.Id + "\n";

            // Имя узла

            if (DNode.Name.Length > 0)
                tip += "NetBIOS имя узла: " + DNode.Name + "\n";

            // Имя узла на карте

            if (DNode.Name.Length > 0)
                tip += "Имя узла на карте: " + DNode.NameOnMap + "\n";

            // IP-адрес

            if (DNode.Ip.Length > 0)
            {
                tip += "IP-адрес: ";
                tip += DNode.Ip + " \n";
            }

            // MAC - адрес

            if (DNode.Mac.Length > 0)
            {
                tip += "MAC-адрес: ";
                tip += DNode.Mac + " \n";
            }

            // Тип устройства

            if (DNode.Type.Length > 0)
                tip += "Тип устройства: " + DNode.Type + "\n";

            // Стандарт передачи

            if (DNode.Standard.Length > 0)
                tip += "Стандарт передачи: " + DNode.Standard + "\n";

            // Протокол передачи

            if (DNode.Protocol.Length > 0)
                tip += "Протокол передачи: " + DNode.Protocol + "\n";

            // Сервисы

            if (DNode.Services.Length > 0)
            {
                tip += "Сервисы: ";
                foreach (string s in DNode.Services)
                    tip += s + " ";
                tip += "\n";
            }

            return tip;
        }

        private void DeviceNode_Load(object sender, EventArgs e)
        {
            label1.Text = DNode.NameOnMap;
            
        }

        private void DeviceNode_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:

                    if (BorderStyle == BorderStyle.None) BorderStyle = BorderStyle.FixedSingle;
                    else BorderStyle = BorderStyle.None;

                    if (DNode.IsSelected) DNode.IsSelected = !DNode.IsSelected;
                    else DNode.IsSelected = !DNode.IsSelected;

                    break;
            }
        }

        private void DeviceNode_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(DeviceInfoTip(), DNode.NameOnMap);
        }
    }
}
