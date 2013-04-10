using System;
using System.Windows.Forms;

namespace gsb
{
    public partial class LoginForm : Form
    {
        /*
         * Constructor
         */

        public LoginForm()
        {
            InitializeComponent();
        }

        /*
         * Event handlers
         */

        private void loginButton_Click(object sender, EventArgs e)
        {
            Database db = Database.Instance;
            UserConnectionState res = db.ConnectUser(this.loginText.Text, this.passwordText.Text);

            if (res == UserConnectionState.Success)
                this.Close();
            else
            {
                this.passwordText.Text = "";
                this.passwordText.Focus();
                MessageBox.Show("Les identifiants spécifiés sont incorrects.");
            }
        }
    }
}
