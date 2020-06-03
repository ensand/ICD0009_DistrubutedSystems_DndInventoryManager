IF db_id('ensand_soft_delete') IS NOT NULL BEGIN
    USE master
    DROP DATABASE "ensand_soft_delete"
END
GO

CREATE DATABASE "ensand_soft_delete"
GO

USE "ensand_soft_delete"
GO

-- My SQL starts from here
-- Possible errors: the computer/server is too fast and the datetime stamp is not specific enough (yyyy-mm-dd hh:mm:ss.[3 ms units]). The milliseconds might not be specific enough.

SET DATEFORMAT ymd
GO

DROP TABLE IF EXISTS Treasure
DROP TABLE IF EXISTS MagicalItem
DROP TABLE IF EXISTS DndCharacter
GO

-- The master table
CREATE TABLE DndCharacter (
    Id VARCHAR(36) NOT NULL,
    DeletedAt DATETIME NOT NULL,
    CreatedAt DATETIME NOT NULL,
    Name VARCHAR(128) NOT NULL,
    CONSTRAINT CharacterPK PRIMARY KEY (Id, DeletedAt)
)

-- The one to one relationship child table
CREATE TABLE Treasure (
    Id VARCHAR(36) NOT NULL,
    DeletedAt DATETIME NOT NULL,
    CreatedAt DATETIME NOT NULL,
    CharacterId VARCHAR(36),
    CharacterDeletedAt DATETIME,
    GoldPieces INT NOT NULL,
    CONSTRAINT TreasurePK PRIMARY KEY (Id, DeletedAt),
    CONSTRAINT CharactersTreasureFK FOREIGN KEY (CharacterId, CharacterDeletedAt) REFERENCES DndCharacter(Id, DeletedAt) ON UPDATE CASCADE
)

-- The one to many relationship child table
CREATE TABLE MagicalItem (
    Id VARCHAR(36) NOT NULL,
    DeletedAt DATETIME NOT NULL,
    CreatedAt DATETIME NOT NULL,
    CharacterId VARCHAR(36),
    CharacterDeletedAt DATETIME,
    MagicalItemName VARCHAR(128) NOT NULL,
    CONSTRAINT MagicalItemPK PRIMARY KEY (Id, DeletedAt),
    CONSTRAINT CharactersMagicalItemFK FOREIGN KEY (CharacterId, CharacterDeletedAt) REFERENCES DndCharacter(Id, DeletedAt) ON UPDATE CASCADE
)
GO

-- Declare the latest possible datetime in server for not deleted entities and current time for 'CreatedAt columns'
DECLARE @MaxDateTime DATETIME = CAST('9999-12-31 23:59:59.99' AS DATETIME)
DECLARE @CurrentTime DATETIME = GETDATE()

-- Initial data, characters table
INSERT INTO DndCharacter (Id, DeletedAt, CreatedAt, Name) VALUES (NEWID(), @MaxDateTime, @CurrentTime, 'Orc McOrcson')
INSERT INTO DndCharacter (Id, DeletedAt, CreatedAt, Name) VALUES (NEWID(), @MaxDateTime, @CurrentTime, 'Fighty McChampion')
INSERT INTO DndCharacter (Id, DeletedAt, CreatedAt, Name) VALUES (NEWID(), @MaxDateTime, @CurrentTime, 'Aadu Tihevarvas')
INSERT INTO DndCharacter (Id, DeletedAt, CreatedAt, Name) VALUES (NEWID(), @MaxDateTime, @CurrentTime, 'Kitty McCat')

-- Adding stuff to Mr Orc
DECLARE @CharacterId VARCHAR(36)
DECLARE @CharacterDeletedAt DATETIME
SELECT @CharacterId = Id FROM DndCharacter WHERE Name like 'Orc McOrcson';
SELECT @CharacterDeletedAt = DeletedAt FROM DndCharacter WHERE Name like 'Orc McOrcson';

-- Initial data, Treasure table
INSERT INTO Treasure (Id, DeletedAt, CreatedAt, CharacterId, CharacterDeletedAt, GoldPieces) VALUES (NEWID(), @MaxDateTime, @CurrentTime, @CharacterId, @CharacterDeletedAt, 666)

-- Initial data, Magical items' table
INSERT INTO MagicalItem (Id, DeletedAt, CreatedAt, CharacterId, CharacterDeletedAt, MagicalItemName) VALUES (NEWID(), @MaxDateTime, @CurrentTime, @CharacterId, @CharacterDeletedAt, 'Magic missile shooting talking skull')
INSERT INTO MagicalItem (Id, DeletedAt, CreatedAt, CharacterId, CharacterDeletedAt, MagicalItemName) VALUES (NEWID(), @MaxDateTime, @CurrentTime, @CharacterId, @CharacterDeletedAt, 'Furry wand')

-- Adding stuff to Mr Kitty McCat
DECLARE @SecondCharacterId VARCHAR(36)
DECLARE @SecondCharacterDeletedAt DATETIME
SELECT @SecondCharacterId = Id FROM DndCharacter WHERE Name like 'Kitty McCat';
SELECT @SecondCharacterDeletedAt = DeletedAt FROM DndCharacter WHERE Name like 'Kitty McCat';

-- Initial data, Treasure table
INSERT INTO Treasure (Id, DeletedAt, CreatedAt, CharacterId, CharacterDeletedAt, GoldPieces) VALUES (NEWID(), @MaxDateTime, @CurrentTime, @SecondCharacterId, @SecondCharacterDeletedAt, 13)

SELECT 'Initial data'
SELECT * FROM DndCharacter
SELECT * FROM Treasure
SELECT * FROM MagicalItem
GO


-- Soft deleting an entry from the master table (no children)
SELECT 'Soft delete - single table'
DECLARE @IdToBeDeleted VARCHAR(36)
SELECT @IdToBeDeleted = Id FROM DndCharacter WHERE Name like 'Aadu Tihevarvas'
UPDATE DndCharacter SET DeletedAt = GETDATE() WHERE Id = @IdToBeDeleted

SELECT '"Aadu Tihevarvas" is marked as deleted'
SELECT * FROM DndCharacter
GO


-- Soft updating an entry from the master table (no children)
SELECT 'Soft update - single table'
--     Declare the latest possible datetime in server for not deleted entities and current time for 'CreatedAt columns'
DECLARE @MaxDateTime DATETIME = CAST('9999-12-31 23:59:59.99' AS DATETIME)
DECLARE @CurrentTime DATETIME = GETDATE()

--     Get the Id
DECLARE @IdToBeUpdated VARCHAR(36)
SELECT @IdToBeUpdated = Id FROM DndCharacter WHERE Name like 'Fighty McChampion'

-- Get the created at timestamp
DECLARE @CreatedAtTimeStamp VARCHAR(36)
SELECT  @CreatedAtTimeStamp = CreatedAt FROM DndCharacter WHERE Name like 'Fighty McChampion'

--     Mark the entry as deleted
UPDATE DndCharacter SET DeletedAt = GETDATE() WHERE Id = @IdToBeUpdated

SELECT '"Fighty McChampion" is marked as deleted'
SELECT * FROM DndCharacter

--     Make a copy with the update
INSERT INTO DndCharacter (Id, DeletedAt, CreatedAt, Name) VALUES (@IdToBeUpdated, @MaxDateTime, @CreatedAtTimeStamp, 'Human Fighty McChampion')

SELECT 'A copy of the old "Fighty McChampion" is made with the updates'
SELECT * FROM DndCharacter
GO


-- Soft deleting one-to-one (master entity)
SELECT 'Soft delete - one-to-one - deleting the master entity'
--     Get Id of the master entity
DECLARE @IdMasterToBeDeleted VARCHAR(36)
SELECT @IdMasterToBeDeleted = Id FROM DndCharacter WHERE Name like 'Kitty McCat'

--     Save current time to variable
DECLARE @CurrentTime DATETIME
SELECT @CurrentTime = GETDATE()

--     Marking the master entity as deleted
SELECT '"Kitty McCat" is marked as deleted'
UPDATE DndCharacter SET DeletedAt = @CurrentTime WHERE Id = @IdMasterToBeDeleted
SELECT * FROM DndCharacter

--     Update the child's foreign key, mark it as deleted
SELECT 'The child of "Kitty McCat" is marked as deleted'
UPDATE Treasure SET DeletedAt = @CurrentTime WHERE CharacterId = @IdMasterToBeDeleted
SELECT * FROM Treasure
GO


-- Soft updating one-to-one (master entity)
SELECT 'Soft update - one-to-one - updating the master entity'
--     Declare the latest possible datetime in server for not deleted entities and current time for 'CreatedAt columns'
DECLARE @MaxDateTime DATETIME = CAST('9999-12-31 23:59:59.99' AS DATETIME)
DECLARE @CurrentTime DATETIME = GETDATE()

--     Get the Id
DECLARE @IdMasterToBeUpdated VARCHAR(36)
SELECT @IdMasterToBeUpdated = Id FROM DndCharacter WHERE Name like 'Orc McOrcson'

--     Mark the entry as deleted
SELECT '"Orc McOrcson" is marked as deleted'
UPDATE DndCharacter SET DeletedAt = GETDATE() WHERE Id = @IdMasterToBeUpdated
SELECT * FROM DndCharacter

--     Make a copy with the update
SELECT 'A copy of the old "Orc McOrcson" is made with the updates'
INSERT INTO DndCharacter (Id, DeletedAt, CreatedAt, Name) VALUES (@IdMasterToBeUpdated, @MaxDateTime, @CurrentTime, 'Orc McSkullbasher')
SELECT * FROM DndCharacter
GO


-- Soft deleting one-to-many (master entity) and current time for 'CreatedAt columns'
SELECT 'Soft delete - one-to-many - deleting the master entity'
DECLARE @MaxDateTime DATETIME = CAST('9999-12-31 23:59:59.99' AS DATETIME)
DECLARE @CurrentTime DATETIME = GETDATE()

--     Get Id of the master entity
DECLARE @IdManyMasterToBeDeleted VARCHAR(36)
SELECT @IdManyMasterToBeDeleted = Id FROM DndCharacter WHERE Name like 'Orc McSkullbasher'

--     Marking the master entity as deleted
SELECT '"Orc McSkullbasher" is marked as deleted'
UPDATE DndCharacter SET DeletedAt = @CurrentTime WHERE Id = @IdManyMasterToBeDeleted AND DeletedAt = @MaxDateTime
SELECT * FROM DndCharacter

--     Update the children's foreign keys, mark the children as deleted
SELECT 'The children of "Orc McSkullbasher" are marked as deleted'
UPDATE MagicalItem SET DeletedAt = @CurrentTime WHERE CharacterId = @IdManyMasterToBeDeleted
SELECT * FROM MagicalItem
GO

-- Soft updating is exactly like the previous examples of updating
SELECT 'Soft updating is exactly like the previous examples of updating'


-- TIME TRAVEL
SELECT 'TIME TRAVEL'
DECLARE @DateTime DATETIME = CAST('2020-03-18 17:49:40.0' AS DATETIME)
DECLARE @MaxDateTime DATETIME = CAST('9999-12-31 23:59:59.99' AS DATETIME)

DROP TABLE IF EXISTS #TempCharacter
DROP TABLE IF EXISTS #TempTreasure
DROP TABLE IF EXISTS #TempMagicalItem

SELECT * INTO #TempCharacter FROM DndCharacter WHERE CreatedAt <= @DateTime
UPDATE #TempCharacter SET DeletedAt = @MaxDateTime WHERE DeletedAt >= @DateTime

SELECT * INTO #TempTreasure FROM Treasure WHERE CreatedAt <= @DateTime
UPDATE #TempTreasure SET DeletedAt = @MaxDateTime WHERE DeletedAt >= @DateTime

SELECT * INTO #TempMagicalItem FROM MagicalItem WHERE CreatedAt <= @DateTime
UPDATE #TempMagicalItem SET DeletedAt = @MaxDateTime WHERE DeletedAt >= @DateTime

SELECT 'Characters at selected time'
SELECT * FROM #TempCharacter
SELECT 'Characters currently'
SELECT * FROM DndCharacter
SELECT 'Treasure at selected time'
SELECT * FROM #TempTreasure
SELECT 'Treasure currently'
SELECT * FROM Treasure
SELECT 'Magical items at selected time'
SELECT * FROM #TempMagicalItem
SELECT 'Magical items currently'
SELECT * FROM MagicalItem

USE ensand_dist_courseProj;

SELECT * FROM AspNetUsers;
SELECT * FROM DndCharacters;
SELECT * FROM OtherEquipments;
SELECT * FROM Weapons;
SELECT * FROM Armors;
SELECT * FROM MagicalItems;
