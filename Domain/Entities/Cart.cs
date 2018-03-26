using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<Cartline> lineCollection = new List<Cartline>();


    }
    public class Cartline
    {
        public Blurb Blurb { get; set; }
        public int Quantity { get; set; }

    }
}
