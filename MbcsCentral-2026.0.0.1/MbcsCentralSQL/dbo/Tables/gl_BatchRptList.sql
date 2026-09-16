CREATE TABLE [dbo].[gl_BatchRptList] (
    [Key]      INT           IDENTITY (1, 1) NOT NULL,
    [ListName] NVARCHAR (32) NULL,
    CONSTRAINT [BatchRptList$PrimaryKey] PRIMARY KEY CLUSTERED ([Key] ASC)
);

