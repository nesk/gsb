using System;
using System.Windows.Forms;

namespace gsb
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);

            if(Database.Instance.UserConnected)
                Application.Run(new MainForm());
        }
    }
}
