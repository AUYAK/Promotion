using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Blurb
    {
        [Key]
        public int BlurbID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DateOfCreate { get; set; }

        public int? CategoryID { get; set; }
        public virtual BlurbCategory Category { get; set; }
    }
}
