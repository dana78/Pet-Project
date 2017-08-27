using System;
using SQLite.Net;
using SQLite.Net.Interop;
using PetApiClient;
using System.Diagnostics;
using PCLStorage;

namespace PetMobile.Data
{

    public class SQLiteDatabase : IDatabase
    {
        private SQLiteConnection db;

        public SQLiteDatabase(ISQLitePlatform platform)
        {
            db = new SQLiteConnection(platform, GetDatabasePath());
            db.CreateTable<OwnerData>();
        }

        private string GetDatabasePath()
        {
            IFolder folder = FileSystem.Current.LocalStorage;
            return PortablePath.Combine(folder.Path.ToString(), Constants.SQLiteDatabaseName);
        }

        public void SaveUser(Owner user)
        {
            if (!user.IdOwner.HasValue)
                throw new ArgumentException("Owner has no id");

            try
            {
                db.Insert(new OwnerData()
                {
                    Id = user.IdOwner.Value,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Phone = user.Phone,
                    Birthday = user.Birthday ?? new DateTime()
                });
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public Owner GetLoggedUser()
        {
            var user = db.Table<OwnerData>().FirstOrDefault();
            return user?.ToOwner();
        }

        public void CleanDatabase()
        {
            db.Execute("DELETE FROM Owner");
        }
    }


}
