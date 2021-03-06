﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDB.DomainModels.Models
{
    [Table("Person")]
    public class Person
    {
        public Person()
        {
            this.PrimaryAddress = new Address();
            this.AlternativeAddresses = new List<AlternativeAddress>();
            this.TelefonNumbers = new List<Telefon>();
            this.Notes = new Notes();
            this.Emails = new Email();
        }

        public long PersonID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public long AddressID { get; set; }



        public virtual Notes Notes { get; set; }
        public virtual Email Emails { get; set; }

        public virtual Address PrimaryAddress { get; set; }

        public virtual List<AlternativeAddress> AlternativeAddresses { get; set; }

        public virtual List<Telefon> TelefonNumbers { get; set; }
    }
}
