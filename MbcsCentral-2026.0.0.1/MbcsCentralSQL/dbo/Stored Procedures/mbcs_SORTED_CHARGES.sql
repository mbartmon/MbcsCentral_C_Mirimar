-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[mbcs_SORTED_CHARGES]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


SELECT pm_Charges.[Sequence Number] AS Expr1, pm_Charges.[Charge Code] AS Expr2, pm_Charges.Amount AS Expr3, pm_Charges.[Starting Date] AS Expr4
FROM pm_Charges
WHERE [pm_Charges].[Charge Code] = '9' AND [pm_Charges].[Starting Date] < GetDate()
END