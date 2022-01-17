using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public partial class Stock
    {
        [Key]
        [Column("StoreID")]
        public int StoreId { get; set; }
        [Key]
        [Column("ISBN13")]
        public long Isbn13 { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(Isbn13))]
        [InverseProperty(nameof(book.Stocks))]
        public virtual book Isbn13Navigation { get; set; } = null!;
        [ForeignKey(nameof(StoreId))]
        [InverseProperty("Stocks")]
        public virtual Store Store { get; set; } = null!;
    }
}
