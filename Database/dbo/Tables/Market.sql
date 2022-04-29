CREATE TABLE [dbo].[Market] (
    [Id]        INT NOT NULL,
    [Id_seller] INT NOT NULL,
    [Id_item]   INT NOT NULL,
    [Price]     INT NOT NULL,
    [Quantity]  INT NOT NULL,
    [Is_sold]   BIT NOT NULL,
    CONSTRAINT [PK_Market] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Market_Item] FOREIGN KEY ([Id_item]) REFERENCES [dbo].[Item] ([Id]),
    CONSTRAINT [FK_Market_User] FOREIGN KEY ([Id]) REFERENCES [dbo].[User] ([Id])
);

