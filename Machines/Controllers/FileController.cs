using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Machines.DataAccess;
using Machines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Machines.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileRepository repo;

        public FileController(IFileRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult DownloadFile(long fileId)
        {
            var file = repo.GetFile(fileId);
            var contentType = GetContentType(file);

            return File(file.Content, contentType, file.Name);
        }

        public IActionResult DeleteFile(long fileId, long faultId)
        {
            repo.DeleteFile(fileId);

            return RedirectToAction("EditFault", "Fault", new { id = faultId });
        }

        private string GetContentType(File file)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(file.Name, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
