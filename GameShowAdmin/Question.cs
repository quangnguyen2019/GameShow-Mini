using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShowAdmin
{
    public class Question
    {
        public string Content { get; set; }
        public string CorrectAnswer { get; set; }

        public List<string> AnswersList = new List<string>();
    }
}
