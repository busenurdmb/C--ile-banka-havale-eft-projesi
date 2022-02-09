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

namespace BankaTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-493DFJA\SQLEXPRESS;Initial Catalog=DbBankaTest;Integrated Security=True");
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 fr = new Form3();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kmt = new SqlCommand("select * from TBLKISILER WHERE HESAPNO=@P1 AND SıFRE=@P2 ", baglanti);
            kmt.Parameters.AddWithValue("@P1", maskedTextBox1.Text);
            kmt.Parameters.AddWithValue("@P2", textBox2.Text);
            SqlDataReader dr = kmt.ExecuteReader();
            if (dr.Read())
            {
                Form2 frm = new Form2();
                frm.hesapno = maskedTextBox1.Text;
                frm.Show();
            }
            else
            {
                MessageBox.Show("hatalı şifre veya hesapno");
            }
            baglanti.Close();
           
           
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBoxMÜŞTERİ.Visible = false;
            groupBoxYÖNETİCİ.Visible = false;
            groupBox1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBoxMÜŞTERİ.Visible = true;
            groupBoxYÖNETİCİ.Visible = false;
            groupBox1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            groupBoxMÜŞTERİ.Visible = false;
            groupBoxYÖNETİCİ.Visible = true;
            groupBox1.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(mskkullanıcı.Text==123456.ToString() && txtsifre.Text == 123.ToString())
            {
                Form4 fr = new Form4();
                fr.Show();
            }
        }
    }
}
