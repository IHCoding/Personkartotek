--
-- Create Table    : 'Telefon'   
-- TelefonID       :  
-- TelefonNumber   :  
-- ProviderID      :  
-- TelefonType     :  
-- PersonID        :  (references Person.PersonID)
-- ProviderID      :  (references Provider.ProviderID)
--
CREATE TABLE Telefon (
    TelefonID      BIGINT IDENTITY(1,1) NOT NULL,
    [Number]  BIGINT NOT NULL,
   
    TelefonType    NVARCHAR(50) NOT NULL,
    PersonID       BIGINT NOT NULL,
    ProviderID     BIGINT NOT NULL,
CONSTRAINT pk_Telefon PRIMARY KEY CLUSTERED (TelefonID), 
    CONSTRAINT [FK_Telefon_ToProvider] FOREIGN KEY ([ProviderID]) REFERENCES [Provider]([ProviderID]), 
    CONSTRAINT [FK_Telefon_ToPerson] FOREIGN KEY ([PersonID]) REFERENCES [Person]([PersonID])
)