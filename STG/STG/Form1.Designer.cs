namespace STG
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Update = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelContext = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BGMPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelHP = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.PlayBulletPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelGameOver = new System.Windows.Forms.Label();
            this.btnLeave = new System.Windows.Forms.Button();
            this.btnStoreGrade = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BGMPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayBulletPlayer)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Update
            // 
            this.Update.Enabled = true;
            this.Update.Interval = 15;
            this.Update.Tick += new System.EventHandler(this.FixedUpdate);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.labelContext);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-7, -29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(819, 633);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick_1);
            // 
            // labelContext
            // 
            this.labelContext.BackColor = System.Drawing.Color.Teal;
            this.labelContext.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelContext.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelContext.Location = new System.Drawing.Point(11, 518);
            this.labelContext.Name = "labelContext";
            this.labelContext.Size = new System.Drawing.Size(593, 103);
            this.labelContext.TabIndex = 4;
            this.labelContext.Text = "label4";
            this.labelContext.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.labelScore);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.BGMPlayer);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.labelHP);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.labelTime);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(618, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(201, 603);
            this.panel2.TabIndex = 2;
            // 
            // BGMPlayer
            // 
            this.BGMPlayer.Enabled = true;
            this.BGMPlayer.Location = new System.Drawing.Point(17, 470);
            this.BGMPlayer.Name = "BGMPlayer";
            this.BGMPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("BGMPlayer.OcxState")));
            this.BGMPlayer.Size = new System.Drawing.Size(75, 23);
            this.BGMPlayer.TabIndex = 9;
            this.BGMPlayer.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(84, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "label6";
            // 
            // labelHP
            // 
            this.labelHP.AutoSize = true;
            this.labelHP.Font = new System.Drawing.Font("Segoe Print", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHP.ForeColor = System.Drawing.Color.Red;
            this.labelHP.Location = new System.Drawing.Point(98, 174);
            this.labelHP.Name = "labelHP";
            this.labelHP.Size = new System.Drawing.Size(80, 47);
            this.labelHP.TabIndex = 5;
            this.labelHP.Text = "XXX";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe Print", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Olive;
            this.label4.Location = new System.Drawing.Point(20, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 47);
            this.label4.TabIndex = 4;
            this.label4.Text = "HP:";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Segoe Print", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.Orange;
            this.labelTime.Location = new System.Drawing.Point(96, 317);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(80, 47);
            this.labelTime.TabIndex = 3;
            this.labelTime.Text = "XXX";
            // 
            // PlayBulletPlayer
            // 
            this.PlayBulletPlayer.Enabled = true;
            this.PlayBulletPlayer.Location = new System.Drawing.Point(27, 244);
            this.PlayBulletPlayer.Name = "PlayBulletPlayer";
            this.PlayBulletPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("PlayBulletPlayer.OcxState")));
            this.PlayBulletPlayer.Size = new System.Drawing.Size(75, 23);
            this.PlayBulletPlayer.TabIndex = 8;
            this.PlayBulletPlayer.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.btnLeave);
            this.panel3.Controls.Add(this.btnStoreGrade);
            this.panel3.Controls.Add(this.labelGameOver);
            this.panel3.Location = new System.Drawing.Point(126, 146);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(346, 318);
            this.panel3.TabIndex = 11;
            this.panel3.Visible = false;
            // 
            // labelGameOver
            // 
            this.labelGameOver.BackColor = System.Drawing.Color.Black;
            this.labelGameOver.Font = new System.Drawing.Font("Mekanik LET", 57.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGameOver.ForeColor = System.Drawing.Color.Silver;
            this.labelGameOver.Location = new System.Drawing.Point(23, 39);
            this.labelGameOver.Name = "labelGameOver";
            this.labelGameOver.Size = new System.Drawing.Size(304, 74);
            this.labelGameOver.TabIndex = 11;
            this.labelGameOver.Text = "Game  Over";
            this.labelGameOver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLeave
            // 
            this.btnLeave.BackgroundImage = global::STG.Properties.Resources.leave3;
            this.btnLeave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLeave.Location = new System.Drawing.Point(36, 232);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(288, 73);
            this.btnLeave.TabIndex = 13;
            this.btnLeave.UseVisualStyleBackColor = true;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // btnStoreGrade
            // 
            this.btnStoreGrade.BackgroundImage = global::STG.Properties.Resources.storegrade;
            this.btnStoreGrade.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStoreGrade.Location = new System.Drawing.Point(36, 153);
            this.btnStoreGrade.Name = "btnStoreGrade";
            this.btnStoreGrade.Size = new System.Drawing.Size(288, 73);
            this.btnStoreGrade.TabIndex = 12;
            this.btnStoreGrade.UseVisualStyleBackColor = true;
            this.btnStoreGrade.Click += new System.EventHandler(this.btnStoreGrade_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::STG.Properties.Resources.clock;
            this.pictureBox2.Location = new System.Drawing.Point(26, 314);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 50);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(3, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(615, 603);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe Print", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Teal;
            this.label3.Location = new System.Drawing.Point(4, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 47);
            this.label3.TabIndex = 11;
            this.label3.Text = "Score:";
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Font = new System.Drawing.Font("Segoe Print", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScore.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelScore.Location = new System.Drawing.Point(98, 243);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(80, 47);
            this.labelScore.TabIndex = 12;
            this.labelScore.Text = "XXX";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 601);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BGMPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayBulletPlayer)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Update;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelContext;
        private System.Windows.Forms.Label labelHP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AxWMPLib.AxWindowsMediaPlayer PlayBulletPlayer;
        private AxWMPLib.AxWindowsMediaPlayer BGMPlayer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLeave;
        private System.Windows.Forms.Button btnStoreGrade;
        private System.Windows.Forms.Label labelGameOver;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label label3;
        //Bprivate AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    }
}

