﻿using Microsoft.AspNetCore.Mvc;
using PSC.Manufacturer.API.Core;
using PSC.Manufacturer.API.Core.Dtos;
using PSC.Manufacturer.API.Core.Entities;
using PSC.Manufacturer.API.DataAccess;

namespace PSC.Manufacturer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository _repository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IApiLogRepository _apiLogRepository;
        private readonly ILogger<ManufacturersController> _logger;

        public ManufacturersController(
            IManufacturerRepository repository,
            IVendorRepository vendorRepository,
            IApiLogRepository apiLogRepository,
            ILogger<ManufacturersController> logger)
        {
            _repository = repository;
            _vendorRepository = vendorRepository;
            _apiLogRepository = apiLogRepository;
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

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = await _repository.GetAll();
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
                if (manufacturer.Mfg_Key != 0)
                {
                    return BadRequest("Cannot specify key for new record. Please remove mfg_key or set to 0.");
                }

                if (!_vendorRepository.CheckIfVendorExists(manufacturer.Vendor_Key))
                {
                    return BadRequest("Vendor does not exist");
                }

                if (manufacturer.Inserted_Date_Time.Year == 0001)
                {
                    manufacturer.Inserted_Date_Time = DateTime.Now;
                }

                var result = await _repository.Create(manufacturer);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        // POST api/<ManufacturersController>
        [HttpPost("collection")]
        public async Task<ActionResult> PostMany([FromBody] IEnumerable<Core.Entities.Manufacturer> manufacturer)
        {
            try
            {
                if (manufacturer.Any(x => x.Mfg_Key != 0))
                {
                    return BadRequest("Cannot specify key for new record. Please remove mfg_key or set to 0.");
                }

                if (!manufacturer.Select(x => x.Vendor_Key).Distinct().All(_vendorRepository.CheckIfVendorExists))
                {
                    return BadRequest("Vendor does not exist");
                }

                foreach (var mfg in manufacturer)
                {
                    if (mfg.Inserted_Date_Time.Year == 0001)
                    {
                        mfg.Inserted_Date_Time = DateTime.Now;
                    }
                }

                var result = await _repository.CreateCollection(manufacturer);
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
                var recordToUpdate = await _repository.GetManufacturerById(id);
                if (recordToUpdate.Mfg_Key == 0)
                {
                    return NotFound();
                }

                if (!_vendorRepository.CheckIfVendorExists(manufacturer.Vendor_Key))
                {
                    return BadRequest("Vendor does not exist");
                }

                if (manufacturer.Inserted_Date_Time.Year == 0001)
                {
                    manufacturer.Inserted_Date_Time = recordToUpdate.Inserted_Date_Time;
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


        [HttpGet("/healthcheck")]
        public async Task<IActionResult> Healthcheck()
        {
            var tracker = new HealthcheckTracker();
            return Ok(
                new[]
                {
                    await tracker.Track(
                        "Database Read",
                        async () =>
                        {
                            await _apiLogRepository.ReadLog();
                        }),
                    await tracker.Track(
                        "Database Write",
                        async () =>
                        {
                            await _apiLogRepository.WriteLog(new ApiLog { Message = "Healthcheck", Date = DateTime.UtcNow });
                        }),
                    await tracker.Track(
                        "Logger",
                        () =>
                        {
                            _logger.LogError("Healthcheck");
                            return Task.CompletedTask;
                        }),
                });
        }
    }
}
