CREATE TABLE [dbo].[gl_BatchJrnHeader] (
    [Key]       INT           IDENTITY (1, 1) NOT NULL,
    [BatchName] NVARCHAR (64) NULL,
    [Company]   SMALLINT      NULL,
    [Journal]   NVARCHAR (32) NULL,
    [Source]    NVARCHAR (32) NULL,
    CONSTRAINT [BatchJrnHeader$PrimaryKey] PRIMARY KEY CLUSTERED ([Key] ASC)
);

