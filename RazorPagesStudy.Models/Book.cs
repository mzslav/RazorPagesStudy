using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPagesStudy.Models
{
    public class Book
    {
 
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(2)]
        public string Name { get; set; }
        [Required]
		[MaxLength(50), MinLength(2)]
		public string Author { get; set; }

        public string? PhotoPath { get; set; }

        public string Description { get; set; }

        public Dept? Department { get; set; }
    }
}
