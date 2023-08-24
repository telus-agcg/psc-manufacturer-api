using PSC.Manufacturer.API.Core.Dtos;

namespace PSC.Manufacturer.API.DataAccess
{
    public interface IManufacturerRepository
    {
        Task<int> Create(Core.Entities.Manufacturer manufacturer);
        Task<List<Core.Entities.Manufacturer>> GetManufacturerByVendorKey(int vendorKey);
        Task<List<Core.Entities.Manufacturer>> Search(ManufacturerFilter filter);
        Task<string> Update(Core.Entities.Manufacturer manufacturer);
        Task<Core.Entities.Manufacturer> GetManufacturerById(int id);
        Task<List<ManufacturerListItemDto>> GetAll();
    }
}
