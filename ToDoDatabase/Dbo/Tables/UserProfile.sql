CREATE TABLE [dbo].[UserProfile]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [UserId] NVARCHAR (128) NULL,
	[UserAppId] BIGINT NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_UserProfileId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_UserProfileId_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_UserProfileId_UsersAppIdS] FOREIGN KEY ([UserAppId]) REFERENCES [dbo].[UsersApp] ([Id])
)
