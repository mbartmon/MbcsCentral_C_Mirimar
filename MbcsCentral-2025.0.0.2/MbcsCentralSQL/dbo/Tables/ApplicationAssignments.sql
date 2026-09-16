CREATE TABLE [dbo].[ApplicationAssignments] (
    [ID]            BIGINT IDENTITY (1, 1) NOT NULL,
    [clientId]      BIGINT NOT NULL,
    [applicationId] INT    NOT NULL,
    [active]        BIT    NULL,
    CONSTRAINT [PK_ApplicationAssignments] PRIMARY KEY CLUSTERED ([ID] ASC)
);

