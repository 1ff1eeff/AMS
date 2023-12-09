using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AMS
{
    
    public partial class SelectProcess : Form
    {
        // Для связи со списком, предназначенным для хранения процессов.

        public List<string> detectedProcesses;

        // Для связи с элементом ListBox.

        public ListBox lb;

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public SelectProcess()
        {
            // Инициализация компонентов.

            InitializeComponent();
        }

        /// <summary>
        /// Событие – "Форма загружена".
        /// </summary>
        private void SelectProcess_Load(object sender, EventArgs e)
        {
            // Анализируем список процессов.

            foreach (string process in detectedProcesses)
            {
                // Добавляем в ListBox на форме новый элемент, содержащий имя процесса.

                listBox1.Items.Add(process);
            }
        }

        /// <summary>
        /// Кнопка "ОК".
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Анализируем элементы копмонента ListBox "Запущенные процессы".

            foreach (string item in listBox1.SelectedItems)
            {
                // Передаём каждый элемент в ListBox указанный при инициализации формы.

                lb.Items.Add(item);
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
    }
}
