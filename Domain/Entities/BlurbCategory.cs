using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BlurbCategory
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Blurb> Blurbs { get; set; }

        public BlurbCategory()
        {
            Blurbs = new List<Blurb>();
        }
    }
}
