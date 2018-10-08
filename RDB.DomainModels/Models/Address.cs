using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.DomainModels.Models
{
    [Table("Address")]
    public class Address
    {
        public Address()
        {
            this.PersonsPrimary = new List<Person>();
            this.AlternativePerson = new List<AlternativeAddress>();
        }

        public int AddressID { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }


        #region References to other domains

        public int PostNrID { get; set; }

        public int PersonID { get; set; }

        public int AlternativeAddressID { get; set; }


        public virtual PostNr PostNr { get; set; }

        public virtual List<Person> PersonsPrimary { get; set; } // the primary person livng at the address

        public virtual List<AlternativeAddress> AlternativePerson { get; set; } // List of alternative persons living at the address

        #endregion

    }
}
