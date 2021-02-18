using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Lab1.Forms;
using Lab1.Data;

namespace Lab1 {
    static class Program {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main() {
            if (!DataBase.ReadDataBase(@"D:\Accounts.txt")) {
                MessageBox.Show("Faild to read database. Try reopen program. :(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
