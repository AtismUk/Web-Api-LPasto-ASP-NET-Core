using System.IO;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;

namespace Web_Api_LPasto_ASP_NET_Core.Services.Unterfaces
{
    public interface IFileService
    {
        FileStream GetImgByGuid(string guid);
    }
}
