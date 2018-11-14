CREATE TABLE [dbo].[CardType] (
    [Id]          BIGINT         NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [Code]        NVARCHAR (200) NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Prefix]      NVARCHAR (10)  NULL,
    [Active]      BIT            CONSTRAINT [DF_CardType_Active] DEFAULT ((0)) NOT NULL,
    [Created]     DATETIME       CONSTRAINT [DF_CardType_Created] DEFAULT (getdate()) NOT NULL,
    [Author]      BIGINT         NOT NULL,
    [Modified]    DATETIME       CONSTRAINT [DF_CardType_Modified] DEFAULT (getdate()) NOT NULL,
    [Editor]      BIGINT         NOT NULL,
    CONSTRAINT [PK_CardType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

