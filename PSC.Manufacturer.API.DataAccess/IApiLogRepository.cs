using PSC.Manufacturer.API.Core.Entities;

namespace PSC.Manufacturer.API.DataAccess
{
    public interface IApiLogRepository
    {
        Task<ApiLog?> ReadLog();
        Task WriteLog(ApiLog log);
    }
}