Use [AeC]

select * from [DESAFIO].[TB_ACESSO]
select * from [DESAFIO].[TB_DADOS]
select * from NLog

-- truncate table [DESAFIO].[TB_DADOS]

select 
	vcUsuario
	,vcSenha
from 
	[DESAFIO].[TB_ACESSO]
where
	biAtivo = 1

insert into  [DESAFIO].[TB_ACESSO]
(
	[vcUsuario]
	,[vcSenha]
)
values
(
	'andre.costa_info@yahoo.com.br'
	,'#Acs_@lur@01#'
)

/*
insert into 
	[DESAFIO].[TB_DADOS]
	(
		[vcTitulo]
		,[vcProfessor]
		,[vcCargaHoraria]
		,[vcDescricao]
	
	)
values
	(
		'teste' --[vcTitulo]
		,'teste' --,[vcProfessor]
		,'teste' --,[vcCargaHoraria]
		,'teste' --,[vcDescricao]
	)

*/
	