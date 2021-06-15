IF NOT EXISTS(SELECT name FROM master.sys.databases where name = 'SelfImprovement')
	CREATE DATABASE SelfImprovement