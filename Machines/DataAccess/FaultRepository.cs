using Dapper;
using Machines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.DataAccess
{
    public class FaultRepository : PostgresRepository, IFaultRepository
    {
        public void CreateFault(Fault fault)
        {
            using var connection = PostgresConnection;

            var sql = @"
                INSERT INTO faults(name, machine_id, description, status, priority, date)
                    VALUES (@name, @machine_id, @description, @status, @priority, @date)
            ";

            var parameters = new
            {
                name = fault.Name,
                machine_id = fault.MachineId,
                description = fault.Description,
                status = fault.Status,
                priority = fault.Priority,
                date = fault.Date
            };

            connection.Execute(sql, parameters);
        }

        public void DeleteFault(long id)
        {
            using var connection = PostgresConnection;

            connection.Execute("DELETE FROM faults WHERE id = @id", new { id });
        }

        public IEnumerable<Fault> GetAllFaults()
        {
            using var connection = PostgresConnection;

            var result = connection.Query<Fault>("SELECT * FROM faults ORDER BY status, priority");

            return result;
        }

        public Fault GetFault(long id)
        {
            using var connection = PostgresConnection;

            var result = connection.QueryFirstOrDefault<Fault>($"SELECT * from faults WHERE id = @id", new { id });

            return result;
        }

        public IEnumerable<Fault> GetFaultsForMachine(long machineId)
        {
            using var connection = PostgresConnection;

            var result = connection.Query<Fault>("SELECT * FROM faults WHERE machine_id = @machineId", new { machineId });

            return result;
        }

        public IEnumerable<UnresolvedFaultViewModel> GetUnresolvedFaults()
        {
            using var connection = PostgresConnection;

            var result = connection.Query<UnresolvedFaultViewModel>(@"
                SELECT 
                    f.id,
	                f.name,
	                m.name as machine_name,
	                f.priority,
	                f.date
                FROM faults f
                LEFT OUTER JOIN machines m on 
	                m.id = f.machine_id

                WHERE status = 0
				
				ORDER BY 
					priority, date");

            return result;
        }

        public void UpdateFault(Fault fault)
        {
            using var connection = PostgresConnection;

            var sql = @"
                UPDATE faults
                SET 
	                name = @name,
	                machine_id = @machine_id,
	                description = @description,
	                status = @status,
	                priority = @priority,
	                date = @date

                WHERE id = @id
            ";

            var parameters = new
            {
                name = fault.Name,
                machine_id = fault.MachineId,
                description = fault.Description,
                status = fault.Status,
                priority = fault.Priority,
                date = fault.Date,
                id = fault.Id
            };

            connection.Execute(sql, parameters);
        }
    }
}
