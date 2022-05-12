using LaMiaPizzeria.Models;

namespace LaMiaPizzeria.Utilis
{
    public static class PizzaData
    {
        private static List<Pizza> pizze;

        public static List<Pizza> GetPizze()
        {
            if (PizzaData.pizze != null)
            {
                return PizzaData.pizze;
            }

           List<Pizza> nuovaListaPizze= new List<Pizza>();

           Pizza pizza1 = new Pizza(1, "Pizza Margherita", "Ingredienti: mozzarella, basilico, pomodoro", "~/img/pizza-margherita.jpg", 4.50);
           nuovaListaPizze.Add(pizza1);
           Pizza pizza2 = new Pizza(2, "Pizza Napoli", "Ingredienti: alici, origano, pomodoro", "~/img/pizza-napoli.jpg", 4.00);
           nuovaListaPizze.Add(pizza2);
           Pizza pizza3 = new Pizza(3, "Pizza Bolognese", "Ingredienti: mozzarella, funghi misti, basilico", "~/img/pizza-bolognese.jpg", 7.00);
           nuovaListaPizze.Add(pizza3);
           Pizza pizza4 = new Pizza(4, "Pizza Boscaiola", "Ingredienti: mozzarella,basilico,pomodoro", "~/img/pizza-boscaiola.jpg", 8.00);
           nuovaListaPizze.Add(pizza4);
           Pizza pizza5 = new Pizza(5, "Pizza Capricciosa", "Ingredienti: Olive nere, capperi, pomodoro, carciofi, prosciutto cotto, funghi", "~/img/pizza-capricciosa.jpg", 6.50);
           nuovaListaPizze.Add(pizza5);
           Pizza pizza6 = new Pizza(6, "Pizza Mari e Monti", "Ingredienti: mozzarella, frutti di mare, porcini, pomodoro", "~/img/pizza-mari-monti.jpg", 8.00);
           nuovaListaPizze.Add(pizza6);
          

            PizzaData.pizze = nuovaListaPizze;

            return PizzaData.pizze;
        }
    }
}
