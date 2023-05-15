namespace PSC.Manufacturer.API.DataAccess
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ManufacturerApiDbContext _context;

        public VendorRepository(ManufacturerApiDbContext context)
        {
            _context = context;
        }

        public bool CheckIfVendorExists(int vendorKey)
        {
            var vendorList = _context.Vendors.ToList();
            return vendorList.Exists(x => x.Vendor_Key == vendorKey);
        }
    }
}
