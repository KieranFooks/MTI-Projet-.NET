CREATE TABLE [dbo].[Item] (
    [Id]          INT  NOT NULL,
    [Name]        TEXT NOT NULL,
    [Description] TEXT NULL,
    CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED ([Id] ASC)
);

