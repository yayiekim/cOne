CREATE TABLE [dbo].[SecurityLogs] (
    [Id]              UNIQUEIDENTIFIER CONSTRAINT [DF_SecurityLogs_Id] DEFAULT (newsequentialid()) NOT NULL,
    [AspNetUserId]    NVARCHAR (128)   NOT NULL,
    [TransactionDate] DATETIME         NOT NULL,
    [SubUserId]       UNIQUEIDENTIFIER NULL,
    [SubUserIdentity] NVARCHAR (50)    NULL,
    [JobDescription]  NVARCHAR (MAX)   NOT NULL,
    [TableModified]   NVARCHAR (50)    NOT NULL,
    [FormAccessed ]   NVARCHAR (50)    NOT NULL,
    [FileGUID]        UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_SecurityLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

