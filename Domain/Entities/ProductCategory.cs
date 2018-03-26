using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class ProductCategory
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Blurbs { get; set; }

        public ProductCategory()
        {
            Blurbs = new List<Product>();
        }
    }
}
