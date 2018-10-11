--
-- Create Table    : 'Provider'   
-- ProviderID      :  
-- ProviderName    :  
--
CREATE TABLE Provider (
    ProviderID     BIGINT IDENTITY(1,1) NOT NULL,
    ProviderName   NVARCHAR(50) NOT NULL,
CONSTRAINT pk_Provider PRIMARY KEY CLUSTERED (ProviderID))