using Microsoft.EntityFrameworkCore;


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
