namespace STG
{
    partial class UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnLeave = new System.Windows.Forms.Button();
            this.btnCheckGrade = new System.Windows.Forms.Button();
            this.BGMStart = new AxWMPLib.AxWindowsMediaPlayer();
            this.btnInfinite = new System.Windows.Forms.Button();
            this.btnGuide = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BGMStart)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.BackgroundImage = global::STG.Properties.Resources.start1;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart.Font = new System.Drawing.Font("Microsoft JhengHei", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStart.Location = new System.Drawing.Point(423, 108);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(172, 49);
            this.btnStart.TabIndex = 0;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            this.btnStart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnStart_MouseClick);
            this.btnStart.MouseEnter += new System.EventHandler(this.btnStart_MouseEnter);
            // 
            // btnLeave
            // 
            this.btnLeave.BackColor = System.Drawing.Color.Transparent;
            this.btnLeave.BackgroundImage = global::STG.Properties.Resources.exit;
            this.btnLeave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLeave.Font = new System.Drawing.Font("Microsoft JhengHei", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLeave.Location = new System.Drawing.Point(442, 476);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(133, 45);
            this.btnLeave.TabIndex = 1;
            this.btnLeave.UseVisualStyleBackColor = false;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // btnCheckGrade
            // 
            this.btnCheckGrade.BackColor = System.Drawing.Color.Transparent;
            this.btnCheckGrade.BackgroundImage = global::STG.Properties.Resources.score;
            this.btnCheckGrade.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheckGrade.Font = new System.Drawing.Font("Microsoft JhengHei", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCheckGrade.Location = new System.Drawing.Point(422, 276);
            this.btnCheckGrade.Name = "btnCheckGrade";
            this.btnCheckGrade.Size = new System.Drawing.Size(173, 52);
            this.btnCheckGrade.TabIndex = 2;
            this.btnCheckGrade.UseVisualStyleBackColor = false;
            this.btnCheckGrade.Click += new System.EventHandler(this.btnCheckGrade_Click);
            // 
            // BGMStart
            // 
            this.BGMStart.Enabled = true;
            this.BGMStart.Location = new System.Drawing.Point(39, 511);
            this.BGMStart.Name = "BGMStart";
            this.BGMStart.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("BGMStart.OcxState")));
            this.BGMStart.Size = new System.Drawing.Size(214, 46);
            this.BGMStart.TabIndex = 3;
            this.BGMStart.Visible = false;
            // 
            // btnInfinite
            // 
            this.btnInfinite.BackColor = System.Drawing.Color.Transparent;
            this.btnInfinite.BackgroundImage = global::STG.Properties.Resources.Infinite;
            this.btnInfinite.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInfinite.Location = new System.Drawing.Point(422, 193);
            this.btnInfinite.Name = "btnInfinite";
            this.btnInfinite.Size = new System.Drawing.Size(173, 52);
            this.btnInfinite.TabIndex = 4;
            this.btnInfinite.UseVisualStyleBackColor = false;
            this.btnInfinite.Click += new System.EventHandler(this.btnInfinite_Click);
            // 
            // btnGuide
            // 
            this.btnGuide.BackColor = System.Drawing.Color.Transparent;
            this.btnGuide.BackgroundImage = global::STG.Properties.Resources.Guide;
            this.btnGuide.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuide.Location = new System.Drawing.Point(422, 361);
            this.btnGuide.Name = "btnGuide";
            this.btnGuide.Size = new System.Drawing.Size(173, 52);
            this.btnGuide.TabIndex = 5;
            this.btnGuide.UseVisualStyleBackColor = false;
            this.btnGuide.Click += new System.EventHandler(this.btnGuide_Click);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::STG.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(984, 587);
            this.Controls.Add(this.btnGuide);
            this.Controls.Add(this.btnInfinite);
            this.Controls.Add(this.BGMStart);
            this.Controls.Add(this.btnCheckGrade);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.btnStart);
            this.MaximumSize = new System.Drawing.Size(1000, 625);
            this.MinimumSize = new System.Drawing.Size(1000, 625);
            this.Name = "UI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "幻靈視界";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.BGMStart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnLeave;
        private System.Windows.Forms.Button btnCheckGrade;
        private AxWMPLib.AxWindowsMediaPlayer BGMStart;
        private System.Windows.Forms.Button btnInfinite;
        private System.Windows.Forms.Button btnGuide;
    }
}