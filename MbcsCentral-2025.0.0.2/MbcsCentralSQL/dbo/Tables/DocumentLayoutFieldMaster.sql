CREATE TABLE [dbo].[DocumentLayoutFieldMaster] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [CategoryId]  INT           NOT NULL,
    [FieldName]   NVARCHAR (12) NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    [Size]        INT           NOT NULL,
    CONSTRAINT [PK_DocumentLayoutFieldMaster] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [IX_DocumentLayoutFieldMaster] UNIQUE NONCLUSTERED ([CategoryId] ASC, [FieldName] ASC)
);

