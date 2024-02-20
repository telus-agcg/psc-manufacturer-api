using Microsoft.EntityFrameworkCore;
using PSC.Manufacturer.API.Core.Entities;

namespace PSC.Manufacturer.API.DataAccess
{
    public class ApiLogRepository : IApiLogRepository
    {
        private readonly ManufacturerApiDbContext _context;

        public ApiLogRepository(ManufacturerApiDbContext context)
        {
            _context = context;
        }

        public async Task<ApiLog?> ReadLog()
        {
            return await _context.ApiLogs.FirstOrDefaultAsync();
        }

        public async Task WriteLog(ApiLog log)
        {
            _context.ApiLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
