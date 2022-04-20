using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.DataBase
{
    internal class PhotoRepository
    {
        private static readonly string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static void AddPhoto(Photo photo) 
        {
            using (var db = new SQLiteConnection(InitDataBase.DbPath))
            {
                if (db.Find<Photo>(x => x.Title == photo.Title && x.UserId == photo.UserId) == null)
                {
                    var path = Path.Combine(folderPath, photo.Title);
                    SavePhotoToHardDisk(path, photo.Content.Select(x => (byte)x).ToArray());
                    photo.Path = path;
                    photo.Content = null;
                    db.Insert(photo);
                    
                }
            }
        }

        public static List<Photo> GetPhotoes() 
        {
            using (var db = new SQLiteConnection(InitDataBase.DbPath))
            {

                return db.Table<Photo>().ToList();
            }
        }

        private static void SavePhotoToHardDisk(string path, byte[] photo) 
        {
            using (var fs = new FileStream(path, FileMode.Create)) 
            {
                fs.Write(photo, 0, photo.Length);
            }
        }
    }
}
