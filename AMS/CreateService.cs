using System;
using System.Windows.Forms;

namespace AMS
{
    public partial class CreateService : Form
    {
        // Для связи с элементом ListBox.

        public ListBox lbService;

        /// <summary>
        /// Стандартный конструктор.
        /// </summary>
        public CreateService()
        {
            // Инициализация компонентов.

            InitializeComponent();
        }

        /// <summary>
        /// Кнопка "ОК".
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Добавляем в ListBox текстовое значение поля для ввода, содержащее имя службы.

            lbService.Items.Add(textBox1.Text);

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
