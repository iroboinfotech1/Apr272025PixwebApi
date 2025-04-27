using Microsoft.AspNetCore.Mvc;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models;

namespace sms.space.management.api.Controllers
{
    [Route("api/SMSService/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        //private readonly DataBaseContext _context;
        //private readonly ICountry _countryImpl;
        //private readonly ILogger<CountriesController> _logger;
        //public CountriesController(ILogger<CountriesController> logger, Implementations.Country countryImpl, DataBaseContext context)
        //{
        //    //_context = context;
        //    _logger = logger;
        //    _countryImpl = countryImpl;
        //}
        private readonly ICountryService _service;
        public CountriesController(ICountryService service)
        {
            this._service = service;
        }


        // GET: api/Countries
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.GetAll();
            return Ok(new ApiResponse(response, ApiStatusCodes.Success, response != null, response != null ? "Record Found" : "No Record Found"));
        }

        // GET: api/Countries/5
        //[HttpGet("{id}")]
        //public ActionResult<Country> GetCountry(int id)
        //{
        //    //var country = await _context.Country.FindAsync(ID);
        //    var country = _countryImpl.Get(id);
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(country);
        //}

        //// PUT: api/Countries/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public IActionResult PutCountry(int id, Country country)
        //{
        //    //_context.Entry(country).State = EntityState.Modified;

        //    //try
        //    //{
        //    //    await _context.SaveChangesAsync();
        //    //}
        //    //catch (DbUpdateConcurrencyException)
        //    //{
        //    //    if (!CountryExists(ID))
        //    //    {
        //    //        return NotFound();
        //    //    }
        //    //    else
        //    //    {
        //    //        throw;
        //    //    }
        //    //}
        //    try
        //    {
        //        if (id != country.ID)
        //        {
        //            return BadRequest();
        //        }
        //        return Ok(_countryImpl.Update(id, country));
        //    }
        //    catch (Exception oEx)
        //    {
        //        _logger.LogError(oEx.Message, oEx);
        //        if (!CountryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}

        //// POST: api/Countries
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public ActionResult<Country> PostCountry(Country country)
        //{
        //    //_context.Country.Add(country);
        //    try
        //    {
        //        //await _context.SaveChangesAsync();
        //        _countryImpl.Add(country);
        //    }
        //    catch (Exception oEx)
        //    {
        //        _logger.LogError(oEx.Message, oEx);
        //        if (CountryExists(country.ID))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetCountry", new { id = country.ID }, country);
        //}

        //// DELETE: api/Countries/5
        //[HttpDelete("{id}")]
        //public IActionResult DeleteCountry(int id)
        //{
        //    //var country = await _context.Country.FindAsync(ID);
        //    var country = _countryImpl.Get(id);
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    //_context.Country.Remove(country);
        //    //await _context.SaveChangesAsync();
        //    _countryImpl.Delete(id);
        //    return NoContent();
        //}

        //private bool CountryExists(int ID)
        //{
        //    //return _context.Country.Any(e => e.ID == ID);
        //    return _countryImpl.Get(ID) != null;
        //}
    }
}
