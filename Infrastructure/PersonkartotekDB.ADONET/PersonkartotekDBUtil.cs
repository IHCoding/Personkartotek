using RDB.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Infrastructure.PersonkartotekDB.ADONET
{
    public class PersonkartotekDBUtil
    {
        public Person CurrentPerson;

        public PersonkartotekDBUtil() => CurrentPerson = new Person()
        {
            PersonID = 0,
            FirstName = " ",
            MiddleName = "",
            LastName = "",
            Email = null,
            Notes = "",
            PrimaryAddress = new Address(),
            AlternativeAddresses = new List<AlternativeAddress>(),
            TelefonNumbers = new List<Telefon>()
        };


        private SqlConnection OpenConnection
        {
            get
            {
                var con = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=Personkartotek.SSDT;Integrated Security=True;Pooling=False;Connect Timeout=30");
                con.Open();
                return con;
            }
        }


        // CRUD operation on a contact person
        #region Create, Update, Delete a Person

        #region Create a contact Person
        public void CreatePersonDB(ref Person person)
        {
            // prepare command string using paramters in string and returning the given identity 

            string CreatePerson = @"INSERT INTO [Person] (FirstName, LastName, Email, Notes, TelfonNumbers, PrimaryAddress, AlternativeAddresses)
                                                    OUTPUT INSERTED.PersonID  
                                                    VALUES (@FirstName, @LastName,@Email, @Note, @TlfNumber, @PA, @AD )";

            using (SqlCommand cmd = new SqlCommand(CreatePerson, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@Email", person.Email);
                cmd.Parameters.AddWithValue("@Note", person.Notes);
                cmd.Parameters.AddWithValue("@TlfNumber", person.Notes);
                cmd.Parameters.AddWithValue("@PA", person.PrimaryAddress);
                cmd.Parameters.AddWithValue("@AA", person.AlternativeAddresses);
                person.PersonID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }
        #endregion


        #region UpdatePerson
        public void UpdatePersonDB(ref Person person)
        {
            string UpdatePerson =
                @"UPDATE person
                        SET FirstName = @FirstName, LastName = @LastName, Email = @Email, TelefonNumbers = @TlfNumber, 
                                Notes = @Note, PrimaryAddress = @PA, AlternativeAddresses=AA
                        WHERE PersonID = @PersonID";

            using (SqlCommand cmd = new SqlCommand(UpdatePerson, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@Email", person.Email);
                cmd.Parameters.AddWithValue("@Note", person.Notes);
                cmd.Parameters.AddWithValue("@TlfNumber", person.TelefonNumbers);
                cmd.Parameters.AddWithValue("@PA", person.PrimaryAddress);
                cmd.Parameters.AddWithValue("@AA", person.AlternativeAddresses);
                cmd.Parameters.AddWithValue("@PersonID", person.PersonID);

                var Pid = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
            }
        }
        #endregion


        #region DeletePerson
        public void DeletePersonDB(ref Person person)
        {
            string DeletePerson =
                @"DELETE FROM Person WHERE (PersonID = @PersonID)";

            using (SqlCommand cmd = new SqlCommand(DeletePerson, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@PersonID", person.PersonID);

                var Pid = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
                person = null;
            }
        }
        #endregion

        #endregion


        // CRUD operation on an address
        #region Create,Update,Delete an Address

        #region Create Address

        public void CreateAddressDB(ref Address adr)
        {
            string CreateAltAddress = @"INSERT INTO [Address] (StreetName, HouseNumber, PostNr, PersonsPrimary, AlternativePerson)
                                                    OUTPUT INSERTED.AddressID  
                                                    VALUES (@strName, @housnr, )";

            using (SqlCommand cmd = new SqlCommand(CreateAltAddress, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@StreetName", adr.StreetName);
                cmd.Parameters.AddWithValue("@HouseNumber", adr.HouseNumber);
                cmd.Parameters.AddWithValue("@PostNr", adr.PostNr);
                cmd.Parameters.AddWithValue("@PersonPrimary", adr.PersonsPrimary);
                cmd.Parameters.AddWithValue("@AlternativePerson", adr.AlternativePerson);
                adr.AddressID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }



        #endregion

        #region Update Contact Address
        public void UpdateAddressDB(ref Address address)
        {
            string UpdateAddress =
                @"UPDATE address
                        SET StreetName = @strName, HouseNumber = @HNumber, PostNrID = @PostId, AlternativeAddressID = @AAddrId, PersonID = @PersonId
                        WHERE AddressID = @AddressId";

            using (SqlCommand cmd = new SqlCommand(UpdateAddress, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@StreetName", address.StreetName);
                cmd.Parameters.AddWithValue("@HNumber", address.HouseNumber);
                cmd.Parameters.AddWithValue("@PostId", address.PostNrID);
                cmd.Parameters.AddWithValue("@AAddrId", address.AlternativeAddressID);
                cmd.Parameters.AddWithValue("@PersonId", address.PersonID);

                var Pid = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
            }
        }
        #endregion

        #region Delete Address
        public void DeleteAddressDB(ref Address adr)
        {
            string DeleteAddress =
                @"DELETE FROM Address WHERE (Address = @addr)";

            using (SqlCommand cmd = new SqlCommand(DeleteAddress, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@addr", adr.AddressID);

                var adId = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
                adr = null;
            }
        }
        #endregion

        #endregion


        // CRUD operation on an Alternative Address
        #region Create, Update, Delete an Alternative Address

        #region Create Alternative Address
        public void CreateAddressDB(ref AlternativeAddress AA)
        {
            string CreateAltAddress = @"INSERT INTO [AlternativeAddress] (AAType, PersonID, AddressID)
                                                    OUTPUT INSERTED.AAID  
                                                    VALUES (@AltAddressType, @PersonId, @AddressId)";

            using (SqlCommand cmd = new SqlCommand(CreateAltAddress, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AltAddressType", AA.AAType);
                cmd.Parameters.AddWithValue("@PersonId", AA.PersonID);
                cmd.Parameters.AddWithValue("@AddressId", AA.AddressID);
                AA.AAID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }
        #endregion


        #region Update Alternative Address
        public void UpdateAlternativeAddressDB(ref AlternativeAddress AA)
        {
            string UpdatePerson =
                @"UPDATE AA
                        SET AAType = @AltAddressType, PersonID = @PersonId, AddressID = @AddressId
                        WHERE AAID = @AltAddressId";

            using (SqlCommand cmd = new SqlCommand(UpdatePerson, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@AltAddressType", AA.AAType);
                cmd.Parameters.AddWithValue("@PersonId", AA.PersonID);
                cmd.Parameters.AddWithValue("@AddressId", AA.AddressID);

                var Pid = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
            }
        }
        #endregion


        #region Delete Alternative Address
        public void DeleteAlternativeAddressDB(ref AlternativeAddress AA)
        {
            string DeleteALtAddress =
                @"DELETE FROM AlternativeAddress WHERE (AAID = @AAId)";

            using (SqlCommand cmd = new SqlCommand(DeleteALtAddress, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AAId", AA.AAID);

                var AAid = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
                AA = null;
            }
        }
        #endregion

        #endregion


        // CRUD operation on a Telefon
        #region Create, Update, Delete a Telefon
        #region Create a Telefon
        public void CreateTelefonDB(ref Telefon tlf)
        {
            string CreateTelefon = @"INSERT INTO [Telefon] (Number, PersonID, ProviderID, TelefonType)
                                                    OUTPUT INSERTED.TelefonID  
                                                    VALUES (@TlfNr, @TlfType, @ProviderId, @PersonId)";

            using (SqlCommand cmd = new SqlCommand(CreateTelefon, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@TlfNr", tlf.Number);
                cmd.Parameters.AddWithValue("@TlfType", tlf.TelefonType);
                cmd.Parameters.AddWithValue("@ProviderId", tlf.ProviderID);
                cmd.Parameters.AddWithValue("@PersonId", tlf.PersonID);
                tlf.PersonID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }
        #endregion


        #region Update a Telefon
        public void UpdateTelefonDB(ref Telefon tlf)
        {
            string UpdateTelefon =
                @"UPDATE telfeon
                        SET Number = @TlfNr, TelefonType = @TlfType, ProviderID = @ProviderId, PersonID = @PersonId
                        WHERE TelefonID = @TelefonId";

            using (SqlCommand cmd = new SqlCommand(UpdateTelefon, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@TlfNr", tlf.Number);
                cmd.Parameters.AddWithValue("@TlfType", tlf.TelefonType);
                cmd.Parameters.AddWithValue("@ProviderId", tlf.ProviderID);
                cmd.Parameters.AddWithValue("@PersonId", tlf.PersonID);

                var TlfId = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
            }
        }
        #endregion


        #region Delete a Telefon
        public void DeleteTelefonDB(ref Telefon tlf)
        {
            string DeleteTelefon =
                @"DELETE FROM Telefon WHERE (TelefonID = @TelefonId)";

            using (SqlCommand cmd = new SqlCommand(DeleteTelefon, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@TelefonId", tlf.TelefonID);

                var pid = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
                tlf = null;
            }
        }
        #endregion

        #endregion


        // Overview of the Contact Person
        #region Get person by name & Get Person List 
        public void GetPersonByName(ref Person person)
        {
            var sqlcmd = @"SELECT  TOP 1 * FROM Person WHERE (FirstName = @fname) AND (LastName=@lname)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@fname", person.FirstName);
                cmd.Parameters.AddWithValue("@lname", person.LastName);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    CurrentPerson.PersonID = (int)rdr["PersonId"];
                    CurrentPerson.FirstName = (string)rdr["fname"];
                    CurrentPerson.LastName = (string)rdr["lname"];
                    CurrentPerson.Email = (string)rdr["Email"];
                    CurrentPerson.Notes = (string)rdr["Note"];
                    CurrentPerson.PrimaryAddress = (Address)rdr["Address"];
                    CurrentPerson.TelefonNumbers = (List<Telefon>)rdr["Numbers"];
                    CurrentPerson.AlternativeAddresses = rdr["AltAddress(es)"] as List<AlternativeAddress>;
                    person = CurrentPerson;
                }
            }
        }


        public List<Person> GetPersonList()
        {
            var sqlcmd = @"SELECT * FROM Person";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                List<Person> PList = new List<Person>();
                Person person = null;
                while (rdr.Read())
                {
                    person = new Person
                    {
                        PersonID = (int)rdr["PersonId"],
                        FirstName = (string)rdr["fname"],
                        LastName = (string)rdr["lname"],
                        Email = (string)rdr["Email"],
                        Notes = (string)rdr["Note"],
                        PrimaryAddress = (Address)rdr["Address"],
                        TelefonNumbers = (List<Telefon>)rdr["Numbers"],
                        AlternativeAddresses = rdr["AltAddress(es)"] as List<AlternativeAddress>
                    };
                    PList.Add(person);
                }
                return PList;
            }
        }
        #endregion

        #region Full tree for person - GetFullContactPersonTreeDB
        public void GetFullContactPersonTreeDB(ref Person fcpt)
        {
            string fullPersonkartotek = @"SELECT  Person.PersonId, Person.FirstName, Person.MiddleName, Person.LastName, Person.Email, Person.Notes, Person.PrimaryAddress, Person.AlternativeAddresses, Person.TelefonNumbers
                                              Address.AddressID, Address.StreetName, Address.HouseNumber, Address.PostNr, Address.PersonPrimary, Person.AlternativePerson
                                              PostNr.PostNrID, PostNr.PostNumber, PostNr._Country, PostNr._City
                                              City.CityID, City.CityName,
                                              Country.CountryID, Country.CountryCode, Country.CountryName,
                                              Telefon.TelefonID, Telefon.Number, Telefon.TelefonType, Telefon.TelefonProvider, 
                                              Provider.ProviderID, Provider.ProviderName
                                              
                FROM      Person INNER JOIN
                Address ON Person.PersonId = Address.Person INNER JOIN
                Telefon ON Person.PersonID = Telefon.Person
                AlternativeAddress ON Person.PersonID = AlternativeAddress.Person
                Address ON Person.PersonID = Address.Person
                WHERE   (Person.PersonID = @PersonId)";

            try
            {
                using (var cmd = new SqlCommand(fullPersonkartotek, OpenConnection))
                {
                    cmd.Parameters.AddWithValue("@PersonId", fcpt.PersonID);
                    SqlDataReader rdr = null;
                    rdr = cmd.ExecuteReader();
                    var personCount = 0;
                    var addressCount = 0;
                    var altAddressCount = 0;
                    int personid = 0;
                    int addid = 0;

                    Person person = new Person();
                    Address address = null;

                    person.PrimaryAddress = new Address { };
                    person.AlternativeAddresses = new List<AlternativeAddress>() { };
                    while (rdr.Read())
                    {
                        var pid = (int)rdr["PersonId"];
                        if (personid != pid) //Hent root Person
                        {
                            personCount++;
                            person.PersonID = pid;
                            personid = person.PersonID;
                            person.FirstName = (string)rdr["FirstName"];
                            person.LastName = (string)rdr["LastName"];
                            person.Email = (string)rdr["Email"];
                            person.Notes = (string)rdr["Note"];
                        }
                        if (!rdr.IsDBNull(5))
                        {
                            addressCount++;
                            var addressid = (int)rdr["addressasseID"];
                            if (addid != addressid)
                            {
                                address = new Address
                                {
                                    PersonsPrimary = new List<Person> { },
                                    AddressID = addressid
                                };
                                person.PrimaryAddress = address;
                            }

                            if (address != null)
                            {
                                addid = address.AddressID;
                                address.StreetName = (string)rdr["StreetName"];
                                address.HouseNumber = (string)rdr["HouseNumber"];
                                address.PostNr = (PostNr)rdr["PostNr"];
                                address.PersonID = (int)rdr["Person"];
                            }
                        }

                        if (!rdr.IsDBNull(12))
                        {
                            altAddressCount++;
                            AlternativeAddress aa = new AlternativeAddress();

                            aa.AAID = (int)rdr["Alt Address ID"];
                            aa.AAType = (string)rdr["Alt Address Type"];
                            aa.Persons = (Person)rdr["Persons"];
                            aa.Address = (Address)rdr["Address"];
                            address.AlternativePerson.Add(aa);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ToString();
                throw;
            }

        }


        #endregion

        #region Get Alternative address list - GetAlternativeAddress
        public List<AlternativeAddress> GetAlternativeAddressesDB(ref Person person)
        {
            string selectToolboxToolString = @"SELECT *
                                                  FROM [AlternativeAddress] 
                                                  WHERE ([Person] = @PersonId)";
            using (var cmd = new SqlCommand(selectToolboxToolString, OpenConnection))
            {

                SqlDataReader rdr = null;
                cmd.Parameters.AddWithValue("@PersonId", person.PersonID);
                rdr = cmd.ExecuteReader();
                List<AlternativeAddress> addressList = new List<AlternativeAddress>();
                AlternativeAddress aa = null;
                while (rdr.Read())
                {
                    aa = new AlternativeAddress
                    {
                        AAType = (string)rdr["AddressType"],
                        Persons = (Person)rdr["Person"],
                        AddressID = (int)rdr["AddressId"]
                    };

                    addressList.Add(aa);
                }
                return addressList;
            }
        }
        #endregion

        // CRUD operation on an email & Notes
        #region Create, Update, Delete an Email & Note
        // Email og Notes er blev oprettet, updated og slettet under Contact Person ovenfor.
        // Da de begge to er defineret som attribute
        #endregion

    }
}