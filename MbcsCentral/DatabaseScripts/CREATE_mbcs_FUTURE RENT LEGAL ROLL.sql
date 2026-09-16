USE [MbcsCentral]
GO

/****** Object:  StoredProcedure [dbo].[mbcs_FUTURE RENT LEGAL ROLL]    Script Date: 1/11/2016 16:11:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[mbcs_FUTURE RENT LEGAL ROLL] 
	-- Add the parameters for the stored procedure here
	@BuildingNumber varchar(3) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT pm_Tenants.[Building Number], pm_Tenants.[Tenant Number], pm_Tenants.[Tenant Name], pm_Tenants.AltBillName, pm_Tenants.AltBillAdd1, pm_Tenants.AltBillAdd2, pm_Tenants.AltBillAdd3, pm_Tenants.[Alternate Billing Address Flag], pm_Tenants.AddName, pm_Tenants.[Alternate Billing Address Flag]
FROM pm_Tenants
WHERE pm_Tenants.[Building Number] = @BuildingNumber
ORDER BY pm_Tenants.[Building Number];
END


GO

