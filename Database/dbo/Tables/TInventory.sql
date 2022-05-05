﻿CREATE TABLE [dbo].[TInventory] (
    [Id_user]  INT NOT NULL,
    [Id_item]  INT NOT NULL,
    [Quantity] INT NOT NULL,
    CONSTRAINT [FK_Inventory_Item] FOREIGN KEY ([Id_item]) REFERENCES [dbo].[TItem] ([Id]),
    CONSTRAINT [FK_Inventory_User] FOREIGN KEY ([Id_user]) REFERENCES [dbo].[TUser] ([Id])
);

