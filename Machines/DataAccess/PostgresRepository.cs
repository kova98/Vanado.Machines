using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.DataAccess
{
    public abstract class PostgresRepository
    {
        protected const string ConnectionString =
            "User ID=vanado;Password=vanado;Host=localhost;Port=5432;Database=machines;";

        protected NpgsqlConnection PostgresConnection
        {
            get
            {
                return new NpgsqlConnection(ConnectionString);
            }
        }
    }
}
