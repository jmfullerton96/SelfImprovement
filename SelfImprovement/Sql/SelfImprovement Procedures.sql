USE SelfImprovement
GO

/*
	Inserts a new task object into the database
*/
IF OBJECT_ID('Insert_New_Task', 'P') IS NOT NULL  
	DROP PROCEDURE Insert_New_Task;  
GO 
CREATE PROCEDURE Insert_New_Task @Name VARCHAR(100), @LastDayCompleted VARCHAR(100), @TaskButton VARCHAR(100), @TaskLabel VARCHAR(100), @ConsecutiveDays INT
AS
	INSERT INTO dbo.Tasks
	VALUES (@Name, @LastDayCompleted, @TaskButton, @TaskLabel, @ConsecutiveDays)
GO

/*
	Completes a task object in the database
*/
IF OBJECT_ID('Complete_Task', 'P') IS NOT NULL  
	DROP PROCEDURE Complete_Task;  
GO 
CREATE PROCEDURE Complete_Task @Name VARCHAR(100)
AS
	Update Tasks 
	SET ConsecutiveDays = ConsecutiveDays + 1, LastDayCompleted = FORMAT(GETDATE(), 'MM/dd/yyyy')
	WHERE Name = @Name

	SELECT ConsecutiveDays 
	FROM dbo.Tasks 
	WHERE Name = @Name
GO