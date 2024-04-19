using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using RPA_Test_New.Domain.Entities;
using RPA_Test_New.Domain.Interfaces;
using System.Data;


namespace RPA_Test_New.Infrastructure.Data.Repositories
{
    public class RpaRepository : Repository, IRpaRepository
    {

        private ILogger<RpaRepository> _logger { get; set; }

        public RpaRepository(IConfiguration configuration
                             ,ILogger<RpaRepository> logger) : base(configuration)
        {
            _logger = logger;
        }

        //INSERTs
        public async Task<bool> InsertData(List<DataExtracted> dataExtracted, CancellationToken ct = default)
        {

            try
            {

                using (var conn = new SqlConnection(ConnectionString))
                {
                    var sql = $@"insert into 
                                	[DESAFIO].[TB_DADOS]
                                	(
                                		[vcTitulo]
                                		,[vcProfessor]
                                		,[vcCargaHoraria]
                                		,[vcDescricao]
                                	
                                	)
                                values
                                	(
                                		@vcTitulo --[vcTitulo]
                                		,@vcProfessor --,[vcProfessor]
                                		,@vcCargaHoraria --,[vcCargaHoraria]
                                		,@vcDescricao --,[vcDescricao]
                                	)";


                    conn.Open();

                    SqlCommand commandInsert = new SqlCommand(sql, conn);

                    commandInsert.Parameters.Add("@vcTitulo", SqlDbType.VarChar);
                    commandInsert.Parameters.Add("@vcProfessor", SqlDbType.VarChar);
                    commandInsert.Parameters.Add("@vcCargaHoraria", SqlDbType.VarChar);
                    commandInsert.Parameters.Add("@vcDescricao", SqlDbType.VarChar);

                    foreach (var item in dataExtracted)
                    {
                        commandInsert.Parameters["@vcTitulo"].Value = item.titulo.ToString();
                        commandInsert.Parameters["@vcProfessor"].Value = item.professor.ToString();
                        commandInsert.Parameters["@vcCargaHoraria"].Value = item.cargaHoraria.ToString();
                        commandInsert.Parameters["@vcDescricao"].Value = item.descricao.ToString();
                    }

                    await commandInsert.ExecuteScalarAsync(ct);

                    conn.Close();

                    return true;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
                
            }

        }
    }
}
