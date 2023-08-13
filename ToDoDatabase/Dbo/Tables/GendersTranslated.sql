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
)
