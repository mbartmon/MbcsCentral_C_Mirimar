CREATE TABLE [dbo].[gl_ArTrans] (
    [Key]         INT           IDENTITY (1, 1) NOT NULL,
    [Company]     SMALLINT      NULL,
    [Department]  SMALLINT      NULL,
    [Account]     INT           NULL,
    [Date]        DATETIME2 (7) NULL,
    [Description] NVARCHAR (60) NULL,
    [Amount]      MONEY         NULL,
    CONSTRAINT [PK_gl_ArTrans] PRIMARY KEY CLUSTERED ([Key] ASC)
);

