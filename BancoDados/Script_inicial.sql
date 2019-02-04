IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Configuracao] (
    [Chave] nvarchar(256) NOT NULL,
    [Valor] nvarchar(256) NULL,
    [Secao] nvarchar(256) NULL,
    [Descricao] nvarchar(256) NULL,
    CONSTRAINT [PK_Configuracao] PRIMARY KEY ([Chave])
);

GO

CREATE TABLE [Contato] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [Celular] nvarchar(30) NULL,
    CONSTRAINT [PK_Contato] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Convite] (
    [Id] int NOT NULL IDENTITY,
    [Titulo] nvarchar(256) NULL,
    [Mensagem] text NULL,
    CONSTRAINT [PK_Convite] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Presente] (
    [Id] int NOT NULL IDENTITY,
    [Titulo] nvarchar(256) NULL,
    [EhNecessario] bit NOT NULL,
    CONSTRAINT [PK_Presente] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Usuario] (
    [Usuario] nvarchar(256) NOT NULL,
    [Senha] nvarchar(256) NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Usuario])
);

GO

CREATE TABLE [Convidado] (
    [ConviteId] int NOT NULL,
    [ContatoId] int NOT NULL,
    [PresenteId] int NOT NULL,
    [Situacao] int NOT NULL,
    CONSTRAINT [PK_Convidado] PRIMARY KEY ([ConviteId], [ContatoId]),
    CONSTRAINT [FK_Convidado_Contato_ContatoId] FOREIGN KEY ([ContatoId]) REFERENCES [Contato] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Convidado_Convite_ConviteId] FOREIGN KEY ([ConviteId]) REFERENCES [Convite] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Convidado_Presente_PresenteId] FOREIGN KEY ([PresenteId]) REFERENCES [Presente] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Convidado_ContatoId] ON [Convidado] ([ContatoId]);

GO

CREATE INDEX [IX_Convidado_PresenteId] ON [Convidado] ([PresenteId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190204114640_Inicial', N'2.2.1-servicing-10028');

GO

