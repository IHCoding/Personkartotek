using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.DomainModels.Models
{
    [Table("Country")]
    public class Country
    {
        public int CountryID { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }
    }
}
