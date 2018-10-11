--
-- Create Table    : 'Email'   
-- EmailID         :  
-- EmailAddress    :  
-- PersonID        :  (references Person.PersonID)
--
CREATE TABLE Email (
    EmailID        BIGINT IDENTITY(1,1) NOT NULL,
    EmailAddress   NVARCHAR(100) NOT NULL,
    PersonID       BIGINT NOT NULL,
CONSTRAINT pk_Email PRIMARY KEY CLUSTERED (EmailID),
CONSTRAINT fk_Email FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)