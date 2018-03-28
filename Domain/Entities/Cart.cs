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
        public void AddItem(Product product, int quantity)
        {
            Cartline line = lineCollection.Where(c => c.Product.ProductID == product.ProductID).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new Cartline { Product = product, Quantity = quantity });
            }
            else line.Quantity += quantity;
        }
        public void RemoveAll(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }
        public decimal ComputeTotalVal()
        {
            return lineCollection.Sum(l => l.Product.Price*l.Quantity);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<Cartline> Lines
        {
            get { return lineCollection; }
        }
    }
    public class Cartline
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
