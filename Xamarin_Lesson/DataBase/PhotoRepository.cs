using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.DataBase
{
    internal class PhotoRepository
    {
        public static void AddPhoto(Photo photo) 
        {
            using (var db = new SQLiteConnection(InitDataBase.DbPath))
            {

                db.Insert(photo);
            }
        }

        public static List<Photo> GetPhotoes() 
        {
            using (var db = new SQLiteConnection(InitDataBase.DbPath))
            {

                return db.Table<Photo>().ToList();
            }
        }
    }
}
