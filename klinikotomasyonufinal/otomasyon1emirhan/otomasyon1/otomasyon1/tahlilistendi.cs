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
    public partial class tahlilistendi : Form
    {
        private string connectionString = baglantistring.ConnectionString;



        public tahlilistendi()
        {
            InitializeComponent();
        }



        private void tahlilistendi_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'otomasyon1DataSet10.tahlil_kimlik' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.

            //this.tahlil_kimlikTableAdapter.Fill(this.otomasyon1DataSet10.tahlil_kimlik);  //?????????????????????????????????????????????????????????????????? neden


            // SqlConnection nesnesi oluşturma

            string tc = oturum.Instance.TcNo;


            string connectionString =baglantistring.ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string query = "SELECT doktor_id FROM doktorlar WHERE doktor_tc_no = @tcc";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@tcc", tc);





            // Verileri yükleme ve DataGridView'e ekleme
            string sorgu = "SELECT * FROM tahlil_kimlik WHERE doktor_id = @tcc";
            SqlCommand command2 = new SqlCommand(sorgu, connection);
            command2.Parameters.AddWithValue("@tcc", tc);
            SqlDataAdapter adapter = new SqlDataAdapter(command2);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;




            FillComboBox();
        }



        private void FillComboBox()
        {
          
            string query = "SELECT tahlil_adi FROM tahlil";



           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
              
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    
                    connection.Open();



                   
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                        comboBox1.Items.Clear();



                        
                        while (reader.Read())
                        {



                            comboBox1.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            List<DataGridViewRow> selectedRows = dataGridView1.SelectedRows.Cast<DataGridViewRow>().ToList();

            
            DialogResult result = MessageBox.Show("Seçili satırları silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                
                foreach (DataGridViewRow row in selectedRows)
                {
                    DataRowView dataRowView = (DataRowView)row.DataBoundItem;
                    DataRow rowView = dataRowView.Row;
                    int selectedId = (int)rowView["id"];
                    string connectionString = baglantistring.ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sorgu = "DELETE FROM tahlil_kimlik WHERE id = @id";
                        using (SqlCommand command = new SqlCommand(sorgu, connection))
                        {
                            command.Parameters.AddWithValue("@id", selectedId);
                            command.ExecuteNonQuery();
                        }
                    }
                    dataGridView1.Rows.Remove(row);
                }
            }
            else
            {
                
                return;
            }






            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int doktorId;
                string tc = oturum.Instance.TcNo;
                string query1 = "SELECT doktor_id FROM doktorlar WHERE doktor_tc_no = @tcc";
                string query2 = "SELECT * FROM tahlil_kimlik WHERE doktor_id = @doktorId";

                connection.Open();

                SqlCommand command1 = new SqlCommand(query1, connection);
                command1.Parameters.AddWithValue("@tcc", tc);
                doktorId = Convert.ToInt32(command1.ExecuteScalar());

                
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command2 = new SqlCommand(query2, connection);
                command2.Parameters.AddWithValue("@doktorId", doktorId);
                adapter.SelectCommand = command2;

                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;

                connection.Close();
            }



        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Doktordetay fr = new Doktordetay();
            fr.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int doktorId;
            string tc = oturum.Instance.TcNo;
            string query = "SELECT doktor_id FROM doktorlar WHERE doktor_tc_no = @tcc";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {



                connection.Open();


                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tcc", tc);



                doktorId = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();
            }


            string tcNo = dataGridView2.SelectedRows[0].Cells["tc_no"].Value.ToString();
            string tahlilAdi = comboBox1.SelectedItem.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO tahlil_kimlik (tahlil, tc_no, doktor_id) VALUES (@tahlilAdi, @tcNo, @doktorId)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@tcNo", tcNo);
                insertCommand.Parameters.AddWithValue("@tahlilAdi", tahlilAdi);
                insertCommand.Parameters.AddWithValue("@doktorId", doktorId);

                insertCommand.ExecuteNonQuery();

                // DataGridView'i güncelleme
                string query2 = "SELECT * FROM tahlil_kimlik WHERE doktor_id = @doktorId";
                SqlCommand command2 = new SqlCommand(query2, connection);
                command2.Parameters.AddWithValue("@doktorId", doktorId);

                SqlDataAdapter adapter = new SqlDataAdapter(command2); // burada sadece SqlCommand kullanıyoruz

                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;

                connection.Close();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string query = "SELECT tc_no, adi, soyadi, on_tanii FROM muayene WHERE adi LIKE '%" + textBox1.Text + "%' OR tc_no LIKE '%" + textBox1.Text + "%' OR soyadi LIKE '%" + textBox1.Text + "%' OR on_tanii LIKE '%" + textBox1.Text + "%'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView2.DataSource = table;

            }

        }
    }
}
       



