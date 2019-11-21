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


-- Create a new table called 'TeeTime' in schema 'SchemaName'
-- Drop the table if it already exists
IF OBJECT_ID('TeeTime', 'U') IS NOT NULL
DROP TABLE TeeTime
GO
-- Create the table in the specified schema
CREATE TABLE TeeTime
(
    [Date] DATETIME NOT NULL, -- primary key column
    [Time] DATETIME NOT NULL,
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