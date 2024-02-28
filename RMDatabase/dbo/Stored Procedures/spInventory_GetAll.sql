CREATE PROCEDURE [dbo].[spInventory_GetAll]
	
AS
BEGIN
	set nocount on;
	SELECT [Id], [ProductId], [Quantity], [PurchasePrice], [PurchaseDate] 
	FROM dbo.Inventory;

END
