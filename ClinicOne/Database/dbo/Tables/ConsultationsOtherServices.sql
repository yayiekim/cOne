CREATE TABLE [dbo].[ConsultationsOtherServices] (
    [Id]             UNIQUEIDENTIFIER CONSTRAINT [DF_ConsultationsOtherServices_Id] DEFAULT (newsequentialid()) NOT NULL,
    [ConsultationId] UNIQUEIDENTIFIER NOT NULL,
    [Description]    NVARCHAR (50)    NULL,
    [Remarks]        NVARCHAR (MAX)   NULL,
    [Amount]         MONEY            NULL,
    CONSTRAINT [PK_ConsultationsOtherServices] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConsultationsOtherServices_Consultations] FOREIGN KEY ([ConsultationId]) REFERENCES [dbo].[Consultations] ([Id]) ON DELETE CASCADE
);

