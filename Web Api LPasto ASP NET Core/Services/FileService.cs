using Web_Api_LPasto_ASP_NET_Core.Services.Unterfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Services
{
    public class FileService : IFileService
    {
        private readonly IHostEnvironment _environment;
        public FileService(IHostEnvironment hostEnvironment)
        {
            _environment = hostEnvironment;   
        }
        public FileStream GetImgByGuid(string Guid)
        {
            var path = Path.Combine(this._environment.ContentRootPath, "img/") + Guid + ".jpeg";

            var img = System.IO.File.OpenRead(path);

            return img;
        }
    }
}
