using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KlinikOtomasyonu1
{
    public partial class duyuru : Form
    {
        public duyuru()
        {
            InitializeComponent();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Doktordetay fr=new Doktordetay();
            fr.Show();
            this.Hide();
        }

        private void duyuru_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'otomasyon1DataSet9.Tbl_Duyuru' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tbl_DuyuruTableAdapter.Fill(this.otomasyon1DataSet9.Tbl_Duyuru);

        }
    }
}
