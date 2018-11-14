CREATE TABLE [dbo].[CardNumber] (
    [Id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [CardTypeId] BIGINT        NOT NULL,
    [CNumber]    NVARCHAR (50) NULL,
    [Expiry]     NVARCHAR (6)  NULL,
    [Active]     BIT           CONSTRAINT [DF_CardNumber_Active] DEFAULT ((0)) NOT NULL,
    [Created]    DATETIME      CONSTRAINT [DF_CardNumber_Created] DEFAULT (getdate()) NOT NULL,
    [Author]     BIGINT        NOT NULL,
    [Modified]   DATETIME      CONSTRAINT [DF_CardNumber_Modified] DEFAULT (getdate()) NOT NULL,
    [Editor]     BIGINT        NOT NULL,
    CONSTRAINT [PK_CardNumber] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CardNumber_CardType] FOREIGN KEY ([CardTypeId]) REFERENCES [dbo].[CardType] ([Id])
);

