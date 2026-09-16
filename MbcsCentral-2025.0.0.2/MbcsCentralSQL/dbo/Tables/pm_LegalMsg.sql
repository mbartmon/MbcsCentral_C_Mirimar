CREATE TABLE [dbo].[pm_LegalMsg] (
    [Code]     NVARCHAR (2)  NOT NULL,
    [Message]  NVARCHAR (60) NULL,
    [clientId] INT           NOT NULL,
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm_LegalMsg] PRIMARY KEY CLUSTERED ([ID] ASC)
);

