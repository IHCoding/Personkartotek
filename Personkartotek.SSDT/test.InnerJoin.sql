SELECT        Address.AddressID, Address.StreetName, Address.HouseNumber, Address.AlternativeAddressID, City.CityID, City.CityName, City.PostNumber, City.Country, Person.PersonID, Person.FirstName, Person.MiddleName, 
                         Person.LastName, Telefon.TelefonID, Telefon.Number, Telefon.TelefonType, Email.EmailID, Email.EmailAddress, Notes.NotesID, Notes.NotesText
FROM            Address INNER JOIN
                         City ON City.CityID = Address.CityID INNER JOIN
                         Person ON Address.AddressID = Person.AddressID INNER JOIN
                         Telefon ON Telefon.PersonID = Person.PersonID INNER JOIN
                         Email ON Email.PersonID = Person.PersonID INNER JOIN
                         Notes ON Notes.PersonID = Person.PersonID
                         where Person.PersonID = 1


