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
    public partial class muayenet : Form
    {
        private string connectionString = baglantistring.ConnectionString;
        public muayenet()
        {
            InitializeComponent();

        }



        private void sonuc_Load(object sender, EventArgs e)
        {
                

            // TODO: Bu kod satırı 'otomasyon1DataSet11.hastabilgileri' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.hastabilgileriTableAdapter.Fill(this.otomasyon1DataSet11.hastabilgileri);
            string connectionString = baglantistring.ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT tc_no, adi, soyadi FROM hastabilgileri WHERE adi LIKE '%" + textBox1.Text + "%' OR tc_no LIKE '%" + textBox1.Text + "%' OR soyadi LIKE '%" + textBox1.Text + "%'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView2.DataSource = table;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0 && dataGridView1.SelectedRows.Count > 0)
            {
                string tc_no = dataGridView2.SelectedRows[0].Cells["tc_no"].Value.ToString();
                string adi = dataGridView2.SelectedRows[0].Cells["adi"].Value.ToString();
                string soyadi = dataGridView2.SelectedRows[0].Cells["soyadi"].Value.ToString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO muayene (adi, soyadi, tc_no, on_tanii) VALUES (@adi, @soyadi, @tc_no, @on_tanii)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@adi", adi);
                        command.Parameters.AddWithValue("@soyadi", soyadi);
                        command.Parameters.AddWithValue("@tc_no", tc_no);
                        command.Parameters.AddWithValue("@on_tanii", dataGridView1.SelectedRows[0].Cells["on_tani"].Value.ToString());

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Muayene kaydı başarıyla eklendi!", "MUAYENE BİLDİRİMİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Muayene kaydı eklenirken hata oluştu!", "HATA!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir hasta ve bir ön tanı seçin!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Doktordetay fr = new
                Doktordetay();
            fr.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                int rowIndex = dataGridView2.SelectedCells[0].RowIndex;
                string tc_no = dataGridView2.Rows[rowIndex].Cells["tc_no"].Value.ToString();

                
                dataGridView2.Rows.RemoveAt(rowIndex);

                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM hastabilgileri WHERE tc_no = @tc_no";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@tc_no", tc_no);

                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Hasta başarıyla silindi!", "SİLME BİLDİRİMİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Hasta silinirken hata oluştu!", "HATA!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir satır seçin!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT tani_id, on_tani FROM on_tanitablo WHERE on_tani LIKE '%" + textBox2.Text + "%' OR tani_id LIKE '%" + textBox2.Text + "%'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

       
       

        private void button4_Click(object sender, EventArgs e)
        {
            tahlilistendi fr = new tahlilistendi();
            fr.Show();
            this.Hide();
        }
    }
}