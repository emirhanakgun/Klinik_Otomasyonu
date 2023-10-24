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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KlinikOtomasyonu1
{
    public partial class ilacform : Form
    {
        private Dictionary<string, List<string>> hastalikIlacMap;
        private DataTable hastalarTable;

        public ilacform()
        {
            InitializeComponent();
        }

        private void ilacform_Load(object sender, EventArgs e)
        {
            
            sqlbaglantisi baglanti = new sqlbaglantisi();
            SqlConnection connection = baglanti.baglanti();
            string query = "SELECT ilac_adi FROM ilaclar";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader["ilac_adi"].ToString());
            }
            reader.Close();
            connection.Close();

            connection = baglanti.baglanti();
            query = "SELECT tc_no, adi, soyadi, on_tanii FROM muayene";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            hastalarTable = new DataTable();
            adapter.Fill(hastalarTable);
            dataGridView1.DataSource = hastalarTable;
            connection.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
            DataGridViewRow selectedRow = dataGridView1.CurrentRow;
            string tcNo = selectedRow.Cells["tc_no"].Value.ToString();
            string hastaAdi = selectedRow.Cells["adi"].Value.ToString();
            string hastaSoyadi = selectedRow.Cells["soyadi"].Value.ToString();
            string onTani = selectedRow.Cells["on_tanii"].Value.ToString();

         
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            string tcNo = dataGridView1.CurrentRow.Cells["tc_no"].Value.ToString();

            
            string ilacAdi = comboBox2.SelectedItem.ToString();

         
            sqlbaglantisi baglanti = new sqlbaglantisi();
            SqlConnection connection = baglanti.baglanti();
            string query = "SELECT ilac_id FROM ilaclar WHERE ilac_adi=@ilacAdi";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ilacAdi", ilacAdi);
            int ilacId = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            
            int adet = Convert.ToInt32(textBox1.Text);
            int kullanilanAdet = Convert.ToInt32(textBox2.Text);
            string acTok = "";
            if (checkBox1.Checked)
            {
                acTok = "Aç";
            }
            else if (checkBox2.Checked)
            {
                acTok = "Tok";
            }

            connection = baglanti.baglanti();
            query = "INSERT INTO recete (tc_no, ilac_id, ilac_adet, ilac_kul_adet, ac_tok) VALUES (@tcNo, @ilacId, @adet, @kullanilanAdet, @acTok)";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@tcNo", tcNo);
            command.Parameters.AddWithValue("@ilacId", ilacId);
            command.Parameters.AddWithValue("@adet", adet);
            command.Parameters.AddWithValue("@kullanilanAdet", kullanilanAdet);
            command.Parameters.AddWithValue("@acTok", acTok);
            command.ExecuteNonQuery();
            connection.Close();
          
            string hastaBilgileri = "Hasta Adı Soyadı: ";
            DataRow[] rows = hastalarTable.Select("tc_no = '" + tcNo + "'");
            if (rows.Length > 0)
            {
                DataRow row = rows[0];
                string adi = row["adi"].ToString();
                string soyadi = row["soyadi"].ToString();
                hastaBilgileri += adi + " " + soyadi + "\n";
            }
            hastaBilgileri += "TC Kimlik No: " + tcNo + "\n";
            hastaBilgileri += "İlaç Adı: " + ilacAdi + "\n";
            hastaBilgileri += "İlaç Adeti: " + adet + "\n";
            hastaBilgileri += "Kullanım Adeti: " + kullanilanAdet + "\n";
            hastaBilgileri += "Açlık/Tokluk: " + acTok;

            
            MessageBox.Show("Reçete başarıyla kaydedildi.\n\n" + hastaBilgileri);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Doktordetay fr=new Doktordetay();
            fr.Show();
            this.Hide();
        }
    }
    }

