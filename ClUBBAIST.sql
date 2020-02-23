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
    MembershipLevel INT NOT NULL,
    FirstName NVARCHAR(25) NOT NULL,
    LastName NVARCHAR(25) NOT NULL,
    [Password] NVARCHAR(25) NOT NULL,
    [Address] NVARCHAR(50) NULL,
    PostalCode NVARCHAR(6) NULL,
    Phone NVARCHAR(10) NULL,
    AltPhone NVARCHAR(10) NULL,
    Email NVARCHAR(100) NULL,
    DateOfBirth DATE NULL,
    Occupation NVARCHAR(50) NULL,
    CompanyName NVARCHAR(50) NULL,
    CompanyAddress NVARCHAR(50) NULL,
    CompanyPostalCode NVARCHAR(6) NULL,
    CompanyPhone NVARCHAR(10) NULL,
    MembershipStartDate DATE NULL,
    Shareholder BIT NULL,
    Approved CHAR NULL
);
GO

IF OBJECT_ID('MembershipLevel','U') IS NOT NULL
DROP TABLE MembershipLevel
GO

CREATE TABLE MembershipLevel
(
    MembershipLevel INT NOT NULL PRIMARY KEY,
    [Description] NVARCHAR(40) NOT NULL
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
    StandingTeeTimeID INT IDENTITY(1,1) PRIMARY KEY, -- primary key column
    MemberNumber1 INT NOT NULL,
    MemberNumber2 INT NOT NULL,
    MemberNumber3 INT NOT NULL,
    MemberNumber4 INT NOT NULL,
    MemberName1 [NVARCHAR](25) NOT NULL,
    MemberName2 [NVARCHAR](25) NOT NULL,
    MemberName3 [NVARCHAR](25) NOT NULL,
    MemberName4 [NVARCHAR](25) NOT NULL,
    DayOfWeek DATETIME NOT NULL,
    [Time] TIME NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    [Status] BIT NOT NULL DEFAULT 0
    -- specify more columns here
)
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


IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_NAME = N'GetGolfer'
)
DROP PROCEDURE GetGolfer
GO
-- Create the stored procedure
CREATE PROCEDURE GetGolfer
(
    @MemberNumber INT = NULL
)
AS
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1
    
    IF @MemberNumber IS NULL
        RAISERROR('GetGolfer Failed - Required Parameter: @MemberNumber',16,1)
    ELSE
        BEGIN

        SELECT * FROM Golfer
        WHERE MemberNumber = @MemberNumber

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


IF EXISTS(
    SELECT *
        FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_NAME = N'CreateStandingTeeTimeRequest'
)
DROP PROCEDURE CreateStandingTeeTimeRequest
GO
CREATE PROCEDURE CreateStandingTeeTimeRequest
(
    @MemberNumber1 INT = NULL,
    @MemberNumber2 INT = NULL,
    @MemberNumber3 INT = NUll,
    @MemberNumber4 INT = NULL,
    @MemberName1 [NVARCHAR](25) = NULL,
    @MemberName2 [NVARCHAR](25) = NULL,
    @MemberName3 [NVARCHAR](25) = NULL,
    @MemberName4 [NVARCHAR](25) = NULL,
    @DayOfWeek DATETIME = NULL,
    @Time TIME = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL
)
AS 
    DECLARE @ReturnCode INT
    SET @ReturnCode = 1

    IF @MemberNumber1 IS NULL AND @MemberNumber2 IS NULL AND @MemberNumber3 IS NULL AND @MemberNumber4 IS NULL AND @MemberName1 IS NULL AND @MemberName2 IS NULL AND @MemberName3 IS NULL AND @MemberName4 IS NULL AND @DayOfWeek IS NULL AND @Time IS NULL AND @StartDate IS NULL AND @EndDate IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: All',16,1)
    ELSE
    IF @MemberNumber1 IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @MemberNumber1',16,1)
    ELSE
    IF @MemberNumber2 IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @MemberNumber2',16,1)
    ELSE
    IF @MemberNumber3 IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @MemberNumber3',16,1)
    ELSE
    IF @MemberNumber4 IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @MemberNumber4',16,1)
    ELSE
    IF @MemberName1 IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @MemberName1',16,1)
    ELSE
    IF @MemberName2 IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @MemberName2',16,1)
    ELSE
    IF @MemberName3 IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @MemberName3',16,1)
    ELSE
    IF @MemberName4 IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @MemberName4',16,1)
    ELSE
    IF @DayOfWeek IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @DayOfWeek',16,1)
    ELSE
    IF @Time IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @[Time]',16,1)
    ELSE
    IF @StartDate IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @StartDate',16,1)
    ELSE
    IF @EndDate IS NULL
        RAISERROR('CreateTeeTimeRequest Failed - Required Parameter: @EndDate',16,1)
    ELSE
        BEGIN
        INSERT INTO StandingTeeTime
            (MemberNumber1,MemberNumber2, MemberNumber3, MemberNumber4, MemberName1, MemberName2, MemberName3, MemberName4, DayOfWeek, [Time],StartDate,EndDate)
        VALUES 
            (@MemberNumber1, @MemberNumber2, @MemberNumber3, @MemberNumber4, @MemberName1, @MemberName2, @MemberName3, @MemberName4, @DayOfWeek, @Time, @StartDate, @EndDate)

        IF @@ERROR = 0
            SET @ReturnCode = 0
        ELSE RAISERROR('CreateTeeTime Failed - Insert Error in Database',16,1)
        END
        RETURN @ReturnCode
GO



-- Insert rows into table 'Golfer'
INSERT INTO Golfer
( -- columns toMembershipNumberata into
 MemberNumber, MembershipLevel, FirstName, LastName, [Password], [Address], PostalCode, Phone, AltPhone, Email, DateOfBirth, Occupation, CompanyName, CompanyAddress, CompanyPostalCode, CompanyPhone, MembershipStartDate, Shareholder, Approved
)
VALUES
(
 1, 2, 'Chase', 'Dubauskas', 'Password1', '28 Nottingham Bay', 'T8A5Z7', '7809745854', '7804493634', 'Chase.D.Dubauskas@gmail.com', '1997-02-15', 'Retail Sales Specialist', 'London Drugs', '', '', '', '2025-02-15', 1, 'N'
),
(
 2, 2, 'Haley', 'Hennig', 'Password1', '28 Nottingham Bay', 'T8A5Z7', '7809745854', '', 'HHennig@gmail.com', '1998-07-20', 'Retail Sales Specialist', 'London Drugs', '', '', '', '2025-02-15', 1, 'Y'
),
(
 3, 3, 'Robert', 'Dubauskas', 'Password1', '28 Nottingham Bay', 'T8A5Z7', '7809745854', '', 'Chase.D.Dubauskas@gmail.com', '1997-02-15', 'Retail Sales Specialist', 'WHoKnows', '', '', '', '2025-02-15', 0, 'Y'
),
(
4, 1, 'Club', 'Admin', 'Admin123', '29 Street', 'T8L4E4', '7805542223', '7809917077','ClubAdmin@gmail.com','','','','','','','', 0, 'N'  
)
 --add more rows here
GO

INSERT INTO MembershipLevel
(
    MembershipLevel, [Description]
)
VALUES
(
    '1','MembershipCommittee'
),
(
    '2', 'Gold'
),
(
    '3', 'Silver'
),
(
    '4', 'Bronze'
),
(
    '5', 'Copper'
)

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

Execute CreateStandingTeeTimeRequest '1','2','3','4','Chase','Haley','ROB','Stark', '2020-08-26','00:07:00','2020-08-26','2020-08-26'