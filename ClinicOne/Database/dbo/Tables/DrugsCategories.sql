CREATE TABLE [dbo].[DrugsCategories] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_DrugsCategories_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name]         NCHAR (50)       NOT NULL,
    [AspNetUserId] NVARCHAR (128)   NULL,
    CONSTRAINT [PK_DrugsCategories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DrugsCategories_ToTable] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

