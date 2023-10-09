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
VALUES (1,'en-Us','Urgent-Important','Urgent & Important tasks/projects to be completed immediately.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (1,'es-Mx','Urgente-Importante','Urgentes e importantes tareas/projectos para completar inmediatamente.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (2,'en-Us','Not Urgent-Important','Not Urgent & Important tasks/projects to be scheduled on your calendar.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (2,'es-Mx','No Urgente-Importante','Importantes, pero no urgentes tareas para programar para más adelante en tu calendario.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (3,'en-Us','Urgent-Not Important','Urgent & Unimportant tasks/projects to be delegated to someone else.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (3,'es-Mx','Urgente-No Importante','Urgentes, pero no importantes tareas para delegar a otra persona.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (4,'en-Us','Not Urgent-Not Important','Not Urgent & Unimportant tasks/projects to be deleted.',1)
INSERT INTO [dbo].[TaskTierTranslated] ([OriginalTaskTierId],[LanguageId],[TierName],[TranslatedDescription],[IsActive])
VALUES (4,'es-Mx','No Urgente-No Importante','No importantes y no urgentes son tareas para eliminarse.',1)


--Instructions
INSERT INTO [dbo].[Instructions]([Name],[IsActive])  VALUES   ('Eisenhower Introduction',1)
INSERT INTO [dbo].[Instructions]([Name],[IsActive])  VALUES   ('Eisenhower Quadrant 1',1)
INSERT INTO [dbo].[Instructions]([Name],[IsActive])  VALUES   ('Eisenhower Quadrant 2',1)
INSERT INTO [dbo].[Instructions]([Name],[IsActive])  VALUES   ('Eisenhower Quadrant 3',1)
INSERT INTO [dbo].[Instructions]([Name],[IsActive])  VALUES   ('Eisenhower Quadrant 4',1)
INSERT INTO [dbo].[Instructions]([Name],[IsActive])  VALUES   ('Eisenhower Conclusion',1)

--dbo.InstructionsTranslated
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (1,'en-Us','Eisenhower Introduction','The Eisenhower Matrix, also known as the Urgent-Important Matrix, is a time management tool that helps individuals categorize and prioritize tasks and responsibilities based on their urgency and importance. It divides tasks into four quadrants',NULL,1)
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (1,'es-Mx','Introducción Eisenhower','La Matriz de Eisenhower, también conocida como la Matriz Urgente-Importante, es una herramienta de gestión del tiempo que ayuda a las personas a categorizar y priorizar tareas y responsabilidades en función de su urgencia e importancia. Divide las tareas en cuatro cuadrantes.',NULL,1)
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (2,'en-Us','Eisenhower Quadrant 1 (Urgent-Important)','Tasks in this quadrant are both time-sensitive and crucial. They require immediate attention and often relate to critical goals or emergencies. Examples include meeting deadlines, addressing crises, or dealing with health issues.',NULL,1)
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (2,'es-Mx','Eisenhower Quadrante 1 (Urgente-Importante)','Las tareas en este cuadrante son tanto urgentes como cruciales. Requieren atención inmediata y a menudo están relacionadas con objetivos críticos o emergencias. Ejemplos incluyen cumplir plazos, abordar crisis o lidiar con problemas de salud.',NULL,1)
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (3,'en-Us','Eisenhower Quadrant 2 (Not Urgent-Important)','Tasks in this quadrant are important for long-term goals and personal development but lack immediate deadlines. They require proactive planning and include activities such as strategic planning, skill development, and building relationships.',NULL,1)
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (3,'es-Mx','Eisenhower Quadrante 2 (No Urgente-Importante)','Las tareas en este cuadrante son importantes para objetivos a largo plazo y desarrollo personal, pero carecen de plazos inmediatos. Requieren planificación proactiva e incluyen actividades como planificación estratégica, desarrollo de habilidades y construcción de relaciones.',NULL,1)
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (4,'en-Us','Eisenhower Quadrant 3 (Urgent-Not Important)','Tasks in this quadrant are urgent but may not contribute significantly to your long-term goals. They are often distractions and can be delegated or minimized. Examples include interruptions, some phone calls, or certain meetings.',NULL,1)
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (4,'es-Mx','Eisenhower Quadrante 3 (Urgente-No Importante)','Las tareas en este cuadrante son urgentes pero pueden no contribuir significativamente a tus objetivos a largo plazo. A menudo son distracciones y pueden delegarse o minimizarse. Ejemplos incluyen interrupciones, algunas llamadas telefónicas o ciertas reuniones.',NULL,1)
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (5,'en-Us','Eisenhower Quadrant 4 (Not Urgent-Not Important)','Tasks in this quadrant are neither time-sensitive nor crucial for your goals. They are time-wasters and should be minimized or eliminated from your daily activities. Examples include excessive social media use, irrelevant emails, or aimless web browsing.',NULL,1)
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (5,'es-Mx','Eisenhower Quadrante 4 (No Urgente-No Importante)','Las tareas en este cuadrante no son ni urgentes ni cruciales para tus objetivos. Son pérdidas de tiempo y deben minimizarse o eliminarse de tus actividades diarias. Ejemplos incluyen el uso excesivo de redes sociales, correos electrónicos irrelevantes o navegación web sin rumbo.',NULL,1)
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (6,'en-Us','Eisenhower Conclusion','By using the Eisenhower Matrix, individuals can make informed decisions about how to allocate their time and focus on tasks that truly matter, leading to increased productivity and better time management.',NULL,1)
INSERT INTO [dbo].[InstructionsTranslated] ([OriginalInstructionId],[LanguageId],[TranslatedName],[TranslatedDescription],[Image],[IsActive])
     VALUES
     (6,'es-Mx','Conclusión Eisenhower','Al utilizar la Matriz de Eisenhower, las personas pueden tomar decisiones informadas sobre cómo asignar su tiempo y enfocarse en tareas que realmente importan, lo que conduce a una mayor productividad y una mejor gestión del tiempo.',NULL,1)

-- TO DO ---- UPDATE IMAGE FOR INSTRUCTIONS




