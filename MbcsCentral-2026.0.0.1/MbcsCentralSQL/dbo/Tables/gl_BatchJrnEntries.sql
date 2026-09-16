CREATE TABLE [dbo].[gl_BatchJrnEntries] (
    [Key]         INT           NULL,
    [Department]  INT           NULL,
    [Account]     INT           NULL,
    [Description] NVARCHAR (60) NULL,
    [Amount]      MONEY         NULL,
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [AutoOffset]  BIT           NOT NULL,
    CONSTRAINT [BatchJrnEntries$PrimaryKey] PRIMARY KEY CLUSTERED ([ID] ASC)
);

