CREATE TABLE [dbo].[RecordTypesCategories] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_RecordTypesCategories_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name]         NVARCHAR (50)    NOT NULL,
    [AspNetUserId] NVARCHAR (128)   NOT NULL,
    [ClassId]      INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RecordTypesCategories_AspNetUser] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RecordTypesCategories_CategoryClass] FOREIGN KEY ([ClassId]) REFERENCES [dbo].[RecordTypesCategoryClass] ([Id])
);

