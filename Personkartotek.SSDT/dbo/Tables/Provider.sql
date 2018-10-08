CREATE TABLE [dbo].[Provider] (
    [ProviderID]   BIGINT   IDENTITY (1, 1) NOT NULL,
    [ProviderName] CHAR (1) NOT NULL,
    CONSTRAINT [pk_Provider] PRIMARY KEY CLUSTERED ([ProviderID] ASC)
);

