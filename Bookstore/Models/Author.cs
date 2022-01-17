using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public partial class Author
    {
        public Author()
        {
            Isbn13s = new HashSet<book>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [StringLength(50)]
        public string? LastName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Birth { get; set; }

        [ForeignKey("AuthorId")]
        [InverseProperty(nameof(book.Authors))]
        public virtual ICollection<book> Isbn13s { get; set; }
    }
}
