using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaMiaPizzeria.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string? ricerca)
        {
            List<Pizza> pizze = new List<Pizza>();

            using(PizzeriaContext db = new PizzeriaContext())
            {
                if(ricerca != null && ricerca != "")
                {
                    pizze = db.Pizze
                        .Where(pizza => pizza.Name.Contains(ricerca))
                        .ToList<Pizza>();
                }
                else
                {
                    pizze = db.Pizze
                        .Include(pizza => pizza.Category)
                        .ToList<Pizza>();
                }
            }
            return Ok(pizze);
        }
    }
}
