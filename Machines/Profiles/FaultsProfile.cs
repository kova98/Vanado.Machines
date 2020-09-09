using AutoMapper;
using Machines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.Profiles
{
    public class FaultsProfile : Profile
    {
        public FaultsProfile()
        {
            CreateMap<UnresolvedFaultViewModel, Fault>();
        }
    }
}
