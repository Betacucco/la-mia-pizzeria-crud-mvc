using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models;
using LaMiaPizzeria.Utilis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            List<Pizza> pizze = DataPizzaPreferite.GetPizze();

            return Ok(pizze.ToList());
        }

        [HttpPost]
        public IActionResult Post(int id)
        {

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            
            Pizza? pizzaDaAggiungere = null;

            using (PizzeriaContext db = new PizzeriaContext())
            {
                foreach (Pizza pizza in db.Pizze.ToList<Pizza>())
                {
                    if (pizza.Id == id)
                    {
                        pizzaDaAggiungere = pizza;
                        break;
                    }
                }               
                DataPizzaPreferite.GetPizze().Add(pizzaDaAggiungere);
                return Ok();
            }

        }


    }
}
