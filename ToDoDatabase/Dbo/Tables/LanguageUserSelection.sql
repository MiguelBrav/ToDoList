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
)
