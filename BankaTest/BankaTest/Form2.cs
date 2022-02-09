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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        public string hesapno;
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-493DFJA\SQLEXPRESS;Initial Catalog=DbBankaTest;Integrated Security=True");
        void LİSTELE()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT  (AD+''+SOYAD) as 'ALICI',TUTAR FROM TBLHAREKET " +
               "inner join TBLKISILER on TBLHAREKET.ALICI=TBLKISILER.HESAPNO WHERE GONDEREN=" + hesapno, baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            SqlDataAdapter da1 = new SqlDataAdapter("SELECT  (AD+''+SOYAD) as 'GONDEREN',TUTAR FROM TBLHAREKET " +
                "inner join TBLKISILER on TBLHAREKET.GONDEREN=TBLKISILER.HESAPNO WHERE ALICI=" + hesapno, baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            labelHESAP.Text = hesapno;
            baglanti.Open();
            SqlCommand kmt = new SqlCommand("select * from TBLKISILER where HESAPNO=@P1", baglanti);
            kmt.Parameters.AddWithValue("@P1", hesapno);
            SqlDataReader dr = kmt.ExecuteReader();
            while (dr.Read())
            {
                labelAD.Text = dr[1]+" "+dr[2];
                labelTC.Text = dr[3].ToString();
                labeTEL.Text = dr[4].ToString();

            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand kmt1 = new SqlCommand("select * from TBLHESAP where HESAPNO=@T1", baglanti);
            kmt1.Parameters.AddWithValue("@T1", hesapno);
            SqlDataReader dr1 = kmt1.ExecuteReader();
            while (dr1.Read())
            {
                labelbakiye.Text = dr1[1].ToString();

            }
            baglanti.Close();


            LİSTELE();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            //gönderen kişinin para azalışı
            baglanti.Open();
            SqlCommand kmt = new SqlCommand("update TBLHESAP SET BAKIYE=BAKIYE-@P1 WHERE HESAPNO=@P2 ", baglanti);
            kmt.Parameters.AddWithValue("@P1", decimal.Parse(txttutar.Text));
            kmt.Parameters.AddWithValue("@P2", hesapno);
            kmt.ExecuteNonQuery();
            baglanti.Close();

            //alıcının para artışı
            baglanti.Open();
            SqlCommand kmt1 = new SqlCommand("update TBLHESAP SET BAKIYE=BAKIYE+@P1 WHERE HESAPNO=@P2 ", baglanti);
            kmt1.Parameters.AddWithValue("@P1", decimal.Parse(txttutar.Text));
            kmt1.Parameters.AddWithValue("@P2", mskhesapno.Text);
            kmt1.ExecuteNonQuery();
            baglanti.Close();
            //hareket
            baglanti.Open();
            SqlCommand kmt2 = new SqlCommand("insert into TBLHAREKET (GONDEREN,ALICI,TUTAR) values (@p1,@p2,@p3)", baglanti);
            kmt2.Parameters.AddWithValue("@p1", hesapno);
            kmt2.Parameters.AddWithValue("@p2", mskhesapno.Text);
            kmt2.Parameters.AddWithValue("@p3", decimal.Parse(txttutar.Text));
            kmt2.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("işlem gerçekleşti");
            LİSTELE();


        }
    }
}
