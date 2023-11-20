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
    public partial class CreateService : Form
    {
        public ListBox lbService;

        public CreateService()
        {
            InitializeComponent();
        }

        // ОК
        private void button7_Click(object sender, EventArgs e)
        {
            lbService.Items.Add(textBox1.Text);
            Close();
        }

        // Отмена
        private void button8_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Нажат Enter
            if (e.KeyCode == Keys.Enter) button7_Click(sender, e);

            // Нажат ESC
            else if (e.KeyCode == Keys.Escape) button8_Click(sender, e);
        }
    }
}
