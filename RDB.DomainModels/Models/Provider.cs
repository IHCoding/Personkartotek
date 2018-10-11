using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.DomainModels.Models
{
    [Table("Provider")]
    public class Provider
    {
        public int ProviderID { get; set; }

        public string ProviderName { get; set; }
    }
}
