using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.DomainModels.Models
{
    [Table("AlternativeAddress")]
    public class AlternativeAddress
    {
        public int AAID { get; set; }

        [Key]
        public string AAType { get; set; }

        public int PersonID { get; set; }
        public int AddressID { get; set; }


        public virtual Person Persons { get; set; }
        public virtual Address Address { get; set; }
    }
}
