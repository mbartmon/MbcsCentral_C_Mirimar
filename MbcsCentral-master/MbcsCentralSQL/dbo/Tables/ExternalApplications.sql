CREATE TABLE [dbo].[ExternalApplications] (
    [Id]                 BIGINT         IDENTITY (1, 1) NOT NULL,
    [MenuName]           NVARCHAR (50)  NULL,
    [MenuContent]        NCHAR (16)     NULL,
    [MenuType]           NCHAR (16)     NULL,
    [ApplicationName]    NCHAR (16)     NULL,
    [ApplicationPath]    NVARCHAR (MAX) NULL,
    [ApplicationAppName] NCHAR (50)     NULL,
    [Sequence]           INT            NOT NULL,
    CONSTRAINT [PK_ExternalApplications] PRIMARY KEY CLUSTERED ([Id] ASC)
);

