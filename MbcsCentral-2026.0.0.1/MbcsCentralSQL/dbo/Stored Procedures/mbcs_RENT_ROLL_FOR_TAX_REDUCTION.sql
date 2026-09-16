


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[mbcs_RENT_ROLL_FOR_TAX_REDUCTION]
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

SELECT pm_Tenants.Apartment, pm_Tenants.Rooms, pm_Tenants.Type, RIGHT('           ' + FORMAT([Rent] + COALESCE([AMOUNT],0) , 'C', 'en-US'), 11) AS [MONTHLY RENT]
FROM pm_Tenants 
LEFT JOIN #Temp ON pm_Tenants.[Sequence Number] = #Temp.[Sequence Number]
WHERE pm_Tenants.[Building Number] = @BuildingNumber
ORDER BY pm_Tenants.[Tenant Number];

DROP TABLE #Temp;
END




