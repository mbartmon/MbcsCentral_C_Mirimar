CREATE TABLE [dbo].[pm_GLTable] (
    [From]     NVARCHAR (3) NULL,
    [To]       NVARCHAR (3) NULL,
    [clientId] INT          NOT NULL,
    [ID]       INT          IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm_GLTable] PRIMARY KEY CLUSTERED ([ID] ASC)
);

