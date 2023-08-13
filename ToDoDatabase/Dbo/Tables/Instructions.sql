﻿CREATE TABLE [dbo].[Instructions]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[Name] NVARCHAR(100) NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_InstructionsId] PRIMARY KEY CLUSTERED ([Id] ASC)
);

