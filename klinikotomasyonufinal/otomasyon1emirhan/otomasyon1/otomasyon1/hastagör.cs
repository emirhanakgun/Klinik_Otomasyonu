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
    public partial class hastagör : Form
    {
        public hastagör()
        {
            InitializeComponent();
        }

        private void hastagör_Load(object sender, EventArgs e)
        {
                // TODO: Bu kod satırı 'otomasyon1DataSet2.hastabilgileri' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.hastabilgileriTableAdapter.Fill(this.otomasyon1DataSet2.hastabilgileri);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Doktordetay fr=new Doktordetay();
            fr.Show();
            this.Hide();
        }
    }
}
