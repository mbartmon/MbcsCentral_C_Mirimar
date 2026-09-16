CREATE TABLE [dbo].[gl_Accounts] (
    [MajorCategory]    NVARCHAR (4)   NULL,
    [MinorCategory]    NVARCHAR (4)   NULL,
    [Account Number]   SMALLINT       NULL,
    [Description]      NVARCHAR (50)  NULL,
    [Key]              INT            IDENTITY (1, 1) NOT NULL,
    [CategoryPath]     NVARCHAR (128) NULL,
    [OldAccountNumber] NVARCHAR (4)   NULL,
    [OldSubNumber]     NVARCHAR (3)   NULL,
    [OldCategory]      NVARCHAR (2)   NULL,
    [active]           BIT            NULL,
    CONSTRAINT [Accounts$PrimaryKey] PRIMARY KEY CLUSTERED ([Key] ASC)
);

