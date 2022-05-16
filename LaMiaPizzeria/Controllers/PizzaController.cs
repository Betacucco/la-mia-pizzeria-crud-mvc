using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models;
using LaMiaPizzeria.Utilis;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers
{
    public class PizzaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<Pizza> pizze = new List<Pizza>();
            using(PizzeriaContext db = new PizzeriaContext())
            {
                pizze = db.Pizze.ToList<Pizza>();
            }

            return View("HomePage", pizze);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                try
                {
                    Pizza postFound = db.Pizze
                         .Where(pizze => pizze.Id == id)
                         .First();

                    return View("Details", postFound);

                }
                catch (InvalidOperationException ex)
                {
                    return NotFound("Il post con id " + id + " non è stato trovato");
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }

            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("FormPizza");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza nuovaPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("FormPizza", nuovaPizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaDaAggiungere = new Pizza(nuovaPizza.Name, nuovaPizza.Description, nuovaPizza.Image, nuovaPizza.Price);
                db.Pizze.Add(pizzaDaAggiungere);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            Pizza? pizzaModificata = null;

            using (PizzeriaContext db = new PizzeriaContext())
            {
                pizzaModificata = db.Pizze
                     .Where(pizza => pizza.Id == id)
                     .FirstOrDefault();

            }

            if (pizzaModificata == null)
            {
                return NotFound();
            }
            else
            {
                return View("Modify", pizzaModificata);
            }
        }

        [HttpPost]
        public IActionResult Modify(int id, Pizza pizzaModificata)
        {
            if (!ModelState.IsValid)
            {
                return View("Modify", pizzaModificata);
            }

            Pizza? pizzaDaModificare = null;

            using (PizzeriaContext db = new PizzeriaContext())
            {
                pizzaDaModificare = db.Pizze
                     .Where(pizza => pizza.Id == id)
                     .FirstOrDefault();


                if (pizzaDaModificare != null)
                {
                    pizzaDaModificare.Name = pizzaModificata.Name;
                    pizzaDaModificare.Description = pizzaModificata.Description;
                    pizzaDaModificare.Image = pizzaModificata.Image;
                    pizzaDaModificare.Price = pizzaModificata.Price;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            using(PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaDaRimuovere = db.Pizze
                    .Where(pizza => pizza.Id == id)
                    .FirstOrDefault();
                
                if(pizzaDaRimuovere != null)
                {
                    db.Pizze.Remove(pizzaDaRimuovere);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}

