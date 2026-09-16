Insert [dbo].[Permission] (permission_type, permission_name, permission_description) VALUES (1, N'gl_batchinvoices_private', N'Batch Invoice Processing - Private');
GO
Insert [dbo].[Permission] (permission_type, permission_name, permission_description) VALUES (1, N'gl_invoiceentry', N'Invoice Entry and Edit');
GO


ALTER TABLE [dbo].[gl_BatchControl]
	ADD [Private]	BIT Default 0 NOT NULL;
GO