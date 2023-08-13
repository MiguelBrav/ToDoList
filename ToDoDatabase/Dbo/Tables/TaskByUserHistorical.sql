CREATE TABLE [dbo].[TaskByUserHistorical]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalTaskId] INT NOT NULL,
	[OriginalTaskTierId] INT NOT NULL,
	[UserAppId] BIGINT NOT NULL,
	[TaskName] NVARCHAR(100) NOT NULL,
	[TaskDescription] NVARCHAR(2000) NOT NULL,
	[DeleteddUserId] NVARCHAR (128) NULL,
	[DeletedDate] DATETIME NOT NULL,
	CONSTRAINT [PK_TaskByUserHistoricalId] PRIMARY KEY CLUSTERED ([Id] ASC)
)
