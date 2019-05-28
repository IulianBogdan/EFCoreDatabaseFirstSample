using System;
using System.Collections.Generic;

namespace EFCoreDatabaseFirstSample.Models
{
    public partial class Address
    {
        public Address()
        {
            AuthorContact = new HashSet<AuthorContact>();
        }

        public int AddressId { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public int? PostCode { get; set; }

        public virtual ICollection<AuthorContact> AuthorContact { get; set; }
    }
}
