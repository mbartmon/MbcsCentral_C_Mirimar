CREATE TABLE [dbo].[pm_STPeriods] (
    [Code]        NVARCHAR (1)  NOT NULL,
    [Description] NVARCHAR (50) NULL,
    [clientId]    INT           NOT NULL,
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm_STPeriods] PRIMARY KEY CLUSTERED ([ID] ASC)
);

