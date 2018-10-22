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
           

            Address altA;
            var primaryAddr = new Address() // primary address
            {
                StreetName = "Finlandsgade",
                HouseNumber = "99",
                Town = new City() { PostNumber = "8260" },
                AlternativePerson = new List<AlternativeAddress>(),
                PersonsPrimary = new List<Person>()
            };

            Person newPerson = new Person() { PersonID = 1, FirstName = "Mike", MiddleName = "", LastName = "Moore", AddressID = 7 };
            personUtil.CreatePersonDB(ref newPerson);

            personUtil.GetFullContactPersonTreeDB(ref newPerson);
            return;

            Person person1 = new Person() { PersonID = 2, FirstName = "Michael", MiddleName = "", LastName = "Rasmussen", AddressID = 8, TelefonNumbers = null, Notes = null, Emails = null, };
            personUtil.GetPersonByName(ref person1);
            personUtil.CreatePersonDB(ref person1);

           
            primaryAddr.PersonsPrimary.Add(person1); // reference from Alt address to Person
            AlternativeAddress altAdr;

            person1.PrimaryAddress = primaryAddr; // reference from person to primary addresss

            altAdr = new AlternativeAddress() // address 2 reference to alternative AAType
            {
                AAType = "Bolig",
                AddressID = 0,
                PersonID = 0,
                Persons = person1,
                Address = altA
            };


            altA = new Address() // alternavtive address
            {
                StreetName = "Hilsinkigade",
                HouseNumber = "11",
                Town = new City() { PostNumber = "8260" },
                AlternativePerson = new List<AlternativeAddress>()
            };


            altA.AlternativePerson.Add(altAdr);

            person1.AlternativeAddresses.Add(altAdr); // reference from person to ALT address

            person1.PrimaryAddress = primaryAddr;

            person1.AlternativeAddresses = personUtil.GetPersonAlternativeAddresses(ref person1);

            #region Test 2
            
            // test 2

            /*
            Person person2 = new Person() { PersonID = 1, FirstName = "Marie", MiddleName = "", LastName = "Lorie", AddressID=8};
            personUtil.CreatePersonDB(ref person2);

            Address person2Address =
                new Address() { AddressID = 1, StreetName = "Herningvej", HouseNumber = "1, 2th", CityID = 1 };
            personUtil.CreateAddressDB(ref person2Address);

            AlternativeAddress person2AlternativeAddress =
                new AlternativeAddress() { AAID = 1, AAType = "Work", PersonID = 1 };
            personUtil.UpdateAlternativeAddressDB(ref person2AlternativeAddress);

            personUtil.GetFullContactPersonTreeDB(ref person2);
            return;
            */

            #endregion
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
