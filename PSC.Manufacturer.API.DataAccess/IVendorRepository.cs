namespace PSC.Manufacturer.API.DataAccess
{
    public interface IVendorRepository
    {
        bool CheckIfVendorExists(int vendorKey);
    }
}
