using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.DomainModels.Models
{
    [Table("PostNr")]
    public class City
    {
        public long CityID { get; set; }

        public string CityName { get; set; }

        public string PostNumber { get; set; }

        public string Country { get; set; }

    }
}
