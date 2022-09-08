using Microsoft.AspNetCore.Http;

namespace Base.ValueObject
{
    public class FileRecordVo
    {
        public FileRecordVo(string parentDirectoryIdentity, IFormFile file)
        {
            ParentDirectoryIdentity = parentDirectoryIdentity;
            File = file;
        }
        public string ParentDirectoryIdentity { get; set; }
        public IFormFile File { get; set; }
    }
}