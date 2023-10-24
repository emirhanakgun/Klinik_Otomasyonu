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
    public partial class hastagiris : Form
    {
        public hastagiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();  
      
        private void hastagiris_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Transparent;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from hastabilgileri Where tc_no=@p1 and     sifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                hastadetay fr = new hastadetay();
                fr.tc = maskedTextBox1.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("HATALI TC & ŞİFRE", "UYARI!!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            giris fr = new giris();
            fr.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            hastakayıtol fr = new hastakayıtol();
            fr.Show();
        }
    }
}



 
       