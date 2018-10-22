using RDB.DomainModels.Models;
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
            Emails = new Email(),
            Notes = new Notes(),
            PrimaryAddress = new Address(),
            AlternativeAddresses = new List<AlternativeAddress>(),
            TelefonNumbers = new List<Telefon>()
        };

        private SqlConnection OpenConnection
        {
            get
            {
                //var con = new SqlConnection(@"Data Source=st-i4dab.uni.au.dk;User ID=E18I4DABau461895;Password=E18I4DABau461895;
                //        Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


                var con = new SqlConnection(
                    @"Data Source=(localdb)\ProjectsV13;Initial Catalog=Personkartotek.SSDT;Integrated Security=True;
                        Pooling=False;Connect Timeout=30");
                con.Open();
                return con;
            }
        }



        // CRUD operation on an address

        #region Create,Update,Delete an Address

        #region Create Address

        public void CreateAddressDB(ref Address adr)
        {
            string CreateAltAddress =
                @"INSERT INTO [Address] (StreetName, HouseNumber, CityID)
                                                    OUTPUT INSERTED.AddressID  
                                                    VALUES (@strName, @housnr, )";

            using (SqlCommand cmd = new SqlCommand(CreateAltAddress, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@StreetName", adr.StreetName);
                cmd.Parameters.AddWithValue("@HouseNumber", adr.HouseNumber);
                cmd.Parameters.AddWithValue("@CityID", adr.CityID);
                adr.AddressID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }



        #endregion

        #region Update Address

        public void UpdateAddressDB(ref Address address)
        {
            string UpdateAddress =
                @"UPDATE address
                        SET StreetName = @strName, HouseNumber = @HNumber, Town = @PostId
                        WHERE AddressID = @AddressId";

            using (SqlCommand cmd = new SqlCommand(UpdateAddress, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@StreetName", address.StreetName);
                cmd.Parameters.AddWithValue("@HNumber", address.HouseNumber);
                cmd.Parameters.AddWithValue("@PostId", address.Town);

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

        #region GetPersonAddress

        public Address GetPersonAddress(ref Person person)
        {
            var sqlcmd = @"SELECT  TOP * 
                           FROM [Address] 
                           WHERE ([Person] = @PID)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PID", person.PersonID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();

                var primaryPerAddr = new Address();
                while (rdr.Read())
                {
                    var addr = new Address
                    {
                        AddressID = (int)rdr["AddrId"],
                        StreetName = (string)rdr["StreenName"],
                        HouseNumber = (string)rdr["HouseNumber"],
                        Town = (City)rdr["Town"]
                    };

                    primaryPerAddr = addr;
                }

                return primaryPerAddr;
            }
        }


        #endregion

        #endregion



        // CRUD operation on a contact person

        #region Create, Update, Delete a Person

        #region Create a contact Person

        public void CreatePersonDB(ref Person person)
        {
            string CreatePerson =
                @"INSERT INTO [Person] (FirstName, MiddleName, LastName, AddressID)   
                                                    OUTPUT INSERTED.PersonID  
                                                    VALUES (@FName, @MName, @LName, @AddressId)";

            using (SqlCommand cmd = new SqlCommand(CreatePerson, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@FName", person.FirstName);
                cmd.Parameters.AddWithValue("@MName", person.MiddleName);
                cmd.Parameters.AddWithValue("@LName", person.LastName);
                cmd.Parameters.AddWithValue("@AddressId", person.AddressID);

                person.PersonID = (int)cmd.ExecuteScalar();
            }
        }

        #endregion

        #region UpdatePerson

        public void UpdatePersonDB(ref Person person)
        {
            string UpdatePerson =
                @"UPDATE person
                        SET FirstName = @FName, MName = @MiddleName, LName = @LastName, AddressID = @AddressId
                        WHERE PersonID = @PersonID";

            using (SqlCommand cmd = new SqlCommand(UpdatePerson, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@FName", person.FirstName);
                cmd.Parameters.AddWithValue("@MName", person.MiddleName);
                cmd.Parameters.AddWithValue("@LName", person.LastName);
                cmd.Parameters.AddWithValue("@AddressId", person.AddressID);

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

        #region GetPersonByName

        public void GetPersonByName(ref Person person)
        {
            var sqlcmd = @"SELECT  TOP * FROM Person WHERE (FirstName = @fname) AND (LastName=@lname)";
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
                    //CurrentPerson.Emails = (Email)rdr["Emails"];
                    //CurrentPerson.Notes = (Notes)rdr["Notes"];
                    //CurrentPerson.PrimaryAddress = (Address)rdr["Address"];
                    //CurrentPerson.TelefonNumbers = (List<Telefon>)rdr["Numbers"];
                    //CurrentPerson.AlternativeAddresses = (List<AlternativeAddress>)rdr["AltAddress(es)"];
                    person = CurrentPerson;
                }
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
                                                    VALUES (@AltAType, @PersonId, @AddressId)";

            using (SqlCommand cmd = new SqlCommand(CreateAltAddress, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@AltAType", AA.AAType);
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
                        SET AAType = @AltAType, PersonID = @PersonId, AddressID = @AddressId
                        WHERE AAID = @AltAddressId";

            using (SqlCommand cmd = new SqlCommand(UpdatePerson, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@AltAType", AA.AAType);
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

        #region GetPersonAlternativeAddresses

        public List<AlternativeAddress> GetPersonAlternativeAddresses(ref Person person)
        {
            var sqlcmd = @"SELECT  TOP * 
                           FROM [AlternativeAddress] 
                           WHERE ([Person] = @PID)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PID", person.PersonID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();

                List<AlternativeAddress> listPerAltAddr = new List<AlternativeAddress>();
                while (rdr.Read())
                {
                    var altAddr = new AlternativeAddress
                    {
                        AAID = (int)rdr["AddrId"],
                        AAType = (string)rdr["AltAType"], // type = work,summerhouse,etc.
                        //Address = (Address)rdr["Address"],
                        //Persons = (Person)rdr["PersonsResiding"]
                    };

                    listPerAltAddr.Add(altAddr);
                }

                return listPerAltAddr;
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
                @"UPDATE Telfeon
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

        #region GetPersonTelefon

        public List<Telefon> GetPersonTelefon(ref Person person)
        {
            var sqlcmd = @"SELECT  TOP * 
                           FROM [Telefon] 
                           WHERE ([Person] = @PID)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PID", person.PersonID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();

                var listTlf = new List<Telefon>();
                while (rdr.Read())
                {
                    var tlf = new Telefon
                    {
                        TelefonID = (int)rdr["TlfID"],
                        Number = (string)rdr["TlfNumber"],
                        TelefonType = (string)rdr["TlfType"],
                        TelefonProvider = (Provider)rdr["TlfProvider"]
                    };

                    listTlf.Add(tlf);
                }

                return listTlf;
            }
        }


        #endregion

        #endregion

        // CRUD operation on a Provider

        #region Create, Update, Delete Provider

        #region Create a Provider

        public void CreateProviderDB(ref Provider provider)
        {
            string CreateProvider = @"INSERT INTO [Provider] ( ProviderName)
                                                    OUTPUT INSERTED.ProviderID  
                                                    VALUES (@ProviderN)";

            using (SqlCommand cmd = new SqlCommand(CreateProvider, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@ProviderN", provider.ProviderName);
                provider.ProviderID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }

        #endregion

        #region Update a Provider

        public void UpdateProviderDB(ref Provider provider)
        {
            string UpdateProvider =
                @"UPDATE Provider
                        SET ProviderName = @ProviderN 
                        WHERE ProviderID = @ProviderID";

            using (SqlCommand cmd = new SqlCommand(UpdateProvider, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@ProviderN", provider.ProviderName);

                var Pid = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
            }
        }

        #endregion

        #region Delete a Provider

        public void DeleteProviderDB(ref Provider provider)
        {
            string DeleteProvider = @"DELETE FROM Provider WHERE (ProviderID = @ProviderId)";

            using (SqlCommand cmd = new SqlCommand(DeleteProvider, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@ProviderId", provider.ProviderID);

                var pid = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
                provider = null;
            }
        }

        #endregion

        #region GetPersonTelefon

        public Provider GetTelefonProvider(ref Telefon tlfProvider)
        {
            var sqlcmd = @"SELECT  TOP * 
                           FROM [Provider] 
                           WHERE ([Telefon] = @TlfId)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@tlfId", tlfProvider.TelefonID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();

                var teleProvider = new Provider();
                while (rdr.Read())
                {
                    var providerInfo = new Provider
                    {
                        ProviderID = (int)rdr["ProviderId"],
                        ProviderName = (string)rdr["ProviderName"]
                    };

                    teleProvider = providerInfo;
                }

                return teleProvider;
            }
        }


        #endregion

        #endregion


        // CRUD operation on an Email 

        #region Create, Update, Delete an Email 

        #region Create Email

        public void CreateEmailDB(ref Email email)
        {
            string CreateEmail = @"INSERT INTO [Email] (EmailAddress, PersonID)
                                                    OUTPUT INSERTED.EmailID  
                                                    VALUES ( @EimailAddress, @PersonID)";

            using (SqlCommand cmd = new SqlCommand(CreateEmail, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@EmailAddress", email.EmailAddress);
                cmd.Parameters.AddWithValue("@PersonID", email.PersonID);
                email.EmailID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }


        #endregion

        #region Update an Email

        public void UpdateEmailDB(ref Email email)
        {
            string UpdateTelefon =
                @"UPDATE telfeon
                        SET EmailAddress  = @email, PersonID = @PersonId
                        WHERE EmailID = @EmailID";

            using (SqlCommand cmd = new SqlCommand(UpdateTelefon, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@email", email.EmailAddress);
                cmd.Parameters.AddWithValue("@PersonId", email.PersonID);

                var emlId = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
            }
        }

        #endregion

        #region Delete an Email

        public void DeleteEmailDB(ref Email email)
        {
            string DeleteEmail =
                @"DELETE FROM Email WHERE (EmailID = @EmailID)";

            using (SqlCommand cmd = new SqlCommand(DeleteEmail, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@EmailID", email.EmailID);

                var pid = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
                email = null;
            }
        }

        #endregion

        #region GetPersonEmail

        public Email GetPersonEmail(ref Person person)
        {
            var sqlcmd = @"SELECT  TOP * 
                           FROM [Email] 
                           WHERE ([Person] = @PID)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PID", person.PersonID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();

                var email = new Email();
                while (rdr.Read())
                {
                    var emailInfo = new Email
                    {
                        EmailID = (int)rdr["EmailId"],
                        EmailAddress = (string)rdr["EmailAddress"]
                    };

                    email = emailInfo;
                }

                return email;
            }
        }


        #endregion

        #endregion


        // CRUD operation on Notes 

        #region Create, Update, Delete Notes 

        #region Create Notes

        public void CreateNotesDB(ref Notes notes)
        {
            string CreateNotes = @"INSERT INTO [Notes] (NotesText, PersonID)
                                                    OUTPUT INSERTED.NotesID  
                                                    VALUES (@NotesText, @PersonID)";

            using (SqlCommand cmd = new SqlCommand(CreateNotes, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@NotesText", notes.NotesText);
                cmd.Parameters.AddWithValue("@PersonID", notes.PersonID);
                notes.PersonID = (int)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record
            }
        }


        #endregion

        #region Update Notes

        public void UpdateNotesDB(ref Notes notes)
        {
            string UpdateNotes =
                @"UPDATE Notes
                        SET NotesText = @NotesText PersonID = @PersonId
                        WHERE NotesID = @NotesID";

            using (SqlCommand cmd = new SqlCommand(UpdateNotes, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@NotesText", notes.NotesText);
                cmd.Parameters.AddWithValue("@PersonId", notes.PersonID);

                var noteId = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
            }
        }

        #endregion

        #region Delete an Email

        public void DeleteNotesDB(ref Notes notes)
        {
            string DeleteNotes =
                @"DELETE FROM Notes WHERE (NotesID = @NotesID)";

            using (SqlCommand cmd = new SqlCommand(DeleteNotes, OpenConnection))
            {
                // Get your parameters ready                    
                cmd.Parameters.AddWithValue("@EmailID", notes.NotesID);

                var pid = (int)cmd.ExecuteNonQuery(); //Returns the identity of the new tuple/record
                notes = null;
            }
        }

        #endregion

        #region GetPersonNotes

        public Notes GetPersonNotes(ref Person person)
        {
            var sqlcmd = @"SELECT  TOP * 
                           FROM [Notes] 
                           WHERE ([Person] = @PID)";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PID", person.PersonID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();

                var personNotes = new Notes();
                while (rdr.Read())
                {
                    var noteInfo = new Notes
                    {
                        NotesID = (int)rdr["NotesId"],
                        NotesText = (string)rdr["Notes"]
                    };

                    personNotes = noteInfo;
                }

                return personNotes;
            }
        }

        #endregion

        #endregion


        // Overview of the Contact Person

        #region Get Person List with Complete Detail

        #region GetPersonList

        public List<Person> GetPersonList_Completenfo()
        {
            var sqlcmd = @"SELECT * FROM Person";
            using (var cmd = new SqlCommand(sqlcmd, OpenConnection))
            {
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                var PList = new List<Person>();
                Person person = null;
                while (rdr.Read())
                {
                    person = new Person
                    {
                        PersonID = (int)rdr["PersonId"],
                        FirstName = (string)rdr["fname"],
                        LastName = (string)rdr["lname"],
                        Emails = (Email)rdr["Email"],
                        Notes = (Notes)rdr["Note"],
                        PrimaryAddress = (Address)rdr["PrimaryAddress"],
                        TelefonNumbers = (List<Telefon>)rdr["Numbers"],
                        AlternativeAddresses = (List<AlternativeAddress>)rdr["AltAddress(es)"]
                    };
                    PList.Add(person);
                }

                return PList;
            }
        }


        #endregion

        #endregion

        #region Full tree for person - GetFullContactPersonTreeDB

        public void GetFullContactPersonTreeDB(ref Person fcpt)
        {
            string fullPersonkartotek =
                @"SELECT    Address.AddressID, Address.StreetName, Address.HouseNumber, 
                                City.CityID, City.CityName, City.PostNumber, City.Country,
                                Person.PersonID, Person.FirstName, Person.MiddleName, Person.LastName,
                                Telefon.TelefonID, Telefon.Number, Telefon.TelefonType, 
                                Email.EmailID, Email.EmailAddress, 
                                Notes.NotesID, Notes.NotesText
                    
                     FROM       Address INNER JOIN
                                City ON City.CityID = Address.CityID 
                                    INNER JOIN
                                Person ON Address.AddressID = Person.AddressID 
                                    INNER JOIN
                                Telefon ON Telefon.PersonID = Person.PersonID 
                                    INNER JOIN
                                Email ON Email.PersonID = Person.PersonID
                                    INNER JOIN
                                Notes ON Notes.PersonID = Person.PersonID
                    WHERE       Person.PersonID = @PersonID";


            using (SqlCommand cmd = new SqlCommand(fullPersonkartotek, OpenConnection))
            {
                cmd.Parameters.AddWithValue("@PersonID", fcpt.PersonID);
                SqlDataReader rdr = null;
                rdr = cmd.ExecuteReader();
                int personCount = 0;
                int addressCount = 0;
                int altAddressCount = 0;
                long personid = 0;
                long addid = 0;

                Person person = new Person();
                Address address = null;

                person.PrimaryAddress = new Address { };
                person.AlternativeAddresses = new List<AlternativeAddress>();
                while (rdr.Read())
                {
                    long pid = (long)rdr["PersonId"];
                    if (personid != pid)
                    {
                        personCount++;
                        person.PersonID = pid;
                        personid = person.PersonID;
                        person.FirstName = (string)rdr["FirstName"];
                        person.LastName = (string)rdr["LastName"];
                        //person.Emails = (string)rdr["EmailAddress"];
                        //person.Notes = (string)rdr["Notes"];
                    }

                    if (!rdr.IsDBNull(5))
                    {
                        addressCount++;
                        long addressid = (long)rdr["AddressID"];
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
                            address.Town = (City)rdr["Town"];
                        }
                    }

                    if (!rdr.IsDBNull(12))
                    {
                        altAddressCount++;
                        AlternativeAddress aa = new AlternativeAddress()
                        {
                            AAID = (long)rdr["AlternativeAddressID"],
                            AAType = (string)rdr["AltAddressType"],
                            //Persons = (Person)rdr["Persons"],
                            //Address = (Address)rdr["Address"]
                        };

                        address.AlternativePerson.Add(aa);
                    }
                }
            }

        }

        #endregion
    }
}