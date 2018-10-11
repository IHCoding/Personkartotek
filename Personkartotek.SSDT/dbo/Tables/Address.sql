--
-- Create Table    : 'Address'   
-- AddressID       :  
-- PersonID        :  
-- StreetName      :  
-- HouseNumber     :  
-- CityID          :  
-- AlternativeAddressID :  
-- CityID          :  (references City.CityID)
--
CREATE TABLE Address (
    AddressID      BIGINT IDENTITY(1,1) NOT NULL,
    StreetName     NVARCHAR(MAX) NOT NULL,
    HouseNumber    NVARCHAR(MAX) NOT NULL,
    
    CityID         BIGINT NOT NULL,
    CONSTRAINT pk_Address PRIMARY KEY CLUSTERED (AddressID),
CONSTRAINT fk_Address FOREIGN KEY (CityID)
    REFERENCES City (CityID)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)