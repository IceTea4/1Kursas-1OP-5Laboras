using System;

namespace _5Laboras
{
	public class Conteiner
	{
		private List<Prenumerator> prenumerators;
		public int Year { get; }

		public Conteiner(int year)
		{
			Year = year;
			prenumerators = new List<Prenumerator>();
		}

		public void AddPrenum(Prenumerator prenumerator)
		{
			prenumerators.Add(prenumerator);
		}

		public List<Prenumerator> GetPrenumerators()
		{
			return prenumerators;
		}
	}
}

