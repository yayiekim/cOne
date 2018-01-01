CREATE TABLE [dbo].[Drugs] (
    [Id]              UNIQUEIDENTIFIER CONSTRAINT [DF_Drugs_Id] DEFAULT (newsequentialid()) NOT NULL,
    [MedicineName]    NVARCHAR (300)   NOT NULL,
    [Code]            NVARCHAR (50)    NULL,
    [Description]     NVARCHAR (50)    NULL,
    [Amount]          MONEY            NULL,
    [Dosage]          NVARCHAR (50)    NULL,
    [DrugCatergoryId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Drugs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Drugs_Category] FOREIGN KEY ([DrugCatergoryId]) REFERENCES [dbo].[DrugsCategories] ([Id]) ON DELETE CASCADE
);

