CREATE TABLE [dbo].[ValueTypes] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [ValueType] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ValueTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

