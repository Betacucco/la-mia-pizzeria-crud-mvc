using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            List<Pizza> pizze = new List<Pizza>();

            using(PizzeriaContext db = new PizzeriaContext())
            {
                pizze = db.Pizze.ToList<Pizza>();
            }
            return Ok(pizze);
        }
    }
}
