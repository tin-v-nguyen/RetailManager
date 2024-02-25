CREATE PROCEDURE [dbo].[spSale_Lookup]
	@CashierId nvarchar(128),
	@SaleDate datetime2
AS
BEGIN
	set nocount on;

	SELECT Id
	FROM dbo.Sale
	WHERE CashierId = @CashierId AND SaleDate = @SaleDate;

END
