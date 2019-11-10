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

        //Establishes the appropriate flow of each panel.
        private void panelSetup()
        {
            panel_TitleScreen.Visible = true;
            panel_SignUp.Visible = false;
            //panel_Game.Visible = false;   //Create this
            //panel_Admin.Visible = false; //Create this
        }

        /*BUTTONS FOR TITLE PAGE. PURELY FOR UX. **************************************************************************/

        //Controls traffic of panels when a new user comes.
        private void button_signUp_Click(object sender, EventArgs e)
        {
            panel_TitleScreen.Visible = false;
            panel_SignUp.Visible = true;
        }

        //Gateway to game
        private void button_login_Click(object sender, EventArgs e)
        {
            //first check: is User and Password filled
            if(textBox_usernameEntry.Text == "" || textBox_passwordEntry.Text == "")
            {
                MessageBox.Show("You are missing either your username or password.\n" +
                    "please enter both your username and password.");
            }
            //Condition to check text files to see if user is a user.
            //Condition to check text files to see if user matches appropriate password
            else
            {
                //panel_Game.Visible = true;
                panel_TitleScreen.Visible = false; 
            }
        }

        /******************************************************************************************************************/

        // BUTTONS FOR THE SIGN UP PAGE. PURELY FOR UX.*********************************************************************

        //Checks to see if all forms are filled before adding user
        private void button_signUp_finish_Click(object sender, EventArgs e)
        {
            //If any entries are not filled in, then do not continue. Tell user to go back and edit.
            if(textBox_signUp_Username.Text=="" 
                || textBox_signUp_Password.Text=="" 
                || dateTimePicker_SignUp_DOB.Text==null
                || textBox_signUp_City.Text==""
                || textBox_signUp_State.Text==""
                || textBox_signUp_County.Text==""
                || textBox_signUp_Diagnosis.Text==""
                )
            {
                MessageBox.Show("You are missing values! Please fill them out. Write N/A if you are not sure.");
            }
            else
            {
                MessageBox.Show("You have been added! Sign In Through the Title Page to start!");
                //CREATE PLAYER METHOD.
                //PLAYER CLASS FUNCTION THAT ADDS USER TO TXT FILES.
                panel_SignUp.Visible = false;
                panel_TitleScreen.Visible = true;
            }
        }

        //Allows user to go back to title screen without creating a new profile. Wipes any entry data thus far.
        private void button_signUp_back_Click(object sender, EventArgs e)
        {
            panel_TitleScreen.Visible = true;
            panel_SignUp.Visible = false;
            textBox_signUp_Username.Text = "";
            textBox_signUp_Password.Text = "";
            textBox_signUp_County.Text = "";
            textBox_signUp_State.Text = "";
            textBox_signUp_City.Text = "";
            textBox_signUp_Diagnosis.Text = "";
        }

        //******************************************************************************************************************
    }
}
