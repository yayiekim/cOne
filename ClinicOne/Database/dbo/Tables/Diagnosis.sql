CREATE TABLE [dbo].[Diagnosis] (
    [Id]                  UNIQUEIDENTIFIER CONSTRAINT [DF_Diagnosis_Id] DEFAULT (newsequentialid()) NOT NULL,
    [Diagnosis]           NVARCHAR (500)   NOT NULL,
    [Description]         NVARCHAR (MAX)   NULL,
    [DiagnosisCategoryId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Diagnosis] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Diagnosis_Categories] FOREIGN KEY ([DiagnosisCategoryId]) REFERENCES [dbo].[DiagnosisCategories] ([Id]) ON DELETE CASCADE
);

