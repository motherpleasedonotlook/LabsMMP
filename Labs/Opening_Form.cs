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

        private void Lower_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Блокируем кнопку при закрытии
            go_button.Enabled = true;
        }
        private void go_button_Click(object sender, EventArgs e)
        {
            if(Simplex_choice.Checked)
            {
                go_button.Enabled = false;
                Simplex_start simplexStart = new Simplex_start("ordinary");
                simplexStart.FormClosed += Lower_FormClosed;
                simplexStart.Show(this);
            }
            if (Mmethod_choice.Checked)
            {
                go_button.Enabled = false;
                Mmethod_start mmethodStart = new Mmethod_start();
                mmethodStart.FormClosed += Lower_FormClosed;
                mmethodStart.Show(this);
            }
            if (Double_simplex_choice.Checked)
            {
                go_button.Enabled = false;
                Simplex_start simplexStart = new Simplex_start("double");
                simplexStart.FormClosed += Lower_FormClosed;
                simplexStart.Show(this);
            }
        }
    }
}
