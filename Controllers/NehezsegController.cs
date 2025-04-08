using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuristaApi2.Models;

namespace TuristaApi2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NehezsegController : ControllerBase
    {
        [HttpPost("Uj")]

        public async Task<IActionResult> PostUj(Nehezseg nehezseg)
        {
            try
            {
                using (var context = new TuristadbContext())
                {
                    await context.Nehezsegs.AddAsync(nehezseg);
                    await context.SaveChangesAsync();
                    return Ok("Sikeres mentés.");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(406, $"Már létező jelzés: {nehezseg.Jelzes} ");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
