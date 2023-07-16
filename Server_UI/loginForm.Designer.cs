namespace Server_UI
{
    partial class loginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            loginButton = new Button();
            usernameField = new TextBox();
            passwordField = new TextBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(198, 39);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 0;
            label1.Text = "Nom d'utilisateur :";
            // 
            // loginButton
            // 
            loginButton.Location = new Point(195, 186);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(99, 28);
            loginButton.TabIndex = 1;
            loginButton.Text = "Connexion";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // usernameField
            // 
            usernameField.Location = new Point(158, 71);
            usernameField.Name = "usernameField";
            usernameField.Size = new Size(188, 23);
            usernameField.TabIndex = 2;
            // 
            // passwordField
            // 
            passwordField.Location = new Point(158, 128);
            passwordField.Name = "passwordField";
            passwordField.Size = new Size(188, 23);
            passwordField.TabIndex = 3;
            passwordField.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(206, 105);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 4;
            label2.Text = "Mot de passe :";
            // 
            // loginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(517, 226);
            Controls.Add(label2);
            Controls.Add(passwordField);
            Controls.Add(usernameField);
            Controls.Add(loginButton);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "loginForm";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Connexion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button loginButton;
        private TextBox usernameField;
        private TextBox passwordField;
        private Label label2;
    }
}