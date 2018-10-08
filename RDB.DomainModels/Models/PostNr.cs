using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.DomainModels.Models
{
    [Table("PostNr")]
    public class PostNr
    {
        public PostNr()
        {
            this._Country = new Country();
            this._City = new City();
        }

        public int PostNrID { get; set; }

        public string PostNumber { get; set; }

        #region References to other domains
        public int CountryID { get; set; }

        public int CityID { get; set; }

        public int AddressID { get; set; }


        public virtual Country _Country { get; set; }

        public virtual City _City { get; set; }

        public virtual Address Address { get; set; }

        #endregion

    }
}
