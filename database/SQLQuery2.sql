-- db creation
IF DB_ID('leaveRequestScript') is NULL
BEGIN 
	CREATE DATABASE leaveRequestScript;
END;
USE leaveRequestScript;
GO

-- table creation 
IF OBJECT_ID('dbo.leaveRequests', 'U') is null
BEGIN
	CREATE TABLE dbo.leaveRequests(
		Id INT IDENTITY(1,1) not null,
		EmployeeID INT not null, 
		StartDate DATETIME not null,
		EndDate DATETIME not null,
		Type NVARCHAR(50) not null,
		Status NVARCHAR(50) not null,
		CreatedAt DATETIME not null,
		ReviewerNote NVARCHAR(500) null,

	CONSTRAINT PK_LeaveRequests PRIMARY KEY (Id),

	CONSTRAINT CK_LeaveRequests_DateRange CHECK (EndDate >= StartDate),

	CONSTRAINT CK_LeaveRequests_Status CHECK (Status IN ('Pending', 'Approved', 'Rejected'))
	);
END;
GO

-- seeding 
INSERT INTO leaveRequests
(
	EmployeeID,
	StartDate,
	EndDate,
	Type,
	Status,
	CreatedAt,
	ReviewerNote
)VALUES(
	1,
	'2026-09-21',
	'2026-09-23',
	'Vacation',
	'Pending',
	'2026-09-10 09:00:00',
	NULL
),(
	2,
	'2026-09-15',
	'2026-09-16',
	'Sick',
	'Approved',
	'2026-09-8 10:30:00',
	'Approved by reviewer'
),(
	3,
	'2026-10-05',
	'2026-10-09',
	'Unpaid',
	'Approved',
	'2026-09-01 14:00:00',
	'Annual leave approved'
),(
	4,
	'2026-09-28',
	'2026-10-29',
	'Vaction',
	'Rejected',
	'2026-09-12 11:15:00',
	'Leave cannot be approved for these dates'
),(
	5,
	'2026-09-21',
	'2026-09-23',
	'Sick',
	'Pending',
	'2026-09-10 09:00:00',
	NULL
),(
	6,
	'2026-09-21',
	'2026-09-23',
	'Sick',
	'Pending',
	'2026-09-10 09:00:00',
	NULL
),(
	7,
	'2026-09-21',
	'2026-09-23',
	'Sick',
	'Pending',
	'2026-09-10 09:00:00',
	NULL
),(
	8,
	'2026-09-21',
	'2026-09-23',
	'Sick',
	'Pending',
	'2026-09-10 09:00:00',
	NULL
),(
	9,
	'2026-09-21',
	'2026-09-23',
	'Sick',
	'Pending',
	'2026-09-10 09:00:00',
	NULL
),(
	10,
	'2026-09-21',
	'2026-09-23',
	'Sick',
	'Pending',
	'2026-09-10 09:00:00',
	NULL
);