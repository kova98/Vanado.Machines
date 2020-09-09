using Machines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.DataAccess
{   
    public interface IFaultRepository
    {
        IEnumerable<UnresolvedFaultViewModel> GetUnresolvedFaults();
        IEnumerable<Fault> GetAllFaults();
        IEnumerable<Fault> GetFaultsForMachine(long machineId);
        Fault GetFault(long id);
        void CreateFault(Fault fault);
        void UpdateFault(Fault fault);
        void DeleteFault(long id);
    }
}
