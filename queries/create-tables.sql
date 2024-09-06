--CREATE TABLE dbo.Customers
--(
--	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
--	Email nvarchar(MAX) NOT NULL,
--	FirstName nvarchar(MAX) NOT NULL,
--	LastName nvarchar(MAX) NOT NULL
--);

--CREATE TABLE dbo.Genres
--(
--	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
--	[Name] nvarchar(MAX) NOT NULL,
--	DisplayName nvarchar(MAX) NOT NULL
--);

--CREATE TABLE dbo.Movies
--(
--	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
--	Title nvarchar(MAX) NOT NULL,
--	ReleaseDate [datetime2](7) NOT NULL,
--	GenreId int NOT NULL FOREIGN KEY REFERENCES Genres(Id),
--	Quantity int NOT NULL
--);

--CREATE TABLE dbo.CustomerMovie
--(
--	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
--	CustomerId int NOT NULL FOREIGN KEY REFERENCES Customers(Id),
--	MovieId int NOT NULL FOREIGN KEY REFERENCES Movies(Id),
--	RentDate [datetime2](7) NOT NULL,
--	ReturnDate [datetime2](7) NULL
--);





