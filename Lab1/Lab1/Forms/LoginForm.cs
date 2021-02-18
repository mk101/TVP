using System;
using System.Windows.Forms;

using Lab1.Data;

namespace Lab1.Forms {
    public partial class LoginForm : Form {
        private CreateUserForm createUserForm;
        public static MainForm InformationForm;
        public LoginForm() {
            InitializeComponent();
            
            createUserForm = new CreateUserForm();
            createUserForm.FormClosed += CreateUserForm_FormClosed;

            InformationForm = new MainForm();
            InformationForm.FormClosed += MainForm_FormClosed;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            this.Show();
            InformationForm = new MainForm();
            InformationForm.FormClosed += MainForm_FormClosed;
        }

        private void CreateUserForm_FormClosed(object sender, FormClosedEventArgs e) {
            this.Show();
            createUserForm = new CreateUserForm();
            createUserForm.FormClosed += CreateUserForm_FormClosed;
        }

        private void exitButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void loginButton_Click(object sender, EventArgs e) {
            string login = userTextBox.Text;
            string password = passwordTextBox.Text;
            if (login == "" || password == "") {
                MessageBox.Show("All fields are required");
                return;
            }

            User user = DataBase.SearchUser(login, password);
            if (user == null) {
                MessageBox.Show("This user does not found :(");
                return;
            }

            InformationForm.user = user;
            InformationForm.Show();
            this.Hide();
            
        }

        private void createUserButton_Click(object sender, EventArgs e) {
            createUserForm.Show();
            this.Hide();
        }
    }
}
