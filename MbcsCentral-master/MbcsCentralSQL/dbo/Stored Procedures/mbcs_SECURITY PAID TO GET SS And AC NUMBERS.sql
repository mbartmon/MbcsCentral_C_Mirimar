

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[mbcs_SECURITY PAID TO GET SS And AC NUMBERS]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT pm_Tenants.[Building Number], pm_Tenants.[Tenant Number], pm_Tenants.[Tenant Name], pm_Tenants.Apartment, pm_Charges.Paid, pm_Tenants.SSNumber1, pm_Tenants.SecurityAccount, pm_Tenants.SecurityBuilding
FROM pm_Tenants LEFT JOIN pm_Charges ON (pm_Tenants.[Sequence Number] = pm_Charges.[Sequence Number]) AND (pm_Tenants.[Tenant Number] = pm_Charges.[Tenant Number]) AND (pm_Tenants.[Building Number] = pm_Charges.[Building Number])
WHERE (((pm_Charges.Paid)>0) AND ((pm_Charges.[Charge Code])= 'S'))
ORDER BY pm_Tenants.[Building Number], pm_Tenants.[Tenant Number];
END


