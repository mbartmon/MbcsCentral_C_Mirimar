CREATE TABLE [dbo].[pm_Occupants] (
    [ID]           INT           NOT NULL,
    [Name]         NVARCHAR (30) NULL,
    [Relationship] NVARCHAR (16) NULL,
    [DOB]          DATETIME      NULL,
    [occupantId]   INT           IDENTITY (1, 1) NOT NULL,
    [clientId]     INT           NOT NULL,
    CONSTRAINT [PK_pm_Occupants] PRIMARY KEY CLUSTERED ([occupantId] ASC)
);

