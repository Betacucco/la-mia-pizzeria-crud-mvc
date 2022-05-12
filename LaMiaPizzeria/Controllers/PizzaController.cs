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
            List<Pizza> pizze = PizzaData.GetPizze();
            return View("HomePage", pizze);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Pizza pizzaFound = GetPizzaById(id);

            if (pizzaFound != null)
            {
                return View("Details", pizzaFound);
            }
            else
            {
                return NotFound("Il post con id " + id + " non e' stato trovato!");
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

            Pizza nuovaPizzaDaAggiungere = new Pizza(PizzaData.GetPizze().Count, nuovaPizza.Name, nuovaPizza.Description, nuovaPizza.Image, nuovaPizza.Price);
            PizzaData.GetPizze().Add(nuovaPizzaDaAggiungere);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Modify(int id)
        {
            Pizza pizzaModificata = GetPizzaById(id);

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
        public IActionResult Modify(int id, Pizza model)
        {
            if (!ModelState.IsValid)
            {
                return View("Modify", model);
            }

            Pizza pizzaBeforeChanges = GetPizzaById(id);

            if (pizzaBeforeChanges != null)
            {
                pizzaBeforeChanges.Name = model.Name;
                pizzaBeforeChanges.Description = model.Description;
                pizzaBeforeChanges.Image = model.Image;
                pizzaBeforeChanges.Price = model.Price;
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            int indidcePizzaDaRimuovere = IndexOfResearch(id);

            if(indidcePizzaDaRimuovere != -1)
            {
                PizzaData.GetPizze().RemoveAt(indidcePizzaDaRimuovere);
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        //METODO CEH RICERCA L'ID NELLA LISTA
        private int IndexOfResearch(int id)
        {
            int indexToRemove = -1;
            List<Pizza> pizzaList = PizzaData.GetPizze();

            for(int i = 0; i < pizzaList.Count; i++)
            {
                if (pizzaList[i].Id == id)
                {
                    indexToRemove = i;
                }
            }
            return indexToRemove;
        }

        //METODO CHE RITORNA LA PIZZA TRAMITE ID
        private Pizza GetPizzaById(int id)
        {
            Pizza pizzaFound = null;

            foreach (Pizza pizza in PizzaData.GetPizze())
            {
                if (pizza.Id == id)
                {
                    pizzaFound = pizza;
                    break;
                }
            }
            return pizzaFound;
        }
    }
}

