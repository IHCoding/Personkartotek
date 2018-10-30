using Infrastructure.PersonkartotekDB.ADONET;
using RDB.DomainModels.Models;
using System.Collections.Generic;

namespace ApplicationLogic
{
    public class PersonkartotekApp
    {
        public void ProgramApp()
        {
            PersonkartotekDBUtil personUtil = new PersonkartotekDBUtil();

            City city = new City() // city
            {
                CityID = 1,
                CityName = "Aarhus",
                Country = "DK",
                PostNumber = "8000"
            };
            personUtil.CreateCityDB(ref city);

            Address primaryAddr = new Address() // primary address
            {
                AddressID = 1,
                StreetName = "Finlandsgade",
                HouseNumber = "99",
                Town = city,
               // CityID = city.CityID,
                AlternativePerson = new List<AlternativeAddress>(),
                PersonsPrimary = new List<Person>()
            };
            
            Email email = new Email() // email
            {
                EmailID = 5,
                EmailAddress = "example@personEmail.com",
                PersonID = 11
            };

            Notes notes = new Notes() // notes
            {
                NotesID = 7,
                NotesText = "All the notes about this person comes soon...",
                PersonID = 11
            };
            
            Provider provider = new Provider() // telefon provider
            {
                ProviderID = 2,
                ProviderName = "Telia"
            };
            
            Telefon tlf = new Telefon() // Telefon
            {
                TelefonID = 26,
                Number = 22663355,
                TelefonType = "Private",
                ProviderID = 2,
                PersonID = 11,
                TelefonProvider = provider
            };
            

            // Create section
            personUtil.CreateAddressDB(ref primaryAddr);
            personUtil.CreateEmailDB(ref email);
            personUtil.CreateNotesDB(ref notes);
            personUtil.CreateTelefonDB(ref tlf);

            Person Father = new Person() { PersonID = 0, FirstName = "Martin", MiddleName = "", LastName = "Moore",
                                                AddressID = 1, PrimaryAddress =primaryAddr, Emails = email, Notes = notes};

            personUtil.CreatePersonDB(ref Father);

            Person Son = new Person() { PersonID = 2, FirstName = "Michael", MiddleName = "", LastName = "Rasmussen", AddressID = 2, TelefonNumbers = null, Notes = null, Emails = null, };

            AlternativeAddress altAdr = new AlternativeAddress() // alternavtive address /address 2 reference to alternative AAType
            {
                AAID = 0,
                AAType = "Bolig",
                AddressID = 1,
                PersonID = 0,
                Persons = Son,
                Address = primaryAddr
            };

            
            primaryAddr.PersonsPrimary.Add(Son); // reference from address to Person
            Son.PrimaryAddress = primaryAddr; // reference from person to primary addresss
            Son.AlternativeAddresses.Add(altAdr); // reference from person to ALT address
            Son.PrimaryAddress = primaryAddr;
            personUtil.CreateAlternativeAddressDB(ref altAdr);


            // Update section
            personUtil.UpdateAddressDB(ref primaryAddr);
            personUtil.UpdateEmailDB(ref email);
            personUtil.UpdateNotesDB(ref notes);
            personUtil.UpdateTelefonDB(ref tlf);
            personUtil.UpdatePersonDB(ref Father);
            primaryAddr.PersonsPrimary.Add(Father); // reference from Alt address to Person

            // Delete section
            personUtil.DeletePersonDB(ref Father);
            personUtil.DeleteTelefonDB(ref tlf);
            personUtil.DeleteNotesDB(ref notes);
            personUtil.DeleteEmailDB(ref email);
            personUtil.DeleteAddressDB(ref primaryAddr);

            return;
        }
    }
}


/*
 Select med ID
 Select all
 Delete 
 update 
 Insert
 

    unitofwork
    {
        begin
        gem address
        Primaer nogle fra Address til Person's FK
        Gem Person
        Primaer nogle fra PErson til Email's FK
        gem Email
        Praer nogle fra Person til Telefo FK
        gem Telefon
        End
    
    }

    
                // try
                // {
                // person.PersonID = (int)cmd.ExecuteScalar();
                // }
                //catchd
                //{
                //    Console.WriteLine("Adresse ID doesn't exist, do you want to add it? [y/n]");
                //    ConsoleKeyInfo input = Console.ReadKey();

                //    if (input.Key == ConsoleKey.Y)
                //    {
                //        //create an insert querry to the dbo.Adresse the same way done with the dbo.person.
                //        CreateAddressDB();
                //    }
                //}

 */
