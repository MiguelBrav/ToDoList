﻿CREATE TABLE [dbo].[Genders]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[GenderName] NVARCHAR(100) NOT NULL,
	[IsActive]	BIT DEFAULT 1,
	CONSTRAINT [PK_GenderId] PRIMARY KEY CLUSTERED ([Id] ASC)
);

