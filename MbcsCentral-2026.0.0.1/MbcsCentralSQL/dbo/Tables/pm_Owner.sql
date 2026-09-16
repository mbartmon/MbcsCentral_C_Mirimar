CREATE TABLE [dbo].[pm_Owner] (
    [Owner Number] SMALLINT     NOT NULL,
    [Address 1]    VARCHAR (30) NULL,
    [Address 2]    VARCHAR (30) NULL,
    [Telephone]    VARCHAR (12) NULL,
    [FederalID]    VARCHAR (11) NULL,
    [Owner Name]   VARCHAR (50) NULL,
    [Billing Name] VARCHAR (80) NULL,
    [email]        VARCHAR (50) NULL,
    [ID]           INT          IDENTITY (1, 1) NOT NULL,
    [clientId]     INT          NOT NULL,
    [InActive]     BIT          NOT NULL,
    [BankRoutingNumber] NCHAR(9) NOT NULL DEFAULT ' ', 
    [BankAccountNumber] NCHAR(17) NOT NULL DEFAULT ' ', 
    CONSTRAINT [PK_Owner] PRIMARY KEY CLUSTERED ([ID] ASC)
);

