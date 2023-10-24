using KlinikOtomasyonu1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace KlinikOtomasyonu1
{
    public partial class hastadetay : Form
    {
        private string connectionString = baglantistring.ConnectionString;
        public hastadetay()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string tc;
        public string adı;
        private void hastadetay_Load(object sender, EventArgs e)
        {

            label1.Text = tc;
            //ad soyad çekme
            SqlCommand komut = new SqlCommand("Select adi,soyadi From hastabilgileri Where tc_no=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", label1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                adsoyad.Text = dr[0] + " " + dr[1];
            }
            adı = adsoyad.Text;
            bgl.baglanti().Close();
            // Randevu Geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter sa = new SqlDataAdapter("Select * From randevu where tc_no=" + tc, bgl.baglanti());
            sa.Fill(dt);
            dataGridView1.DataSource = dt;
            // Branşları Çekme
        }





        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          

           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            randevual fr = new randevual();
            fr.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Kullanıcının girdiği TC no değerini alın
            string tcNo = label1.Text;

            // SQL sorgusunu hazırlayın
            string sqlSorgu = "SELECT oda FROM odalar WHERE tc_no = @tcNo";

            // SqlConnection nesnesi oluşturun ve bağlantıyı açın
            using (SqlConnection baglanti = new SqlConnection(connectionString))
            {
                baglanti.Open();

                // SqlCommand nesnesi oluşturun ve parametreleri ayarlayın
                using (SqlCommand komut = new SqlCommand(sqlSorgu, baglanti))
                {
                    komut.Parameters.AddWithValue("@tcNo", tcNo);

                    // Sorguyu çalıştırın ve sonucu MessageBox ile gösterin
                    object sonuc = komut.ExecuteScalar();
                    if (sonuc != null)
                    {
                        string odaNumarasi = sonuc.ToString();
                        MessageBox.Show("Kullanıcı " + odaNumarasi + " numaralı odada kalmaktadır.", "ODA YATIŞ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kişi herhangi bir odada kalmamaktadır.", "HATA!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}