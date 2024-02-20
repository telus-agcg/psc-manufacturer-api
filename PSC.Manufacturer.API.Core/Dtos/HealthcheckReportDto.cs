namespace PSC.Manufacturer.API.Core.Dtos
{
    public class HealthcheckReportDto
    {
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public double ResponseTimeMs { get; set; }
    }
}
