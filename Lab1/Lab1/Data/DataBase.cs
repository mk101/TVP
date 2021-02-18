using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Lab1.Data {
    class DataBase {
        private static List<User> users;
        private static string dbPath;

        /*
         * Формат файла базы данных:
         * <кол-во пользователей>
         * Логин
         * Пароль
         * Имя
         * Фамилия
         * Почта
         */

        public static bool ReadDataBase(string path) {
            dbPath = path;
            users = new List<User>();
            try {
                using (var sr = new StreamReader(path)) {
                    int count = int.Parse(sr.ReadLine());
                    for (int i = 0; i < count; i++) {
                        string login = sr.ReadLine();
                        if (login == null) throw new Exception();
                        string password = sr.ReadLine();
                        if (password == null) throw new Exception();
                        string name = sr.ReadLine();
                        if (name == null) throw new Exception();
                        string lastName = sr.ReadLine();
                        if (lastName == null) throw new Exception();
                        string email = sr.ReadLine();
                        if (email == null) throw new Exception();
                        users.Add(new User(login, password, name, lastName, email));
                    }
                }
            } catch (Exception) {
                return false;
            }
            return true;
        }

        public static bool SaveDataBase() {
            try {
                using (var sw = new StreamWriter(dbPath, false)) {
                    sw.WriteLine(users.Count);
                    for (int i = 0; i < users.Count; i++) {
                        sw.WriteLine(users[i].Login);
                        sw.WriteLine(users[i].Password);
                        sw.WriteLine(users[i].Name);
                        sw.WriteLine(users[i].LastName);
                        sw.WriteLine(users[i].Email);
                    }
                }
            } catch(Exception) {
                return false;
            }
            
            return true;
        }

        public static bool CreateUser(User user) {
            if (users.Exists( u => u.Login == user.Login )) {
                return false;
            }

            users.Add(user);
            return true;
        }

        public static User SearchUser(string login, string password) {
            return users.Find(u => (u.Login == login && u.Password == password));
        }
    }
}
