using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gsb
{
    public partial class LoginForm : Form
    {
        /*
         * Properties
         */

        String defaultLogin;
        String defaultPassword;

        /*
         * Constructor
         */

        public LoginForm()
        {
            InitializeComponent();
        }

        /*
         * Events
         */

        private void LoginForm_Load(object sender, EventArgs e)
        {
            this.defaultLogin = this.loginText.Text;
            this.defaultPassword = this.passwordText.Text;
        }
    }
}
