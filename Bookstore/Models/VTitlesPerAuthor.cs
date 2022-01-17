using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    [Keyless]
    public partial class VTitlesPerAuthor
    {
        [StringLength(100)]
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public int? Titles { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total { get; set; }
    }
}
