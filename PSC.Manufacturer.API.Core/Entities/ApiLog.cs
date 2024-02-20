namespace PSC.Manufacturer.API.Core.Entities
{
    public class ApiLog
    {
        public long ApiLogKey { get; set; }
        public DateTime Date { get; set; }
        public string Thread { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Logger { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? Exception { get; set; }
        public string? Controller { get; set; }
        public string? Url { get; set; }
        public string? CorrelationId { get; set; }
    }
}
