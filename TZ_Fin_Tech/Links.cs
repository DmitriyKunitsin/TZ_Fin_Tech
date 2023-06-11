using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ_Fin_Tech
{
    internal class Links
    {
        public int IzdelUp { get; set; }
        public int Izdel { get; set; }
        public int Kol { get; set; }

        public Links(int izdelUp, int izdel ,int kol)
        {
            IzdelUp = izdelUp;
            Izdel = izdel;
            Kol = kol;
        }
        public Links()
        {
            this.IzdelUp = 0;
            this.Izdel = 0;
            this.Kol = 0;
        }
    }
}
