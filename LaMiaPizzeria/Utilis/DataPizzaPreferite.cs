using LaMiaPizzeria.Models;

namespace LaMiaPizzeria.Utilis
{
    public static class DataPizzaPreferite
    {
        private static List<Pizza>? pizze;

        public static List<Pizza> GetPizze()
        {
            if (DataPizzaPreferite.pizze != null)
            {
                return DataPizzaPreferite.pizze;
            }

            List<Pizza> listaPizzePreferite = new List<Pizza>();

            DataPizzaPreferite.pizze = listaPizzePreferite;

            return DataPizzaPreferite.pizze;

        }
    }
}
