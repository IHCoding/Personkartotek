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
            this.Town = new City();
        }

        public long AddressID { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }


        #region References to other domains
        //public long PersonID { get; set; }

        public int CityID { get; set; }

        public int AlternativeAddressID { get; set; }


        public virtual City Town { get; set; }

        public virtual List<Person> PersonsPrimary { get; set; } // the primary person livng at the address

        public virtual List<AlternativeAddress> AlternativePerson { get; set; } // List of alternative persons living at the address

        #endregion

    }
}
