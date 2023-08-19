CREATE TABLE [dbo].[UsersApp] (
    [Id]                              BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]                          NVARCHAR (128) NULL,
    [BirthDate]                       DATE           NULL,
    [Email]                           VARCHAR (200)  NOT NULL,
    [Name]                            VARCHAR (200)  NOT NULL,
    [CreatedDate]                     DATETIME       DEFAULT (getutcdate()) NOT NULL,
    [LastModificationDate]            DATETIME       DEFAULT (getutcdate()) NOT NULL,
    [Gender]                          INT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_UserApp] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersApp_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Language]
(
	[Id] CHAR(5) NOT NULL, 
    [Description] NVARCHAR(100) NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_LanguageId] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Genders]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[GenderName] NVARCHAR(100) NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_GenderId] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[GendersTranslated]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalGenderId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_GendersTranslatedId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_GendersTranslatedId_InstructionsId] FOREIGN KEY ([OriginalGenderId]) REFERENCES [dbo].[Genders] ([Id]),
	CONSTRAINT [FK_GendersTranslatedId_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
);


CREATE TABLE [dbo].[Instructions]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[Name] NVARCHAR(100) NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_InstructionsId] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[InstructionsTranslated]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalInstructionId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
	[TranslatedName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[Image] NVARCHAR(2000) NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_InstructionsTranslatedId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_InstructionsTranslatedId_InstructionsId] FOREIGN KEY ([OriginalInstructionId]) REFERENCES [dbo].[Instructions] ([Id]),
	CONSTRAINT [FK_InstructionsTranslatedId_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
);

CREATE TABLE [dbo].[LanguageUserSelection]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[LanguageId] [char](5) NOT NULL,
    	[UserId] NVARCHAR (128) NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_LanguageUserSelectionId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_LanguageUserSelectionId_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id]),
	CONSTRAINT [FK_LanguageUserSelectionId_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

CREATE TABLE [dbo].[TaskByUser]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalTaskTierId] INT NOT NULL,
	[UserAppId] BIGINT NOT NULL,
	[TaskName] NVARCHAR(100) NOT NULL,
	[TaskDescription] NVARCHAR(2000) NOT NULL,
	[IsDeleted] BIT DEFAULT 0,
	[CreatedUserId] NVARCHAR (128) NULL,
	[CreatedDate] DATETIME NOT NULL,
	[FinishDate] DATETIME  NULL,
	[LastModificationDate] DATETIME  NULL
	CONSTRAINT [PK_TaskByUserId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TaskByUserId_Users] FOREIGN KEY ([CreatedUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_TaskByUserId_UsersAppIdS] FOREIGN KEY ([UserAppId]) REFERENCES [dbo].[UsersApp] ([Id])
);

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
);

CREATE TABLE [dbo].[TaskTier]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[TierName] NVARCHAR(100) NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_TaskTierId] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[TaskTierTranslated]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[OriginalTaskTierId] INT NOT NULL,
	[LanguageId] [char](5) NOT NULL,
	[TierName] NVARCHAR(100) NOT NULL,
	[TranslatedDescription] NVARCHAR(2000) NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_TaskTierTranslatedId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_TaskTier_TaskTierTranslatedId] FOREIGN KEY ([OriginalTaskTierId]) REFERENCES [dbo].[TaskTier] ([Id]),
	CONSTRAINT [FK_TaskTierTranslatedId_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Language] ([Id])
);

CREATE TABLE [dbo].[UserProfile]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
    [UserId] NVARCHAR (128) NULL,
	[UserAppId] BIGINT NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	[UrlImage] NVARCHAR(2000) NULL,
	CONSTRAINT [PK_UserProfileId] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_UserProfileId_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_UserProfileId_UsersAppIdS] FOREIGN KEY ([UserAppId]) REFERENCES [dbo].[UsersApp] ([Id])
);


