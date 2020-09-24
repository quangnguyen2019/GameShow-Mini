using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameShowAdmin
{
    public partial class AddingQuestForm : Form
    {
        Globals globals = new Globals();
        List<Question> QuestionsList = null;
        string path;

        public AddingQuestForm()
        {
            InitializeComponent();
            path = globals.PATH;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            globals.GetAllQuestions(ref QuestionsList, grvQuest);

            var col_0 = grvQuest.Columns[0];
            var col_1 = grvQuest.Columns[1];

            col_0.HeaderText = "Câu Hỏi";
            col_1.HeaderText = "Đáp Án";

            col_0.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col_1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // set width of columns
            col_0.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            // Set font size for datagridview
            grvQuest.ColumnHeadersDefaultCellStyle.Font = new Font(Font.FontFamily, 9);
            grvQuest.DefaultCellStyle.Font = new Font(Font.FontFamily, 9);
        }

        public event EventHandler UpdateQuestionsList;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Check Inputs
            if (!CheckInputs())
            {
                return;
            }

            // Save to text file
            Question quest = GetNewQuestion();
            string text = File.ReadAllText(path);

            if (text.Contains(quest.Content.Trim()))
            {
                MessageBox.Show("Câu hỏi đã tồn tại !");
                return;
            }

            bool result = SaveToTextFile(quest);

            if (result)
            {
                MessageBox.Show("Lưu thành công !");
                globals.GetAllQuestions(ref QuestionsList, grvQuest);
                Reset();

                // update question list in Admin form
                UpdateQuestionsList(this, e);
            }
        }

        bool CheckInputs()
        {
            // check inputs
            if (txtNewQuest.Text.Trim() == "")
            {
                MessageBox.Show("Chưa có nội dung câu hỏi !!");
                txtNewQuest.Focus();
                return false;
            }

            bool checkedCorrectAnswer = false;

            foreach (Control ctrl in ucAnswer1.Controls)
            {
                if (ctrl is TextBox && ctrl.Text.Trim() == "")
                {
                    MessageBox.Show("Nội dung các đáp án còn thiếu !!");
                    ctrl.Focus();
                    return false;
                }
                else if (ctrl is RadioButton && ((RadioButton)ctrl).Checked)
                {
                    checkedCorrectAnswer = true;
                }
            }

            if (!checkedCorrectAnswer)
            {
                MessageBox.Show("Chưa chọn đáp án đúng !!");
                checkedCorrectAnswer = false;
                return false;
            }

            return true;
        }

        private Question GetNewQuestion()
        {
            Question quest = new Question();
            quest.Content = txtNewQuest.Text.Trim();
            quest.AnswersList.Add(ucAnswer1.AnswerA.Text);
            quest.AnswersList.Add(ucAnswer1.AnswerB.Text);
            quest.AnswersList.Add(ucAnswer1.AnswerC.Text);
            quest.AnswersList.Add(ucAnswer1.AnswerD.Text);

            if (ucAnswer1.RadioBtnA.Checked)
            {
                quest.CorrectAnswer = ucAnswer1.AnswerA.Text;
            }
            else if (ucAnswer1.RadioBtnB.Checked)
            {
                quest.CorrectAnswer = ucAnswer1.AnswerB.Text;
            }
            else if (ucAnswer1.RadioBtnC.Checked)
            {
                quest.CorrectAnswer = ucAnswer1.AnswerC.Text;
            }
            else if (ucAnswer1.RadioBtnD.Checked)
            {
                quest.CorrectAnswer = ucAnswer1.AnswerD.Text;
            }

            return quest;
        }

        private bool SaveToTextFile(Question quest)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path, true);

                // @ + idQuestion + . + contentQuest
                writer.WriteLine("@" + quest.Content);
                foreach (string answer in quest.AnswersList)
                {
                    writer.WriteLine("#" + answer);
                }
                writer.WriteLine("*" + quest.CorrectAnswer);

                writer.Close();
                return true;
            }
            catch {
                return false;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txtNewQuest.Text = "";

            foreach (Control ctrl in ucAnswer1.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = "";
                }
                else if (ctrl is RadioButton && ((RadioButton)ctrl).Checked)
                {
                    ((RadioButton)ctrl).Checked = false;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
               "Xóa dòng đang được chọn?", 
               "Xóa Câu Hỏi", 
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                //int index = grvQuest.CurrentRow.Index;
                //string oldStr;
                //string newStr = "";

                //// read text file
                //string text = File.ReadAllText(path);

                //// replace question
                //oldStr = "@" + QuestionsList[index].Content;
                //text = text.Replace(oldStr, newStr);
                //File.WriteAllText(path, text);

                //// replace answers
                //for (int i = 0; i < 4; i++)
                //{
                //    oldStr = "#" + QuestionsList[index].AnswersList[i];
                //    text = text.Replace(oldStr, newStr);
                //    File.WriteAllText(path, text);
                //}

                //// replace correct answer
                //oldStr = "*" + QuestionsList[index].CorrectAnswer;
                //text = text.Replace(oldStr, newStr);
                //File.WriteAllText(path, text);

                int index = grvQuest.CurrentRow.Index;
                string oldStr;
                string newStr = "";

                oldStr = "@" + QuestionsList[index].Content;

                for (int i = 0; i < 4; i++)
                {
                    oldStr += "\r\n#" + QuestionsList[index].AnswersList[i];
                }

                oldStr += "\r\n*" + QuestionsList[index].CorrectAnswer;

                // read text file
                string text = File.ReadAllText(path);
                text = text.Replace(oldStr, newStr);
                File.WriteAllText(path, text);

                //remove blank lines
                var lines = File.ReadAllLines(path)
                                .Where(arg => !string.IsNullOrWhiteSpace(arg));
                File.WriteAllLines(path, lines);

                globals.GetAllQuestions(ref QuestionsList, grvQuest);
                UpdateQuestionsList(this, e);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Check Inputs
            if (!CheckInputs())
                return;

            DialogResult result = MessageBox.Show(
                "Cập nhật dòng đang được chọn?", 
                "Cập Nhật Câu Hỏi", 
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                int index = grvQuest.CurrentRow.Index;

                // Old string
                string oldStr = "@" + QuestionsList[index].Content;

                // New string
                Question quest = GetNewQuestion();
                string newStr = "@" + quest.Content;

                // read text file
                string text = File.ReadAllText(path);

                // replace question
                text = text.Replace(oldStr, newStr);
                File.WriteAllText(path, text);

                // replace answers
                for (int i = 0; i < 4; i++)
                {
                    oldStr = "\n#" + QuestionsList[index].AnswersList[i];
                    newStr = "\n#" + quest.AnswersList[i];
                    text = text.Replace(oldStr, newStr);
                    File.WriteAllText(path, text);
                }

                // replace correct answer
                oldStr = "*" + QuestionsList[index].CorrectAnswer;
                newStr = "*" + quest.CorrectAnswer;
                text = text.Replace(oldStr, newStr);
                File.WriteAllText(path, text);

                globals.GetAllQuestions(ref QuestionsList, grvQuest);
                UpdateQuestionsList(this, e);
            }
        }

        private void grvQuest_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = grvQuest.CurrentRow.Index;
            var temp = QuestionsList[index];

            txtNewQuest.Text = temp.Content;
            ucAnswer1.AnswerA.Text = temp.AnswersList[0];
            ucAnswer1.AnswerB.Text = temp.AnswersList[1];
            ucAnswer1.AnswerC.Text = temp.AnswersList[2];
            ucAnswer1.AnswerD.Text = temp.AnswersList[3];

            if (temp.CorrectAnswer == ucAnswer1.AnswerA.Text)
            {
                ucAnswer1.RadioBtnA.Checked = true;
            }
            else if (temp.CorrectAnswer == ucAnswer1.AnswerB.Text)
            {
                ucAnswer1.RadioBtnB.Checked = true;
            }
            else if (temp.CorrectAnswer == ucAnswer1.AnswerC.Text)
            {
                ucAnswer1.RadioBtnC.Checked = true;
            }
            else if (temp.CorrectAnswer == ucAnswer1.AnswerD.Text)
            {
                ucAnswer1.RadioBtnD.Checked = true;
            }
        }
    }
}
