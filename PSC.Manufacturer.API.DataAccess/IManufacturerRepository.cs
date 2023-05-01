namespace PSC.Manufacturer.API.DataAccess
{
    public interface IManufacturerRepository
    {
        Task<int> Create(Core.Entities.Manufacturer manufacturer);
        Task<string> Update(Core.Entities.Manufacturer manufacturer);
        Task<Core.Entities.Manufacturer> GetManufacturerById(int id);
    }
}
