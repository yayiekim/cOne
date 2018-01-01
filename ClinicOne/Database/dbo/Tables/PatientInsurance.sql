CREATE TABLE [dbo].[PatientInsurance] (
    [Id]               UNIQUEIDENTIFIER CONSTRAINT [DF_PatientInsurance_Id] DEFAULT (newsequentialid()) NOT NULL,
    [CardNumber]       NVARCHAR (50)    NULL,
    [AccountNumber]    NVARCHAR (50)    NULL,
    [InsuranceCompany] NVARCHAR (100)   NOT NULL,
    [PatientId]        UNIQUEIDENTIFIER NOT NULL,
    [IsActive]         BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_PatientHealthCard] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PatientInsurance_Patients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([Id]) ON DELETE CASCADE
);

