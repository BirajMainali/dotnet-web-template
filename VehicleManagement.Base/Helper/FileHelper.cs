using System;
using System.IO;
using System.Threading.Tasks;
using Base.ValueObject;

namespace Base.Helper
{
    public static class FileHelper
    {
        private const string Root = "Content/";

        public static async Task<string> SavePhysicalFile(FileRecordVo recordVo)
        {
            var path = Path.Combine(Root, recordVo.ParentDirectoryIdentity);
            EnsureDirectoryIsCreated(path);
            var extension = Path.GetExtension(recordVo.File.FileName);
            var encryptedFileName = Guid.NewGuid() + extension;
            var filePath = Path.Combine(path, encryptedFileName);
            await using var stream = new FileStream(filePath, FileMode.Create); 
            await recordVo.File.CopyToAsync(stream);
            return encryptedFileName;
        }

        public static void RemovePhysicalFile(string parentDirectoryIdentity, string fileName)
        {
            var removableFormat = Path.Combine(Root, parentDirectoryIdentity, fileName);
            File.Delete(removableFormat);
        }

        private static void EnsureDirectoryIsCreated(string rootDirectory)
        {
            if (!Directory.Exists(rootDirectory)) Directory.CreateDirectory(rootDirectory);
        }
    }
}