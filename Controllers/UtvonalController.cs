using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;
using TuristaApi2.Models;

namespace TuristaApi2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UtvonalController : ControllerBase
    {
        [HttpGet("ById")]
        public IActionResult GetById(int id)
        {
            try
            {
                using (var context = new TuristadbContext())
                {
                    var response = context.Utvonals.Include(f => f.Nehezseg).FirstOrDefault(f => f.Id == id);
                    if (response == null)
                    {
                        var jsonObject = new JsonObject { ["id"] = response.Id, ["allomasok"] = response.Allomasok, ["tav"] = response.Tav, ["nehezseg"] = response.Nehezseg.Leiras }; 
                        return Ok(jsonObject);
                    }
                    else
                    {
                        return StatusCode(419, "Valószínűleg nincs ilyen túra.");
                    }
                   
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("All")]
        public IActionResult GetUtvonal()
        {
            try
            {
                using (var context = new TuristadbContext())
                {
                    var response = context.Utvonals.Include(f => f.Nehezseg).ToList();
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Hiba a beolvasás során: " + ex.Message);
            }
        }
    }
}
