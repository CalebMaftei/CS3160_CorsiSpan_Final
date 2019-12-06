using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cmaftei_Corsi_Span
{
    public partial class Corsi_Span : Form
    {
        private List<Player> players = new List<Player>();
        private Block[] blocks = new Block[9];
        private Button[] blockButtons = new Button[9];
        private Scoreboard scoreBoard = new Scoreboard();
        private PlayerHistoryManager phm;
        private Sequence sequence = new Sequence();
        private Player activePlayer;
        private Admin admin;
        private Tracker tracker;
        private int currentLevel = 2;
        private string gameMode = "normal";
        private XOR_Encryption xorEncrypt = new XOR_Encryption();
        private const int encryptionKey = 307;

        public Corsi_Span()
        {
            InitializeComponent();
            PanelSetup();
            LoadBlocksWithButtons();
            LoadPlayers();
            LoadComboBox();

            //Gives buttons their respective event methods through delegated functions
            for (int i = 0; i < blockButtons.Count(); i++)
            {
                int x;
                x = i;
                //Every Click adds to the userSequence
                blockButtons[x].Click += (o, e) => sequence.addToUserSequence(blocks[x].GetBlockID());
                //Every Click should highlight the cube to let the user know that it was
                blockButtons[x].Click += (o, e) => ColorChange(blockButtons[x], blocks[x].GetBlockColor(), true);
                //When Mouse leaves block, it resets the highlight
                blockButtons[x].MouseLeave += (o, e) => ColorChange(blockButtons[x], blocks[x].GetBlockColor(), false);
            }

            admin = new Admin("admin", "password");
        }

        /// <summary>
        /// All Button actions in the following section are used exclusively for the title page.
        /// </summary>
        /*****************************************************************************************************************/

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
            else if(CheckForInValidLogin(textBox_usernameEntry.Text, textBox_passwordEntry.Text))
            {
                MessageBox.Show("Invalid username/password.\n" +
                    "please enter a different username/password. Press the Sign Up\n" +
                    "Button to create a new account.");
            }
            //Passed! Prepare Game State
            else
            {
                //Based on the valid user entry, finds it in the list of players.
                if(textBox_usernameEntry.Text.ToLower() == admin.GetUsername())
                {
                    panel_AdminPage.Visible = true;
                    //panel_TitleScreen.Visible = false;
                }
                else
                {
                    foreach (Player p in players)
                    {
                        if (textBox_usernameEntry.Text.ToLower() == p.GetUserName())
                        {
                            activePlayer = p;
                            tracker = new Tracker(activePlayer);
                            break;
                        }
                    }
                    label_game_currentPlayer.Text = "Current Player: " + activePlayer.GetUserName().ToUpper();
                    label_game_score.Text = "SCORE/LEVEL : " + currentLevel.ToString();
                    label_game_mode.Text = "Game Mode: " + gameMode.ToUpper();
                    textBox_usernameEntry.Text = "";
                    textBox_passwordEntry.Text = "";
                    panel_Game.Visible = true;
                    panel_TitleScreen.Visible = false;
                    tracker.LogStartMessage();
                }
                textBox_usernameEntry.Text = "";
                textBox_passwordEntry.Text = "";
            }
        }

        /******************************************************************************************************************/

        /// <summary>
        /// All Button actions in the following sections are used exclusively for the Sign Up page.
        /// </summary>
        /******************************************************************************************************************/

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
                    0,
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
                    players.Add(player);
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

        /******************************************************************************************************************/

        /// <summary>
        /// All Button Actions in the following section are used exclusively for the game page.
        /// </summary>
        /*BUTTONS FOR GAME PAGE *******************************************************************************************/
        
        //logs player out. Saves score value if score is higher then current bestScore. Brings user to main screen.
        private void button_game_logout_Click(object sender, EventArgs e)
        {
            phm = new PlayerHistoryManager(activePlayer.GetUserName());

            //On logout, checks if session should replace any values in user's scoreHistory. 
            activePlayer.SetScoreHistory(phm.UserHistoryUpdate(currentLevel, activePlayer.GetScoreHistory()));

            //Updates the file regardless if scores changed or not.
            activePlayer.UpdatePlayerInfo();

            //Sends high score from scoreHistory to user's best score. If high score is > best score,
            //best score is overwritten, and file is updated.
            activePlayer.SetBestScore(activePlayer.GetScoreHistory()[0]);

            MessageBox.Show("THANK YOU!");
            MessageBox.Show("\t\t\tYour new History is:\n" + phm.ProduceUserHistory(activePlayer.GetScoreHistory()));

            //Reset the level for the next player who logs in.
            currentLevel = 2;
            label_game_score.Text = "SCORE/LEVEL : " + currentLevel.ToString();
            panel_TitleScreen.Visible = true;
            panel_Game.Visible = false;

            tracker.LogEndMessage();
        }

        //Iterates through players to find highest score.
        private void button_game_checkScoreboard_Click(object sender, EventArgs e)
        {
            tracker.LogScoreBoardCheckMessage();
            if (currentLevel > activePlayer.GetBestScore())
            {
                activePlayer.SetBestScore(currentLevel);
            }
            scoreBoard.LoadScores(players);
            MessageBox.Show(String.Format("SCOREBOARD:\n{0}",scoreBoard.ToString())); //Maybe switch this is for a form of some sort to style scoreboard
        }

        //Shows User their Current Score, as well as their top 10 sessions. Updates do not happen here. That is when the player logs out.
        private void button_game_viewUserHistory_Click(object sender, EventArgs e)
        {
            tracker.LogUserHistoryCheckMessage();
            string display = String.Format(
                "===========================================\n" +
                "||\t\tCURRENT SESSION SCORE:     {0}\t\t||\n", currentLevel);
            //manages scores of player history
            phm = new PlayerHistoryManager(activePlayer.GetUserName());

            display += phm.ProduceUserHistory(activePlayer.GetScoreHistory());

            MessageBox.Show(display);
        }

        //Generates the sequence to prompt the user. Also decides the game mode.
        private void button_game_startRound_Click(object sender, EventArgs e)
        {
            tracker.LogSequenceStartMessage();

            MessageBox.Show("Starting the round! REMINDER: The Sequence is the cubes that go dark red! " +
                "Click them at the end of the sequnce!");
            //Disable all buttons to prevent disruption of sequence demonstration
            foreach(Button b in blockButtons)
            {
                b.Enabled = false;
            }

            //generateSequence
            sequence.GenerateSequence(currentLevel);
            
            //Generate and Play Sequence
            foreach (int blockNum in sequence.GetSystemSequence())
            {
                Task buttonFlash = Task.Factory.StartNew(() =>
                {
                    blockButtons[blockNum].BackColor = sequence.GetActiveBlockColor();
                    Thread.Sleep(1500);
                    blockButtons[blockNum].BackColor = blocks[blockNum].GetBlockColor();
                    Thread.Sleep(500);
                }
                );
                buttonFlash.Wait();
            }
            
            //Visibility for startRound_btn goes away
            button_game_startRound.Visible = false;
            
            //Visibility for checkSequence is true
            button_game_checkMySequence.Visible = true;
            
            //Let user know the sequence has finished
            MessageBox.Show("Now Your Turn! Match the Sequence.");
            
            //enable buttons now.
            foreach (Button b in blockButtons)
            {
                b.Enabled = true;
            }
        }

        //Checks the sequence that the user has created
        private void button_game_checkMySequence_Click(object sender, EventArgs e)
        {
            switch (gameMode)
            {
                case "reverse":
                    if (sequence.ReverseSequenceCheck())
                    {
                        MessageBox.Show("Congratulations! That is Correct!");
                        currentLevel++;
                        label_game_score.Text = "SCORE/LEVEL : " + currentLevel.ToString();
                        ModeGenerate();
                        label_game_mode.Text = "Game Mode: " + gameMode.ToUpper();

                        tracker.LogCorrectSequenceMessage(gameMode);
                    }
                    else
                    {
                        MessageBox.Show("Sorry! That is incorrect. Try Again!");

                        tracker.LogIncorrectSequenceMessage(gameMode);
                    }
                    break;
                case "normal":
                    if (sequence.sequenceCheck())
                    {
                        MessageBox.Show("Congratulations! That is Correct!");
                        currentLevel++;
                        label_game_score.Text = "SCORE/LEVEL : " + currentLevel.ToString();
                        ModeGenerate();
                        label_game_mode.Text = "Game Mode: " + gameMode.ToUpper();

                        tracker.LogCorrectSequenceMessage(gameMode);
                    }
                    else
                    {
                        MessageBox.Show("Sorry! That is incorrect. Try Again!");

                        tracker.LogIncorrectSequenceMessage(gameMode);
                    }
                    break;
            }

            //Reset Sequences & Buttons
            sequence.ResetSequences();
            button_game_checkMySequence.Visible = false;
            button_game_startRound.Visible = true;
        }

        /******************************************************************************************************************/

        //Creates all the blocks that will be used in game, as well as associates them with their respective buttons.
        private void LoadBlocksWithButtons()
        {
            //Creating all block data
            Random rand = new Random();
            blocks[0] = new Block(Color.Blue, 1, rand.Next(10,375));
            blocks[1] = new Block(Color.Green, 2, rand.Next(10, 375));
            blocks[2] = new Block(Color.Purple, 3, rand.Next(10, 375));
            blocks[3] = new Block(Color.Pink, 4, rand.Next(10, 375));
            blocks[4] = new Block(Color.Red, 5, rand.Next(10, 375));
            blocks[5] = new Block(Color.Orange, 6, rand.Next(10, 375));
            blocks[6] = new Block(Color.Yellow, 7, rand.Next(10, 375));
            blocks[7] = new Block(Color.LimeGreen, 8, rand.Next(10, 375));
            blocks[8] = new Block(Color.Teal, 9, rand.Next(10, 375));

            //loading all buttons into array
            blockButtons[0] = button_block1;
            blockButtons[1] = button_block2;
            blockButtons[2] = button_block3;
            blockButtons[3] = button_block4;
            blockButtons[4] = button_block5;
            blockButtons[5] = button_block6;
            blockButtons[6] = button_block7;
            blockButtons[7] = button_block8;
            blockButtons[8] = button_block9;

            //iterate through each button and associate it to the data for each block
            for(int i = 0; i < blockButtons.Count(); i++)
            {
                blockButtons[i].Location = new Point(blocks[i].GetXLocation(), blocks[i].GetYLocation());
                blockButtons[i].BackColor = blocks[i].GetBlockColor();
            }
        }

        //On opening program, pull from db and create all players.
        private void LoadPlayers()
        {
            try
            {
                using (StreamReader sr = File.OpenText(AppDomain.CurrentDomain.BaseDirectory +
                @"playerInfo/loadPlayers.txt"))
                {
                    string line;
                    string[] info;
                    //while ((line = xorEncrypt.EncryptDecrypt(sr.ReadLine(),307)) != null)
                    while ((line = xorEncrypt.EncryptDecrypt(sr.ReadLine(), 307)) != null)
                    {
                        info = line.Split(',');

                        Player player = new Player(
                            info[0], info[1], Int32.Parse(info[2])
                            , new List<int> { Int32.Parse(info[3]), Int32.Parse(info[4]), Int32.Parse(info[5])
                        , Int32.Parse(info[6]), Int32.Parse(info[7]), Int32.Parse(info[8]), Int32.Parse(info[9])
                        , Int32.Parse(info[10]), Int32.Parse(info[11]), Int32.Parse(info[12])}
                            , DateTime.Parse(info[13]),
                            info[14], info[15], info[16], info[17]);

                        player.ResetSaveInfo();
                        players.Add(player);
                    }
                }
            }
            catch(FileNotFoundException e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        //Establishes the appropriate flow of each panel.
        private void PanelSetup()
        {
            panel_TitleScreen.Visible = true;
            panel_SignUp.Visible = false;
            panel_Game.Visible = false;
            panel_AdminPage.Visible = false;
        }

        //Determines if the round is reverse or not.
        private void ModeGenerate()
        {
            gameMode = sequence.GetGameMode();
            if(gameMode =="reverse")
            {
                panel_gameHeader.BackColor = Color.ForestGreen;
                label_game_mode.ForeColor = Color.White;
                label_game_currentPlayer.ForeColor = Color.White;
                label_game_score.ForeColor = Color.White;
            }
            else
            {
                panel_gameHeader.BackColor = Color.BurlyWood;
                label_game_mode.ForeColor = Color.Black;
                label_game_currentPlayer.ForeColor = Color.Black;
                label_game_score.ForeColor = Color.Black;
            }
        }

        //Toggle background color for blocks, based on user clicks
        private void ColorChange(Button block, Color resetColor, bool flash)
        {
            block.BackColor = (flash) ? sequence.GetActiveBlockColor() : resetColor;
        }

        //false means a user already exists so no flag is needed, true means user is not found in DB, therefore flag is true.
        private bool CheckForInValidLogin(string user, string pass)
        {
            if(user.ToLower() == admin.GetUsername() && pass == admin.GetPassword())
            {
                return false;
            }
            try
            {
                using (StreamReader sr = File.OpenText(AppDomain.CurrentDomain.BaseDirectory +
                @"playerInfo/loadPlayers.txt"))
                {
                    string line;
                    string[] info;
                    while ((line = xorEncrypt.EncryptDecrypt(sr.ReadLine(), 307)) != null)
                    {
                        info = line.Split(',');
                        if (info[0] == user.ToLower() && info[1] == pass)
                        {
                            return false;
                        }
                    }
                }
                return true; //if login doesn't match anything in DB, then failed attempt.
            }
            catch(FileNotFoundException e)
            {
                MessageBox.Show(e.Message + "\n\nDB could not be found. Giving Temporary Player Account.");
                activePlayer = new Player("temp", "temp", 0, DateTime.Now, "City", "State", "County", "N/A");
                tracker = new Tracker(activePlayer);
                return false;
            }            
        }

        private void LoadComboBox()
        {
            foreach(Player p in players)
            {
                comboBox_admin_UserDropDown.Items.Add(p.GetUserName());
            }
        }

        private void button_admin_viewUserInfo_Click(object sender, EventArgs e)
        {
            if(comboBox_admin_UserDropDown.Text == "")
            {
                richTextBox_admin_PresentationScreen.Text = "PLEASE SELECT A USER FIRST.\n\n<===== Select The User Here";
            }
            else
            {
                foreach (Player p in players)
                {
                    if (p.GetUserName() == comboBox_admin_UserDropDown.Text)
                    {
                        activePlayer = p;
                        break;
                    }
                }

                richTextBox_admin_PresentationScreen.Font = new Font("Microsoft Sans Serif", 12);
                richTextBox_admin_PresentationScreen.Text = "";
                richTextBox_admin_PresentationScreen.Text = String.Format(
                    "USER INFORMATION:\n\n" +
                    "Username : {0}\n\n" +
                    "Password : {1}\n\n" +
                    "Date Of Birth : {2}\n\n" +
                    "City: {3}\n\n" +
                    "State: {4}\n\n" +
                    "County: {5}\n\n" +
                    "Diagnosis: {6}\n\n"
                    , activePlayer.GetUserName(), activePlayer.GetPassword(), activePlayer.GetDOB().ToString()
                    , activePlayer.GetCity(), activePlayer.GetState(), activePlayer.GetCounty()
                    , activePlayer.GetDiagnosis());
            }
            
        }

        private void button_admin_viewUserScores_Click(object sender, EventArgs e)
        {
            if (comboBox_admin_UserDropDown.Text == "")
            {
                richTextBox_admin_PresentationScreen.Text = "PLEASE SELECT A USER FIRST.\n\n<===== Select The User Here";
            }
            else
            {
                foreach (Player p in players)
                {
                    if (p.GetUserName() == comboBox_admin_UserDropDown.Text)
                    {
                        activePlayer = p;
                        break;
                    }
                }

                richTextBox_admin_PresentationScreen.Font = new Font("Microsoft Sans Serif", 12);
                richTextBox_admin_PresentationScreen.Text = "";
                richTextBox_admin_PresentationScreen.Text = String.Format(
                    "USER SCORE HISTORY:\n\n" +
                    "Best Score : {0}\n\n" +
                    "Top 10 Scores ( 1 being highest) :\n\n"
                    , activePlayer.GetBestScore());

                for(int i = 0; i < 10; i++)
                {
                    richTextBox_admin_PresentationScreen.Text += String.Format(
                        "{0} score:\t{1}\n", i+1, activePlayer.GetScoreHistory()[i]);
                }
            }
        }

        private void button_admin_viewScoreboard_Click(object sender, EventArgs e)
        {
            scoreBoard.LoadScores(players);

            richTextBox_admin_PresentationScreen.Font = new Font("Microsoft Sans Serif", 25);
            richTextBox_admin_PresentationScreen.Text = "";
            richTextBox_admin_PresentationScreen.Text = "SCOREBOARD : \n\n";
            richTextBox_admin_PresentationScreen.Text += scoreBoard.ToString();
        }

        private void button_admin_logOut_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thank you!");
            panel_AdminPage.Visible = false;
        }

        private void button_admin_viewUserLogs_Click(object sender, EventArgs e)
        {
            if (comboBox_admin_UserDropDown.Text == "")
            {
                richTextBox_admin_PresentationScreen.Text = "PLEASE SELECT A USER FIRST.\n\n<===== Select The User Here";
            }
            else
            {
                foreach (Player p in players)
                {
                    if (p.GetUserName() == comboBox_admin_UserDropDown.Text)
                    {
                        activePlayer = p;
                        break;
                    }
                }

                richTextBox_admin_PresentationScreen.Font = new Font("Microsoft Sans Serif", 10);
                richTextBox_admin_PresentationScreen.Text = "";

                using (StreamReader sr = new StreamReader(activePlayer.GetTrackerLog()))
                {
                    string line;
                    while((line = sr.ReadLine()) != null)
                    {
                        richTextBox_admin_PresentationScreen.Text += xorEncrypt.EncryptDecrypt(line,7919) + "\n\n";
                    }
                }
            }
        }
    }
}
