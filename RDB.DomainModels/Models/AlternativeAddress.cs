using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.DomainModels.Models
{
    [Table("AlternativeAddress")]
    public class AlternativeAddress
    {
        public long AAID { get; set; }

        [Key]
        public string AAType { get; set; }

        // AATbl has PID og AID
        public int PersonID { get; set; }
        public int AddressID { get; set; }


        public virtual Person Persons { get; set; }
        public virtual Address Address { get; set; }
    }
}
