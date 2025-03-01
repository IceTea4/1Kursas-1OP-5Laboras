using System;

namespace _5Laboras
{
	public static class TaskUtils
	{
        public static List<Prenumerator> AllPrenum(List<Conteiner> conteiners)
        {
            var allprenum = conteiners.SelectMany(container => container.GetPrenumerators());

            return allprenum.ToList();
        }

        public static List<Prenumerator> SelectedPrenumerators(List<Conteiner> conteiners, int month, int startYear, int endYear)
        {
            var selectedPrenumerators = conteiners
                .Where(c => c.Year >= startYear && c.Year <= endYear)
                .SelectMany(conteiner => conteiner.GetPrenumerators())
                .Where(prenumerator => month >= prenumerator.Start && month <= prenumerator.Duration + prenumerator.Start - 1)
                .OrderBy(pren => pren.Address)
                .ThenBy(pren => pren.Surname);

            return selectedPrenumerators.ToList();
        }

        public static void ProductCount(List<Prenumerator> prenumerators, List<Product> products)
        {
            prenumerators.ForEach(prenumerator =>
            {
                products.Where(product => product.Code == prenumerator.Code)
                       .ToList()
                       .ForEach(product => product.AddCount(prenumerator.Count));
            });
        }
    }
}

