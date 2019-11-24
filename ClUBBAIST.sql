-- Create a new database called 'CLUBBAIST'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'CLUBBAIST'
)
CREATE DATABASE CLUBBAIST
GO

USE CLUBBAIST
GO

-- Create a new table called 'TeeTime' in schema 'SchemaName'
-- Drop the table if it already exists
IF OBJECT_ID('TeeTime', 'U') IS NOT NULL
DROP TABLE TeeTime
GO
-- Create the table in the specified schema
CREATE TABLE TeeTime
(
    [Date] DATETIME NOT NULL, -- primary key column
    [Time] TIME NOT NULL,
    Golfer1 [NVARCHAR](25) NOT NULL,
    Golfer2 [NVARCHAR](25),
    Golfer3 [NVARCHAR](25),
    Golfer4 [NVARCHAR](25)
    PRIMARY KEY ([Date],[Time])

    -- specify more columns here
);
GO

-- Create a new table called 'Golfer' in schema 'SchemaName'
-- Drop the table if it already exists
IF OBJECT_ID('Golfer', 'U') IS NOT NULL
DROP TABLE Golfer
GO
-- Create the table in the specified schema
CREATE TABLE Golfer
(
    MemberNumber INT NOT NULL PRIMARY KEY, -- primary key column
    MembershipLevel [NVARCHAR](25) NOT NULL,
    Name [NVARCHAR](25) NOT NULL
);
GO

-- Create a new table called 'StandingTeeTime'
-- Drop the table if it already exists
IF OBJECT_ID('StandingTeeTime', 'U') IS NOT NULL
DROP TABLE StandingTeeTime
GO
-- Create the table in the specified schema
CREATE TABLE StandingTeeTime
(
    StandingTeeTimeID INT NOT NULL PRIMARY KEY, -- primary key column
    MemberNumber1 INT NOT NULL,
    MemberNumber2 INT NOT NULL,
    MemberNumber3 INT NOT NULL,
    MemberNumber4 INT NOT NULL,
    MemberName1 [NVARCHAR](25) NOT NULL,
    MemberName2 [NVARCHAR](25) NOT NULL,
    MemberName3 [NVARCHAR](25) NOT NULL,
    MemberName4 [NVARCHAR](25) NOT NULL,
    DayOfWeek DATETIME NOT NULL,
    [Time] DATETIME NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL
    -- specify more columns here
);
GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_NAME = N'FindDailyTeeSheet'
)
DROP PROCEDURE FindDailyTeeSheet
GO
-- Create the stored procedure
CREATE PROCEDURE FindDailyTeeSheet
(
    @Date DATETIME = NULL
)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1
    
    IF @Date IS NULL
        RAISERROR('FindDailyTeeSheet Failed - Required Parameter: @Date',16,1)
    ELSE
        BEGIN

        SELECT * FROM TeeTime
        WHERE [Date] = @Date

        IF @@ERROR = 0
            SET @ReturnCode = 0
        ELSE RAISERROR('FindDailyTeeSheet Failed - Select Error in Database',16,1)
        END
        RETURN @ReturnCode
GO

IF EXISTS(
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_NAME = N'CreateTeeTime'
)
DROP PROCEDURE CreateTeeTime
GO

CREATE PROCEDURE CreateTeeTime
(
    @Date DATETIME = NULL,
    @Time TIME = NULL,
    @Golfer1 NVARCHAR(25) = NULL,
    @Golfer2 NVARCHAR(25) = NULL,
    @Golfer3 NVARCHAR(25) = NULL,
    @Golfer4 NVARCHAR(25) = NULL
)
AS 
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1
    IF @Date IS NULL AND @Time IS NULL AND @Golfer1 IS NULL
        RAISERROR('CreateTeeTime Failed - Required Parameters: All',16,1)
    ELSE
    IF @Date IS NULL
        RAISERROR('CreateTeeTime Failed - Required Parameters: Date', 16,1)
    ELSE
    IF @Time IS NULL
        RAISERROR('CreateTeeTime Failed - Required Parameters: Time',16,1)
    ELSE
    IF @Golfer1 IS NULL
        RAISERROR('CreateTeeTime Failed- Required Parameters: Time', 16,1)
    ELSE
        BEGIN
        INSERT INTO TeeTime([Date],[Time],Golfer1,Golfer2,Golfer3,Golfer4)
        VALUES (@Date, @Time, @Golfer1, @Golfer2, @Golfer3, @Golfer4)

        IF @@ERROR = 0
            SET @ReturnCode = 0
        ELSE RAISERROR('CreateTeeTime Failed - Insert Error in Database',16,1)
        END
        RETURN @ReturnCode
GO

-- Insert rows into table 'Golfer'
INSERT INTO Golfer
( -- columns toMembershipNumberata into
 MemberNumber, MembershipLevel, [Name]
)
VALUES
( -- first row: values for the columns in the list above
 1, 'Gold', 'Chase'
),
( -- first row: values for the columns in the list above
 2, 'Bronze', 'Haley'
),( -- first row: values for the columns in the list above
 3, 'Silver', 'Rob'
),( -- first row: values for the columns in the list above
 4, 'Gold', 'John'
),( -- first row: values for the columns in the list above
 5, 'Gold', 'Pam'
),( -- first row: values for the columns in the list above
 6, 'Silver', 'Maurice'
),( -- first row: values for the columns in the list above
 7, 'Bronze', 'Jack'
),( -- first row: values for the columns in the list above
 8, 'Gold', 'Marty'
),( -- first row: values for the columns in the list above
 9, 'Gold', 'Jessica'
)
-- add more rows here
GO


-- Insert rows into table 'TeeTime'
INSERT INTO TeeTime
( -- columns to insert data into
 [Date], [Time], Golfer1, Golfer2, Golfer3, Golfer4
)
VALUES
( -- first row: values for the columns in the list above
 '2020-08-15', '07:00:00', 'Chase', 'Haley', 'Rob', 'Stark'
),
(
'2020-08-15', '07:07:00', 'Marty', 'Pam', 'Braeden', 'Phil'
),
(
'2020-08-15', '07:30:00', 'Hello', 'GolfLover', null, null
),
(
'2020-08-15', '08:15:00', 'Marty', 'Pam', 'Braeden', 'Phil'
),
(
'2020-08-15', '09:07:00', 'Marty', 'Pam', 'Braeden', 'Phil'
),
(
'2020-08-15', '10:07:00', 'Marty', 'Pam', 'Braeden', 'Phil'
),
(
'2020-08-15', '17:07:00', 'Marty', 'Pam', 'Braeden', 'Phil'
),
(
'2020-08-15', '15:07:00', 'Marty', 'Pam', 'Braeden', 'Phil'
),
(
'2020-08-15', '16:07:00', 'Marty', 'Pam', 'Braeden', 'Phil'
),
(
'2020-08-15', '18:07:00', 'Marty', 'Pam', 'Braeden', 'Phil'
)

-- add more rows here
GO

Execute FindDailyTeeSheet '2020-08-15'