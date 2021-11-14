using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.DataBase
{
    static class InitDataBase
    {
        private static readonly string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static readonly string dataBaseName = "local.db";
        public static readonly string DbPath = Path.Combine(folderPath, dataBaseName);
        public static void CreateDataBase()
        {
            var path = Path.Combine(folderPath, dataBaseName);
            if (!File.Exists(path))
            {
                File.Create(path);
                using (var db = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite, true))
                {
                    db.CreateTable<User>();
                }
            }
        }
    }
}
