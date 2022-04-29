CREATE TABLE [dbo].[User] (
    [Id]       INT  NOT NULL,
    [Name]     TEXT NOT NULL,
    [Money]    INT  NOT NULL,
    [Password] TEXT NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

