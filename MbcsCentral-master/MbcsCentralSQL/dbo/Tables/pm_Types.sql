CREATE TABLE [dbo].[pm_Types] (
    [Type]              NVARCHAR (2)  NOT NULL,
    [Description]       NVARCHAR (18) NULL,
    [Billing Name]      NVARCHAR (18) NULL,
    [Print Bill]        BIT           NULL,
    [Lease]             BIT           NULL,
    [clientId]          INT           NOT NULL,
    [ID]                INT           IDENTITY (1, 1) NOT NULL,
    [printApt]          BIT           NULL,
    [LeaseName]         NVARCHAR (50) NULL,
    [LeaseReverse]      NVARCHAR (50) NULL,
    [DecontrolledLegal] BIT           NOT NULL,
    CONSTRAINT [PK_pm_Types] PRIMARY KEY CLUSTERED ([ID] ASC)
);

