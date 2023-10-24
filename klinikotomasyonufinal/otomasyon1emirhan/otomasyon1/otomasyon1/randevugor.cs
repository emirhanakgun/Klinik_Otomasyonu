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
    public partial class randevugor : Form
    {
        public randevugor()
        {
            InitializeComponent();
        }

        private void randevugor_Load(object sender, EventArgs e)
        {
            

          
            if (int.TryParse(textBox1.Text, out int selectedBransId))
            {
             string connectionString=baglantistring.ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                
                string query = "SELECT r.* FROM randevu r INNER JOIN doktorlar d ON r.doktor_id = d.doktor_id WHERE d.brans_id = @brans_id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@brans_id", selectedBransId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

              
                dataGridView1.DataSource = dataTable;

               
                connection.Close();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int selectedBransId = Convert.ToInt32(textBox1.Text); // TextBox'tan girilen brans_id değerini kullanıyoruz
            string connectionString = baglantistring.ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            
            string query = "SELECT r.* FROM randevu r INNER JOIN doktorlar d ON r.doktor_id = d.doktor_id WHERE d.brans_id = @brans_id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@brans_id", selectedBransId); // TextBox'tan alınan brans_id değerini kullanıyoruz.
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

           
            dataGridView1.DataSource = dataTable;

          
            connection.Close();
        }
    }
}