using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KlinikOtomasyonu1
{
    public partial class hastakayıtol : Form
    {
        public hastakayıtol()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into hastabilgileri (adi,soyadi,tc_no,Hastatelefon,Hastasifre,cinsiyet,dogum_tarihi) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", textBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox2.Text);
            komut.Parameters.AddWithValue("@p3", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p4", maskedTextBox2.Text);
            komut.Parameters.AddWithValue("@p5", textBox3.Text);
            komut.Parameters.AddWithValue("@p6", comboBox1.Text);
            komut.Parameters.AddWithValue("@p7", maskedTextBox3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Gerçekleşmiştir Şifreniz: " + textBox3.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hastagiris fr = Application.OpenForms["hastagiris"] as hastagiris;
            if (fr != null)
            {
                fr.WindowState = FormWindowState.Normal;
                fr.Focus();
            }
            else
            {
                fr = new hastagiris();
                fr.Show();
            }
            this.Hide();
        }

        private void hastakayıtol_Load(object sender, EventArgs e)
        {

        }
    }
}