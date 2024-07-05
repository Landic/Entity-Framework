CREATE DATABASE Goods
GO

USE Goods
GO

CREATE TABLE Countries (
    Id INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100)
);

CREATE TABLE Ñities (
    Id INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100)
);

CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100),
    DateOfBirth DATE,
    Gender NVARCHAR(10),
    Email NVARCHAR(100),
    CountryId INT FOREIGN KEY REFERENCES Countries(Id),
    CityId INT FOREIGN KEY REFERENCES Ñities(Id)
);

CREATE TABLE InterestAreas (
    Id INT PRIMARY KEY IDENTITY,
    AreaName NVARCHAR(100)
);

CREATE TABLE CustomerInterests (
    CustomerId INT FOREIGN KEY  REFERENCES Customers(Id),
    InterestAreaId INT FOREIGN KEY (InterestAreaId) REFERENCES InterestAreas(Id)
);

CREATE TABLE Promotions (
    Id INT PRIMARY KEY IDENTITY,
    PromotionName NVARCHAR(100),
    StartDate DATE,
    EndDate DATE,
    CountryId INT FOREIGN KEY REFERENCES Countries(Id)
);

CREATE TABLE PromotionGoods (
    Id INT PRIMARY KEY IDENTITY,
    PromotionId INT,
    GoodName NVARCHAR(100),
    FOREIGN KEY (PromotionId) REFERENCES Promotions(Id)
);


INSERT INTO Countries (Title) VALUES ('United States'), ('United Kingdom'), ('Canada'), ('Australia');


INSERT INTO Ñities (Title) VALUES ('New York'), ('London'), ('Toronto'), ('Sydney');


INSERT INTO Customers (FullName, DateOfBirth, Gender, Email, CountryId, CityId) VALUES ('John Doe', '1990-05-15', 'Male', 'johndoe@example.com', 1, 1),
       ('Jane Smith', '1985-08-20', 'Female', 'janesmith@example.com', 2, 2),
       ('Michael Brown', '1988-03-10', 'Male', 'michaelbrown@example.com', 3, 3),
       ('Emily Johnson', '1995-11-25', 'Female', 'emilyjohnson@example.com', 4, 4);


INSERT INTO InterestAreas (AreaName) VALUES ('Sports'), ('Music'), ('Technology'), ('Travel');
 

INSERT INTO CustomerInterests (CustomerId, InterestAreaId) VALUES (1, 1), (2, 2), (3, 3), (4, 4);




INSERT INTO Promotions (PromotionName, StartDate, EndDate, CountryId) VALUES ('Summer Sale', '2024-06-01', '2024-08-31', 1),
       ('Holiday Special', '2024-11-15', '2024-12-31', 2),
       ('Tech Expo', '2024-09-01', '2024-09-15', 3),
       ('Spring Break Deals', '2024-03-01', '2024-03-15', 4);



INSERT INTO PromotionGoods (PromotionId, GoodName) VALUES (1, 'Smartphones'), (1, 'Laptops'), (2, 'Books'), (3, 'Gadgets'), (4, 'Travel Packages');
