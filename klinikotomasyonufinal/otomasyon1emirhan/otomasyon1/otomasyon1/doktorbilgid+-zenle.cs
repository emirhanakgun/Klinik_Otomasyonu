using KlinikOtomasyonu1;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KlinikOtomasyonu1
{
    public partial class doktorbilgidüzenle : Form
    {
        public doktorbilgidüzenle()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut45 = new SqlCommand("insert into doktorlar(doktor_Adi,doktor_soyadi,Doktorbrans,doktor_tc_no,Doktor_sifre) values (@d1,@d2,@d3,@d4,@d5)", bgl.baglanti());
            komut45.Parameters.AddWithValue("@d1", textBox1.Text);
            komut45.Parameters.AddWithValue("@d2", textBox2.Text);
            komut45.Parameters.AddWithValue("@d3", comboBox1.Text);
            komut45.Parameters.AddWithValue("@d4", maskedTextBox1.Text);
            komut45.Parameters.AddWithValue("@d5", textBox3.Text);
            komut45.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi", "BİLGİ!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komut12 = new SqlCommand("Delete from doktorlar where doktor_tc_no=@p1", bgl.baglanti());
            komut12.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut12.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update doktorlar set doktor_Adi=@c1,doktor_soyadi=@c2,Doktorbrans=@c3,doktor_tc_no=@c4,doktor_sifre=@c5 where doktor_tc_no=@c4", bgl.baglanti());
            komut.Parameters.AddWithValue("@c1", textBox1.Text);
            komut.Parameters.AddWithValue("@c2", textBox2.Text);
            komut.Parameters.AddWithValue("@c3", comboBox1.Text);
            komut.Parameters.AddWithValue("@c4", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@c5", textBox3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void doktorbilgidüzenle_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            //Branşları comboxa aktarma
            SqlCommand komut1 = new SqlCommand("Select brans_adi From branslar", bgl.baglanti());
            SqlDataReader dr3 = komut1.ExecuteReader();
            while (dr3.Read())
            {
                _ = comboBox1.Items.Add(dr3[0]);
            }
            bgl.baglanti().Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[secilen].Cells[8].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Doktordetay fr= new Doktordetay();
            fr.Show();
            this.Hide();
        }
    }
    }
    






       
      
       


      