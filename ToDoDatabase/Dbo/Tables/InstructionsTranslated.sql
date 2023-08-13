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
)
