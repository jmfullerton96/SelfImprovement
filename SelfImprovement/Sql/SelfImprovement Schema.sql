USE SelfImprovement
GO

IF OBJECT_ID(N'dbo.Tasks') IS NOT NULL
	DROP TABLE [dbo].[Tasks]
GO
CREATE TABLE [dbo].[Tasks]
(
	Name VARCHAR(100),
	TaskComplete BIT, /*BIT stores boolean values as 1, 0, 0r NULL for TRUE, FALSE, and NULL respectively*/
	TaskButton VARCHAR(100),
	TaskLabel VARCHAR(100),
	ConsecutiveDays INT
)