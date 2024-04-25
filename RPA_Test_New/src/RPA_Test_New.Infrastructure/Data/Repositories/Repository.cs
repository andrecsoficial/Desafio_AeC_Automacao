using Microsoft.Extensions.Configuration;

namespace RPA_Test_New.Infrastructure.Data.Repositories
{
    public class Repository
    {
        protected string ConnectionString { get; }

        protected Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected Repository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Default");
        }
    }
}
