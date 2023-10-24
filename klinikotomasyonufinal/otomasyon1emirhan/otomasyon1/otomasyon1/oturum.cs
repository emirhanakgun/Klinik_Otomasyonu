using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlinikOtomasyonu1
{
    internal class oturum
    {
        private static oturum instance;
        private string tcNo;

        private oturum() { }

        public static oturum Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new oturum();
                }
                return instance;
            }
        }

        public string TcNo
        {
            get { return tcNo; }
            set { tcNo = value; }
        }
    }
}