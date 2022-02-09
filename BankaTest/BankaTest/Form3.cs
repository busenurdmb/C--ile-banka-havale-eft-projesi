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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-493DFJA\SQLEXPRESS;Initial Catalog=DbBankaTest;Integrated Security=True");
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand kmt = new SqlCommand("insert into TBLKISILER (AD,SOYAD,TC,TELEFON,HESAPNO,SIFRE) values (@P1,@P2,@P3,@P4,@P5,@P6)", baglanti);
            kmt.Parameters.AddWithValue("@P1", txtad.Text);
            kmt.Parameters.AddWithValue("@P2", txtsoyad.Text);
            kmt.Parameters.AddWithValue("@P3", msktc.Text);
            kmt.Parameters.AddWithValue("@P4", msktel.Text);
            kmt.Parameters.AddWithValue("@P5", mskhesap.Text);
            kmt.Parameters.AddWithValue("@P6", txtsıfre.Text);
            kmt.ExecuteNonQuery();
            baglanti.Close();

            baglanti.Open();
            SqlCommand kmt1 = new SqlCommand("insert into TBLHESAP (HESAPNO) values (@P1)", baglanti);
            kmt1.Parameters.AddWithValue("@P1", mskhesap.Text);
           
            kmt1.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("SİSTEME KAYDEDİLDİ");




        }

        private void button2_Click(object sender, EventArgs e)
        {
           int sorgu;
            Random rast = new Random();
            int sayi = rast.Next(100000,1000000);
            mskhesap.Text = sayi.ToString();
            baglanti.Open();
            SqlCommand kmt = new SqlCommand("select * from TBLHESAP", baglanti);
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
               
                sorgu = Convert.ToInt32(dr[0]);
                if (sayi == sorgu)
                {
                    mskhesap.Text = sayi.ToString();
                    MessageBox.Show("aynı Hesap no mevcut var");
                    int sayi2 = rast.Next(100000, 1000000);
                    sayi = sayi2;
                    mskhesap.Text = sayi.ToString();


                }
                }
            baglanti.Close();
            
        
           

            

         




        }
    }
}
