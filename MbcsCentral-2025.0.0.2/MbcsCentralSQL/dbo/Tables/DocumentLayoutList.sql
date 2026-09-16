CREATE TABLE [dbo].[DocumentLayoutList] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [DocumentName]     NVARCHAR (50)  NOT NULL,
    [DocumentLocation] NVARCHAR (MAX) NULL,
    [DefaultFont]      NVARCHAR (MAX) NOT NULL,
    [CategoryId]       INT            NOT NULL,
    [Pages]            INT            NOT NULL,
    CONSTRAINT [PK_DocumentLayoutList] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [IX_DocumentName] UNIQUE NONCLUSTERED ([DocumentName] ASC)
);

