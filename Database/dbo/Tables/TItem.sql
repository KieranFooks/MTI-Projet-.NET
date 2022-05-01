CREATE TABLE [dbo].[TItem] (
    [Id]          INT  IDENTITY (1, 1) NOT NULL,
    [Name]        TEXT NOT NULL,
    [Description] TEXT NULL,
    CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED ([Id] ASC)
);

