CREATE TABLE [dbo].[Clients] (
    [clientId]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [clientName]     VARCHAR (50)  NULL,
    [clientAddress1] VARCHAR (50)  NULL,
    [clientAddress2] VARCHAR (50)  NULL,
    [clientCity]     VARCHAR (50)  NULL,
    [clientState]    VARCHAR (2)   NULL,
    [clientZip]      VARCHAR (10)  NULL,
    [clientPhone]    VARCHAR (50)  NULL,
    [Emails]         VARCHAR (MAX) NULL,
    [WinGLEnabled]   BIT           NOT NULL,
    [AchCompanyName] NCHAR(50) NULL, 
    [AchFederalID] NCHAR(10) NULL, 
    [AchBatchNumber] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([clientId] ASC)
);

