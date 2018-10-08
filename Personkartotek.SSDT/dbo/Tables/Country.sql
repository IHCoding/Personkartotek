CREATE TABLE [dbo].[Country] (
    [CountryID]   BIGINT   IDENTITY (1, 1) NOT NULL,
    [CountryName] CHAR (1) NOT NULL,
    [CountryCode] CHAR (1) NOT NULL,
    CONSTRAINT [pk_Country] PRIMARY KEY CLUSTERED ([CountryID] ASC)
);

