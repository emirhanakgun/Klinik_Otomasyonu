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
    public partial class sekreterpaneli : Form
    {
        public sekreterpaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();  
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            randevual fr=new randevual();
            fr.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyuru (duyuru) values (@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", richTextBox1.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru oluşturulmuştur..Sağlıklı günler dileriz :))))", "Duyuru Bildirimi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            yatısver fr=new yatısver();
            fr.Show();
            this.Hide();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            randevugor fr=new randevugor();

            fr.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tahlilistendi fr=new tahlilistendi();
            fr.Show();  
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           ilacekle fr=new ilacekle();  
            fr.Show();  
            this.Hide();    
        }

        private void sekreterpaneli_Load(object sender, EventArgs e)
        {

        }
    }
}
