using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuristaApi2.Models;

namespace TuristaApi2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UtvonalController : ControllerBase
    {
        [HttpGet("All")]
        public IActionResult GetUtvonal()
        {
            try
            {
                using (var context = new TuristadbContext())
                {
                    var utvonal = context.Utvonals.ToList();
                    if (utvonal == null || utvonal.Count == 0)
                    {
                        return NotFound("Nincs elérhető útvonal.");
                    }
                    return Ok(utvonal);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }

        [HttpGet("ById")]

        public IActionResult GetUtvonalById(int id)
        {
            try
            {
                using (var context = new TuristadbContext())
                {
                    var utvonal = context.Utvonals.Include(f=>f.Allomasok).Include(f=>f.Tav).Include(f=>f.Nehezseg).FirstOrDefault(f => f.Id == id);
                    if (utvonal == null)
                    {
                        return NotFound("Valószínűleg nincs ilyen túra.");
                    }
                    return Ok(utvonal);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status419AuthenticationTimeout, ex.Message);
            }
        }
    }
}
