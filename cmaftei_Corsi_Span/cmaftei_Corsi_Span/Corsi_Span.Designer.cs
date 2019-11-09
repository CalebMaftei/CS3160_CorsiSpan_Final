namespace cmaftei_Corsi_Span
{
    partial class Corsi_Span
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_TitleScreen = new System.Windows.Forms.Panel();
            this.label_Title = new System.Windows.Forms.Label();
            this.label_usernamePrompt = new System.Windows.Forms.Label();
            this.label_passwordPrompt = new System.Windows.Forms.Label();
            this.textBox_usernameEntry = new System.Windows.Forms.TextBox();
            this.textBox_passwordEntry = new System.Windows.Forms.TextBox();
            this.button_login = new System.Windows.Forms.Button();
            this.button_signUp = new System.Windows.Forms.Button();
            this.panel_TitleScreen.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_TitleScreen
            // 
            this.panel_TitleScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel_TitleScreen.Controls.Add(this.button_signUp);
            this.panel_TitleScreen.Controls.Add(this.button_login);
            this.panel_TitleScreen.Controls.Add(this.textBox_passwordEntry);
            this.panel_TitleScreen.Controls.Add(this.textBox_usernameEntry);
            this.panel_TitleScreen.Controls.Add(this.label_passwordPrompt);
            this.panel_TitleScreen.Controls.Add(this.label_usernamePrompt);
            this.panel_TitleScreen.Controls.Add(this.label_Title);
            this.panel_TitleScreen.Location = new System.Drawing.Point(-2, -1);
            this.panel_TitleScreen.Name = "panel_TitleScreen";
            this.panel_TitleScreen.Size = new System.Drawing.Size(1288, 699);
            this.panel_TitleScreen.TabIndex = 0;
            // 
            // label_Title
            // 
            this.label_Title.AutoSize = true;
            this.label_Title.Font = new System.Drawing.Font("Modern No. 20", 71.99999F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Title.ForeColor = System.Drawing.Color.BurlyWood;
            this.label_Title.Location = new System.Drawing.Point(289, 42);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(693, 147);
            this.label_Title.TabIndex = 0;
            this.label_Title.Text = "Corsi Span";
            // 
            // label_usernamePrompt
            // 
            this.label_usernamePrompt.AutoSize = true;
            this.label_usernamePrompt.Font = new System.Drawing.Font("Modern No. 20", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_usernamePrompt.ForeColor = System.Drawing.Color.BurlyWood;
            this.label_usernamePrompt.Location = new System.Drawing.Point(349, 276);
            this.label_usernamePrompt.Name = "label_usernamePrompt";
            this.label_usernamePrompt.Size = new System.Drawing.Size(197, 41);
            this.label_usernamePrompt.TabIndex = 1;
            this.label_usernamePrompt.Text = "Username: ";
            // 
            // label_passwordPrompt
            // 
            this.label_passwordPrompt.AutoSize = true;
            this.label_passwordPrompt.Font = new System.Drawing.Font("Modern No. 20", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_passwordPrompt.ForeColor = System.Drawing.Color.BurlyWood;
            this.label_passwordPrompt.Location = new System.Drawing.Point(356, 353);
            this.label_passwordPrompt.Name = "label_passwordPrompt";
            this.label_passwordPrompt.Size = new System.Drawing.Size(190, 41);
            this.label_passwordPrompt.TabIndex = 2;
            this.label_passwordPrompt.Text = "Password: ";
            // 
            // textBox_usernameEntry
            // 
            this.textBox_usernameEntry.Font = new System.Drawing.Font("Modern No. 20", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_usernameEntry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox_usernameEntry.Location = new System.Drawing.Point(552, 273);
            this.textBox_usernameEntry.Name = "textBox_usernameEntry";
            this.textBox_usernameEntry.Size = new System.Drawing.Size(350, 50);
            this.textBox_usernameEntry.TabIndex = 3;
            // 
            // textBox_passwordEntry
            // 
            this.textBox_passwordEntry.Font = new System.Drawing.Font("Modern No. 20", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_passwordEntry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox_passwordEntry.Location = new System.Drawing.Point(552, 350);
            this.textBox_passwordEntry.Name = "textBox_passwordEntry";
            this.textBox_passwordEntry.PasswordChar = '*';
            this.textBox_passwordEntry.Size = new System.Drawing.Size(350, 50);
            this.textBox_passwordEntry.TabIndex = 4;
            // 
            // button_login
            // 
            this.button_login.BackColor = System.Drawing.Color.BurlyWood;
            this.button_login.Font = new System.Drawing.Font("Modern No. 20", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_login.Location = new System.Drawing.Point(363, 447);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(539, 67);
            this.button_login.TabIndex = 5;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = false;
            // 
            // button_signUp
            // 
            this.button_signUp.BackColor = System.Drawing.Color.BurlyWood;
            this.button_signUp.Font = new System.Drawing.Font("Modern No. 20", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_signUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_signUp.Location = new System.Drawing.Point(363, 540);
            this.button_signUp.Name = "button_signUp";
            this.button_signUp.Size = new System.Drawing.Size(539, 67);
            this.button_signUp.TabIndex = 6;
            this.button_signUp.Text = "Sign Up";
            this.button_signUp.UseVisualStyleBackColor = false;
            // 
            // Corsi_Span
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 691);
            this.Controls.Add(this.panel_TitleScreen);
            this.Name = "Corsi_Span";
            this.Text = "Corsi Span";
            this.panel_TitleScreen.ResumeLayout(false);
            this.panel_TitleScreen.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_TitleScreen;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_passwordPrompt;
        private System.Windows.Forms.Label label_usernamePrompt;
        private System.Windows.Forms.TextBox textBox_passwordEntry;
        private System.Windows.Forms.TextBox textBox_usernameEntry;
        private System.Windows.Forms.Button button_signUp;
        private System.Windows.Forms.Button button_login;
    }
}

