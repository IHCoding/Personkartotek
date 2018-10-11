
--SELECT        Address.AddressID, Address.PersonID, Address.StreetName, Address.HouseNumber, Address.AlternativeAddressID, Address.CityID, City.CityID AS Expr1, City.CityName, City.PostNumber, City.Country
--FROM            Address INNER JOIN City ON Address.CityID =City.CityID

--Test for at inserte data i Person table.
--insert into Person (FirstName, MiddleName, LastName, AddressID)
--values ('last', 'middleN', 'Olsen' , 3)



SELECT    Address.AddressID, Address.StreetName, Address.HouseNumber, 
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
                    WHERE       Person.PersonID = 1