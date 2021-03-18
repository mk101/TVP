using System;
using System.Windows.Forms;

using Lab1.Data;

namespace Lab1.Forms {
    public partial class MainForm : Form {
        public User user;
        public MainForm() {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void logoutButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void MainForm_Shown(object sender, EventArgs e) {
            groupBox.Text = $"Information on {user.Login}";
            nameTextBox.Text = user.Name;
            lastNameTextBox.Text = user.LastName;
            emailTextBox.Text = user.Email;
        }

        private void nameCheckBox_CheckedChanged(object sender, EventArgs e) {
            nameTextBox.ReadOnly = !nameCheckBox.Checked;
            if (nameCheckBox.Checked == false) {
                user.Name = nameTextBox.Text;
                DataBase.SaveDataBase();
            }
        }

        private void lastNameCheckBox_CheckedChanged(object sender, EventArgs e) {
            lastNameTextBox.ReadOnly = !lastNameCheckBox.Checked;
            if (lastNameCheckBox.Checked == false) {
                user.LastName = lastNameTextBox.Text;
                DataBase.SaveDataBase();
            }
        }

        private void emailCheckBox_CheckedChanged(object sender, EventArgs e) {
            emailTextBox.ReadOnly = !emailCheckBox.Checked;
            if (emailCheckBox.Checked == false) {
                user.Email = emailTextBox.Text;
                DataBase.SaveDataBase();
            }
        }
    }
}
