using Machines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.DataAccess
{
    public interface IMachineRepository
    {
        IEnumerable<Machine> GetAllMachines();
        Machine GetMachine(long id);
        void CreateMachine(Machine machine);
        void UpdateMachine(Machine machine);
        void DeleteMachine(long id);
    }
}
