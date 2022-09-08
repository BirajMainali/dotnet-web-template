using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Base.Providers
{
    public class CustomFileProvider : IFileProvider
    {
        private readonly string _root;
        private PhysicalFileProvider _provider;

        public CustomFileProvider(string root)
        {
            _root = root;
            SetProvider();
        }

        public IDirectoryContents GetDirectoryContents(string subpath) => GetProvider().GetDirectoryContents(subpath);

        public IFileInfo GetFileInfo(string subpath) => GetProvider().GetFileInfo(subpath);

        public IChangeToken Watch(string filter) => GetProvider().Watch(filter);

        private void SetProvider()
        {
            if (!Directory.Exists(_root)) Directory.CreateDirectory(_root);

            _provider ??= new PhysicalFileProvider(_root);
        }

        private IFileProvider GetProvider() => _provider;
    }
}