using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.DomainModels.Models
{
    [Table("City")]
    public class City
    {
        public int CityID { get; set; }

        public string CityName { get; set; }

        public string PostNumber { get; set; }
        public int CountryID { get; set; }

        public virtual Country country { get; set; }
    }
}
