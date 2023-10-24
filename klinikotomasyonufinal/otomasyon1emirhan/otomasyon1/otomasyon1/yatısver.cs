using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KlinikOtomasyonu1
{
    public partial class yatısver : Form
    {
       
        private SqlConnection connT;
        private SqlCommand cmd12;
        public yatısver()
        {
            InitializeComponent();
            string connectionString = baglantistring.ConnectionString;
            connT = new SqlConnection(connectionString);
            connT.Open();
            cmd12 = new SqlCommand("", connT);
        }
        sqlbaglantisi bgl = new sqlbaglantisi();


        private void yatısver_Load(object sender, EventArgs e)
        {
            
            comboBox1.DataSource = GetHastaBilgileri();
            comboBox1.DisplayMember = "tc_no";
            comboBox1.ValueMember = "tc_no";

            
            string connString = baglantistring.ConnectionString;
            string query = "SELECT DISTINCT oda FROM odalar";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd1 = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader["oda"].ToString());
                        }
                    }
                }
            }


            SqlConnection baglanti = bgl.baglanti();

            string sql = "SELECT oda, durum FROM odalar";

            SqlCommand cmd = new SqlCommand(sql, baglanti);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
               
                int oda_no = dr.GetInt32(0);
                string durum = dr.GetString(1);

                // Durum dolu ise ilgili butonun arkaplan rengini kırmızı yap
                if (durum == "dolu")
                {
                    if (oda_no == 100)
                    {
                        button1.BackColor = Color.Red;
                    }
                    else if (oda_no == 101)
                    {
                        button2.BackColor = Color.Red;
                    }
                    else if (oda_no == 102)
                    {
                        button3.BackColor = Color.Red;
                    }
                    else if (oda_no == 103)
                    {
                        button4.BackColor = Color.Red;
                    }
                    else if (oda_no == 104)
                    {
                        button5.BackColor = Color.Red;
                    }
                    else if (oda_no == 105)
                    {
                        button6.BackColor = Color.Red;
                    }
                    else if (oda_no == 106)
                    {
                        button7.BackColor = Color.Red;
                    }
                    else if (oda_no == 107)
                    {
                        button8.BackColor = Color.Red;
                    }
                    else if (oda_no == 108)
                    {
                        button9.BackColor = Color.Red;
                    }
                }
            }

            
            baglanti.Close();
        }

        private DataTable GetHastaBilgileri()
        {
            string connString = baglantistring.ConnectionString;
            string query = "SELECT tc_no from hastabilgileri";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Doktordetay fr = new Doktordetay();
            fr.Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            
            string tc_no = comboBox1.Text;
            string oda_no = comboBox2.Text;


            SqlConnection baglanti = bgl.baglanti();
            

            
            string kontrolSql = "SELECT COUNT(*) FROM odalar WHERE tc_no = @tc_no";
            SqlCommand kontrolCmd = new SqlCommand(kontrolSql, baglanti);
            kontrolCmd.Parameters.AddWithValue("@tc_no", tc_no);

            int count = (int)kontrolCmd.ExecuteScalar();

            if (count == 0)
            {
                string durumSql = "SELECT durum FROM odalar WHERE oda = @oda_no";
                SqlCommand durumCmd = new SqlCommand(durumSql, baglanti);
                durumCmd.Parameters.AddWithValue("@oda_no", oda_no);

                
                string durum = (string)durumCmd.ExecuteScalar();

                if (durum == "dolu")
                {
                    MessageBox.Show("Bu oda zaten dolu. Başka bir oda seçin.", "HATA!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    baglanti.Close();
                    return;
                }

                string sql = "UPDATE odalar SET tc_no = @tc_no, durum='dolu' WHERE oda = @oda_no";

                SqlCommand cmd = new SqlCommand(sql, baglanti);

              

                
                cmd.Parameters.AddWithValue("@tc_no", tc_no);
                cmd.Parameters.AddWithValue("@oda_no", oda_no);

               
                cmd.ExecuteNonQuery();
                string adi = "";
                string soyadi = "";
                string sqlHasta = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no = @tc_no";
                SqlCommand cmdHasta = new SqlCommand(sqlHasta, baglanti);
                cmdHasta.Parameters.AddWithValue("@tc_no", tc_no);
                SqlDataReader reader = cmdHasta.ExecuteReader();
                while (reader.Read())
                {
                    adi = reader["adi"].ToString();
                    soyadi = reader["soyadi"].ToString();
                }
                reader.Close();

                // MessageBox ile kullanıcının adı, soyadı, TC numarası ve kaldığı oda numarasını ekrana yazdırın
                MessageBox.Show("YATIŞ VERİLDİ\n\nAdı: " + adi + "\nSoyadı: " + soyadi + "\nTC Numarası: " + tc_no + "\nKaldığı Oda: " + oda_no, " Yatış Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);



               
                baglanti.Close();

               

                if (comboBox2.Text == "100" || comboBox2.SelectedIndex == 0)
                {
                    button1.BackColor = Color.Red;
                }
                else if (comboBox2.Text == "101" || comboBox2.SelectedIndex == 1)
                {
                    button2.BackColor = Color.Red;
                }
                else if (comboBox2.Text == "102" || comboBox2.SelectedIndex == 2)
                {
                    button3.BackColor = Color.Red;
                }
                else if (comboBox2.Text == "103" || comboBox2.SelectedIndex == 3)
                {
                    button4.BackColor = Color.Red;
                }
                else if (comboBox2.Text == "104" || comboBox2.SelectedIndex == 4)
                {
                    button5.BackColor = Color.Red;
                }
                else if (comboBox2.Text == "105" || comboBox2.SelectedIndex == 5)
                {
                    button6.BackColor = Color.Red;
                }
                else if (comboBox2.Text == "106" || comboBox2.SelectedIndex == 6)
                {
                    button7.BackColor = Color.Red;
                }
                else if (comboBox2.Text == "107" || comboBox2.SelectedIndex == 7)
                {
                    button8.BackColor = Color.Red;
                }
                else if (comboBox2.Text == "108" || comboBox2.SelectedIndex == 8)
                {
                    button9.BackColor = Color.Red;
                }
                else
                {
                    MessageBox.Show("Bu hasta zaten bir odada yatıyor. Başka bir odaya yerleştirmeyi deneyin.", "HATA!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //Çıkış Ver
        private void button13_Click(object sender, EventArgs e)
        {
         
            string oda_no = comboBox2.Text;

          
            SqlConnection baglanti = bgl.baglanti();
          

       
            string tc_no = "";
            string sqlHasta2 = "SELECT tc_no FROM odalar WHERE oda = @oda_no2";
            SqlCommand cmdHasta2 = new SqlCommand(sqlHasta2, baglanti);
            cmdHasta2.Parameters.AddWithValue("@oda_no2", oda_no);
            SqlDataReader reader2 = cmdHasta2.ExecuteReader();
            while (reader2.Read())
            {
                tc_no = reader2["tc_no"].ToString();
            }
            reader2.Close();

          
            string sql = "UPDATE odalar SET tc_no = null, durum='Boş' WHERE oda = @oda_no";
            SqlCommand cmd = new SqlCommand(sql, baglanti);

           
            cmd.Parameters.AddWithValue("@oda_no", oda_no);

            cmd.ExecuteNonQuery();

           
            string adi = "";
            string soyadi = "";
            string sqlHasta1 = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no = @tc_no2";
            SqlCommand cmdHasta1 = new SqlCommand(sqlHasta1, baglanti);
            cmdHasta1.Parameters.AddWithValue("@tc_no2", tc_no);
            SqlDataReader reader = cmdHasta1.ExecuteReader();
            while (reader.Read())
            {
                adi = reader["adi"].ToString();
                soyadi = reader["soyadi"].ToString();
            }
            reader.Close();

            // MessageBox ile kullanıcının adı, soyadı, TC numarası ve kaldığı oda numarasını ekrana yazdırın
            MessageBox.Show("ÇIKIŞ VERİLDİ\n\nAdı: " + adi + "\nSoyadı: " + soyadi + "\nTC Numarası: " + tc_no + "\nÇıktığı Oda: " + oda_no, "Çıkış Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Hand);

          
            baglanti.Close();
          


            if (comboBox2.Text == "100" || comboBox2.SelectedIndex == 0)
            {
                button1.BackColor = Color.Lime;
            }
            else if (comboBox2.Text == "101" || comboBox2.SelectedIndex == 1)
            {
                button2.BackColor = Color.Lime;
            }
            else if (comboBox2.Text == "102" || comboBox2.SelectedIndex == 2)
            {
                button3.BackColor = Color.Lime;
            }
            else if (comboBox2.Text == "103" || comboBox2.SelectedIndex == 3)
            {
                button4.BackColor = Color.Lime;
            }
            else if (comboBox2.Text == "104" || comboBox2.SelectedIndex == 4)
            {
                button5.BackColor = Color.Lime;
            }
            else if (comboBox2.Text == "105" || comboBox2.SelectedIndex == 5)
            {
                button6.BackColor = Color.Lime;
            }
            else if (comboBox2.Text == "106" || comboBox2.SelectedIndex == 6)
            {
                button7.BackColor = Color.Lime;
            }
            else if (comboBox2.Text == "107" || comboBox2.SelectedIndex == 7)
            {
                button8.BackColor = Color.Lime;
            }
            else if (comboBox2.Text == "108" || comboBox2.SelectedIndex == 8)
            {
                button9.BackColor = Color.Lime;
            }
            else
            {
                MessageBox.Show("Bu hasta z.", "HATA!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no=@tc_no";
            cmd12.CommandText = query;
            cmd12.Parameters.AddWithValue("@tc_no", comboBox1.Text);
            SqlDataReader dr = cmd12.ExecuteReader();
            if (dr.Read())
            {
                string adiSoyadi = dr["adi"].ToString() + " " + dr["soyadi"].ToString();
                label4.Text = adiSoyadi;
            }
            else
            {
                label4.Text = "";
            }
            dr.Close();
            cmd12.Parameters.Clear();
        }

            
        private void button1_Click(object sender, EventArgs e)
        {
           
            string oda_no = "100";

            
            SqlConnection baglanti = bgl.baglanti();
           

            
            string durumSql = "SELECT durum FROM odalar WHERE oda = @oda_no";
            SqlCommand durumCmd = new SqlCommand(durumSql, baglanti);
            durumCmd.Parameters.AddWithValue("@oda_no", oda_no);

            
            string durum = (string)durumCmd.ExecuteScalar();

            if (durum == "dolu")
            {
               
                string sql = "SELECT tc_no FROM odalar WHERE oda = @oda_no";
                SqlCommand cmd = new SqlCommand(sql, baglanti);
                cmd.Parameters.AddWithValue("@oda_no", oda_no);

                
                string tc_no = (string)cmd.ExecuteScalar();

                // MessageBox ile oda numarası ve o odada kalan kişinin bilgilerini ekrana yazdırın
                string adi = "";
                string soyadi = "";
                string sqlHasta = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no = @tc_no";
                SqlCommand cmdHasta = new SqlCommand(sqlHasta, baglanti);
                cmdHasta.Parameters.AddWithValue("@tc_no", tc_no);
                SqlDataReader reader = cmdHasta.ExecuteReader();
                while (reader.Read())
                {
                    adi = reader["adi"].ToString();
                    soyadi = reader["soyadi"].ToString();
                }
                reader.Close();
                MessageBox.Show("Bu oda dolu. Kalan kişinin bilgileri:\n\nAdı: " + adi + "\nSoyadı: " + soyadi + "\nTC Numarası: " + tc_no + "\nOda Numarası: " + oda_no, "Oda Dolu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                MessageBox.Show("Bu oda boş.", "Oda Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string oda_no = "101";

           
            SqlConnection baglanti = bgl.baglanti();  

            
            string durumSql = "SELECT durum FROM odalar WHERE oda = @oda_no";
            SqlCommand durumCmd = new SqlCommand(durumSql, baglanti);
            durumCmd.Parameters.AddWithValue("@oda_no", oda_no);

           
            string durum = (string)durumCmd.ExecuteScalar();

            if (durum == "dolu")
            {
               
                string sql = "SELECT tc_no FROM odalar WHERE oda = @oda_no";
                SqlCommand cmd = new SqlCommand(sql, baglanti);
                cmd.Parameters.AddWithValue("@oda_no", oda_no);

                
                string tc_no = (string)cmd.ExecuteScalar();

               
                string adi = "";
                string soyadi = "";
                string sqlHasta = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no = @tc_no";
                SqlCommand cmdHasta = new SqlCommand(sqlHasta, baglanti);
                cmdHasta.Parameters.AddWithValue("@tc_no", tc_no);
                SqlDataReader reader = cmdHasta.ExecuteReader();
                while (reader.Read())
                {
                    adi = reader["adi"].ToString();
                    soyadi = reader["soyadi"].ToString();
                }
                reader.Close();
                MessageBox.Show("Bu oda dolu. Kalan kişinin bilgileri:\n\nAdı: " + adi + "\nSoyadı: " + soyadi + "\nTC Numarası: " + tc_no + "\nOda Numarası: " + oda_no, "Oda Dolu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                MessageBox.Show("Bu oda boş.", "Oda Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string oda_no = "102";

            
            SqlConnection baglanti = bgl.baglanti();

            
            string durumSql = "SELECT durum FROM odalar WHERE oda = @oda_no";
            SqlCommand durumCmd = new SqlCommand(durumSql, baglanti);
            durumCmd.Parameters.AddWithValue("@oda_no", oda_no);

            
            string durum = (string)durumCmd.ExecuteScalar();

            if (durum == "dolu")
            {
                
                string sql = "SELECT tc_no FROM odalar WHERE oda = @oda_no";
                SqlCommand cmd = new SqlCommand(sql, baglanti);
                cmd.Parameters.AddWithValue("@oda_no", oda_no);

                
                string tc_no = (string)cmd.ExecuteScalar();

             
                string adi = "";
                string soyadi = "";
                string sqlHasta = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no = @tc_no";
                SqlCommand cmdHasta = new SqlCommand(sqlHasta, baglanti);
                cmdHasta.Parameters.AddWithValue("@tc_no", tc_no);
                SqlDataReader reader = cmdHasta.ExecuteReader();
                while (reader.Read())
                {
                    adi = reader["adi"].ToString();
                    soyadi = reader["soyadi"].ToString();
                }
                reader.Close();
                MessageBox.Show("Bu oda dolu. Kalan kişinin bilgileri:\n\nAdı: " + adi + "\nSoyadı: " + soyadi + "\nTC Numarası: " + tc_no + "\nOda Numarası: " + oda_no, "Oda Dolu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                MessageBox.Show("Bu oda boş.", "Oda Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

           
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            string oda_no = "103";

           
            SqlConnection baglanti = bgl.baglanti();

            
            string durumSql = "SELECT durum FROM odalar WHERE oda = @oda_no";
            SqlCommand durumCmd = new SqlCommand(durumSql, baglanti);
            durumCmd.Parameters.AddWithValue("@oda_no", oda_no);

            
            string durum = (string)durumCmd.ExecuteScalar();

            if (durum == "dolu")
            {
               
                string sql = "SELECT tc_no FROM odalar WHERE oda = @oda_no";
                SqlCommand cmd = new SqlCommand(sql, baglanti);
                cmd.Parameters.AddWithValue("@oda_no", oda_no);

                
                string tc_no = (string)cmd.ExecuteScalar();

                
                string adi = "";
                string soyadi = "";
                string sqlHasta = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no = @tc_no";
                SqlCommand cmdHasta = new SqlCommand(sqlHasta, baglanti);
                cmdHasta.Parameters.AddWithValue("@tc_no", tc_no);
                SqlDataReader reader = cmdHasta.ExecuteReader();
                while (reader.Read())
                {
                    adi = reader["adi"].ToString();
                    soyadi = reader["soyadi"].ToString();
                }
                reader.Close();
                MessageBox.Show("Bu oda dolu. Kalan kişinin bilgileri:\n\nAdı: " + adi + "\nSoyadı: " + soyadi + "\nTC Numarası: " + tc_no + "\nOda Numarası: " + oda_no, "Oda Dolu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                MessageBox.Show("Bu oda boş.", "Oda Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
            string oda_no = "104";

            
            SqlConnection baglanti = bgl.baglanti();

            
            string durumSql = "SELECT durum FROM odalar WHERE oda = @oda_no";
            SqlCommand durumCmd = new SqlCommand(durumSql, baglanti);
            durumCmd.Parameters.AddWithValue("@oda_no", oda_no);

           
            string durum = (string)durumCmd.ExecuteScalar();

            if (durum == "dolu")
            {
             
                string sql = "SELECT tc_no FROM odalar WHERE oda = @oda_no";
                SqlCommand cmd = new SqlCommand(sql, baglanti);
                cmd.Parameters.AddWithValue("@oda_no", oda_no);

               
                string tc_no = (string)cmd.ExecuteScalar();

                
                string adi = "";
                string soyadi = "";
                string sqlHasta = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no = @tc_no";
                SqlCommand cmdHasta = new SqlCommand(sqlHasta, baglanti);
                cmdHasta.Parameters.AddWithValue("@tc_no", tc_no);
                SqlDataReader reader = cmdHasta.ExecuteReader();
                while (reader.Read())
                {
                    adi = reader["adi"].ToString();
                    soyadi = reader["soyadi"].ToString();
                }
                reader.Close();
                MessageBox.Show("Bu oda dolu. Kalan kişinin bilgileri:\n\nAdı: " + adi + "\nSoyadı: " + soyadi + "\nTC Numarası: " + tc_no + "\nOda Numarası: " + oda_no, "Oda Dolu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                MessageBox.Show("Bu oda boş.", "Oda Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

          
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            string oda_no = "105";

          
            SqlConnection baglanti = bgl.baglanti();

            
            string durumSql = "SELECT durum FROM odalar WHERE oda = @oda_no";
            SqlCommand durumCmd = new SqlCommand(durumSql, baglanti);
            durumCmd.Parameters.AddWithValue("@oda_no", oda_no);

            
            string durum = (string)durumCmd.ExecuteScalar();

            if (durum == "dolu")
            {
                
                string sql = "SELECT tc_no FROM odalar WHERE oda = @oda_no";
                SqlCommand cmd = new SqlCommand(sql, baglanti);
                cmd.Parameters.AddWithValue("@oda_no", oda_no);

               
                string tc_no = (string)cmd.ExecuteScalar();

               
                string adi = "";
                string soyadi = "";
                string sqlHasta = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no = @tc_no";
                SqlCommand cmdHasta = new SqlCommand(sqlHasta, baglanti);
                cmdHasta.Parameters.AddWithValue("@tc_no", tc_no);
                SqlDataReader reader = cmdHasta.ExecuteReader();
                while (reader.Read())
                {
                    adi = reader["adi"].ToString();
                    soyadi = reader["soyadi"].ToString();
                }
                reader.Close();
                MessageBox.Show("Bu oda dolu. Kalan kişinin bilgileri:\n\nAdı: " + adi + "\nSoyadı: " + soyadi + "\nTC Numarası: " + tc_no + "\nOda Numarası: " + oda_no, "Oda Dolu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                MessageBox.Show("Bu oda boş.", "Oda Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

          
            baglanti.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
            string oda_no = "106";

            
            SqlConnection baglanti = bgl.baglanti();

          
            string durumSql = "SELECT durum FROM odalar WHERE oda = @oda_no";
            SqlCommand durumCmd = new SqlCommand(durumSql, baglanti);
            durumCmd.Parameters.AddWithValue("@oda_no", oda_no);

            // Durum sütunundaki değeri kontrol edin
            string durum = (string)durumCmd.ExecuteScalar();

            if (durum == "dolu")
            {
                
                string sql = "SELECT tc_no FROM odalar WHERE oda = @oda_no";
                SqlCommand cmd = new SqlCommand(sql, baglanti);
                cmd.Parameters.AddWithValue("@oda_no", oda_no);

               
                string tc_no = (string)cmd.ExecuteScalar();

               
                string adi = "";
                string soyadi = "";
                string sqlHasta = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no = @tc_no";
                SqlCommand cmdHasta = new SqlCommand(sqlHasta, baglanti);
                cmdHasta.Parameters.AddWithValue("@tc_no", tc_no);
                SqlDataReader reader = cmdHasta.ExecuteReader();
                while (reader.Read())
                {
                    adi = reader["adi"].ToString();
                    soyadi = reader["soyadi"].ToString();
                }
                reader.Close();
                MessageBox.Show("Bu oda dolu. Kalan kişinin bilgileri:\n\nAdı: " + adi + "\nSoyadı: " + soyadi + "\nTC Numarası: " + tc_no + "\nOda Numarası: " + oda_no, "Oda Dolu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                MessageBox.Show("Bu oda boş.", "Oda Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

           
            baglanti.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
            string oda_no = "107";

            
            SqlConnection baglanti = bgl.baglanti();

            
            string durumSql = "SELECT durum FROM odalar WHERE oda = @oda_no";
            SqlCommand durumCmd = new SqlCommand(durumSql, baglanti);
            durumCmd.Parameters.AddWithValue("@oda_no", oda_no);

            
            string durum = (string)durumCmd.ExecuteScalar();

            if (durum == "dolu")
            {
                
                string sql = "SELECT tc_no FROM odalar WHERE oda = @oda_no";
                SqlCommand cmd = new SqlCommand(sql, baglanti);
                cmd.Parameters.AddWithValue("@oda_no", oda_no);

              
                string tc_no = (string)cmd.ExecuteScalar();

                
                string adi = "";
                string soyadi = "";
                string sqlHasta = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no = @tc_no";
                SqlCommand cmdHasta = new SqlCommand(sqlHasta, baglanti);
                cmdHasta.Parameters.AddWithValue("@tc_no", tc_no);
                SqlDataReader reader = cmdHasta.ExecuteReader();
                while (reader.Read())
                {
                    adi = reader["adi"].ToString();
                    soyadi = reader["soyadi"].ToString();
                }
                reader.Close();
                MessageBox.Show("Bu oda dolu. Kalan kişinin bilgileri:\n\nAdı: " + adi + "\nSoyadı: " + soyadi + "\nTC Numarası: " + tc_no + "\nOda Numarası: " + oda_no, "Oda Dolu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                MessageBox.Show("Bu oda boş.", "Oda Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

           
            baglanti.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            string oda_no = "108";

          
            SqlConnection baglanti = bgl.baglanti();

         
            string durumSql = "SELECT durum FROM odalar WHERE oda = @oda_no";
            SqlCommand durumCmd = new SqlCommand(durumSql, baglanti);
            durumCmd.Parameters.AddWithValue("@oda_no", oda_no);

            
            string durum = (string)durumCmd.ExecuteScalar();

            if (durum == "dolu")
            {
                
                string sql = "SELECT tc_no FROM odalar WHERE oda = @oda_no";
                SqlCommand cmd = new SqlCommand(sql, baglanti);
                cmd.Parameters.AddWithValue("@oda_no", oda_no);

                
                string tc_no = (string)cmd.ExecuteScalar();

                
                string adi = "";
                string soyadi = "";
                string sqlHasta = "SELECT adi, soyadi FROM hastabilgileri WHERE tc_no = @tc_no";
                SqlCommand cmdHasta = new SqlCommand(sqlHasta, baglanti);
                cmdHasta.Parameters.AddWithValue("@tc_no", tc_no);
                SqlDataReader reader = cmdHasta.ExecuteReader();
                while (reader.Read())
                {
                    adi = reader["adi"].ToString();
                    soyadi = reader["soyadi"].ToString();
                }
                reader.Close();
                MessageBox.Show("Bu oda dolu. Kalan kişinin bilgileri:\n\nAdı: " + adi + "\nSoyadı: " + soyadi + "\nTC Numarası: " + tc_no + "\nOda Numarası: " + oda_no, "Oda Dolu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                MessageBox.Show("Bu oda boş.", "Oda Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

          
            baglanti.Close();
        }
    }
}