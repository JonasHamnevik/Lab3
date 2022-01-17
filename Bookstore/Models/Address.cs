using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public partial class Address
    {
        public Address()
        {
            Customers = new HashSet<Customer>();
            Stores = new HashSet<Store>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string StreetName { get; set; } = null!;
        public int? StreetNr { get; set; }
        [StringLength(50)]
        public string City { get; set; } = null!;

        [InverseProperty(nameof(Customer.Address))]
        public virtual ICollection<Customer> Customers { get; set; }
        [InverseProperty(nameof(Store.Address))]
        public virtual ICollection<Store> Stores { get; set; }
    }
}
