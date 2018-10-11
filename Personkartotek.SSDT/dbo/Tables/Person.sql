--
-- Create Table    : 'Person'   
-- PersonID        :  
-- FirstName       :  
-- MiddleName      :  
-- LastName        :  
-- Email           :  
-- Notes           :  
-- AddressID       :  
-- TelefonID       :  
-- AAID            :  
-- AddressID       :  (references Address.AddressID)
--
CREATE TABLE Person (
	PersonID       BIGINT IDENTITY(1,1) NOT NULL,
	FirstName      VARCHAR(50) NOT NULL,
	MiddleName     NVARCHAR(50) NOT NULL,
	LastName       NVARCHAR(50) NOT NULL,
	
	AddressID      BIGINT NOT NULL,
[EmailID] BIGINT NULL, 
	[NotesID] BIGINT NULL, 
	[TelefonID] BIGINT NULL, 
	CONSTRAINT pk_Person PRIMARY KEY CLUSTERED (PersonID), 
	CONSTRAINT fk_Person FOREIGN KEY (AddressID) 
	REFERENCES Address (AddressID)
)