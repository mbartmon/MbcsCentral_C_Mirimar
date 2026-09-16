CREATE TABLE [dbo].[gl_Journals] (
    [Layout]    NVARCHAR (16) NULL,
    [Column]    SMALLINT      NULL,
    [Field]     SMALLINT      NULL,
    [Range]     BIT           NULL,
    [Type]      SMALLINT      NULL,
    [Heading]   NVARCHAR (16) NULL,
    [PrintFlag] BIT           NULL,
    [Size]      SMALLINT      NULL,
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [Journals$PrimaryKey] PRIMARY KEY CLUSTERED ([Id] ASC)
);

