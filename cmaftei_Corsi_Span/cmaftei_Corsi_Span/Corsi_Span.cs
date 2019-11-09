using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cmaftei_Corsi_Span
{
    public partial class Corsi_Span : Form
    {
        public Corsi_Span()
        {
            InitializeComponent();
            panelSetup();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            panel_TitleScreen.Visible = false;
            panel_SignUp.Visible = true;
        }

        private void panelSetup()
        {
            panel_TitleScreen.Visible = true;
            panel_SignUp.Visible = false;
        }
    }
}
