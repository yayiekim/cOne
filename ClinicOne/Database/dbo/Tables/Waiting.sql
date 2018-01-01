CREATE TABLE [dbo].[Waiting] (
    [Id]           UNIQUEIDENTIFIER CONSTRAINT [DF_Waiting_Id] DEFAULT (newsequentialid()) NOT NULL,
    [PatientId]    UNIQUEIDENTIFIER NOT NULL,
    [Schedule]     DATETIME         NOT NULL,
    [Remarks]      NVARCHAR (500)   NULL,
    [AspNetUserId] NVARCHAR (128)   NOT NULL,
    [IsAdmitted]   BIT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Waiting] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Waiting_AspNetUser] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Waiting_Patients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([Id])
);

