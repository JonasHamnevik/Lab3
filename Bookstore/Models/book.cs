using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public partial class book
    {
        public book()
        {
            Stocks = new HashSet<Stock>();
            Authors = new HashSet<Author>();
        }

        [Key]
        [Column("ISBN13")]
        public long Isbn13 { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = null!;
        [StringLength(50)]
        public string Language { get; set; } = null!;
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Column(TypeName = "date")]
        public DateTime Publication { get; set; }

        [InverseProperty(nameof(Stock.Isbn13Navigation))]
        public virtual ICollection<Stock> Stocks { get; set; }

        [ForeignKey("Isbn13")]
        [InverseProperty(nameof(Author.Isbn13s))]
        public virtual ICollection<Author> Authors { get; set; }
    }
}
