using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TuristaApi2.Models;

namespace TuristaApi2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TuraController : ControllerBase
    {
        [HttpPut("Modosit")]

        public async Task<IActionResult> PutModosit(Tura tura)
        {
            try
            {
                using (var context = new TuristadbContext())
                {
                    context.Turas.Update(tura);
                    await context.SaveChangesAsync();
                    return Ok("Sikeres mentés.");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return StatusCode(404, "Hiányzó túra!");
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
