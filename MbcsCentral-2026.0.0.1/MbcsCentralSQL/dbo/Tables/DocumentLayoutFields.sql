CREATE TABLE [dbo].[DocumentLayoutFields] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [DocumentId] INT           NOT NULL,
    [FieldId]    INT           NOT NULL,
    [Left]       MONEY         NOT NULL,
    [Top]        MONEY         NOT NULL,
    [Size]       INT           NOT NULL,
    [Class]      VARCHAR (MAX) NULL,
    CONSTRAINT [PK_DocumentLayoutFields] PRIMARY KEY CLUSTERED ([ID] ASC)
);

