CREATE TABLE [dbo].[PatientsRecords] (
    [Id]             UNIQUEIDENTIFIER CONSTRAINT [DF_PatientsRecords_Id] DEFAULT (newsequentialid()) NOT NULL,
    [ConsultationId] UNIQUEIDENTIFIER NOT NULL,
    [RecordType]     NVARCHAR (500)   NOT NULL,
    [RecordValue]    NVARCHAR (50)    NOT NULL,
    [ImageValue]     IMAGE            NULL,
    CONSTRAINT [PK_PatientsRecords] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PatientsRecords_Consultations] FOREIGN KEY ([ConsultationId]) REFERENCES [dbo].[Consultations] ([Id]) ON DELETE CASCADE
);

