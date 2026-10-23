
IF DB_ID(N'CrudWindowsForms') IS NULL
    CREATE DATABASE CrudWindowsForms;
GO
USE CrudWindowsForms;
GO
IF OBJECT_ID(N'dbo.People', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.People
    (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Age INT NOT NULL
    );
END;
GO

-- pilotos activos de la parrilla oficial F1 2026 y expilotos
-- Edad del piloto
-- esta tabla solo tiene las columnas Id, Name y Age.
DECLARE @F1Pilotos TABLE (Name VARCHAR(100) NOT NULL, Age INT NOT NULL);

INSERT INTO @F1Pilotos (Name, Age)
VALUES
('George Russell (ACTIVO F1 2026)', 28),
('Kimi Antonelli (ACTIVO F1 2026)', 20),
('Charles Leclerc (ACTIVO F1 2026)', 28),
('Lewis Hamilton (ACTIVO F1 2026)', 41),
('Lando Norris (ACTIVO F1 2026)', 26),
('Oscar Piastri (ACTIVO F1 2026)', 25),
('Max Verstappen (ACTIVO F1 2026)', 29),
('Isack Hadjar (ACTIVO F1 2026)', 22),
('Liam Lawson (ACTIVO F1 2026)', 24),

('Arvid Lindblad (ACTIVO F1 2026)', 19),
('Pierre Gasly (ACTIVO F1 2026)', 30),
('Franco Colapinto (ACTIVO F1 2026)', 23),
('Esteban Ocon (ACTIVO F1 2026)', 30),
('Oliver Bearman (ACTIVO F1 2026)', 21),
('Nico Hulkenberg (ACTIVO F1 2026)', 39),
('Gabriel Bortoleto (ACTIVO F1 2026)', 21),
('Carlos Sainz (ACTIVO F1 2026)', 32),
('Alexander Albon (ACTIVO F1 2026)', 30),
('Fernando Alonso (ACTIVO F1 2026)', 45),
('Lance Stroll (ACTIVO F1 2026)', 27),
('Sergio Perez (ACTIVO F1 2026)', 36),
('Valtteri Bottas (ACTIVO F1 2026)', 37),

('Michael Schumacher (RETIRADO, LEYENDA)', 57),
('Ayrton Senna (FALLECIDO, LEYENDA)', 34),
('Sebastian Vettel (RETIRADO)', 39),
('Kimi Raikkonen (RETIRADO)', 46),
('Nico Rosberg (RETIRADO)', 41),
('Jenson Button (RETIRADO)', 46),
('Felipe Massa (RETIRADO)', 45),
('Mika Hakkinen (RETIRADO)', 58),
('Damon Hill (RETIRADO)', 66),
('Alain Prost (RETIRADO)', 71),
('Niki Lauda (FALLECIDO, LEYENDA)', 70),
('Nelson Piquet (RETIRADO)', 74),
('Juan Manuel Fangio (FALLECIDO, LEYENDA)', 84),
('Nigel Mansell (RETIRADO)', 73),
('Daniel Ricciardo (EXPILOTO F1)', 37),
('Kevin Magnussen (EXPILOTO F1)', 34),
('Romain Grosjean (EXPILOTO F1)', 40),
('Emerson Fittipaldi (RETIRADO, LEYENDA)', 79),
('Jackie Stewart (RETIRADO, LEYENDA)', 87),
('Stirling Moss (FALLECIDO, LEYENDA)', 90),
('Gilles Villeneuve (FALLECIDO, LEYENDA)', 32);

INSERT INTO dbo.People (Name, Age)
SELECT pilotos.Name, pilotos.Age
FROM @F1Pilotos AS pilotos
WHERE NOT EXISTS
(
    SELECT 1
    FROM dbo.People AS existentes
    WHERE existentes.Name = pilotos.Name
);
GO
SELECT Id, Name, Age FROM dbo.People ORDER BY Id;
