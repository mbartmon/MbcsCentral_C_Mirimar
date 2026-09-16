CREATE TABLE [dbo].[gl_Category] (
    [Key]                 NVARCHAR (4)  NULL,
    [SubOf]               NVARCHAR (4)  NULL,
    [CategoryDescription] NVARCHAR (32) NULL,
    [Sequence]            SMALLINT      NULL,
    [OldKey]              NVARCHAR (2)  NULL,
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [Category$PrimaryKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);

