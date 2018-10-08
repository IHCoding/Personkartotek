CREATE TABLE [dbo].[Telefon] (
    [TelefonID]     BIGINT   IDENTITY (1, 1) NOT NULL,
    [TelefonNumber] INT      NOT NULL,
    [TelefonType]   CHAR (1) NOT NULL,
    [PersonID]      BIGINT   NOT NULL,
    [ProviderID]    BIGINT   NOT NULL,
    CONSTRAINT [pk_Telefon] PRIMARY KEY CLUSTERED ([TelefonID] ASC),
    CONSTRAINT [fk_Telefon] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([PersonID]) ON UPDATE CASCADE,
    CONSTRAINT [fk_Telefon2] FOREIGN KEY ([ProviderID]) REFERENCES [dbo].[Provider] ([ProviderID]) ON UPDATE CASCADE
);

