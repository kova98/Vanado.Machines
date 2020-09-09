using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Machines.DataAccess;
using Machines.Extensions;
using Machines.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Machines.Controllers
{
    public class FaultController : Controller
    {
        private readonly IFaultRepository faultRepo;
        private readonly IMachineRepository machineRepo;
        private readonly IFileRepository fileRepo;

        public FaultController(IFaultRepository faultRepo, IMachineRepository machineRepo, IFileRepository fileRepo)
        {
            this.faultRepo = faultRepo;
            this.machineRepo = machineRepo;
            this.fileRepo = fileRepo;
        }

        public IActionResult Index()
        {
            return FaultList();
        }

        public IActionResult FaultList()
        {
            var faults = faultRepo.GetAllFaults();
            var viewModels = new List<FaultListViewModel>();

            foreach (var fault in faults)
            {
                viewModels.Add(new FaultListViewModel
                {
                    Fault = fault,
                    Files = fileRepo.GetFiles(fault.Id)
                });
            }

            return View("FaultList", viewModels);
        }

        public IActionResult AddFault()
        {
            return EditFault(-1);
        }

        public IActionResult EditFault(long id)
        {
            var viewModel = new EditFaultViewModel
            {
                Machines = machineRepo.GetAllMachines().OrderBy(m => m.Name),
                Files = fileRepo.GetFiles(id),
                Fault = faultRepo.GetFault(id) ?? new Fault()
            };

            return View("EditFault", viewModel);
        }

        public async Task<IActionResult> SaveFault(Fault fault, List<IFormFile> files)
        {
            if (FilesValid(files))
            {
                await UploadFiles(fault.Id, files);
            }

            var faultExists = faultRepo.GetFault(fault.Id) != null;

            if (faultExists)
            {
                faultRepo.UpdateFault(fault);
            }
            else
            {
                faultRepo.CreateFault(fault);
            }

            return RedirectToAction("FaultList");
        }

        public IActionResult DeleteFault(long id)
        {
            faultRepo.DeleteFault(id);

            return RedirectToAction("FaultList");
        }

        public IActionResult MarkResolved(long id)
        {
            var fault = faultRepo.GetFault(id);
            fault.Status = FaultStatus.Resolved;

            faultRepo.UpdateFault(fault);

            return RedirectToAction("Index", "Home");
        }
        private bool FilesValid(List<IFormFile> files)
        {
            if (files.Count == 0)
            {
                return false;
            }

            foreach (var file in files)
            {
                if (file.Length > BytesToMegabytes(5))
                {
                    return false;
                }
            }

            return true;
        }

        private async Task UploadFiles(long faultId, List<IFormFile> files)
        {
            var filesModel = new List<File>();

            foreach (var file in files)
            {
                filesModel.Add(new File
                {
                    FaultId = faultId,
                    Content = await file.GetBytes(),
                    Name = file.FileName
                });
            }

            fileRepo.UploadFiles(filesModel);
        }

        private long BytesToMegabytes(int bytes)
        {
            return bytes * (long)Math.Pow(10, 6);
        }
    }
}
