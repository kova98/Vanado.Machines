using Machines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.DataAccess
{
    public interface IFileRepository
    {
        void UploadFiles(IEnumerable<File> files);
        File GetFile(long fileId);
        IEnumerable<File> GetFiles(long faultId);
        void DeleteFile(long fileId);
    }
}
