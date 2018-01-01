CREATE TABLE [dbo].[PrescribedMedications] (
    [Id]             UNIQUEIDENTIFIER CONSTRAINT [DF_PrescribedMedications_Id] DEFAULT (newsequentialid()) NOT NULL,
    [ConsultationId] UNIQUEIDENTIFIER NOT NULL,
    [Medication]     NVARCHAR (500)   NOT NULL,
    [Quantity]       INT              NULL,
    [Remarks]        NVARCHAR (MAX)   NULL,
    [Amount]         MONEY            NULL,
    [Strength]       NVARCHAR (500)   NOT NULL,
    [Volume]         NVARCHAR (500)   NULL,
    [Frequency]      NVARCHAR (500)   NOT NULL,
    [Route]          NVARCHAR (500)   NOT NULL,
    CONSTRAINT [PK_PrescribedMedications] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrescribedMedications_Consultations] FOREIGN KEY ([ConsultationId]) REFERENCES [dbo].[Consultations] ([Id]) ON DELETE CASCADE
);

