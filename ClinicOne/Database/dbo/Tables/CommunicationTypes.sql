﻿CREATE TABLE [dbo].[CommunicationTypes] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CommunicationTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

