using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public partial class Store
    {
        public Store()
        {
            Stocks = new HashSet<Stock>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? Homepage { get; set; }
        [Column("AddressID")]
        public int? AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Stores")]
        public virtual Address? Address { get; set; }
        [InverseProperty(nameof(Stock.Store))]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
