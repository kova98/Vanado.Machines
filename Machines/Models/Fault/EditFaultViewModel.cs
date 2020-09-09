using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.Models
{
    public class EditFaultViewModel
    {
        public Fault Fault { get; set; }
        public IEnumerable<Machine> Machines { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}
