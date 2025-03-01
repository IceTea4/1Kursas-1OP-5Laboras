using System;
using System.Text;

namespace _5Laboras
{
	public static class InOut
	{
		public static List<Conteiner> ReadConteiners(string file)
		{
			List<Conteiner> conteiners = new List<Conteiner>();

			string[] filePath = Directory.GetFiles(file, "*.csv");

			foreach (string path in filePath)
			{
				conteiners.Add(ReadConteiner(path));
			}

			return conteiners;
		}

		private static Conteiner ReadConteiner(string path)
		{
			using (StreamReader reader = new StreamReader(@path, Encoding.UTF8))
			{
				string line = reader.ReadLine();

				Conteiner conteiner = new Conteiner(int.Parse(line));

				while ((line = reader.ReadLine()) != null)
				{
					Prenumerator prenumerator = new Prenumerator(line);
					conteiner.AddPrenum(prenumerator);
				}

				return conteiner;
			}
		}

		public static List<Product> ReadProducts(string file)
		{
			using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
			{
				string line;

                List<Product> products = new List<Product>();

                while ((line = reader.ReadLine()) != null)
				{
					Product product = new Product(line);

					products.Add(product);
				}

				return products;
			}
		}

		public static void PrintConteiners(List<Conteiner> conteiners, string file, string header)
		{
			using (StreamWriter writer = new StreamWriter(file, true, Encoding.UTF8))
			{
				writer.WriteLine(header);

                foreach (Conteiner conteiner in conteiners)
				{
					if (conteiner.GetPrenumerators().Count != 0)
					{
                        writer.WriteLine(conteiner.Year);
                        writer.WriteLine(new string('-', 77));
						writer.WriteLine(String.Format($"| {"Pavardė",-15} | {"Adresas",-15} | {"Pradžia",-7} | {"Trukmė",-6} | {"Kodas",-8} | {"Kiekis",-7} |"));
                        writer.WriteLine(new string('-',77));

                        foreach (Prenumerator prenumerator in conteiner.GetPrenumerators())
                        {
                            writer.WriteLine(prenumerator.ToString());
                        }

                        writer.WriteLine(new string('-', 77));
                    }
                }

				writer.WriteLine();
            }
		}

		public static void PrintProducts(List<Product> products, string file, string header)
		{
			using (StreamWriter writer = new StreamWriter(file, true, Encoding.UTF8))
			{
				writer.WriteLine(header);
                writer.WriteLine(new string('-', 48));
                writer.WriteLine(String.Format($"| {"Kodas",-8} | {"Pavadinimas",-15} | {"Kaina",-6} | {"Kiekis",-6} |"));
                writer.WriteLine(new string('-', 48));

                foreach (Product product in products)
				{
                    writer.WriteLine(product.ToString());
                    writer.WriteLine(new string('-', 48));
                }

				writer.WriteLine();
            }
        }

        public static void PrintOrderPrice(List<Product> products, List<Prenumerator> prenumerators, string file, string header)
		{
			using (StreamWriter writer = new StreamWriter(file, true, Encoding.UTF8))
			{
				writer.WriteLine(header);
                writer.WriteLine(new string('-', 28));
				writer.WriteLine(String.Format($"| {"Pavardė",-15} | {"Kaina",-6} |"));
                writer.WriteLine(new string('-', 28));

				var updatedPrenumerators = prenumerators.Join(products,
					prenum => prenum.Code,
					prod => prod.Code,
					(prenum, prod) => new
					{
						Subscriber = prenum.Surname,
						Price = prod.Price * prenum.Count * prenum.Duration
					});

				foreach (var el in updatedPrenumerators)
				{
					writer.WriteLine(String.Format($"| {el.Subscriber,-15} | {el.Price,6} |"));
                    writer.WriteLine(new string('-', 28));
                }

				writer.WriteLine();
            }
        }

		public static void PrintSelected(List<Prenumerator> prenumerators, string file, string header)
		{
			using (StreamWriter writer = new StreamWriter(file, true, Encoding.UTF8))
			{
				writer.WriteLine(header);
                writer.WriteLine(new string('-', 109));
                writer.WriteLine(String.Format($"| {"Pavardė",-15} | {"Adresas",-15} |  1  |  2  |  3  |  4  |  5  |  6  |  7  |  8  |  9  | 10  | 11  | 12  |"));
                writer.WriteLine(new string('-', 109));

                foreach (Prenumerator prenumerator in prenumerators)
				{
                    writer.WriteLine(String.Format($"| {prenumerator.Surname,-15} | {prenumerator.Address,-15} | {OrderedMonths(prenumerator)}"));
                    writer.WriteLine(new string('-', 109));
                }

                writer.WriteLine();
            }
        }

		private static string OrderedMonths(Prenumerator prenumerator)
		{
			string line = "";

			for (int i = 1; i < 13; i++)
			{
				if (prenumerator.Start <= i && prenumerator.Duration + prenumerator.Start - 1 >= i)
				{
					if (i == 12)
					{
                        line += " *  |";
                    }
					else
					{
                        line += " *  | ";
                    }
                }
				else
				{
                    if (i == 12)
                    {
                        line += " .  |";
                    }
                    else
                    {
                        line += " .  | ";
                    }
				}
			}

			return line;
		}
	}
}

