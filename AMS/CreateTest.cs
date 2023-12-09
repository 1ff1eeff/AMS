using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace AMS
{
    public partial class CreateTest : Form
    {
        // Для связи со списком, содержащем информацию о выбранных узлах.

        public List<AmsNode> selectedNodes = new List<AmsNode>();

        // Для связи с компонентом ListView.

        public System.Windows.Forms.ListView lvMonitoringNodes = new System.Windows.Forms.ListView();

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public CreateTest()
        {
            // Инициализация компонентов.

            InitializeComponent();
        }

        /// <summary>
        /// Кнопка "ОК".
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Если список содержит информацию.

            if (selectedNodes.Count > 0)
            {
                // Анализируем список.

                foreach (AmsNode selectedNode in selectedNodes)
                {
                    // Объявляем и инициализируем массив строк для хранения информации о параметрах узла.

                    string[] nodeStatus = new string[] { "", "", "", "", "", "", "" };

                    // Первый элемент массива – имя узла.

                    nodeStatus[0] = selectedNode.Name;

                    // Второй элемент массива – IP-адрес узла.

                    nodeStatus[1] = selectedNode.Ip;

                    // Параметр "доступность" не выбран для отслеживания.

                    if (!checkBox1.Checked)
                    {
                        // Третий элемент массива: " - ".

                        nodeStatus[2] = " - ";
                    }

                    // Параметр "время отклика" не выбран для отслеживания.

                    if (!checkBox2.Checked)
                    {
                        // Четвёртый элемент массива: " - ".

                        nodeStatus[3] = " - ";
                    }

                    // Параметр "процент потери пакетов" не выбран для отслеживания.

                    if (!checkBox3.Checked)
                    {
                        // Пятый элемент массива: " - ".

                        nodeStatus[4] = " - ";
                    }

                    // Если указаны сервисы для мониторинга.

                    if (selectedNode.Services != null)
                    {
                        // Анализируем список.

                        foreach (string service in selectedNode.Services)
                        {
                            // Если поле "службы" содержит записи.

                            if (service.Length > 0)
                            {
                                // Добавляем службу в шестой элемент массива.

                                nodeStatus[5] += service + ";";
                            }
                        }
                    }

                    // Параметр "работоспособность сервисов" не выбран для отслеживания.

                    if (!checkBox4.Checked)
                    {
                        // Шестой элемент массива: " - ".

                        nodeStatus[5] = " - ";
                    }

                    // Если задан ID узла.

                    if (selectedNode.Id.Length > 0)
                    {
                        // Седьмой элемент массива

                        nodeStatus[6] = selectedNode.Id;
                    }

                    ListViewItem item = new ListViewItem(nodeStatus);

                    // Устройства с текущим ID ещё нет в списке.

                    bool idInList = false;

                    // Анализируем элементы компонента ListView, содержащем информацию о выбранных узлах.

                    foreach (ListViewItem lvi in lvMonitoringNodes.Items)
                    {
                        // Если ID задан и элемент с таким ID уже есть.

                        if (lvi.SubItems[6].Text.Length > 0 && lvi.SubItems[6].Text == selectedNode.Id)
                        {
                            // Устройство с текущим ID есть в списке.

                            idInList = true;

                            // Переходим к следующему элементу.

                            break;
                        }
                    }

                    // Если устройства с текущим ID нет в списке.

                    if (!idInList)
                    {
                        // Добавляем в ListView созданный элемент.

                        lvMonitoringNodes.Items.Add(item);
                    }
                }
            }

            // Закрываем форму.

            Close();
        }

        /// <summary>
        /// Кнопка "Отмена"
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Закрываем форму.

            Close();
        }

        /// <summary>
        /// Событие – "Форма загружена".
        /// </summary>
        private void CreateTest_Load(object sender, EventArgs e)
        {
            // Если список "selectedNodes" содержит элементы.

            if (selectedNodes.Count != 0)
            {
                // Анализируем список, содержащий информацию о выбранных узлах.

                foreach (AmsNode selectedNode in selectedNodes)
                {
                    // Добавляем имя узла и его IP-адрес, как новый элемент в компонент формы "Выбранные узлы".

                    listBox1.Items.Add(selectedNode.Name + " " + selectedNode.Ip);
                }
            }
        }        
    }
}
