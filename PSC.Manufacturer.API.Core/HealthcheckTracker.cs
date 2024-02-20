using PSC.Manufacturer.API.Core.Dtos;

namespace PSC.Manufacturer.API.Core
{
    public class HealthcheckTracker
    {
        public async Task<HealthcheckReportDto> Track(string name, Func<Task> healthcheck)
        {
            var date = DateTime.UtcNow;
            var time = 0d;
            var result = "Ok";
            try
            {
                await healthcheck();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }
            finally
            {
                time = DateTime.UtcNow.Subtract(date).TotalMilliseconds;
            }

            return new HealthcheckReportDto
            {
                Description = name,
                Status = result,
                ResponseTimeMs = time,
            };
        }
    }
}
