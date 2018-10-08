CREATE TABLE [dbo].[Address] (
    [AddressID]   BIGINT   IDENTITY (1, 1) NOT NULL,
    [PersonID]    INT      NOT NULL,
    [StreetName]  CHAR (1) NOT NULL,
    [HouseNumber] CHAR (1) NOT NULL,
    [PostNrID]    INT      NOT NULL,
    [AAID]        INT      NOT NULL,
    [CityID]      BIGINT   NOT NULL,
    CONSTRAINT [pk_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC),
    CONSTRAINT [fk_Address] FOREIGN KEY ([CityID]) REFERENCES [dbo].[City] ([CityID]) ON DELETE CASCADE ON UPDATE CASCADE
);

