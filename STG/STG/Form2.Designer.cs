namespace STG
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnLeave = new System.Windows.Forms.Button();
            this.btnCheckGrade = new System.Windows.Forms.Button();
            this.BGMStart = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.BGMStart)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.BackgroundImage = global::STG.Properties.Resources.start1;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStart.Location = new System.Drawing.Point(401, 246);
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
            this.btnLeave.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLeave.Location = new System.Drawing.Point(418, 443);
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
            this.btnCheckGrade.Font = new System.Drawing.Font("微軟正黑體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCheckGrade.Location = new System.Drawing.Point(400, 339);
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
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::STG.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(984, 586);
            this.Controls.Add(this.BGMStart);
            this.Controls.Add(this.btnCheckGrade);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.btnStart);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
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
    }
}