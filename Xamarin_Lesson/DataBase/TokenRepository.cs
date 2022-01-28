using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.DataBase
{
    internal class TokenRepository
    {
        public static void AddToken(Token token, RefreshToken refreshToken) 
        {
            using (var db = new SQLiteConnection(InitDataBase.DbPath))
            {
                if (db.Table<Token>().Count() != 0)
                {
                    db.DeleteAll<Token>();
                    db.Insert(token);
                    db.DeleteAll<RefreshToken>();
                    db.Insert(refreshToken);
                }
                else 
                {
                    db.Insert(token);
                    db.Insert(refreshToken);
                }
                   
            }
        }

        public static Token GetToken() 
        {
            using (var db = new SQLiteConnection(InitDataBase.DbPath)) 
            {
                var token = db.Table<Token>().FirstOrDefault();
                token.RefreshToken = db.Table<RefreshToken>().FirstOrDefault();
                return token;
            }
        }
    }
}
