namespace ClientSide
{
    partial class ClientSide
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCurrentScore = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtHighestScore = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ucAnswerBtn1 = new ucAnswerBtn();
            this.lbCountdown = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNewQuest = new System.Windows.Forms.TextBox();
            this.lbIndexQuest = new System.Windows.Forms.Label();
            this.panelOverlay = new System.Windows.Forms.Panel();
            this.lbWaitingStart = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelOverlay.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(758, 489);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(74, 36);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(10, 10);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(317, 46);
            this.txtMessage.TabIndex = 12;
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(187, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 18);
            this.label3.TabIndex = 16;
            this.label3.Text = "Current Score :";
            // 
            // txtCurrentScore
            // 
            this.txtCurrentScore.AutoSize = true;
            this.txtCurrentScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentScore.Location = new System.Drawing.Point(306, 19);
            this.txtCurrentScore.Name = "txtCurrentScore";
            this.txtCurrentScore.Size = new System.Drawing.Size(16, 18);
            this.txtCurrentScore.TabIndex = 17;
            this.txtCurrentScore.Text = "0";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(10, 10);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(317, 305);
            this.richTextBox1.TabIndex = 19;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(489, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 18);
            this.label1.TabIndex = 23;
            this.label1.Text = "Chat";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.richTextBox1);
            this.panel2.Location = new System.Drawing.Point(493, 82);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(339, 327);
            this.panel2.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.txtMessage);
            this.panel3.Location = new System.Drawing.Point(493, 415);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(339, 68);
            this.panel3.TabIndex = 26;
            // 
            // txtHighestScore
            // 
            this.txtHighestScore.AutoSize = true;
            this.txtHighestScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHighestScore.Location = new System.Drawing.Point(306, 49);
            this.txtHighestScore.Name = "txtHighestScore";
            this.txtHighestScore.Size = new System.Drawing.Size(16, 18);
            this.txtHighestScore.TabIndex = 14;
            this.txtHighestScore.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(187, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 18);
            this.label2.TabIndex = 15;
            this.label2.Text = "Highest Score :";
            // 
            // ucAnswerBtn1
            // 
            this.ucAnswerBtn1.AnswerA = "A";
            this.ucAnswerBtn1.AnswerB = "B";
            this.ucAnswerBtn1.AnswerC = "C";
            this.ucAnswerBtn1.AnswerD = "D";
            this.ucAnswerBtn1.Location = new System.Drawing.Point(35, 271);
            this.ucAnswerBtn1.Name = "ucAnswerBtn1";
            this.ucAnswerBtn1.Size = new System.Drawing.Size(262, 212);
            this.ucAnswerBtn1.TabIndex = 18;
            // 
            // lbCountdown
            // 
            this.lbCountdown.AutoSize = true;
            this.lbCountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCountdown.ForeColor = System.Drawing.Color.Red;
            this.lbCountdown.Location = new System.Drawing.Point(381, 38);
            this.lbCountdown.Name = "lbCountdown";
            this.lbCountdown.Size = new System.Drawing.Size(82, 31);
            this.lbCountdown.TabIndex = 20;
            this.lbCountdown.Text = "00:10";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtNewQuest);
            this.panel1.Location = new System.Drawing.Point(35, 82);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(428, 167);
            this.panel1.TabIndex = 24;
            // 
            // txtNewQuest
            // 
            this.txtNewQuest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNewQuest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNewQuest.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewQuest.Location = new System.Drawing.Point(10, 10);
            this.txtNewQuest.Multiline = true;
            this.txtNewQuest.Name = "txtNewQuest";
            this.txtNewQuest.Size = new System.Drawing.Size(406, 145);
            this.txtNewQuest.TabIndex = 10;
            this.txtNewQuest.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewQuest_KeyPress);
            // 
            // lbIndexQuest
            // 
            this.lbIndexQuest.AutoSize = true;
            this.lbIndexQuest.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIndexQuest.Location = new System.Drawing.Point(32, 46);
            this.lbIndexQuest.Name = "lbIndexQuest";
            this.lbIndexQuest.Size = new System.Drawing.Size(108, 18);
            this.lbIndexQuest.TabIndex = 28;
            this.lbIndexQuest.Text = "Question 1 / 10";
            // 
            // panelOverlay
            // 
            this.panelOverlay.Controls.Add(this.lbWaitingStart);
            this.panelOverlay.Location = new System.Drawing.Point(12, 19);
            this.panelOverlay.Name = "panelOverlay";
            this.panelOverlay.Size = new System.Drawing.Size(462, 523);
            this.panelOverlay.TabIndex = 29;
            // 
            // lbWaitingStart
            // 
            this.lbWaitingStart.AutoSize = true;
            this.lbWaitingStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbWaitingStart.Location = new System.Drawing.Point(119, 259);
            this.lbWaitingStart.Name = "lbWaitingStart";
            this.lbWaitingStart.Size = new System.Drawing.Size(203, 31);
            this.lbWaitingStart.TabIndex = 30;
            this.lbWaitingStart.Text = "Waitting to start";
            // 
            // ClientSide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 554);
            this.Controls.Add(this.panelOverlay);
            this.Controls.Add(this.lbIndexQuest);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbCountdown);
            this.Controls.Add(this.ucAnswerBtn1);
            this.Controls.Add(this.txtCurrentScore);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHighestScore);
            this.Controls.Add(this.btnSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ClientSide";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Player";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientSide_FormClosed);
            this.Load += new System.EventHandler(this.ClientSide_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelOverlay.ResumeLayout(false);
            this.panelOverlay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtCurrentScore;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label txtHighestScore;
        private System.Windows.Forms.Label label2;
        private ucAnswerBtn ucAnswerBtn1;
        private System.Windows.Forms.Label lbCountdown;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtNewQuest;
        private System.Windows.Forms.Label lbIndexQuest;
        private System.Windows.Forms.Panel panelOverlay;
        private System.Windows.Forms.Label lbWaitingStart;
    }
}

