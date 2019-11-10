using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cmaftei_Corsi_Span
{
    public partial class Corsi_Span : Form
    {
        string currentUser;
        int currentUserScore;

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
            panel_Game.Visible = false;
            //panel_Admin.Visible = false; //Create this
        }

        /*BUTTONS FOR TITLE PAGE. PURELY FOR UX. **************************************************************************/

        //Controls traffic of panels when a new user comes.
        private void button_signUp_Click(object sender, EventArgs e)
        {
            panel_TitleScreen.Visible = false;
            panel_SignUp.Visible = true;
        }

        //Gateway to game (NEEDS VALIDATION OR USER AND PASSWORD)
        private void button_login_Click(object sender, EventArgs e)
        {
            //first check: is User and Password filled
            if(textBox_usernameEntry.Text == "" || textBox_passwordEntry.Text == "")
            {
                MessageBox.Show("You are missing either your username or password.\n" +
                    "please enter both your username and password.");
            }
            //second check: user credentials match what is in DB
            else if(CheckForValidLogin(textBox_usernameEntry.Text, textBox_passwordEntry.Text))
            {
                MessageBox.Show("Invalid username/password.\n" +
                    "please enter a different username/password. Press the Sign Up\n" +
                    "Button to create a new account.");
            }
            //Passed! Prepare Game State
            else
            {
                currentUser = textBox_usernameEntry.Text;
                label_game_currentPlayer.Text = "Current Player: " + currentUser.ToUpper();
                label_game_mode.Text = "Game Mode: NORMAL";
                panel_Game.Visible = true;
                panel_TitleScreen.Visible = false; 
            }
        }

        /******************************************************************************************************************/

        /*BUTTONS FOR THE SIGN UP PAGE. PURELY FOR UX.*********************************************************************/

        //Checks to see if all forms are filled before adding user
        private void button_signUp_finish_Click(object sender, EventArgs e)
        {
            //If any entries are not filled in, then do not continue. Tell user to go back and edit.
            if (textBox_signUp_Username.Text == ""
                || textBox_signUp_Password.Text == ""
                || dateTimePicker_SignUp_DOB.Value == null
                || textBox_signUp_City.Text == ""
                || textBox_signUp_State.Text == ""
                || textBox_signUp_County.Text == ""
                || textBox_signUp_Diagnosis.Text == ""
                )
            {
                MessageBox.Show("You are missing values! Please fill them out. Write N/A if you are not sure.");
            }
            else
            {
                //create player
                Player player = new Player(
                    textBox_signUp_Username.Text.ToLower(),
                    textBox_signUp_Password.Text,
                    dateTimePicker_SignUp_DOB.Value,
                    textBox_signUp_City.Text.ToLower(),
                    textBox_signUp_State.Text.ToLower(),
                    textBox_signUp_County.Text.ToLower(),
                    textBox_signUp_Diagnosis.Text.ToLower());

                //save player info in textfiles if that user doesn't already exists.
                if (player.CheckForRedundnantUser())
                {
                    MessageBox.Show("That user already exists. Please try another username/ password.");
                    textBox_signUp_Username.Text = "";
                    textBox_signUp_Password.Text = "";
                }
                else
                {
                    MessageBox.Show("You have been added! Sign In Through the Title Page to start!");
                    player.SaveUserData();

                    //places user at beginning page, and clears out entry in case new user wants to sign up as well.
                    //Possible Method for refactoring purposes.
                    panel_SignUp.Visible = false;
                    panel_TitleScreen.Visible = true;
                    textBox_signUp_Username.Text = "";
                    textBox_signUp_Password.Text = "";
                    textBox_signUp_County.Text = "";
                    textBox_signUp_State.Text = "";
                    textBox_signUp_City.Text = "";
                    textBox_signUp_Diagnosis.Text = "";
                }
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

        //false means a user already exists so no flag is needed, true means user is not found in DB, therefore flag is true.
        private bool CheckForValidLogin(string user, string pass)
        {
            string playerLoginInfoQuestion = String.Format("{0},{1}", user.ToLower(), pass);

            using (StreamReader sr = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + 
                @"playerInfo/user_loginInfo/login_Info.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == playerLoginInfoQuestion) //login matches something in the DB. Login is good.
                    {
                        return false;
                    }
                }
            }
            return true; //if login doesn't match anything in DB, then failed attempt.
        }
    }
}
