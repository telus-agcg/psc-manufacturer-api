namespace PSC.Manufacturer.API.Core.Dtos
{
    public class ManufacturerDto
    {
        public int Mfg_Key { get; set; }
        public string Mfg_RAPID_ICC { get; set; }
        public string Mfg_Name { get; set; }
        public string Mfg_Name_2 { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string City { get; set; }
        public string State_Code { get; set; }
        public string Zip_Code { get; set; }
        public string Edi_Contact { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EMail_Address { get; set; }
        public DateTime Inserted_Date_Time { get; set; }
        public int Vendor_Key { get; set; }
        public int? Old_Mfg_Key { get; set; }
        public bool Include_In_Dashboard_Filter { get; set; }
    }
}
