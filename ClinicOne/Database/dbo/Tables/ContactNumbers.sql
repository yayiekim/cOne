CREATE TABLE [dbo].[ContactNumbers] (
    [Id]                  UNIQUEIDENTIFIER CONSTRAINT [DF_ContactNumbers_Id] DEFAULT (newsequentialid()) NOT NULL,
    [AspNetUserId]        NVARCHAR (128)   NOT NULL,
    [CommunicationTypeId] INT              NOT NULL,
    [CommunicationValue]  NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_ContactNumbers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContactNumbers_AspNetUser] FOREIGN KEY ([AspNetUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ContactNumbers_CommunicationTypes] FOREIGN KEY ([CommunicationTypeId]) REFERENCES [dbo].[CommunicationTypes] ([Id])
);

