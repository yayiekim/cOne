CREATE TABLE [dbo].[DropDownCommon] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [InputFieldId] NVARCHAR (50) NOT NULL,
    [Value]        NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

