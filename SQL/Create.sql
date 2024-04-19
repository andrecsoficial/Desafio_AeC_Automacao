Use AeC


-- drop table [DESAFIO].[TB_DADOS]
-- truncate table [DESAFIO].[TB_DADOS]

create schema DESAFIO

create table [DESAFIO].[TB_DADOS]
(
	[TB_DADOS_uiId] [uniqueidentifier] primary key default NewSequentialID() not null
	,[vcTitulo] [varchar] (100) not null
	,[vcProfessor] [varchar] (100) not null
	,[vcCargaHoraria] [varchar] (10) not null
	,[vcDescricao] [varchar] (600) not null
	,[timestampTransaction] datetime default getdate()
)


create table NLog
(
	[ID] [int] PRIMARY KEY IDENTITY(1,1) not null
	,[MachineName] [nvarchar](200) null
	,[Level] [varchar](5) not null
	,[Logged] [datetime] not null
	,[UserName] [nvarchar](200) NULL
	,[ThreadId] [nvarchar](200) not null
	,[Message] [nvarchar](max) not null
	,[Exception] [nvarchar](max) null
	,[Logger] [nvarchar](250) not null
)

