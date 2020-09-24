using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameShowAdmin
{
    public class Globals
    {
        public string PATH = @"D:\Adcademic\LTWin\Projects\EndOfSemester\1660471\QuestionsList.txt";

        public void GetAllQuestions(ref List<Question> list, DataGridView gridView = null)
        {
            StreamReader reader = new StreamReader(PATH);
            List<Question> QuestionsList = new List<Question>();
            Question quest = null;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("@"))
                {
                    quest = new Question();
                    quest.Content = line.Substring(1);
                }
                else if (line.StartsWith("#"))
                {
                    quest.AnswersList.Add(line.Substring(1));
                }
                else if (line.StartsWith("*"))
                {
                    quest.CorrectAnswer = line.Substring(1);

                    // Push question to QuestionList
                    QuestionsList.Add(quest);
                }
            }

            reader.Close();
            list = QuestionsList;

            // Trường hợp khi có dùng datagridview
            if (gridView != null)
                gridView.DataSource = QuestionsList;
        }
    }
}
