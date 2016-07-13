using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastBar_Event_Browser
{
    //Used to store information about currently logged-in user in the database.
    //Currently a special table in the database for which there is no more than
    //one row, but does leave open the possibility of expanding the app to allow
    //more than one signed-in user which can be switched (like the Twitter app).
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Email { get; set; }
        [MaxLength(1024)]
        public string CipheredAccessToken { get; set; }

        //Not stored in the database directly, but automatically ciphers and
        //escapes token and puts it into CipheredAccessToken, which is stored.
        [Ignore]
        public string Token
        {
            get
            {
                return APIManager.CipherToken(DatabaseManager.UnEscape(CipheredAccessToken));
            }
            set
            {
                CipheredAccessToken = DatabaseManager.Escape(APIManager.CipherToken(value));
            }
        }
    }
}
