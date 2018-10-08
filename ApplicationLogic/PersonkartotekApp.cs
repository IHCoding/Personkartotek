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
            Person person = new Person() { PersonID = 1 };

            personUtil.GetFullContactPersonTreeDB(ref person);
            return;

            //Person newPerson = new Person() { FirstName = "Barak", MiddleName = "Hussain", LastName = "Obama", Email = " ", Notes = "Find about him on Google!" };

            //personUtil.CreatePersonDB(ref newPerson);




            Person person1 = new Person() { FirstName = "Barak", MiddleName = "", LastName = "Obama", Email = "12342018@yahoo.com ", Notes = "He is a student at IHA" };
            personUtil.CreatePersonDB(ref person1);
            personUtil.GetPersonByName(ref person1);

            Address primaryAddr, altA;
            primaryAddr = new Address() // primary address
            {
                StreetName = "Finlandsgade",
                HouseNumber = "99",
                PersonID = person1.PersonID,
                PostNrID = 8200,
                AlternativePerson = new List<AlternativeAddress>(),
                PersonsPrimary = new List<Person>()
            };

            primaryAddr.PersonsPrimary.Add(person1); // reference fra Alt address til Person
            AlternativeAddress altAdr;

            person1.PrimaryAddress = primaryAddr; // reference fra person til primaert addresss


            altAdr = new AlternativeAddress() // address 2 reference til alternative klass el. AAType
            {
                AAType = "Bolig",
                AddressID = 0,
                PersonID = 0,
                Persons = person1,
                Address = altA

            };


            altA = new Address() // alternavtive address
            {
                StreetName = "Hinskigade",
                HouseNumber = "11",
                PersonID = person1.PersonID,
                PostNrID = 8200,
                AlternativePerson = new List<AlternativeAddress>()
            };


            // ... adding to the database ...
            altA.AlternativePerson.Add(altAdr);

            person1.AlternativeAddresses.Add(altAdr); // reference fra person til ALT address

            person1.PrimaryAddress = primaryAddr;



            personUtil.CreatePersonDB(ref person1);  // identical to line 25 ???

            person1.AlternativeAddresses = personUtil.GetAlternativeAddressesDB(ref person1);


            // A1 is set to Person & saved to database via pk.
            // AAT has PID og AID
        }
    }
}
