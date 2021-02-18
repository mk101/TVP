using System;
using System.Windows.Forms;

using Lab1.Data;

namespace Lab1.Forms {
    public partial class CreateUserForm : Form {
        public CreateUserForm() {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void createButton_Click(object sender, EventArgs e) {
            string login = userNameTextBox.Text;
            string password = passwordTextBox.Text;

            if (login == "" || password == "") {
                MessageBox.Show("All fields are required");
                return;
            }

            if (password != confirmPasswordtextBox.Text) {
                MessageBox.Show("Password mismatch");
                return;
            }

            if (!DataBase.CreateUser(new User(login, password))) {
                MessageBox.Show("This user already exists");
                return;
            }

            DataBase.SaveDataBase();

            LoginForm.InformationForm.user = DataBase.SearchUser(login, password);
            LoginForm.InformationForm.Show();
            this.Hide();
        }
    }
}
