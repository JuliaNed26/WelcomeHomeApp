INSERT INTO dbo.Cities (Id, Name, CountryId)
VALUES 
	(NEWID(), 'Kyiv', (SELECT TOP 1 Id FROM dbo.Countries WHERE Name = 'Ukraine'));