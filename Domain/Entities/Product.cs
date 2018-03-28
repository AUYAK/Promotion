using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DateOfCreate { get; set; }
        public decimal Price { get; set; }
        public int? CategoryID { get; set; }
        public virtual ProductCategory Category { get; set; }
    }
}
