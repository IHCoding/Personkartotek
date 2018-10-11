SELECT  Person.PersonID, Person.FirstName, Person.MiddleName, Person.LastName, Person.Email, Person.Notes, Person.PrimaryAddress, Person.AlternativeAddresses, Person.TelefonNumbers,
                                              Address.AddressID, Address.StreetName, Address.HouseNumber, Address.PostNr, Address.PersonPrimary, Person.AlternativePerson,
                                              City.CityID, City.CityName, City.PostNumber, City.Country,
                                              Telefon.TelefonID, Telefon.Number, Telefon.TelefonType, Telefon.TelefonProvider, 
                                              Provider.ProviderID, Provider.ProviderName
                                              
                FROM Person 
				INNER JOIN
                Address ON Person.PersonID = Address.Person 
				INNER JOIN
                Telefon ON Person.PersonID = Telefon.Person
				INNER JOIN
                AlternativeAddress ON Person.PersonID = AlternativeAddress.Person
				INNER JOIN
                Address ON Person.PersonID = Address.Person
                WHERE   (Person.PersonID = 1)
