CREATE TABLE [dbo].[TUser] (
    [Id]       INT  IDENTITY (1, 1) NOT NULL,
    [Name]     TEXT NOT NULL,
    [Money]    INT  NOT NULL,
    [Password] TEXT NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

