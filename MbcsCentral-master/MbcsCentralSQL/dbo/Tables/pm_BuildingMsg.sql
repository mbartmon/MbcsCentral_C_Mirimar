CREATE TABLE [dbo].[pm_BuildingMsg] (
    [BuildingNumber] NVARCHAR (3)  NULL,
    [Message]        NVARCHAR (60) NULL,
    [clientId]       INT           NOT NULL,
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm_BuildingMsg] PRIMARY KEY CLUSTERED ([ID] ASC)
);

