using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Media;

namespace STG
{
    public partial class Form4 : Form
    {
        int addScore, addTime;
        SoundPlayer EnterBtn;
        SoundPlayer ClickBtn;

        public Form4(int Score, int Time)
        {
            InitializeComponent();
            EnterBtn = new SoundPlayer(Application.StartupPath + @"\SFX\click_touch.wav");
            btnCancel.MouseEnter += btnSend_MouseEnter;
            ClickBtn = new SoundPlayer(Application.StartupPath + @"\SFX\click_click.wav");
            btnCancel.MouseClick += btnSend_MouseClick;

            addScore = Score;
            addTime = Time;
            labelScore.Text = Score.ToString();
            labelTime.Text = Time.ToString();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            DateTime savenow = DateTime.Now;
            if (txtName.Text == "")
            {
                MessageBox.Show("Please enter your name!", "Message");
                btnSend.Enabled = true;
            }
            else
            {
                try
                {
                    SqlConnection db = new SqlConnection();
                    string p = AppDomain.CurrentDomain.BaseDirectory;
                    p = p.Replace("\\bin\\Debug", "");
                    AppDomain.CurrentDomain.SetData("DataDirectory", p);
                    db.ConnectionString = @"Data Source=(LocalDB)\v11.0;" +
                        "AttachDbFilename=|DataDirectory|GradeDatabase.mdf;" +
                        "Integrated Security=True";
                    db.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = db;
                    cmd.CommandText = "INSERT INTO Grade(Name,Score,Time,Date) VALUES(@Name,@Score,@Time,@Date)";
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Score", addScore);
                    cmd.Parameters.AddWithValue("@Time", addTime);
                    cmd.Parameters.AddWithValue("@Date", savenow.ToString("yyyy-MM-dd HH:mm"));

                    cmd.ExecuteNonQuery();
                    db.Close();
                    Form4_Load(sender, e);
                    btnSend.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn.Play();
        }

        private void btnSend_MouseClick(object sender, MouseEventArgs e)
        {
            ClickBtn.Play();
        }
    }
}
