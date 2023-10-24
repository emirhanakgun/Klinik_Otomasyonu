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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KlinikOtomasyonu1
{
    public partial class ilacekle : Form
    {
        public ilacekle()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglanti = new sqlbaglantisi();
        private void ilacekle_Load(object sender, EventArgs e)
        {

            // TODO: Bu kod satırı 'otomasyon1DataSet14.ilaclar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.ilaclarTableAdapter.Fill(this.otomasyon1DataSet14.ilaclar);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ilacAdi = textBox1.Text;
            string connectionString = baglantistring.ConnectionString;
            string query = "INSERT INTO ilaclar (ilac_adi) VALUES (@ilacAdi)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ilacAdi", ilacAdi);
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("İlaç adı başarıyla eklendi.");
                    }
                    else
                    {
                        MessageBox.Show("İlaç adı eklenirken bir hata oluştu.");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sekreterpaneli fr=new sekreterpaneli();
            fr.Show();
            this.Hide();
        }
    }
}
