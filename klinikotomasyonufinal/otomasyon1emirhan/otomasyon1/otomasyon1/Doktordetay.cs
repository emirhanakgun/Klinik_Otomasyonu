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

namespace KlinikOtomasyonu1
{
    public partial class Doktordetay : Form
    {
        public Doktordetay()
        {
            InitializeComponent();
        }

       sqlbaglantisi bgl=new sqlbaglantisi();
        public string tcc;
        public string adıı;
        public string mail;
        public string telefon;
        private void Doktordetay_Load(object sender, EventArgs e)
        {

            string tc = oturum.Instance.TcNo;
            oturum.Instance.TcNo = tc;
            // SQL bağlantısını oluştur


            SqlConnection baglanti = bgl.baglanti();
            string tcc = label7.Text;

            
            string sql = "SELECT doktor_Adi + ' ' + doktor_soyadi as adsoyad FROM doktorlar WHERE doktor_tc_no = @tcc";

            
            SqlCommand komut = new SqlCommand(sql, baglanti);
            komut.Parameters.AddWithValue("@tcc", tcc);

            // SqlDataReader nesnesini kullanarak verileri oku ve label2'ye yazdır
            
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                label2.Text = dr["adsoyad"].ToString();
            }

            
            dr.Close();
            baglanti.Close();
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            doktorbilgidüzenle fr = new doktorbilgidüzenle();
            fr.Show();

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            doktorbilgidüzenle fr = new doktorbilgidüzenle();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ilacform fr= new ilacform();
            fr.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hastagör fr=new hastagör();
            fr.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tahlilistendi fr=new tahlilistendi();
            fr.Show();  
            this.Hide();    
        }

        private void button4_Click(object sender, EventArgs e)
        {
            yatısver fr = new yatısver();
            fr.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            duyuru fr= new duyuru();    
            fr.Show();
            this.Hide();    
        }

        private void button7_Click(object sender, EventArgs e)
        {
            muayenet fr= new muayenet();    
            fr.Show();  
            this.Hide();    
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sonucal fr = new sonucal();
            fr.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            doktorrandevugor fr=new doktorrandevugor();
            fr.Show();  
            this.Hide();    
        }
    }
}
