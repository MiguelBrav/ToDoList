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
)
