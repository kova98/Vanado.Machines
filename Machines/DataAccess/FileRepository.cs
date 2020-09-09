using Dapper;
using Machines.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Machines.DataAccess
{
    public class FileRepository : PostgresRepository, IFileRepository
    {
        public void DeleteFile(long fileId)
        {
            using var connection = PostgresConnection;

            connection.Execute("DELETE FROM files WHERE id = @id", new { id = fileId });
        }

        public File GetFile(long id)
        {
            using var connection = PostgresConnection;

            var result = connection.QueryFirstOrDefault<File>("SELECT * FROM files WHERE id = @id", new { id });

            return result;
        }

        public IEnumerable<File> GetFiles(long faultId)
        {
            using var connection = PostgresConnection;

            var result = connection.Query<File>("SELECT * FROM files WHERE fault_id = @faultId", new { faultId });

            return result;
        }

        public void UploadFiles(IEnumerable<File> files)
        {
            using var connection = PostgresConnection;

            foreach (var file in files)
            {
                connection.Execute(
                    "INSERT INTO files (fault_id, content, name) VALUES (@faultId, @content, @name)",
                    new { faultId = file.FaultId, content = file.Content, name = file.Name });
            }
        }
    }
}
