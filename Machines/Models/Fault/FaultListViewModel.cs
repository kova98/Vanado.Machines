using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.Models
{
    public class FaultListViewModel
    {
        public Fault Fault { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}
