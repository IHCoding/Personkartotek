CREATE TABLE [dbo].[AlternativeAddress] (
    [PersonID]  BIGINT   NOT NULL,
    [AddressID] BIGINT   NOT NULL,
    [AAType]    CHAR (1) NOT NULL,
    [AAID]      INT      NOT NULL,
    CONSTRAINT [pk_AlternativeAddress] PRIMARY KEY CLUSTERED ([PersonID] ASC, [AddressID] ASC),
    CONSTRAINT [fk_AlternativeAddress] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([PersonID]) ON DELETE CASCADE ON UPDATE CASCADE
);

