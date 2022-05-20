using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models;
using LaMiaPizzeria.Utilis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaMiaPizzeria.Controllers
{
    public class PizzaController : Controller
    {
        [HttpGet]
        public IActionResult Index(string ricerca)
        {
            List<Pizza> pizze = new List<Pizza>();
            using(PizzeriaContext db = new PizzeriaContext())
            {
                if (ricerca != null)
                {
                    pizze = db.Pizze
                        .Where(post => post.Name.Contains(ricerca))
                        .ToList<Pizza>();
                }
                else
                {
                    pizze = db.Pizze.ToList<Pizza>();
                }
            }

            return View("HomePage", pizze);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaDaMostrare = db.Pizze
                    .Where(pizza => pizza.Id == id)
                    .Include(pizza => pizza.Category)
                    .FirstOrDefault();
                
                if (pizzaDaMostrare == null)
                {
                    return NotFound();
                }
                else
                {
                    return View("Details", pizzaDaMostrare);
                }
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Category> categories = db.Category.ToList();

                PizzaCategory nuovaPizzaConCategoria = new PizzaCategory();
                nuovaPizzaConCategoria.Pizza = new Pizza();
                nuovaPizzaConCategoria.Categories = categories;

                return View("Create", nuovaPizzaConCategoria);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCategory nuovaPizza)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    List<Category> categories = db.Category.ToList();

                    nuovaPizza.Categories = categories;
                }

                return View("Create", nuovaPizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza nuovaPizzaConCategoria = new Pizza();
                nuovaPizzaConCategoria.Name = nuovaPizza.Pizza.Name;
                nuovaPizzaConCategoria.Description = nuovaPizza.Pizza.Description;
                nuovaPizzaConCategoria.Image = nuovaPizza.Pizza.Image;
                nuovaPizzaConCategoria.Price = nuovaPizza.Pizza.Price;
                nuovaPizzaConCategoria.CategoryId = nuovaPizza.Pizza.CategoryId;

                db.Pizze.Add(nuovaPizzaConCategoria);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Pizza? pizzaModificata = null;

            List<Category> categories = new List<Category>();

            using (PizzeriaContext db = new PizzeriaContext())
            {
                pizzaModificata = db.Pizze
                     .Where(pizza => pizza.Id == id)
                     .FirstOrDefault();

                categories = db.Category.ToList<Category>();
            }

            if (pizzaModificata != null)
            {
                PizzaCategory pizzaDopoModifica = new PizzaCategory();
                pizzaDopoModifica.Pizza = pizzaModificata;
                pizzaDopoModifica.Categories = categories;

                return View("Update", pizzaDopoModifica);           
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Update(int id, PizzaCategory pizzaModificata)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    List<Category> categories = db.Category.ToList();

                    pizzaModificata.Categories = categories;
                }

                return View("Update", pizzaModificata);
            }

            Pizza? pizzaDaModificare = null;

            using (PizzeriaContext db = new PizzeriaContext())
            {
                pizzaDaModificare = db.Pizze
                     .Where(pizza => pizza.Id == id)
                     .FirstOrDefault();


                if (pizzaDaModificare != null)
                {
                    pizzaDaModificare.Name = pizzaModificata.Pizza.Name;
                    pizzaDaModificare.Description = pizzaModificata.Pizza.Description;
                    pizzaDaModificare.Image = pizzaModificata.Pizza.Image;
                    pizzaDaModificare.Price = pizzaModificata.Pizza.Price;
                    pizzaDaModificare.CategoryId = pizzaModificata.Pizza.CategoryId;
                    
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

                    return RedirectToAction("Index", "Pizza");
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}

