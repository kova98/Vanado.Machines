using Dapper;
using Machines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.DataAccess
{
    public class MachineRepository : PostgresRepository, IMachineRepository
    {
        public void CreateMachine(Machine machine)
        {
            using var connection = PostgresConnection;

            connection.Execute("INSERT INTO machines (name) VALUES (@name)", new { name = machine.Name });
        }

        public void DeleteMachine(long id)
        {
            using var connection = PostgresConnection;

            connection.Execute("DELETE FROM machines WHERE id = @id", new { id });
        }

        public IEnumerable<Machine> GetAllMachines()
        {
            using var connection = PostgresConnection;

            var result = connection.Query<Machine>("SELECT * FROM machines ORDER BY name");

            return result;
        }

        public Machine GetMachine(long id)
        {
            using var connection = PostgresConnection;

            var result = connection.QueryFirstOrDefault<Machine>("SELECT * FROM machines WHERE id = @id", new { id });

            return result;
        }

        public void UpdateMachine(Machine machine)
        {
            using var connection = PostgresConnection;

            connection.Execute("UPDATE machines SET name = @name WHERE id = @id", new { name = machine.Name, id = machine.Id });
        }
    }
}
