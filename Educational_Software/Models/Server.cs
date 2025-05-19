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
        //private static DatabaseHandler db;
        private static User? user;
        //public Server()
        //{
            //DatabaseHandler db = new DatabaseHandler();
            //user = null;
        //}
        public static User sign_up(string name, string lastname, string email, string password)
        {
            Debug.WriteLine("going to sign up 2");
            if (DatabaseHandler.add_user(name, lastname, email, password))
            {
                Debug.WriteLine("successful signup going to create user object");
                List<User> users = DatabaseHandler.get_user(email, password);
                Debug.WriteLine("successful user object creation return user object");
                return users[0];
            }
            else
            {
                Debug.WriteLine("unsuccessful signup return user object null");
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
