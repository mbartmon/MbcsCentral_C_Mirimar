CREATE TABLE [dbo].[gl_Department] (
    [Department Number] SMALLINT      NULL,
    [Name]              NVARCHAR (50) NULL,
    [Street Address]    NVARCHAR (50) NULL,
    [City]              NVARCHAR (50) NULL,
    [State]             NVARCHAR (2)  NULL,
    [Zip]               NVARCHAR (11) NULL,
    [Company Number]    SMALLINT      NULL,
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [InActive]          BIT           NOT NULL,
    CONSTRAINT [Department$PrimaryKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);

