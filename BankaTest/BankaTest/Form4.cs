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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-493DFJA\SQLEXPRESS;Initial Catalog=DbBankaTest;Integrated Security=True");
        private void Form4_Load(object sender, EventArgs e)
        {
            //hesabına para yatanlar
            SqlDataAdapter da = new SqlDataAdapter("SELECT  (AD+' '+SOYAD) as 'ALICI',TUTAR FROM TBLHAREKET " +
                "inner join TBLKISILER on TBLHAREKET.ALICI=TBLKISILER.HESAPNO " , baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //hesaba para gönderenler
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT  (AD+' '+SOYAD) as 'GONDEREN',TUTAR FROM TBLHAREKET " +
                "inner join TBLKISILER on TBLHAREKET.GONDEREN=TBLKISILER.HESAPNO"  , baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
        }

        
    }
}
