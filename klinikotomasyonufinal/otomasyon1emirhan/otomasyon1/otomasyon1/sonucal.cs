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

namespace KlinikOtomasyonu1
{
    public partial class sonucal : Form
    {
       baglantistring bgl=new baglantistring();
        public sonucal()
        {
            InitializeComponent();
        }

        private void sonucal_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'otomasyon1DataSet12.tahlil_sonuc' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tahlil_sonucTableAdapter.Fill(this.otomasyon1DataSet12.tahlil_sonuc);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new sqlbaglantisi().baglanti())
            {

                string query = "SELECT tc_no, adi, soyadi, on_tanii FROM muayene WHERE adi LIKE '%" + textBox1.Text + "%' OR tc_no LIKE '%" + textBox1.Text + "%' OR soyadi LIKE '%" + textBox1.Text + "%' OR on_tanii LIKE '%" + textBox1.Text + "%'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Doktordetay fr = new Doktordetay();
            fr.Show();
            this.Hide();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                string selectedTC = dataGridView1.SelectedRows[0].Cells["tc_no"].Value.ToString();



               
                string query = "SELECT * FROM tahlil_kimlik WHERE tc_no = @TC";
                using (SqlConnection conn = new sqlbaglantisi().baglanti()) 
                {
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TC", selectedTC);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView2.DataSource = table;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string durum = "";
            Random rand = new Random();
            int randomSayi = rand.Next(6);
            switch (randomSayi)
            {
                case 0:
                    durum = "Sağlıklı";
                    break;
                case 1:
                    durum = "İyi";
                    break;
                case 2:
                    durum = "Değerler Normal";
                    break;
                case 3:
                    durum = "Değerler Fena değil";
                    break;
                case 4:
                    durum = "Değerler Kötü";
                    break;
                case 5:
                    durum = "Değerler Çok Kötü";
                    break;
                default:
                    durum = "Çok İyi";
                    break;
            }

            
            string selectedTahlil = dataGridView2.SelectedRows[0].Cells["tahlil"].Value.ToString();
            string selectedTC = dataGridView1.SelectedRows[0].Cells["tc_no"].Value.ToString();

           
            string query = "INSERT INTO tahlil_sonuc (tc_no, tahlil_adi, sonuc) VALUES (@TC, @Tahlil, @Sonuc)";
            using (SqlConnection conn = new sqlbaglantisi().baglanti())
            {
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TC", selectedTC);
                    cmd.Parameters.AddWithValue("@Tahlil", selectedTahlil);
                    cmd.Parameters.AddWithValue("@Sonuc", durum);
                    cmd.ExecuteNonQuery();
                }
            }

           
            query = "SELECT * FROM tahlil_sonuc WHERE tc_no = @TC AND tahlil_adi = @Tahlil";
            using (SqlConnection conn = new sqlbaglantisi().baglanti()) 
            {
               
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TC", selectedTC);
                    cmd.Parameters.AddWithValue("@Tahlil", selectedTahlil);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dataGridView3.DataSource = table;
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}