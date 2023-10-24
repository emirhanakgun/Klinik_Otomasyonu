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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;





namespace KlinikOtomasyonu1
{
    public partial class randevual : Form
    {
        private string connectionString = baglantistring.ConnectionString;
        public randevual()
        {
            InitializeComponent();

        }
        private void randevual_Load_1(object sender, EventArgs e)
        {



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();



                
                string query = "SELECT brans_id, brans_adi FROM branslar";



               
                SqlCommand command = new SqlCommand(query, connection);



              
                SqlDataReader reader = command.ExecuteReader();



            
                while (reader.Read())
                {
                   
                    int brans_id = reader.GetInt32(0);
                    string brans_adi = reader.GetString(1);



                    
                    comboBox1.Items.Add(new KeyValuePair<int, string>(brans_id, brans_adi));
                }



                
                reader.Close();
                command.Dispose();
            }



        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {



              
                connection.Open();



                
                int selectedBransId = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;



                
                string query = "SELECT doktor_adi, doktor_soyadi, doktor_tc_no FROM doktorlar WHERE brans_id = @brans_id";



              
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@brans_id", selectedBransId);



                
                SqlDataReader reader = command.ExecuteReader();



               
                comboBox2.Items.Clear();



                
                while (reader.Read())
                {
                    // Get the values for the current row
                    string doktorAd = reader.GetString(0);
                    string doktorSoyadi = reader.GetString(1);
                    string doktorTcNo = reader.GetString(2);



                   
                    Doktor doktor = new Doktor(doktorAd, doktorSoyadi, doktorTcNo);



                    
                    comboBox2.Items.Add(doktor);
                }



                
                reader.Close();
                command.Dispose();
                connection.Close();



            
                comboBox2.DisplayMember = "ToString";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO hastabilgileri (adi, soyadi, tc_no) VALUES (@adi, @soyadi, @tc_no)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@adi", textBox1.Text);
                command.Parameters.AddWithValue("@soyadi", textBox2.Text);
                command.Parameters.AddWithValue("@tc_no", maskedTextBox1.Text);
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {

                }
                else
                {
                    MessageBox.Show("Kayıt eklenirken bir hata oluştu.");
                }

                int selectedBransId = ((KeyValuePair<int, string>)comboBox1.SelectedItem).Key;


                string query2 = "INSERT INTO randevu (tc_no,brans_id,doktor_id,randevu_tarihi,randevu_saati) VALUES (@a_tc_no, @brans_id, @doktor_tc_no, @randevu_tarihi, @randevu_saati)";

                SqlCommand command2 = new SqlCommand(query2, connection);
                command2.Parameters.AddWithValue("@a_tc_no", maskedTextBox1.Text);
                command2.Parameters.AddWithValue("@brans_id", selectedBransId);
                command2.Parameters.AddWithValue("@doktor_tc_no", ((Doktor)comboBox2.SelectedItem).DoktorTcNo);
                command2.Parameters.AddWithValue("@randevu_tarihi", dateTimePicker1.Value.Date);
                command2.Parameters.AddWithValue("@randevu_saati", comboBox3.SelectedItem.ToString());



               
                int result2 = command2.ExecuteNonQuery();



                if (result2 > 0)
                {
                    MessageBox.Show("Kayıt başarıyla eklendi.");
                }
                else
                {
                    MessageBox.Show("Kayıt eklenirken bir hata oluştu.");
                }




               
                command.Dispose();
                command2.Dispose();
                connection.Close();
            }
        }
    }
}