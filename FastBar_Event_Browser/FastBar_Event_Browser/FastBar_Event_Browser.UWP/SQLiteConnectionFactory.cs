using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Windows.Storage;

namespace FastBar_Event_Browser.UWP
{
    class SQLiteConnectionFactory : ISQLite
    {
        public SQLiteConnectionFactory() { }
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "EventDatabase.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}
