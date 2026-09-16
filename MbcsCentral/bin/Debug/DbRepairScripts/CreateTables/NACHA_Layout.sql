SET ANSI_NULLS, QUOTED_IDENTIFIER OFF;

GO

CREATE TABLE [dbo].[NACHA_Layout] (
    [Id]          INT           NOT NULL,
    [Record_Type] NCHAR (1)     NOT NULL,
    [Position]    INT           NOT NULL,
    [Length]      INT           NOT NULL,
    [Requirement] NCHAR (1)     NOT NULL,
    [Contents]    VARCHAR (MAX) NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [DataType]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_NACHA_Layout_TypePosition] UNIQUE NONCLUSTERED ([Record_Type] ASC, [Position] ASC)
);

GO

SET ANSI_NULLS, QUOTED_IDENTIFIER ON;

GO
INSERT INTO [dbo].[permission] (permission_type, permission_name, permission_description) VALUES(1, 'menuNacha', 'Edit NACHA File Layout') 
GO