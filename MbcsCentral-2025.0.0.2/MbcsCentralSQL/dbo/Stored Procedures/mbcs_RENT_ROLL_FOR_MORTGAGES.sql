-- =============================================
-- Author:		Mike Bartmon
-- Create date: 3/19/2017
-- Description:	Deletes a single building and all of its tenants
-- =============================================
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[mbcs_RENT_ROLL_FOR_MORTGAGES]
	-- Add the parameters for the stored procedure here
	@BuildingNumber varchar(3) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
IF OBJECT_ID('#Temp', 'U') IS NOT NULL
  DROP TABLE #Temp; 

CREATE TABLE #Temp (
	[Sequence Number] int,
	[Charge Code] varchar(2),
	Amount money,
	[Starting Date] datetime
	)

INSERT INTO #Temp
EXEC mbcs_SORTED_CHARGES

SELECT pm_Tenants.Apartment, pm_Tenants.Rooms, pm_Tenants.Type, pm_Tenants.[Tenant Name], [Rent] + COALESCE([AMOUNT],0) AS [MONTHLY RENT], pm_Tenants.[Lease Expiration Date]
FROM pm_Tenants 
LEFT JOIN #Temp ON pm_Tenants.[Sequence Number] = #Temp.[Sequence Number] 
WHERE (((pm_Tenants.[Building Number])=@BuildingNumber))
ORDER BY pm_Tenants.[Tenant Number]

DROP TABLE #Temp;
END






