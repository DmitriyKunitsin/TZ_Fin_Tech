using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ_Fin_Tech
{
    public class Izdel
    {
        
        public string Name { get; set; }
        public int Price { get; set; }
        public int Id { get; set; }
        public int Parent_id { get; set; }

        public Izdel(string name, int price, int id)
        {
            Name = name;
            Price = price;
            Id = id;
        }
        public Izdel()
        {
            this.Name = string.Empty;
            this.Price = 0;
            this.Id = 0;
            this.Parent_id = 0;
        }
        
    }
   
}
