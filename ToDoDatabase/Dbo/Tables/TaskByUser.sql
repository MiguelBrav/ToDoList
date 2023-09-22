CREATE TABLE [dbo].[TaskByUser]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalTaskTierId] INT NOT NULL,
	[UserAppId] BIGINT NOT NULL,
	[TaskName] NVARCHAR(100) NOT NULL,
	[TaskDescription] NVARCHAR(2000) NOT NULL,
	[IsDeleted] BIT DEFAULT 0,
	[IsCompleted] BIT DEFAULT 0,
	[CreatedUserId] NVARCHAR (128) NULL,
	[CreatedDate] DATETIME NOT NULL,
	[ExpectedDateTime] DATETIME NULL,
	[FinishDate] DATETIME  NULL,
	[LastModificationDate] DATETIME  NULL
	CONSTRAINT [PK_TaskByUserId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TaskByUserId_Users] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_TaskByUserId_UsersAppIdS] FOREIGN KEY ([UserAppId]) REFERENCES [dbo].[UsersApp] ([Id])
)
