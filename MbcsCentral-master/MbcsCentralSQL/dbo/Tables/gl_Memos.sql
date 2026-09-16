CREATE TABLE [dbo].[gl_Memos] (
    [VendorNumber]     INT            NULL,
    [CompanyNumber]    INT            NULL,
    [DepartmentNumber] INT            NULL,
    [CheckGroup]       SMALLINT       NULL,
    [Memo]             NVARCHAR (120) NULL,
    [BatchNumber]      INT            NOT NULL,
    [MemoId]           INT            IDENTITY (1, 1) NOT NULL,
    [Keep]             BIT            NOT NULL,
    CONSTRAINT [Memos$PrimaryKey1] PRIMARY KEY CLUSTERED ([MemoId] ASC)
);

