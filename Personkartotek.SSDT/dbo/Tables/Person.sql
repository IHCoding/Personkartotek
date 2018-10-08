CREATE TABLE [dbo].[Person] (
    [PersonID]   BIGINT   IDENTITY (1, 1) NOT NULL,
    [FirstName]  CHAR (1) NOT NULL,
    [MiddleName] CHAR (1) NOT NULL,
    [LastName]   CHAR (1) NOT NULL,
    [Email]      CHAR (1) NOT NULL,
    [Notes]      CHAR (1) NOT NULL,
    [TelefonID]  INT      NOT NULL,
    [AAID]       INT      NOT NULL,
    [AddressID]  BIGINT   NOT NULL,
    CONSTRAINT [pk_Person] PRIMARY KEY CLUSTERED ([PersonID] ASC),
    CONSTRAINT [fk_Person] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([AddressID]) ON UPDATE CASCADE
);

