CREATE PROCEDURE [dbo].[spProduct_GetAll]
	
AS

BEGIN	
	set nocount on;

	SELECT Id, ProductName, [Description], RetailPrice, QuantityInStock, IsTaxable
	FROM dbo.Product
	ORDER BY ProductName;
END
