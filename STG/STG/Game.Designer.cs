namespace STG
{
    partial class Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.Update = new System.Windows.Forms.Timer(this.components);
            this.PlayBulletPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.FunctionObjTimer = new System.Windows.Forms.Timer(this.components);
            this.labelContext = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnLeave = new System.Windows.Forms.Button();
            this.btnStoreGrade = new System.Windows.Forms.Button();
            this.labelGameOver = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelAttack = new System.Windows.Forms.Label();
            this.labelOP = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.labelScore = new System.Windows.Forms.Label();
            this.BGMPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.labelHP = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PlayBulletPlayer)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BGMPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Update
            // 
            this.Update.Enabled = true;
            this.Update.Interval = 5;
            this.Update.Tick += new System.EventHandler(this.FixedUpdate);
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
            // FunctionObjTimer
            // 
            this.FunctionObjTimer.Enabled = true;
            this.FunctionObjTimer.Interval = 3000;
            this.FunctionObjTimer.Tick += new System.EventHandler(this.FunctionObjTimer_Tick);
            // 
            // labelContext
            // 
            this.labelContext.BackColor = System.Drawing.Color.Teal;
            this.labelContext.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelContext.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContext.Location = new System.Drawing.Point(15, 518);
            this.labelContext.Name = "labelContext";
            this.labelContext.Size = new System.Drawing.Size(593, 103);
            this.labelContext.TabIndex = 4;
            this.labelContext.Text = "label4";
            this.labelContext.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelContext_MouseClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.btnLeave);
            this.panel3.Controls.Add(this.btnStoreGrade);
            this.panel3.Controls.Add(this.labelGameOver);
            this.panel3.Location = new System.Drawing.Point(132, 147);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(346, 318);
            this.panel3.TabIndex = 11;
            this.panel3.Visible = false;
            // 
            // btnLeave
            // 
            this.btnLeave.BackgroundImage = global::STG.Properties.Resources.exit;
            this.btnLeave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLeave.Location = new System.Drawing.Point(101, 245);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(133, 44);
            this.btnLeave.TabIndex = 13;
            this.btnLeave.UseVisualStyleBackColor = true;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // btnStoreGrade
            // 
            this.btnStoreGrade.BackgroundImage = global::STG.Properties.Resources.score;
            this.btnStoreGrade.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStoreGrade.Location = new System.Drawing.Point(82, 156);
            this.btnStoreGrade.Name = "btnStoreGrade";
            this.btnStoreGrade.Size = new System.Drawing.Size(175, 53);
            this.btnStoreGrade.TabIndex = 12;
            this.btnStoreGrade.UseVisualStyleBackColor = true;
            this.btnStoreGrade.Click += new System.EventHandler(this.btnStoreGrade_Click);
            // 
            // labelGameOver
            // 
            this.labelGameOver.BackColor = System.Drawing.Color.Black;
            this.labelGameOver.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGameOver.ForeColor = System.Drawing.Color.Silver;
            this.labelGameOver.Location = new System.Drawing.Point(23, 39);
            this.labelGameOver.Name = "labelGameOver";
            this.labelGameOver.Size = new System.Drawing.Size(304, 74);
            this.labelGameOver.TabIndex = 11;
            this.labelGameOver.Text = "Game  Over";
            this.labelGameOver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.labelContext);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-7, -29);
            this.panel1.MaximumSize = new System.Drawing.Size(819, 633);
            this.panel1.MinimumSize = new System.Drawing.Size(819, 633);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(819, 633);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Controls.Add(this.labelAttack);
            this.panel2.Controls.Add(this.labelOP);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.trackBarVolume);
            this.panel2.Controls.Add(this.labelScore);
            this.panel2.Controls.Add(this.BGMPlayer);
            this.panel2.Controls.Add(this.labelHP);
            this.panel2.Controls.Add(this.labelTime);
            this.panel2.Location = new System.Drawing.Point(617, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(201, 603);
            this.panel2.TabIndex = 2;
            // 
            // labelAttack
            // 
            this.labelAttack.AutoSize = true;
            this.labelAttack.BackColor = System.Drawing.Color.Transparent;
            this.labelAttack.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelAttack.ForeColor = System.Drawing.Color.White;
            this.labelAttack.Location = new System.Drawing.Point(147, 493);
            this.labelAttack.Name = "labelAttack";
            this.labelAttack.Size = new System.Drawing.Size(25, 26);
            this.labelAttack.TabIndex = 17;
            this.labelAttack.Text = "1";
            this.labelAttack.Visible = false;
            // 
            // labelOP
            // 
            this.labelOP.AutoSize = true;
            this.labelOP.BackColor = System.Drawing.Color.Transparent;
            this.labelOP.Font = new System.Drawing.Font("Microsoft JhengHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelOP.ForeColor = System.Drawing.Color.White;
            this.labelOP.Location = new System.Drawing.Point(8, 492);
            this.labelOP.Name = "labelOP";
            this.labelOP.Size = new System.Drawing.Size(43, 26);
            this.labelOP.TabIndex = 16;
            this.labelOP.Text = "No";
            this.labelOP.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::STG.Properties.Resources.Attack;
            this.pictureBox2.Location = new System.Drawing.Point(128, 411);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(70, 70);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft JhengHei", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 444);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 37);
            this.label1.TabIndex = 14;
            this.label1.Text = "OP";
            this.label1.Visible = false;
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.BackColor = System.Drawing.Color.Black;
            this.trackBarVolume.Location = new System.Drawing.Point(13, 275);
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(165, 45);
            this.trackBarVolume.TabIndex = 13;
            this.trackBarVolume.Visible = false;
            this.trackBarVolume.Scroll += new System.EventHandler(this.trackBarVolume_Scroll);
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.BackColor = System.Drawing.Color.Transparent;
            this.labelScore.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScore.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelScore.Location = new System.Drawing.Point(16, 231);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(60, 27);
            this.labelScore.TabIndex = 12;
            this.labelScore.Text = "XXX";
            // 
            // BGMPlayer
            // 
            this.BGMPlayer.Enabled = true;
            this.BGMPlayer.Location = new System.Drawing.Point(21, 14);
            this.BGMPlayer.Name = "BGMPlayer";
            this.BGMPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("BGMPlayer.OcxState")));
            this.BGMPlayer.Size = new System.Drawing.Size(75, 23);
            this.BGMPlayer.TabIndex = 9;
            this.BGMPlayer.Visible = false;
            // 
            // labelHP
            // 
            this.labelHP.AutoSize = true;
            this.labelHP.BackColor = System.Drawing.Color.Transparent;
            this.labelHP.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHP.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelHP.Location = new System.Drawing.Point(16, 158);
            this.labelHP.Name = "labelHP";
            this.labelHP.Size = new System.Drawing.Size(60, 27);
            this.labelHP.TabIndex = 5;
            this.labelHP.Text = "XXX";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.ForeColor = System.Drawing.Color.Linen;
            this.labelTime.Location = new System.Drawing.Point(8, 365);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(60, 27);
            this.labelTime.TabIndex = 3;
            this.labelTime.Text = "XXX";
            this.labelTime.Visible = false;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 601);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.PlayBulletPlayer)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BGMPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Update;
        private AxWMPLib.AxWindowsMediaPlayer PlayBulletPlayer;

        private System.Windows.Forms.Timer FunctionObjTimer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private System.Windows.Forms.Label labelScore;
        private AxWMPLib.AxWindowsMediaPlayer BGMPlayer;
        private System.Windows.Forms.Label labelHP;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelContext;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLeave;
        private System.Windows.Forms.Button btnStoreGrade;
        private System.Windows.Forms.Label labelGameOver;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelAttack;
        private System.Windows.Forms.Label labelOP;

        //Bprivate AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    }
}

