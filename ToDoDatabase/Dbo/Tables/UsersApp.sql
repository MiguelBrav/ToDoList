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



