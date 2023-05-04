using Microsoft.EntityFrameworkCore;
using PSC.Manufacturer.API.Core;
using PSC.Manufacturer.API.Core.Dtos;


namespace PSC.Manufacturer.API.DataAccess
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ManufacturerApiDbContext _context;

        public ManufacturerRepository(ManufacturerApiDbContext context)
        {
            _context = context;
        }

        public async Task<Core.Entities.Manufacturer> GetManufacturerById(int id)
        {
            throw new NotImplementedException();
            var result = await _context.Manufacturers.AsNoTracking().FirstOrDefaultAsync(x=>x.Mfg_Key == id);
            return (result != null) ? result : new Core.Entities.Manufacturer();
        }
        
        public async Task<List<Core.Entities.Manufacturer>> GetManufacturerByVendorKey(int vendorKey)
        {
            var result = await _context.Manufacturers.AsNoTracking().Where(x => x.Vendor_Key == vendorKey)
                .ToListAsync();
            return result;
        }
        
        public async Task<List<Core.Entities.Manufacturer>> Search(ManufacturerFilter filter)
        {
            var query = _context.Manufacturers.AsQueryable().AsNoTracking();
            if (filter.ManufacturerKey.HasValue)
            {
                query = query.Where(x => x.Mfg_Key == filter.ManufacturerKey);
            }

            if (!string.IsNullOrEmpty(filter.MfgName))
            {
                query = query.Where(x => x.Mfg_Name.Contains(filter.MfgName));

                var queryMfgName2 = _context.Manufacturers.AsQueryable().AsNoTracking().Where(x => x.Mfg_Name_2.Contains(filter.MfgName));

                query = query.Union(queryMfgName2);
            }

            return await query.OrderBy(x => x.Mfg_Name).Page(filter.PageNumber, filter.PageSize).ToListAsync();
        }

        public async Task<int> Create(Core.Entities.Manufacturer manufacturer)
        {
            var result = _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();
            return result.Entity.Mfg_Key;
        }

        public async Task<string> Update(Core.Entities.Manufacturer manufacturer)
        {
            var result = _context.Manufacturers.Update(manufacturer);
            await _context.SaveChangesAsync();
            return "Updated";
        }
    }
}
