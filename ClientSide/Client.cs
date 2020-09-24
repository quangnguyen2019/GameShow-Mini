using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameShowAdmin;
using DTO;

namespace ClientSide
{
    public partial class ClientSide : Form
    {
        IPEndPoint IP;
        Socket clientSocket;
        
        Player player;
        string selectedAnswer = "";
        string correctAnswer = "";

        int currentScore = 0;
        int minutes, seconds;
        int origLength = 0;
        int tempLength = 3;
        int indexQuest = 0;
        string totalTime = "01:00";

        bool answered = false;
        bool started = false;
        bool finished = false;

        System.Timers.Timer timerCountdown;
        System.Timers.Timer timerWaiting;

        public ClientSide()
        {
            InitializeComponent();
            MaximizeBox = false;

            AskingNameForm frmAskingName = new AskingNameForm();
            frmAskingName.ClosingClientFrm += Frm_ClosingClientFrm;
            frmAskingName.ShowDialog();

            player = new Player();
            player.Name = frmAskingName.txtName.Text;
        }

        private void GameShowForm_UpdateQuestsClient(object sender, EventArgs e)
        {
            MessageBox.Show("Updated quests client-side");
        }

        private void Frm_ClosingClientFrm(object sender, EventArgs e)
        {
            Close();
        }

        private void ClientSide_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Connect();

            // Show client's IP in listview
            if (clientSocket.LocalEndPoint != null)
            {
                string text = clientSocket.LocalEndPoint.ToString();
                richTextBox1.SelectedText = text + ":  " + player.Name + "\r\n";

                // send player info to server
                player.PlayerIP = clientSocket.LocalEndPoint;
                Send("@@" + player.Name);
            }

            lbCountdown.Text = totalTime;
            string[] arrTime = lbCountdown.Text.Split(':');
            minutes = int.Parse(arrTime[0]);
            seconds = int.Parse(arrTime[1]);

            // Timer coutdown
            timerCountdown = new System.Timers.Timer(1000);
            timerCountdown.Elapsed += Timer_Elapsed;

            // Start Timer waiting
            timerWaiting = new System.Timers.Timer(1000);
            timerWaiting.Elapsed += TimerWaiting_Elapsed;
            timerWaiting.Enabled = true;
            timerWaiting.Start();
        }

        private void TimerWaiting_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (tempLength > origLength)
            {
                lbWaitingStart.Text += ".";
                origLength++;
            }
            else
            {
                lbWaitingStart.Text = "Waiting to start";
                origLength = 0;
            }
        }

        void Connect()
        {
            IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(IP);
            }
            catch
            {
                MessageBox.Show(
                    "Không thể kết nối tới Server !!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            Thread listener = new Thread(Receive);
            listener.IsBackground = true;
            listener.Start();
        }

        void CloseClient()
        {
            clientSocket.Close();
        }

        void Send(string str)
        {
            if (str != string.Empty)
            {
                clientSocket.Send(Serialize(str));
            }
        }

        void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];
                    clientSocket.Receive(data);

                    string textData = (string)Deserialize(data);

                    if (textData.StartsWith("Start"))
                    {
                        panelOverlay.Visible = false;

                        started = true;
                        timerCountdown.Enabled = true;
                        timerCountdown.Start();

                        // stop timer waiting
                        timerWaiting.Stop();

                        // updated question number
                        int index = textData.IndexOf(':') + 1;
                        string questsListLength = textData.Substring(index);

                        lbIndexQuest.Text = $"Question {indexQuest} / {questsListLength}";
                    }

                    else if (textData.StartsWith("Update_Quests"))
                    {
                        // updated question number
                        int index = textData.IndexOf(':') + 1;
                        string questsListLength = textData.Substring(index);

                        lbIndexQuest.Text = $"Question {indexQuest} / {questsListLength}";
                    }

                    // question structure: "@question#answer1#answer2#answer3#answer4*correctAnswer"
                    ///
                    else if (textData.StartsWith("@") && finished == false)
                    {
                        if (started)
                        {
                            timerCountdown.Enabled = true;
                        }

                        // increase the order of questions
                        indexQuest++;

                        // put question content into controls's text
                        int index = textData.IndexOf("#");
                        txtNewQuest.Text = (textData.Substring(0, index)).Substring(1);

                        ucAnswerBtn1.AnswerA = "A.  " + ReturnAnswer(textData, ref index, '#');
                        ucAnswerBtn1.AnswerB = "B.  " + ReturnAnswer(textData, ref index, '#');
                        ucAnswerBtn1.AnswerC = "C.  " + ReturnAnswer(textData, ref index, '#');
                        ucAnswerBtn1.AnswerD = "D.  " + ReturnAnswer(textData, ref index, '*');

                        // set "Click" event to buttons
                        foreach (Control ctrl in ucAnswerBtn1.Controls)
                        {
                            ctrl.Click += AnswerBtn_Click;
                        }

                        // Correct answer
                        correctAnswer = textData.Substring(index + 1);

                        // reset answer buttons's status
                        ResetAnswerBtn();
                        answered = false;
                    }

                    // receive result from Server
                    else if (textData.StartsWith("*"))
                    {
                        AddMessage(textData.Substring(1));
                    }

                    // Tin nhắn từ server
                    else if (textData.StartsWith("%"))
                    {
                        AddMessage("Server: " + textData.Substring(1));
                    }
                }
            }
            catch
            {
                CloseClient();
            }
        }

        private void ResetAnswerBtn()
        {
            ucAnswerBtn1.Enabled = true;

            foreach (Control ctrl in ucAnswerBtn1.Controls)
            {
                ctrl.BackColor = Color.FromArgb(224, 224, 224);
                ctrl.ForeColor = Color.Black;
            }
        }

        string ReturnAnswer(string str, ref int index, char separatorChar)
        {
            int startIndex = index + 1;
            index = str.IndexOf(separatorChar, startIndex);
            string answer = str.Substring(startIndex, index - startIndex);

            return answer;
        }

        byte[] Serialize(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, obj);

            return stream.ToArray();
        }

        object Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();

            return formatter.Deserialize(stream);
        }

        private void AnswerBtn_Click(object sender, EventArgs e)
        {
            if (!answered)
            {
                Button btn = ((Button)sender);
                btn.BackColor = Color.DarkGray;
                btn.ForeColor = Color.White;

                DialogResult result = MessageBox.Show(
                                          "Đây là đáp án cuối cùng của bạn?", "",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question
                                      );

                if (result == DialogResult.Yes)
                {
                    selectedAnswer = btn.Text.Substring(4);

                    if (selectedAnswer == correctAnswer)
                    {
                        btn.BackColor = Color.DodgerBlue;
                        btn.ForeColor = Color.White;

                        currentScore += 1;
                        txtCurrentScore.Text = currentScore.ToString();

                        if (player.PlayerScore < currentScore)
                        {
                            player.PlayerScore = currentScore;
                            txtHighestScore.Text = player.PlayerScore.ToString();

                            // send player's highest score to server
                            Send(player.PlayerScore.ToString());
                        }
                    }
                    else
                    {
                        if (player.PlayerScore < currentScore)
                        {
                            player.PlayerScore = currentScore;
                            txtHighestScore.Text = player.PlayerScore.ToString();
                        }

                        currentScore = 0;
                        txtCurrentScore.Text = currentScore.ToString();

                        foreach (Control ctrl in ucAnswerBtn1.Controls)
                        {
                            if (ctrl.Text.Substring(4) == correctAnswer)
                            {
                                ctrl.BackColor = Color.DodgerBlue;
                                ctrl.ForeColor = Color.White;
                                break;
                            }
                        }
                    }

                    ucAnswerBtn1.Enabled = false;
                    answered = true;

                    // Send result to Server
                    Send("%" + selectedAnswer + "  -  " + lbCountdown.Text);

                    // Stop Temporarily Timer Countdown
                    timerCountdown.Enabled = false;
                }
                else
                {
                    btn.BackColor = Color.FromArgb(224, 224, 224);
                    btn.ForeColor = Color.Black;
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Send("%" + txtMessage.Text);
            AddMessage("You:  " + txtMessage.Text);
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send("%" + txtMessage.Text.Trim());
                AddMessage("You:  " + txtMessage.Text.Trim());
            }
        }

        private void AddMessage(string str)
        {
            richTextBox1.SelectedText = Environment.NewLine + str;
            txtMessage.Clear();
        }

        private void ClientSide_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Send("Closed");
            }
            catch {}
        }

        private void txtNewQuest_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void panelOverlay_SizeChanged(object sender, EventArgs e)
        {
            lbWaitingStart.Location = new Point(
                (panelOverlay.Width - lbWaitingStart.Width) / 2,
                (panelOverlay.Height - lbWaitingStart.Height) / 2
            );
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (seconds == 0)
            {
                minutes -= 1;
                seconds = 59;
            }
            else
            {
                seconds -= 1;
            }

            // update time countdown 
            lbCountdown.Text = "0" + minutes + ":" + (seconds < 10 ? "0" + seconds : "" + seconds);

            if (minutes == 0 && seconds == 0)
            {
                finished = true;
                ucAnswerBtn1.Enabled = false;

                string str = Environment.NewLine;
                str += "\t>>>>>>>>>>>>  FINISHED  <<<<<<<<<<<<";
                AddMessage(str);

                // Notify to Server
                Send("%>>>  FINISHED  <<<");
                
                timerCountdown.Stop();
                timerCountdown.Dispose();
                return;
            }
        }
    }
}
