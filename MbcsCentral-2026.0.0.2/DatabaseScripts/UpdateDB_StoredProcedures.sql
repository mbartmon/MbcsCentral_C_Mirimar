/****** Object:  StoredProcedure [dbo].[DeletePmBuilding]    Script Date: 2017-03-19 12:23:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mike Bartmon
-- Create date: 3/19/2017
-- Description:	Deletes a single building and all of its tenants
-- =============================================
CREATE PROCEDURE [dbo].[DeleteGlDepartment] 
	-- Add the parameters for the stored procedure here
	@CompanyNumber varchar(2),
	@DepartmentNumber varchar(3),
	@NumberOfTransactions int = 0 OUTPUT,
	@NumberOfMemos int = 0 OUTPUT,
	@NumberOfBatchReports int = 0 OUTPUT,
	@NumberOfDepartments int = 0 OUTPUT
AS
	DECLARE @ErrorMessage nvarchar(4000);
	DECLARE @ErrorSeverity int;

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	BEGIN TRY
		
		BEGIN TRANSACTION

		DELETE FROM [dbo].[gl_Transactions] WHERE [CompanyNumber] = @CompanyNumber AND [DepartmentNumber] = @DepartmentNumber
		SET @NumberOfTransactions = @@ROWCOUNT;

		DELETE FROM [dbo].[gl_Memos] WHERE [CompanyNumber] = @CompanyNumber AND [DepartmentNumber] = @DepartmentNumber
		SET @NumberOfMemos = @@ROWCOUNT;

		--DELETE FROM [dbo].[gl_BatchRptList] WHERE [Sequence Number] IN (SELECT [Sequence Number] FROM [dbo].[pm_Tenants] WHERE [Building Number] = @BuildingNumber)
		--SET @NumberOfBatchReports = @@ROWCOUNT;

		DELETE FROM [dbo].[gl_Department] WHERE [Company Number] = @CompanyNumber AND [Department Number] = @DepartmentNumber
		SET @NumberOfDepartments = @@ROWCOUNT;

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT >= 0
			ROLLBACK TRANSACTION
			SET @NumberOfDepartments = -1
			SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY();
			RAISERROR(@ErrorMessage, @ErrorSeverity,1);
	END CATCH
END

GO

