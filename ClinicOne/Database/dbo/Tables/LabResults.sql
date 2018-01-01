CREATE TABLE [dbo].[LabResults] (
    [Id]                 UNIQUEIDENTIFIER CONSTRAINT [DF_LabResults_Id] DEFAULT (newsequentialid()) NOT NULL,
    [ConsultationId]     UNIQUEIDENTIFIER NOT NULL,
    [Remarks]            NVARCHAR (MAX)   NULL,
    [RecordType]         NVARCHAR (500)   NOT NULL,
    [RecordValue]        NVARCHAR (50)    NULL,
    [ImageValue]         IMAGE            NULL,
    [RecordCategoryName] NVARCHAR (500)   NOT NULL,
    CONSTRAINT [PK_LabResults] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LabResults_Consultations] FOREIGN KEY ([ConsultationId]) REFERENCES [dbo].[Consultations] ([Id]) ON DELETE CASCADE
);

