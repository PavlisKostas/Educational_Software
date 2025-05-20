using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Educational_Software.Models
{
    internal static class Server
    {
        
        private static User? user;
        
        public static User sign_up(string name, string lastname, string email, string password)
        {
            if (DatabaseHandler.add_user(name, lastname, email, password))
            {
            
                List<User> users = DatabaseHandler.get_user(email, password);
          
                return users[0];
            }
            else
            {
                return null;
            }
        }

        public static User sign_in(string email, string password)
        {
            List<User> users = DatabaseHandler.get_user(email, password);
            if (users.Count > 0)
            {
                return users[0];
            }

            return null;
        }

        public static void sign_out()
        {
            user = null;
        }

    }
}
