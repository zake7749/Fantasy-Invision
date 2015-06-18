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

namespace STG
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO:  這行程式碼會將資料載入 'gradeDatabaseDataSet.Grade' 資料表。您可以視需要進行移動或移除。
            this.gradeTableAdapter.Fill(this.gradeDatabaseDataSet.Grade);
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
    }
}
