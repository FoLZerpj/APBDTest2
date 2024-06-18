using APBDTest2.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBDTest2.Controllers;

[Route("api/publishingHouses")]
[ApiController]
public class PublishingHousesController : ControllerBase
{

    private readonly AppDbContext _context;
    public PublishingHousesController(AppDbContext context)
    { 
        _context = context;
    }

    [HttpGet]
    public IActionResult GetPublishingHouses(string? cityFilter, string? countryFilter)
    {
        try
        {
            var query = _context.PublishingHouses.AsQueryable();
            if (cityFilter != null)
            {
                query = query.Where(h => h.City == cityFilter);
            }

            if (countryFilter != null)
            {
                query = query.Where(h => h.Country == countryFilter);
            }

            query = query.OrderBy(h => h.Country).ThenBy(h => h.Name);

            var query1 = query.Select(d => new PublishingHouseData(d.IdPublishingHouse, d.Name, d.Country, d.City, _context.Books.Where(b => b.PublishingHouse == d).ToList()));
            
            return Ok(query1.ToList());
        }
        catch (Exception e) { return BadRequest(e); }
    }
}

public record PublishingHouseData(int IdPublishingHouse, string Name, string Country, string City, List<Book> Books);