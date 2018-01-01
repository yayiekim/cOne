CREATE TABLE [dbo].[Consultations] (
    [Id]              UNIQUEIDENTIFIER CONSTRAINT [DF_Consultations_Id] DEFAULT (newsequentialid()) NOT NULL,
    [AspNetUserId]    NVARCHAR (128)   NOT NULL,
    [PatientId]       UNIQUEIDENTIFIER NOT NULL,
    [TransactionDate] DATE             NOT NULL,
    CONSTRAINT [PK_Consultations] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Consultations_AspNetUsers] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Consultations_Patient] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([Id]) ON DELETE CASCADE
);

