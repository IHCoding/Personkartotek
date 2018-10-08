using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.DomainModels.Models
{
    [Table("Provider")]
    public class Provider
    {
        public Provider()
        {
            this.Telefons = new List<Telefon>();
        }

        public int ProviderID { get; set; }

        public string ProviderName { get; set; }

        public virtual List<Telefon> Telefons { get; set; }
    }
}
