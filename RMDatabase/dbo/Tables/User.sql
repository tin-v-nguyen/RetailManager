CREATE TABLE [dbo].[User]
(
	--auth user id comes from authentication database, entity framework, which is a 128 char GUID
    [Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
	--EF uses 256
    [EmailAddress] NVARCHAR(256) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate() 
)
