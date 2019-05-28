using System;
using System.Collections.Generic;

namespace EFCoreDatabaseFirstSample.Models
{
    public partial class AuthorContact
    {
        public AuthorContact()
        {
            Author = new HashSet<Author>();
        }

        public int AuthorContactId { get; set; }
        public decimal? PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Author> Author { get; set; }
    }
}
