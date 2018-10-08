using RDB.DomainModels.Models;
using System.Data.Entity;

namespace RDB.DomainModels
{
    public class PersonkartotekContext : DbContext
    {
        public PersonkartotekContext() : base() { }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Address> PrimaryAddresses { get; set; }

        public DbSet<AlternativeAddress> AlternativeAddresses { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<PostNr> PostNumbers { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Telefon> TelefonNumbers { get; set; }

        public DbSet<Provider> TelefonProviders { get; set; }

    }
}
