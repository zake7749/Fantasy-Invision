﻿using System;
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
    public partial class Form3 : Form
    {
        SoundPlayer EnterBtn;
        SoundPlayer ClickBtn;

        public Form3()
        {
            InitializeComponent();
            EnterBtn = new SoundPlayer(Application.StartupPath + @"\SFX\click_touch.wav");
            btnLeave.MouseEnter += btnDelete_MouseEnter;
            ClickBtn = new SoundPlayer(Application.StartupPath + @"\SFX\click_click.wav");
            btnLeave.MouseClick += btnDelete_MouseClick;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SqlConnection db = new SqlConnection();
            string p = AppDomain.CurrentDomain.BaseDirectory;
            p = p.Replace("\\bin\\Debug", "");
            AppDomain.CurrentDomain.SetData("DataDirectory", p);
            db.ConnectionString = @"Data Source=(LocalDB)\v11.0;" +
                "AttachDbFilename=|DataDirectory|GradeDataBase.mdf;" +
                "Integrated Security=True";
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Grade", db);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(txtDelete.Text=="")
                MessageBox.Show("Please enter a name you want to delete!", "Message");
            else
            {
                try
                {
                    SqlConnection db = new SqlConnection();
                    string p = AppDomain.CurrentDomain.BaseDirectory;
                    p = p.Replace("\\bin\\Debug", "");
                    AppDomain.CurrentDomain.SetData("DataDirectory", p);
                    db.ConnectionString = @"Data Source=(LocalDB)\v11.0;" +
                        "AttachDbFilename=|DataDirectory|GradeDataBase.mdf;" +
                        "Integrated Security=True";
                    db.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = db;
                    cmd.CommandText = "DELETE FROM Grade WHERE Name=@Name";
                    cmd.Parameters.AddWithValue("@Name", txtDelete.Text);
                    cmd.ExecuteNonQuery();
                    db.Close();
                    Form3_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }        
        }

        private void btnDelete_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn.Play();
        }

        private void btnDelete_MouseClick(object sender, MouseEventArgs e)
        {
            ClickBtn.Play();
        }
    }
}
