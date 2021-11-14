using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.DataBase
{
    public class UserRepository
    {
        public static void AddUser(User user)
        {
            using (var db = new SQLiteConnection(InitDataBase.DbPath))
            {
                if (db.Table<User>().Count() != 0)
                {
                    db.DeleteAll<User>();
                    db.Insert(user);
                    Console.WriteLine(db.Table<User>().Count());
                }
                else
                    db.Insert(user);
            }
        }

        public static void RemoveUser(User user)
        {
            using (var db = new SQLiteConnection(InitDataBase.DbPath))
            {
                db.Delete<User>(user.Id);
            }
        }

        public static  User GetUser()
        {
            using (var db = new SQLiteConnection(InitDataBase.DbPath))
            {
                return db.Table<User>().FirstOrDefault();
            }
        }

        public static User GetUserById(string id)
        {
            using (var db = new SQLiteConnection(InitDataBase.DbPath))
            {
                return db.Table<User>().FirstOrDefault(x => x.Id == id);
            }
        }

        public static User GetUserByEmail(string email)
        {
            using (var db = new SQLiteConnection(InitDataBase.DbPath))
            {
                return db.Table<User>().FirstOrDefault(x => x.Email == email);
            }
        }
    }
}
