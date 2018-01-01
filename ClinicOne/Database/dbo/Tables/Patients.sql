CREATE TABLE [dbo].[Patients] (
    [Id]                 UNIQUEIDENTIFIER CONSTRAINT [DF_Patients_Id] DEFAULT (newsequentialid()) NOT NULL,
    [OriginAspNetUserId] NVARCHAR (128)   NOT NULL,
    [FirstName]          NVARCHAR (50)    NOT NULL,
    [MiddleName]         NVARCHAR (50)    NULL,
    [LastName]           NVARCHAR (50)    NOT NULL,
    [BirthDate]          DATE             NOT NULL,
    [BloodType]          NVARCHAR (50)    NULL,
    [Address1]           NVARCHAR (500)   NULL,
    [Address2]           NVARCHAR (500)   NULL,
    [ContactNumber1]     NVARCHAR (50)    NULL,
    [ContactNumber2]     NVARCHAR (50)    NULL,
    [Gender]             NVARCHAR (6)     NULL,
    [SSS]                NVARCHAR (50)    NULL,
    [TIN]                NVARCHAR (50)    NULL,
    [Philhealth]         NVARCHAR (50)    NULL,
    CONSTRAINT [PK_Patients_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Patients_AspNetUsers] FOREIGN KEY ([OriginAspNetUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

