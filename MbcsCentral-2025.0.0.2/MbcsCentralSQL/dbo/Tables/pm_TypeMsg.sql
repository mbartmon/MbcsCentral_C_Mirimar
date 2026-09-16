CREATE TABLE [dbo].[pm_TypeMsg] (
    [Type]     NVARCHAR (2)  NULL,
    [Message]  NVARCHAR (60) NULL,
    [clientId] INT           NOT NULL,
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm_TypeMsg] PRIMARY KEY CLUSTERED ([ID] ASC)
);

