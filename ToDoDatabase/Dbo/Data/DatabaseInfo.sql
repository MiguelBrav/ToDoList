--dbo.Language
INSERT INTO [dbo].[Language] ([Id] ,[Description]) VALUES ('en-US','English Language')
INSERT INTO [dbo].[Language] ([Id] ,[Description]) VALUES ('es-Mx','Spanish Language')

--dbo.Genders
INSERT INTO [dbo].[Genders] ([GenderName]) VALUES ('Male')
INSERT INTO [dbo].[Genders] ([GenderName]) VALUES ('Female')
INSERT INTO [dbo].[Genders] ([GenderName]) VALUES ('Undefined')

--dbo.GendersTranslated
INSERT INTO [dbo].[GendersTranslated] ([OriginalGenderId],[LanguageId],[Name]) VALUES (1,'en-Us','Male')
INSERT INTO [dbo].[GendersTranslated] ([OriginalGenderId],[LanguageId],[Name]) VALUES (1,'es-Mx','Hombre')
INSERT INTO [dbo].[GendersTranslated] ([OriginalGenderId],[LanguageId],[Name]) VALUES (2,'en-Us','Female')
INSERT INTO [dbo].[GendersTranslated] ([OriginalGenderId],[LanguageId],[Name]) VALUES (2,'es-Mx','Mujer')
INSERT INTO [dbo].[GendersTranslated] ([OriginalGenderId],[LanguageId],[Name]) VALUES (3,'en-Us','Undefined')
INSERT INTO [dbo].[GendersTranslated] ([OriginalGenderId],[LanguageId],[Name]) VALUES (3,'es-Mx','Indefinido')

--dbo.TaskTier
INSERT INTO [dbo].[TaskTier] ([TierName],[IsActive]) VALUES ('Important-Urgent',1)
INSERT INTO [dbo].[TaskTier] ([TierName],[IsActive]) VALUES ('Important-Not Urgent',1)
INSERT INTO [dbo].[TaskTier] ([TierName],[IsActive]) VALUES ('Not Important-Urgent',1)
INSERT INTO [dbo].[TaskTier] ([TierName],[IsActive]) VALUES ('Not Important-Not Urgent',1)

--dbo.TaskTierTranslated
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (1,'en-Us','Important-Urgent','Urgent & Important tasks/projects to be completed immediately.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (1,'es-Mx','Importante-Urgente','Urgentes e importantes tareas/projectos para completar inmediatamente.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (2,'en-Us','Important-Not Urgent','Not Urgent & Important tasks/projects to be scheduled on your calendar.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (2,'es-Mx','Importante-No Urgente','Importantes, pero no urgentes tareas para programar para más adelante en tu calendario.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (3,'en-Us','Not Important-Urgent','Urgent & Unimportant tasks/projects to be delegated to someone else.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (3,'es-Mx','No Importante-Urgente','Urgentes, pero no importantes tareas para delegar a otra persona.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (4,'en-Us','Not Important-Not Urgent','Not Urgent & Unimportant tasks/projects to be deleted.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (4,'es-Mx','No Importante-No Urgente','No importantes y no urgentes son tareas para eliminarse.',1)
