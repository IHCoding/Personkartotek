using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.DomainModels.Models
{
    [Table("Telefon")]
    public class Telefon
    {
        public int TelefonID { get; set; }

        public long Number { get; set; }

        public int PersonID { get; set; }

        public int ProviderID { get; set; }

        [Key]
        public string TelefonType { get; set; }

        public virtual Provider TelefonProvider { get; set; }
    }
}