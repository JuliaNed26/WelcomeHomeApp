INSERT INTO dbo.Cities (Name, CountryId)
VALUES 
	('Kyiv', (SELECT TOP 1 Id FROM dbo.Countries WHERE Name = 'Ukraine'));