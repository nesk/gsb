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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
