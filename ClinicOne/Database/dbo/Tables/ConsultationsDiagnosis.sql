CREATE TABLE [dbo].[ConsultationsDiagnosis] (
    [Id]             UNIQUEIDENTIFIER CONSTRAINT [DF_ConsultationsDiagnosis_Id] DEFAULT (newsequentialid()) NOT NULL,
    [ConsultationId] UNIQUEIDENTIFIER NOT NULL,
    [Diagnosis]      NVARCHAR (MAX)   NOT NULL,
    [Remarks]        NVARCHAR (MAX)   NULL,
    [Amount]         MONEY            NULL,
    CONSTRAINT [PK_ConsultationsDiagnosis] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsultationsDiagnosis_Consultations] FOREIGN KEY ([ConsultationId]) REFERENCES [dbo].[Consultations] ([Id]) ON DELETE CASCADE
);

