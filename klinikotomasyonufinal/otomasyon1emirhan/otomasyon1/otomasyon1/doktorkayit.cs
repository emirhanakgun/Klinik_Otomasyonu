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
    public partial class doktorkayit : Form
    {
        public doktorkayit()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void doktorkayit_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Transparent;

           
            SqlDataAdapter da = new SqlDataAdapter("SELECT brans_adi FROM branslar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);

            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "brans_adi";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into doktorlar (Doktor_adi,Doktor_soyadi,Doktor_tc_no,doktor_cinsiyet,doktorbrans,Doktor_sifre) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", textBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox2.Text);
            komut.Parameters.AddWithValue("@p3", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p4", comboBox1.Text);
            komut.Parameters.AddWithValue("@p5", comboBox2.Text);
            komut.Parameters.AddWithValue("@p6", textBox3.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Gerçekleşmiştir Şifreniz: " + textBox3.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doktorgiris fr = Application.OpenForms["doktorgiris"] as doktorgiris;
            if (fr != null)
            {
                fr.WindowState = FormWindowState.Normal;
                fr.Focus();
            }
            else
            {
                fr = new doktorgiris();
                fr.Show();
            }
            this.Hide();
        }
    }
}