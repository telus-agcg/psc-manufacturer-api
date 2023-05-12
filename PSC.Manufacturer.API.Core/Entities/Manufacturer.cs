using System.ComponentModel.DataAnnotations;

namespace PSC.Manufacturer.API.Core.Entities
{
    public class Manufacturer
    {
        [Key]
        public int Mfg_Key { get; set; }

        [Required(ErrorMessage = "Mfg_RAPID_ICC is required")]
        [MaxLength(13, ErrorMessage = "Mfg_RAPID_ICC cannot be longer than 13 characters")]
        public string Mfg_RAPID_ICC { get; set; }

        [Required(ErrorMessage = "Mfg_Name is required")]
        [MaxLength(50, ErrorMessage = "Mfg_Name cannot be longer than 50 characters")]
        public string Mfg_Name { get; set; }

        [Required(ErrorMessage = "Mfg_Name_2 is required")]
        [MaxLength(50, ErrorMessage = "Mfg_Name_2 cannot be longer than 50 characters")]
        public string Mfg_Name_2 { get; set; }

        [Required(ErrorMessage = "Address_1 is required")]
        [MaxLength(30, ErrorMessage = "Address_1 cannot be longer than 30 characters")]
        public string Address_1 { get; set; }

        [Required(ErrorMessage = "Address_2 is required")]
        [MaxLength(30, ErrorMessage = "Address_2 cannot be longer than 30 characters")]
        public string Address_2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(50, ErrorMessage = "City cannot be longer than 50 characters")]
        public string City { get; set; }

        [MaxLength(2, ErrorMessage ="State_Code cannot be longer than 50 characters")]
        public string? State_Code { get; set; }

        [Required(ErrorMessage = "Zip_Code is required")]
        [MaxLength(10, ErrorMessage = "Zip_Code cannot be longer than 10 characters")]
        public string Zip_Code { get; set; }

        [Required(ErrorMessage = "Edi_Contact is required")]
        [MaxLength(30, ErrorMessage = "City cannot be longer than 30 characters")]
        public string Edi_Contact { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [MaxLength(12, ErrorMessage = "Phone cannot be longer than 12 characters")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Fax is required")]
        [MaxLength(12, ErrorMessage = "Fax cannot be longer than 12 characters")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "EMail_Address is required")]
        [MaxLength(100, ErrorMessage = "EMail_Address cannot be longer than 100 characters")]
        [EmailAddress]
        public string EMail_Address { get; set; }

        public DateTime Inserted_Date_Time { get; set; }

        [Required(ErrorMessage = "Vendor_Key is required")]
        public int Vendor_Key { get; set; }
        
        public int? Old_Mfg_Key { get; set; }

        [Required(ErrorMessage = "Include_In_Dashboard_Filter is required")]
        public bool Include_In_Dashboard_Filter { get; set; }
    }
}
