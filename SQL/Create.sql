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

