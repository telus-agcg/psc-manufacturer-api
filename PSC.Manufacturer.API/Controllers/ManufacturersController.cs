using Microsoft.AspNetCore.Mvc;
using PSC.Manufacturer.API.Core;
using PSC.Manufacturer.API.Core.Dtos;
using PSC.Manufacturer.API.DataAccess;

namespace PSC.Manufacturer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository _repository;
        private readonly ILogger<ManufacturersController> _logger;

        public ManufacturersController(IManufacturerRepository repository, ILogger<ManufacturersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET api/<ManufacturersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var result = await _repository.GetManufacturerById(id);
                if (result.Mfg_Key ==0)
                {
                    return NotFound();
                }

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        
        [HttpGet("GetByVendorKey/{vendorKey}")]
        public async Task<ActionResult> GetByVendorKey(int vendorKey)
        {
            try
            {
                var result = await _repository.GetManufacturerByVendorKey(vendorKey);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
        
        [HttpGet("Search")]
        public async Task<ActionResult> Search([FromQuery] ManufacturerFilter filter)
        {
            try
            {
                var result = await _repository.Search(filter);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        // POST api/<ManufacturersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Core.Entities.Manufacturer manufacturer)
        {
            try
            {
                var result = await _repository.Create(manufacturer);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        // PUT api/<ManufacturersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Core.Entities.Manufacturer manufacturer)
        {
            try
            {
                var result = await _repository.GetManufacturerById(id);
                if (result.Mfg_Key == 0)
                {
                    return NotFound();
                }

                var updateResult = await _repository.Update(manufacturer);
                return new OkObjectResult(updateResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
    }
}
