using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;

namespace STG
{
    public partial class Form2 : Form
    {
        Form1 NewGame;
        SoundPlayer EnterBtn;
        SoundPlayer ClickBtn;

        public Form2()
        {
            InitializeComponent();
            EnterBtn = new SoundPlayer(Application.StartupPath + @"\SFX\click_touch.wav");
            btnCheckGrade.MouseEnter += btnStart_MouseEnter;
            btnLeave.MouseEnter += btnStart_MouseEnter;
            btnInfinite.MouseEnter += btnStart_MouseEnter;
            btnGuide.MouseEnter += btnStart_MouseEnter;
            ClickBtn = new SoundPlayer(Application.StartupPath + @"\SFX\click_click.wav");
            btnCheckGrade.MouseClick += btnStart_MouseClick;
            btnLeave.MouseClick += btnStart_MouseClick;
            btnInfinite.MouseClick += btnStart_MouseClick;
            btnGuide.MouseClick += btnStart_MouseClick;
            BGMStart.URL = @"SFX\STB.wav";
            BGMStart.settings.volume = 70;
            BGMStart.settings.setMode("Loop", true);
            BGMStart.Ctlcontrols.play();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            BGMStart.Ctlcontrols.stop();
            NewGame = new Form1(10);
            NewGame.Show();
        }

        private void btnCheckGrade_Click(object sender, EventArgs e)
        {
            Form3 checkform = new Form3();
            checkform.Show();
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStart_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn.Play();
        }

        private void btnStart_MouseClick(object sender, MouseEventArgs e)
        {
            ClickBtn.Play();
       }

        private void Form2_MouseEnter(object sender, EventArgs e)
        {
            BGMStart.Ctlcontrols.play();
        }

        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            BGMStart.Ctlcontrols.play();
        }

        private void btnInfinite_Click(object sender, EventArgs e)
        {
            BGMStart.Ctlcontrols.stop();
            NewGame = new Form1(1000000);
            NewGame.Show();
        }

        private void btnGuide_Click(object sender, EventArgs e)
        {
            Form5 GuideF = new Form5();
            GuideF.Show();
        }
       
    }
}
