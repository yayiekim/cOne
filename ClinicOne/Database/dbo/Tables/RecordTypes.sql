CREATE TABLE [dbo].[RecordTypes] (
    [Id]                    UNIQUEIDENTIFIER CONSTRAINT [DF_RecordTypes_Id] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [Name]                  NVARCHAR (100)   NOT NULL,
    [ValueTypeId]           INT              NOT NULL,
    [RecordTypesCategoryId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_RecordTypes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RecordTypes_Categories] FOREIGN KEY ([RecordTypesCategoryId]) REFERENCES [dbo].[RecordTypesCategories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RecordTypes_ValueTypes] FOREIGN KEY ([ValueTypeId]) REFERENCES [dbo].[ValueTypes] ([Id]),
    CONSTRAINT [AK_RecordTypes_Name] UNIQUE NONCLUSTERED ([Name] ASC, [RecordTypesCategoryId] ASC)
);

