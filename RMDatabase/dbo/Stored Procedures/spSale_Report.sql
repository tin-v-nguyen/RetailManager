CREATE PROCEDURE [dbo].[spSale_Report]
	
AS
BEGIN
	set nocount on;
	SELECT [s].[SaleDate], [s].[SubTotal], [s].[Tax], [s].[Total], u.FirstName, u.LastName, u.EmailAddress
	FROM dbo.Sale s
	INNER JOIN dbo.[User] u on s.CashierId = u.Id;
END
