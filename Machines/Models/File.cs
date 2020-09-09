using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.Models
{
    public class File
    {
        public long Id { get; set; }
        public long FaultId { get; set; }
        public byte[] Content { get; set; }
        public string Name { get; set; }
    }
}
