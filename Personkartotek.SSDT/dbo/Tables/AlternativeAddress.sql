--
-- Create Table    : 'AlternativeAddress'   
-- PersonID        :  (references Person.PersonID)
-- AddressID       :  (references Address.AddressID)
-- AAType          :  
-- AAID            :  
-- PersonID1       :  
-- Address         :  
--
CREATE TABLE AlternativeAddress (
    PersonID       BIGINT NOT NULL,
    AddressID      BIGINT NOT NULL,
    AAType         NVARCHAR(50) NOT NULL,
[AAID] BIGINT NOT NULL, 
    CONSTRAINT pk_AlternativeAddress PRIMARY KEY CLUSTERED ([AAID]),
CONSTRAINT fk_AlternativeAddress FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
CONSTRAINT fk_AlternativeAddress2 FOREIGN KEY (AddressID)
    REFERENCES Address (AddressID)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)