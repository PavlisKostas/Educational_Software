using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Software.Models
{
    internal class Answer
    {
        public int studentId { get; }
        public int section { get; }
        public int question { get; }
        public int time { get; }
        public float rating { get; }
        public bool userAnswer { get; }

        public Answer(int studentId, int section, int question, int time, float rating, bool userAnswer)
        {
            this.studentId = studentId;
            this.section = section;
            this.question = question;
            this.time = time;
            this.rating = rating;
            this.userAnswer = userAnswer;
        }
    }
}
