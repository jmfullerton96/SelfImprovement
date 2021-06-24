USE SelfImprovement
GO

IF OBJECT_ID('Insert_New_Task', 'P') IS NOT NULL  
	DROP PROCEDURE Insert_New_Task;  
GO 
CREATE PROCEDURE Insert_New_Task @Name VARCHAR(100), @TaskComplete BIT, @TaskButton VARCHAR(100), @TaskLabel VARCHAR(100), @ConsecutiveDays INT
AS
	INSERT INTO dbo.Tasks
	VALUES (@Name, @TaskComplete, @TaskButton, @TaskLabel, @ConsecutiveDays)
GO

IF OBJECT_ID('Complete_Task', 'P') IS NOT NULL  
	DROP PROCEDURE Complete_Task;  
GO 
CREATE PROCEDURE Complete_Task @Name VARCHAR(100)
AS
	Update Tasks 
	SET ConsecutiveDays = ConsecutiveDays + 1, TaskComplete = 1 
	WHERE Name = @Name

	SELECT ConsecutiveDays 
	FROM dbo.Tasks 
	WHERE Name = @Name
GO