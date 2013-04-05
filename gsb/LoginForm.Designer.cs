namespace gsb
{
    partial class LoginForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.loginText = new System.Windows.Forms.TextBox();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // loginText
            // 
            this.loginText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginText.Location = new System.Drawing.Point(13, 13);
            this.loginText.Name = "loginText";
            this.loginText.Size = new System.Drawing.Size(200, 26);
            this.loginText.TabIndex = 0;
            // 
            // passwordText
            // 
            this.passwordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordText.Location = new System.Drawing.Point(13, 45);
            this.passwordText.Name = "passwordText";
            this.passwordText.Size = new System.Drawing.Size(200, 26);
            this.passwordText.TabIndex = 1;
            this.passwordText.UseSystemPasswordChar = true;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(13, 77);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(200, 26);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "Connexion";
            this.loginButton.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.passwordText);
            this.Controls.Add(this.loginText);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 9, 9);
            this.Text = "Connexion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loginText;
        private System.Windows.Forms.TextBox passwordText;
        private System.Windows.Forms.Button loginButton;
    }
}

