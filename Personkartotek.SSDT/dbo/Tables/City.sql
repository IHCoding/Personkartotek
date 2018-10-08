CREATE TABLE [dbo].[City] (
    [CityID]     BIGINT   IDENTITY (1, 1) NOT NULL,
    [CityName]   CHAR (1) NOT NULL,
    [PostNumber] INT      NOT NULL,
    [CountryID]  INT      NOT NULL,
    [PostNrID]   BIGINT   NOT NULL,
    CONSTRAINT [pk_City] PRIMARY KEY CLUSTERED ([CityID] ASC),
    CONSTRAINT [fk_City] FOREIGN KEY ([PostNrID]) REFERENCES [dbo].[PostNr] ([PostNrID]) ON DELETE CASCADE ON UPDATE CASCADE
);

