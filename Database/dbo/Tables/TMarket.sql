CREATE TABLE [dbo].[TMarket] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [Id_seller] INT NOT NULL,
    [Id_item]   INT NOT NULL,
    [Price]     INT NOT NULL,
    [Quantity]  INT NOT NULL,
    [Is_sold]   BIT NOT NULL,
    CONSTRAINT [PK_Market] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Market_Item] FOREIGN KEY ([Id_item]) REFERENCES [dbo].[TItem] ([Id]),
    CONSTRAINT [FK_Market_User] FOREIGN KEY ([Id_seller]) REFERENCES [dbo].[TUser] ([Id])
);



