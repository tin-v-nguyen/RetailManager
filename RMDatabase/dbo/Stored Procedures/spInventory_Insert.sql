CREATE PROCEDURE [dbo].[spInventory_Insert]
	--[ProductId], [Quantity], [PurchasePrice], [PurchaseDate] 
	@ProductId int,
	@Quantity int,
	@PurchasePrice money,
	@PurchaseDate datetime2
AS
BEGIN
	set nocount on;

	INSERT INTO dbo.Inventory(ProductId, Quantity, PurchasePrice, PurchaseDate)
	VALUES (@ProductId, @Quantity, @PurchasePrice, @PurchaseDate);
END


