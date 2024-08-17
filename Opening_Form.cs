using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs
{
    public partial class Opening_Form : Form
    {
        public Opening_Form()
        {
            InitializeComponent();
        }

        private void Opening_Form_Load(object sender, EventArgs e)
        {
            Simplex_choice.Checked = true;
        }

        private void Simplex_start_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Блокируем кнопку при закрытии SecondaryForm
            go_button.Enabled = true;
        }
        private void go_button_Click(object sender, EventArgs e)
        {
            if(Simplex_choice.Checked)
            {
                go_button.Enabled = false;
                Simplex_start simplexStart = new Simplex_start();
                simplexStart.FormClosed += Simplex_start_FormClosed;
                simplexStart.Show(this);
            }
        }
    }
}
