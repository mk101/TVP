using System;

namespace Lab1.Data {
    public class User {
        public string Login;
        public string Password;
        public string Name;
        public string LastName;
        public string Email;

        public User(string login, string password) {
            Login = login;
            Password = password;
            Name = "";
            LastName = "";
            Email = "";
        }

        public User(string login, string password, string name, string lastName, string email) {
            Login = login;
            Password = password;
            Name = name;
            LastName = lastName;
            Email = email;
        }
    }
}
