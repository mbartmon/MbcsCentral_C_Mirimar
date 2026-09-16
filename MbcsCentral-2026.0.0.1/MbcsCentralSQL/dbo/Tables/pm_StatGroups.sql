CREATE TABLE [dbo].[pm_StatGroups] (
    [Group Number] SMALLINT     NOT NULL,
    [From Group]   NVARCHAR (3) NULL,
    [To Group]     NVARCHAR (3) NULL,
    [clientId]     INT          NOT NULL,
    [ID]           INT          IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_pm_StatGroups] PRIMARY KEY CLUSTERED ([ID] ASC)
);

