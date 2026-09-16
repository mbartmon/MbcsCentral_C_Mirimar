CREATE TABLE [dbo].[do_Documents] (
    [DocumentId]  INT            IDENTITY (1, 1) NOT NULL,
    [PageNumber]  INT            NOT NULL,
    [Filename]    NVARCHAR (128) NOT NULL,
    [Description] NVARCHAR (128) NOT NULL,
    [GroupId1]    INT            NOT NULL,
    [GroupId2]    INT            NOT NULL,
    [GroupId3]    INT            NOT NULL,
    [Building]    NVARCHAR (3)   NOT NULL,
    [Tenant]      NVARCHAR (4)   NOT NULL,
    [TenantSeq]   INT            NOT NULL,
    [Vendor]      NVARCHAR (4)   NOT NULL,
    [Invoice]     NVARCHAR (50)  NOT NULL,
    [Date]        DATETIME       NOT NULL,
    [Date2]       DATETIME       NOT NULL,
    [Created]     DATETIME       NOT NULL,
    [OrigDocId]   INT            NOT NULL,
    CONSTRAINT [PK_do_Documents] PRIMARY KEY CLUSTERED ([DocumentId] ASC)
);

