CREATE TABLE [dbo].[DiagnosisCategories] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_DiagnosisCategories_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Name]         NVARCHAR (50)    NOT NULL,
    [AspNetUserId] NVARCHAR (128)   NOT NULL,
    CONSTRAINT [PK_DiagnosisCategories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DiagnosisCategories_AspNetUser] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

