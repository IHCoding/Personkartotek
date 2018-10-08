using RDB.DomainModels;
using RDB.DomainModels.Models;
using System;


namespace ApplicationLogic
{
    class Program
    {
        static void Main()
        {
            using (var db = new PersonkartotekContext())
            {

                #region Person
                // Create Personkartotek
                Console.WriteLine("\nEnter person details to Personkartotek: ");
                Console.WriteLine("Enter first name: ");
                var firstName = Console.ReadLine();

                Console.WriteLine("Enter middle name (optional): ");
                var midlleName = Console.ReadLine();


                Console.WriteLine("Enter last name: ");
                var lastName = Console.ReadLine();


                Console.WriteLine("Enter email: ");
                var email = Console.ReadLine();

                Console.WriteLine("Enter notes about the person: ");
                var notes = Console.ReadLine();

                var person = new Person
                {
                    FirstName = firstName,
                    MiddleName = midlleName,
                    LastName = lastName,
                    Email = email,
                    Notes = notes,
                };

                #endregion  Person

                #region Address
                // Create Address
                Console.WriteLine("\nCreate address(es) for a person.");
                ConsoleKeyInfo input;

                do
                {
                    Console.WriteLine("Enter street name: ");
                    var streenName = Console.ReadLine();

                    Console.WriteLine("Enter house number: ");
                    var houseNumber = Console.ReadLine();

                    Console.WriteLine("Enter post number: ");
                    var postNumber = Console.ReadLine();

                    Console.WriteLine("Enter city: ");
                    var cityName = Console.ReadLine();

                    Console.WriteLine("Enter country code: ");
                    var countryCode = Console.ReadLine();

                    Console.WriteLine("Enter country name: ");
                    var countryName = Console.ReadLine();

                    var address = new Address
                    {
                        StreetName = streenName,
                        HouseNumber = houseNumber
                    };

                    var postnr = new PostNr
                    {
                        PostNumber = postNumber
                    };

                    var city = new City
                    {
                        CityName = cityName
                    };

                    var country = new Country
                    {
                        CountryCode = countryCode,
                        CountryName = countryName
                    };


                    address.PostNr = postnr;
                    postnr._Country = country;
                    postnr._City = city;

                    db.PostNumbers.Add(postnr);
                    db.Cities.Add(city);
                    db.Countries.Add(country);


                    person.PrimaryAddress.Add(address);

                    Console.WriteLine("\nAdd more addresses? [y/n]");
                    input = Console.ReadKey();
                } while (input.Key != ConsoleKey.N);
                #endregion

                #region AA

                // create alternative address
                Console.WriteLine("\nCreate Alternative Address(es): ");

                foreach (var address in person.PrimaryAddress)
                {
                    Console.WriteLine("Address type for: {0} {1}", address.StreetName, address.HouseNumber);
                    var type = Console.ReadLine();

                    var addressType = new AlternativeAddress
                    {
                        AAType = type
                    };

                    address.AlternativePerson.Add(addressType);

                    db.AlternativeAddresses.Add(addressType);

                    db.PrimaryAddresses.Add(address);
                }

                #endregion

                #region Telefon
                // create Telefon
                Console.WriteLine("\nCreate Telefon number(es) ");

                do
                {
                    Console.WriteLine("Enter telefon number: ");
                    var nummer = Console.ReadLine();

                    Console.WriteLine("Enter telefon number's use: ");
                    var use = Console.ReadLine();

                    Console.WriteLine("Enter telefon provider: ");
                    var provider = Console.ReadLine();

                    var telefonNumber = new Telefon
                    {
                        Number = nummer,
                        TelefonType = use
                    };

                    var telfonProvider = new Provider
                    {
                        ProviderName = provider
                    };

                    telfonProvider.Telefons.Add(telefonNumber);
                    person.TelefonNumbers.Add(telefonNumber);

                    db.TelefonNumbers.Add(telefonNumber);
                    db.TelefonProviders.Add(telfonProvider);

                    Console.WriteLine("Add more numbers? [y/n] ");
                    input = Console.ReadKey();

                } while (input.Key != ConsoleKey.N);


                #endregion

                db.Persons.Add(person);
                db.SaveChanges();
                Console.WriteLine("\n\nPerson data saved in databasen!");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
