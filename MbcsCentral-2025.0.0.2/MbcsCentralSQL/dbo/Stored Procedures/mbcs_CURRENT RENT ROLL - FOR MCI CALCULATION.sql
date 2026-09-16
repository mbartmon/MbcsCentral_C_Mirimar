



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[mbcs_CURRENT RENT ROLL - FOR MCI CALCULATION] 
	-- Add the parameters for the stored procedure here
	@BuildingNumber varchar(3) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT pm_Tenants.[Building Number], pm_Tenants.[Tenant Number], pm_Tenants.[Tenant Name], pm_Tenants.Type, pm_Tenants.Apartment, pm_Tenants.RentLegal, pm_Tenants.Rent, pm_Tenants.Rooms, pm_Tenants.[Move-in Date], pm_Tenants.[Future Rent 1], pm_Tenants.[Future Date 1]
FROM pm_Tenants
WHERE pm_Tenants.[Building Number]=@BuildingNumber;
END




