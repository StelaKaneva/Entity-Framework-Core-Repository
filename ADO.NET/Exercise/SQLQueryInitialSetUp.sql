CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY, [Name] VARCHAR(50))

CREATE TABLE Towns (Id INT PRIMARY KEY IDENTITY, [Name] VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))

CREATE TABLE Minions (Id INT PRIMARY KEY IDENTITY, [Name] VARCHAR(50), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))

CREATE TABLE EvilnessFactors (Id INT PRIMARY KEY IDENTITY, [Name] VARCHAR(50))

CREATE TABLE Villains (Id INT PRIMARY KEY IDENTITY, [Name] VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))

CREATE TABLE MinionsVillains (MinionId INT FOREIGN KEY REFERENCES Minions(Id), VillainId INT FOREIGN KEY REFERENCES Villains(Id), CONSTRAINT PK_Minions_Villains PRIMARY KEY(MinionId, VillainId))


INSERT INTO Countries (Name) VALUES ('Bulgaria'), ('Norway'), ('Cyprus'), ('Greece'), ('UK')

INSERT INTO Towns (Name, CountryCode) VALUES ('Plovdiv', 1), ('Oslo', 2), ('Larnaca', 3), ('Athens', 4), ('London', 5)

INSERT INTO Minions (Name, Age, TownId) VALUES ('Stoyan', 12, 1), ('George', 22, 2), ('Ivan', 25, 3),('Kiro', 35, 4), ('Niki', 25, 5)

INSERT INTO EvilnessFactors VALUES ('good'), ('super good'), ('super evil'), ('evil'), ('bad')

INSERT INTO Villains VALUES ('Gru', 1), ('Ivo', 2), ('Teo', 3), ('Sto', 4), ('Pro', 5)

INSERT INTO MinionsVillains VALUES (1, 1), (2, 2), (3, 3), (4, 4), (5, 5)



 
