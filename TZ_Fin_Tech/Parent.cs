using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ_Fin_Tech
{
    internal class Parent
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Kol { get; set; }
        public int IzdelUP_id { get; set; }

        public Parent()
        {
            this.Name = string.Empty;
            this.Price = 0;
            this.Kol = 0;
            this.IzdelUP_id = 0;
        }
    }
}
