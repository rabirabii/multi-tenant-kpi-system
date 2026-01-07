using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Context
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("KpiRemastered")
                ?? throw new InvalidOperationException("Connection string 'KpiRemastered' not found.");
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}
