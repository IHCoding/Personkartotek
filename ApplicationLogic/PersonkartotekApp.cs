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
            Person newPerson = new Person() { PersonID = 1, FirstName = "Mike", MiddleName = "", LastName = "Moore", };
            personUtil.CreatePersonDB(ref newPerson);

            personUtil.GetFullContactPersonTreeDB(ref newPerson);
            return;


            Person person1 = new Person() { PersonID = 2, FirstName = "Michael", MiddleName = "", LastName = "Rasmussen", TelefonNumbers = null, Notes = null, Emails = null, };
            personUtil.GetPersonByName(ref person1);
            personUtil.CreatePersonDB(ref person1);

            Address primaryAddr, altA;
            primaryAddr = new Address() // primary address
            {
                StreetName = "Finlandsgade",
                HouseNumber = "99",
                Town = new City() { PostNumber = "8260" },
                AlternativePerson = new List<AlternativeAddress>(),
                PersonsPrimary = new List<Person>()
            };

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

 */
