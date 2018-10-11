--
-- Create Table    : 'Notes'   
-- NotesID         :  
-- NotesText       :  
-- PersonID        :  (references Person.PersonID)
--
CREATE TABLE Notes (
    NotesID        BIGINT IDENTITY(1,1) NOT NULL,
    NotesText      NVARCHAR(2000) NOT NULL,
    PersonID       BIGINT NOT NULL,
CONSTRAINT pk_Notes PRIMARY KEY CLUSTERED (NotesID),
CONSTRAINT fk_Notes FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)