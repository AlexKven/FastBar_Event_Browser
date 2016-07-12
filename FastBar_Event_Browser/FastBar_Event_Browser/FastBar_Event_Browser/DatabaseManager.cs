using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FastBar_Event_Browser
{
    internal static class DatabaseManager
    {
        static SQLiteConnection Connection;

        public static void Initialize()
        {
            Connection = DependencyService.Get<ISQLite>().GetConnection();
            
        }
    }
}
