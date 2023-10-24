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
    public partial class doktorrandevugor : Form
    {
        private string connectionString = baglantistring.ConnectionString;
        public doktorrandevugor()
        {
            InitializeComponent();
        }

        private void doktorrandevugor_Load(object sender, EventArgs e)
        {
         
            string doktorTcNo = oturum.Instance.TcNo;





            using (SqlConnection connection = new SqlConnection(connectionString))
            {





                
                string randevuSorgu = "SELECT * FROM Randevu WHERE doktor_id = @doktorTcNo AND randevu_tarihi >= CONVERT(date, GETDATE()) ORDER BY randevu_tarihi ASC";
                using (SqlCommand randevuCommand = new SqlCommand(randevuSorgu, connection))
                {
                    randevuCommand.Parameters.AddWithValue("@doktorTcNo", doktorTcNo);
                    SqlDataAdapter adapter = new SqlDataAdapter(randevuCommand);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);





                   
                    dataGridView1.DataSource = dt;
                }





                // Doktorun önceki randevularını randevu tablosundan çek
                string oncekiRandevuSorgu = "SELECT * FROM Randevu WHERE doktor_id = @doktorTcNo AND (randevu_tarihi < CONVERT(date, GETDATE()) OR (randevu_tarihi = CONVERT(date, GETDATE()) AND randevu_saati < CONVERT(time, GETDATE()))) ORDER BY randevu_tarihi ASC, randevu_saati ASC";
                using (SqlCommand oncekiRandevuCommand = new SqlCommand(oncekiRandevuSorgu, connection))
                {
                    oncekiRandevuCommand.Parameters.AddWithValue("@doktorTcNo", doktorTcNo);
                    SqlDataAdapter adapter = new SqlDataAdapter(oncekiRandevuCommand);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);





                  
                    dataGridView2.DataSource = dt;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    dataGridView1.Rows.Remove(row);
                }
            }

            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    dataGridView2.Rows.Remove(row);
                }
            }

            // Veritabanındaki Randevu tablosunu güncelle
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Randevu WHERE randevu_id = @randevuId";
                SqlCommand command = new SqlCommand(query, connection);

               
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        int randevuId = Convert.ToInt32(row.Cells["randevu_id"].Value);
                        command.Parameters.AddWithValue("@randevuId", randevuId);
                        command.ExecuteNonQuery();
                    }
                }

                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        int randevuId = Convert.ToInt32(row.Cells["randevu_id"].Value);
                        command.Parameters.AddWithValue("@randevuId", randevuId);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}