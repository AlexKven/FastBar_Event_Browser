using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using FastBar_Event_Browser.Droid;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteConnectionFactory))]
namespace FastBar_Event_Browser.Droid
{
    class SQLiteConnectionFactory : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "EventDatabase.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}
