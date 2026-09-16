



-- =============================================
-- Author:		Mike Bartmon
-- Create date: 9/28/2012
-- Description:	Deletes a single building and all of its tenants
-- =============================================
CREATE PROCEDURE [dbo].[DeletePmBuilding] 
	-- Add the parameters for the stored procedure here
	@BuildingNumber varchar(3),
	@NumberOfTenants int = 0 OUTPUT,
	@NumberOfOccupants int = 0 OUTPUT,
	@NumberOfLegal int = 0 OUTPUT,
	@NumberOfCharges int = 0 OUTPUT,
	@NumberOfBuildings int = 0 OUTPUT
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	BEGIN TRY
		
		BEGIN TRANSACTION

		DELETE FROM [dbo].[pm_Occupants] WHERE [ID] IN ( SELECT [Sequence Number] FROM [dbo].[pm_Tenants] WHERE [Building Number] = @BuildingNumber)
		SET @NumberOfOccupants = @@ROWCOUNT;

		DELETE FROM [dbo].[pm_Legal] WHERE [ID] IN (SELECT [Sequence Number] FROM [dbo].[pm_Tenants] WHERE [Building Number] = @BuildingNumber)
		SET @NumberOfLegal = @@ROWCOUNT;

		DELETE FROM [dbo].[pm_Charges] WHERE [Sequence Number] IN (SELECT [Sequence Number] FROM [dbo].[pm_Tenants] WHERE [Building Number] = @BuildingNumber)
		SET @NumberOfCharges = @@ROWCOUNT;

		DELETE FROM [dbo].[pm_Tenants] WHERE [Building Number] = @BuildingNumber
		SET @NumberOfTenants = @@ROWCOUNT;

		DELETE FROM [dbo].[pm_Building] WHERE [Building Number] = @BuildingNumber
		SET @NumberOfBuildings = @@ROWCOUNT;

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
			SET @NumberOfTenants = -1
			DECLARE @ErrorMessage nvarchar(4000), @ErrorSeverity int;
			SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY();
			RAISERROR(@ErrorMessage, @ErrorSeverity,1);
	END CATCH
END







