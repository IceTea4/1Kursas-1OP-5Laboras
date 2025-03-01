namespace _5Laboras
{
    class Program
    {
        static void Main(string[] args)
        {
            const string result = "Rezultatai.txt";

            File.Delete(result);

            List<Conteiner> conteiners = InOut.ReadConteiners(@"../../../Data");

            List<Product> products = InOut.ReadProducts(@"../../../Leidiniai.csv");

            InOut.PrintConteiners(conteiners, result, "Pradiniai duomenų failai:");
            InOut.PrintProducts(products, result, "Pradinis leidinių failas:");

            List<Prenumerator> allprenum = TaskUtils.AllPrenum(conteiners);

            InOut.PrintOrderPrice(products, allprenum, result, "Užsakymo kaina prenumeratoriui:");

            Console.WriteLine("Menesis:");
            int month = int.Parse(Console.ReadLine());
            Console.WriteLine("Pradine data:");
            int startYear = int.Parse(Console.ReadLine());
            Console.WriteLine("Galine data:");
            int endYear = int.Parse(Console.ReadLine());

            List<Prenumerator> selectedPrenumerators = TaskUtils.SelectedPrenumerators(conteiners, month, startYear, endYear);

            InOut.PrintSelected(selectedPrenumerators, result, $"Atrinkti prenumeratoriai nuo {startYear} iki {endYear} metų:");

            TaskUtils.ProductCount(selectedPrenumerators, products);

            InOut.PrintProducts(products, result, "Atrinktų leidinių kiekiai:");
        }
    }
}

