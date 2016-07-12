using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastBar_Event_Browser
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
