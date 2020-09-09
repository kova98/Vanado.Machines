using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Machines.DataAccess;
using Machines.Models;
using Microsoft.AspNetCore.Mvc;

namespace Machines.Controllers
{
    public class MachineController : Controller
    {
        private readonly IMachineRepository machineRepo;
        private readonly IFaultRepository faultRepo;

        public MachineController(IMachineRepository machineRepo, IFaultRepository faultRepo)
        {
            this.machineRepo = machineRepo;
            this.faultRepo = faultRepo;
        }

        public IActionResult Index()
        {
            return MachineList();
        }

        public IActionResult AddMachine()
        {
            return EditMachine(-1);
        }

        public IActionResult EditMachine(long id)
        {
            var machine = machineRepo.GetMachine(id);
            if (machine == null)
            {
                machine = new Machine();
                ViewBag.Action = "Add";
            }
            else
            {
                ViewBag.Action = "Edit";
            }

            return View("EditMachine", machine);
        }

        public IActionResult SaveMachine(Machine machine)
        {
            var machineExists = machineRepo.GetMachine(machine.Id) != null;

            if (machineExists)
            {
                machineRepo.UpdateMachine(machine);
            }
            else
            {
                machineRepo.CreateMachine(machine);
            }

            return RedirectToAction("MachineList");
        }

        public IActionResult MachineList()
        {
            var machines = machineRepo.GetAllMachines();

            return View("MachineList", machines);
        }

        public IActionResult DeleteMachine(long id)
        {
            machineRepo.DeleteMachine(id);

            return RedirectToAction("MachineList");
        }

        public IActionResult MachineFaults(long id)
        {
            var faults = faultRepo.GetFaultsForMachine(id);
            var machine = machineRepo.GetMachine(id);

            var viewModel = new MachineFaultsViewModel
            {
                Faults = faults,
                MachineName = machine.Name
            };

            return View(viewModel);
        }
    }
}
