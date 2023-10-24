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
    public partial class giris : Form
    {
        public giris()
        {
            InitializeComponent();
        }
        private void giris_Load(object sender, EventArgs e)
        {
            panel1.BackColor= Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doktorgiris fr = new doktorgiris();
            fr.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hastagiris fr = new hastagiris();
            fr.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sekretergiris fr = new sekretergiris();
            fr.Show();
            this.Hide();
        }
    }
}
