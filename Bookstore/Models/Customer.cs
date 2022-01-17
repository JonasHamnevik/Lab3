using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public partial class Customer
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        public string LastName { get; set; } = null!;
        [StringLength(50)]
        public string Email { get; set; } = null!;
        [Column("AddressID")]
        public int AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        [InverseProperty("Customers")]
        public virtual Address Address { get; set; } = null!;
    }
}
