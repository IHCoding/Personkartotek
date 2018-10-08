
--
-- Create Table    : 'Country'   
-- CountryID       :  
-- CountryName     :  
-- CountryCode     :  
--
CREATE TABLE Country (
    CountryID      BIGINT IDENTITY(1,1) NOT NULL,
    CountryName    CHAR NOT NULL,
    CountryCode    CHAR NOT NULL,
CONSTRAINT pk_Country PRIMARY KEY CLUSTERED (CountryID))
GO

--
-- Create Table    : 'Provider'   
-- ProviderID      :  
-- ProviderName    :  
--
CREATE TABLE Provider (
    ProviderID     BIGINT IDENTITY(1,1) NOT NULL,
    ProviderName   CHAR NOT NULL,
CONSTRAINT pk_Provider PRIMARY KEY CLUSTERED (ProviderID))
GO

--
-- Create Table    : 'PostNr'   
-- PostNrID        :  
-- PostNumber      :  
-- CountryID1      :  (references Country.CountryID)
--
CREATE TABLE PostNr (
    PostNrID       BIGINT IDENTITY(1,1) NOT NULL,
    PostNumber     INT NOT NULL,
    CountryID1     BIGINT NOT NULL,
CONSTRAINT pk_PostNr PRIMARY KEY CLUSTERED (PostNrID),
CONSTRAINT fk_PostNr FOREIGN KEY (CountryID1)
    REFERENCES Country (CountryID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
GO

--
-- Create Table    : 'City'   
-- CityID          :  
-- CityName        :  
-- PostNumber      :  
-- CountryID       :  
-- PostNrID        :  (references PostNr.PostNrID)
--
CREATE TABLE City (
    CityID         BIGINT IDENTITY(1,1) NOT NULL,
    CityName       CHAR NOT NULL,
    PostNumber     INT NOT NULL,
    CountryID      INT NOT NULL,
    PostNrID       BIGINT NOT NULL,
CONSTRAINT pk_City PRIMARY KEY CLUSTERED (CityID),
CONSTRAINT fk_City FOREIGN KEY (PostNrID)
    REFERENCES PostNr (PostNrID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
GO

--
-- Create Table    : 'Address'   
-- AddressID       :  
-- PersonID        :  
-- StreetName      :  
-- HouseNumber     :  
-- PostNrID        :  
-- AAID            :  
-- CityID          :  (references City.CityID)
--
CREATE TABLE Address (
    AddressID      BIGINT IDENTITY(1,1) NOT NULL,
    PersonID       INT NOT NULL,
    StreetName     CHAR NOT NULL,
    HouseNumber    CHAR NOT NULL,
    PostNrID       INT NOT NULL,
    AAID           INT NOT NULL,
    CityID         BIGINT NOT NULL,
CONSTRAINT pk_Address PRIMARY KEY CLUSTERED (AddressID),
CONSTRAINT fk_Address FOREIGN KEY (CityID)
    REFERENCES City (CityID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
GO

--
-- Create Table    : 'Person'   
-- PersonID        :  
-- FirstName       :  
-- MiddleName      :  
-- LastName        :  
-- Email           :  
-- Notes           :  
-- TelefonID       :  
-- AAID            :  
-- AddressID       :  (references Address.AddressID)
--
CREATE TABLE Person (
    PersonID       BIGINT IDENTITY(1,1) NOT NULL,
    FirstName      CHAR NOT NULL,
    MiddleName     CHAR NOT NULL,
    LastName       CHAR NOT NULL,
    Email          CHAR NOT NULL,
    Notes          CHAR NOT NULL,
    TelefonID      INT NOT NULL,
    AAID           INT NOT NULL,
    AddressID      BIGINT NOT NULL,
CONSTRAINT pk_Person PRIMARY KEY CLUSTERED (PersonID),
CONSTRAINT fk_Person FOREIGN KEY (AddressID)
    REFERENCES Address (AddressID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
GO

--
-- Create Table    : 'Telefon'   
-- TelefonID       :  
-- TelefonNumber   :  
-- TelefonType     :  
-- PersonID        :  (references Person.PersonID)
-- ProviderID      :  (references Provider.ProviderID)
--
CREATE TABLE Telefon (
    TelefonID      BIGINT IDENTITY(1,1) NOT NULL,
    TelefonNumber  INT NOT NULL,
    TelefonType    CHAR NOT NULL,
    PersonID       BIGINT NOT NULL,
    ProviderID     BIGINT NOT NULL,
CONSTRAINT pk_Telefon PRIMARY KEY CLUSTERED (TelefonID),
CONSTRAINT fk_Telefon FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
CONSTRAINT fk_Telefon2 FOREIGN KEY (ProviderID)
    REFERENCES Provider (ProviderID)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
GO

--
-- Create Table    : 'AlternativeAddress'   
-- PersonID        :  (references Person.PersonID)
-- AddressID       :  (references Address.AddressID)
-- AAType          :  
-- AAID            :  
--
CREATE TABLE AlternativeAddress (
    PersonID       BIGINT NOT NULL,
    AddressID      BIGINT NOT NULL,
    AAType         CHAR(1) NOT NULL,
    AAID           INT NOT NULL,
CONSTRAINT pk_AlternativeAddress PRIMARY KEY CLUSTERED (PersonID,AddressID),
CONSTRAINT fk_AlternativeAddress FOREIGN KEY (PersonID)
    REFERENCES Person (PersonID)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
--	,
--CONSTRAINT fk_AlternativeAddress2 FOREIGN KEY (AddressID)
--    REFERENCES Address (AddressID)
--    ON DELETE CASCADE
--    ON UPDATE CASCADE)
GO