using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Software.Models
{
    internal class User
    {
        private int id;
        public string name { get; } 
        public string lastname { get; }
        public string email { get; }
        public string password { get; }

        public User(int id, string name, string lastname, string email, string password) {
            this.id = id;
            this.name = name;
            this.lastname = lastname;
            this.email = email;
            this.password = password;
        }

        public List<Answer> get_answers()
        {
            //DatabaseHandler db = new DatabaseHandler();
            List<Answer> answers = DatabaseHandler.get_answers(id);
            return answers;
        }
        public Answer answer(int section, int question, int time, float rating)
        {
            return new Answer(id, section, question, time, rating);
        }

    }
}
