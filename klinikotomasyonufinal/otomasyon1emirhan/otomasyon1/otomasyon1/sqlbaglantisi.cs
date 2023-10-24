using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace KlinikOtomasyonu1
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-203KHF6\\SQLEMİRHAN;Initial Catalog=otomasyon1;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
