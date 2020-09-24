using Broardcast_ScheduleAdmin;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameShowAdmin
{
    public partial class GameShowForm : Form
    {
        Globals globals = new Globals();
        const int Port = 9999;

        
        List<Question> QuestionsList = null;
        int index = 0;
        bool started = false;
        bool firstMessage = true;
        int numberFinisher = 0;

        IPEndPoint IP;
        Socket serverSocket;
        List<Socket> clientList;
        List<Player> PlayersList;

        public GameShowForm()
        {
            InitializeComponent();
            MaximizeBox = false;

            BrdScheduleForm frm = new BrdScheduleForm();
            frm.ShowDialog();
        }

        private void GameShowForm_Load(object sender, EventArgs e)
        {
            Connect();

            PlayersList = new List<Player>();
            globals.GetAllQuestions(ref QuestionsList);
            ShowQuestion(index);

            CheckForIllegalCrossThreadCalls = false;
            btnStart_Next.Enabled = false;
            btnResult.Enabled = false;

            //prevent editing question
            foreach (Control ctrl in ucAnswerVer1.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.KeyPress += txt_KeyPress;
                }
                else if (ctrl is RadioButton)
                {
                    ((RadioButton)ctrl).AutoCheck = false;
                }
            }

            lbIndexQuest.Text = $"Question {index + 1} / {QuestionsList.Count}";
        }

        string NextQuestion(int index)
        {
            string text = "@" + QuestionsList[index].Content +
                          "#" + QuestionsList[index].AnswersList[0] +
                          "#" + QuestionsList[index].AnswersList[1] +
                          "#" + QuestionsList[index].AnswersList[2] +
                          "#" + QuestionsList[index].AnswersList[3] +
                          "*" + QuestionsList[index].CorrectAnswer;
            return text;
        }

        private void ShowQuestion(int index)
        {
            var temp = QuestionsList[index];

            txtNewQuest.Text = temp.Content;
            ucAnswerVer1.AnswerA.Text = temp.AnswersList[0];
            ucAnswerVer1.AnswerB.Text = temp.AnswersList[1];
            ucAnswerVer1.AnswerC.Text = temp.AnswersList[2];
            ucAnswerVer1.AnswerD.Text = temp.AnswersList[3];

            foreach (Control ctrl in ucAnswerVer1.Controls)
            {
                if (ctrl is RadioButton)
                {
                    ((RadioButton)ctrl).AutoCheck = true;
                }
            }

            if (temp.CorrectAnswer == ucAnswerVer1.AnswerA.Text)
            {
                ucAnswerVer1.RadioBtnA.Checked = true;
            }
            else if (temp.CorrectAnswer == ucAnswerVer1.AnswerB.Text)
            {
                ucAnswerVer1.RadioBtnB.Checked = true;
            }
            else if (temp.CorrectAnswer == ucAnswerVer1.AnswerC.Text)
            {
                ucAnswerVer1.RadioBtnC.Checked = true;
            }
            else if (temp.CorrectAnswer == ucAnswerVer1.AnswerD.Text)
            {
                ucAnswerVer1.RadioBtnD.Checked = true;
            }

            foreach (Control ctrl in ucAnswerVer1.Controls)
            {
                if (ctrl is RadioButton)
                {
                    ((RadioButton)ctrl).AutoCheck = false;
                }
            }
        }

        #region "Socket"

        void Connect()
        {
            clientList = new List<Socket>();

            IP = new IPEndPoint(IPAddress.Any, 9999);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(IP);
            lbConnectionStatus.Text = "Wating for connection...";

            Thread listener = new Thread(() => { 
                try
                {
                    while (true)
                    {
                        serverSocket.Listen(10);
                        Socket clientSocket = serverSocket.Accept();
                        clientList.Add(clientSocket);

                        PlayersList.Add(new Player() { 
                            PlayerIP = clientSocket.RemoteEndPoint,
                            Name = "Client",
                            PlayerScore = 0
                        });

                        if (!started)
                        {
                            if (clientList.Count > 0)
                            {
                                btnStart_Next.Enabled = true;
                            }
                        }

                        lbConnectionStatus.Text =
                            $"{clientList.Count} client(s) connected  -  " +
                            "Wating for connection...";

                        Send(clientSocket, NextQuestion(index));

                        Thread receive = new Thread(Receive);
                        receive.IsBackground = true;
                        receive.Start(clientSocket);
                    }
                }
                catch
                {
                    IP = new IPEndPoint(IPAddress.Any, 9999);
                    serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
            });
            listener.IsBackground = true;
            listener.Start();
        }

        void CloseServer()
        {
            serverSocket.Close();
        }

        void Send(Socket clientSocket, string str)
        {
            try
            {
                clientSocket.Send(
                    Serialize(str)
                );
            }
            catch {}
        }

        void SendMessage(Socket clientSocket)
        {
            if (txtMessage.Text.Trim() != string.Empty)
            {
                clientSocket.Send(
                    Serialize("%" + txtMessage.Text.Trim())
                );
            }
        }

        void Receive(object obj)
        {
            Socket clientSocket = obj as Socket;

            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];
                    clientSocket.Receive(data);

                    string dataString = (string)Deserialize(data);

                    if (dataString.StartsWith("@@"))
                    {
                        var keyIP = clientSocket.RemoteEndPoint;

                        //update server-side player scores
                        foreach (Player player in PlayersList)
                        {
                            if (player.PlayerIP == keyIP)
                            {
                                player.Name = dataString.Substring(2);
                                break;
                            }
                        }
                    }
                    else if (dataString.Contains("%"))
                    {
                        dataString = dataString.Substring(1);

                        foreach (Player player in PlayersList)
                        {
                            if (player.PlayerIP == clientSocket.RemoteEndPoint)
                            {
                                AddMessage($"From {player.Name}:  " + dataString);
                            }
                        }

                        if (dataString.Contains("FINISHED"))
                        {
                            numberFinisher++;

                            // Publish the result when players' time is up
                            if (numberFinisher == PlayersList.Count)
                            {
                                PublishResult();
                            }
                        }
                    }
                    else if (dataString == "Closed")
                    {
                        // Remove clientSocket out of clientList
                        clientList.Remove(clientSocket);

                        //btnStart_Next.Enabled = false;
                        lbConnectionStatus.Text =
                               $"{clientList.Count} client(s) connected";
                    }
                    else
                    {
                        var keyIP = clientSocket.RemoteEndPoint;

                        //update server-side player scores
                        foreach (Player player in PlayersList)
                        {
                            if (player.PlayerIP == keyIP)
                            {
                                player.PlayerScore = int.Parse(dataString);
                            }
                        }
                    }
                }
            }
            catch
            {
                clientList.Remove(clientSocket);
                clientSocket.Close();
            }
        }

        private void AddMessage(string str)
        {
            if (firstMessage)
            {
                firstMessage = false;
                richTextBox1.SelectedText = str;
            }
            else 
                richTextBox1.SelectedText = Environment.NewLine + str;
            
            txtMessage.Text = "";
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

        #endregion

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!started)
            {
                started = true;
                btnStart_Next.Text = "Next";

                foreach (Socket clientSocket in clientList)
                {
                    Send(clientSocket, $"Start_QuestsListLength:{QuestionsList.Count}");
                }

                lbConnectionStatus.Text = $"{clientList.Count} client(s) connected";

                CloseServer();
                return;
            }

            if (index < QuestionsList.Count - 1)
            {
                index++;
                ShowQuestion(index);

                // send to all clients
                foreach (Socket clientSoc in clientList)
                {
                    Send(clientSoc, NextQuestion(index));
                }
            }
            
            if (index == QuestionsList.Count - 1)
            {
                btnStart_Next.Enabled = false;
                btnResult.Enabled = true;
            }

            // Add space line 
            AddMessage("");

            // update question number
            lbIndexQuest.Text = $"Question {index + 1} / {QuestionsList.Count}";
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            foreach (Socket socket in clientList)
            {
                SendMessage(socket);
            }
            AddMessage("You:  " + txtMessage.Text.Trim());
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (Socket socket in clientList)
                {
                    SendMessage(socket);
                }
                AddMessage("You:  " + txtMessage.Text.Trim());
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            PublishResult();
        }

        private void PublishResult()
        {
            string str = Environment.NewLine;
            str += "  ================  RESULT  ================" + Environment.NewLine;

            Player winner = PlayersList[0];
            bool noWinner = true;

            if (PlayersList.Count == 1)
            {
                noWinner = false;
            }

            foreach (Player player in PlayersList)
            {
                str += $"{player.Name} 's Score:  ";
                str += player.PlayerScore.ToString() + Environment.NewLine;

                if (player.PlayerScore > winner.PlayerScore)
                {
                    winner = player;

                    // The players' scores are tied
                    if (noWinner)
                        noWinner = false;
                }
            }

            if (!noWinner)
            {
                str += $">>>>  Winner:  {winner.Name}" + Environment.NewLine;
            }
            else
            {
                str += ">>>>>  The players' scores are tied";
            }

            AddMessage(str);

            foreach (Socket clientSocket in clientList)
            {
                Send(clientSocket, "*" + str);
            }
        }

        private void questionManagement_Click(object sender, EventArgs e)
        {
            AddingQuestForm frm = new AddingQuestForm();
            frm.UpdateQuestionsList += AddingQuestfrm_UpdateQuestionsList;
            frm.Show();
        }

        private void AddingQuestfrm_UpdateQuestionsList(object sender, EventArgs e)
        {
            globals.GetAllQuestions(ref QuestionsList);
            lbIndexQuest.Text = $"Question {index + 1} / {QuestionsList.Count}";

            if (index == QuestionsList.Count - 2)
            {
                btnStart_Next.Enabled = true;
            }

            foreach(Socket clientSocket in clientList)
            {
                Send(clientSocket, $"Update_Quests_QuestsListLength:{QuestionsList.Count}");
            }
        }
    }
}
