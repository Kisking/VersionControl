using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.entities;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Random rng = new Random(1234);
        List<Person> population = new List<Person>();
        List<Birthprobability> Birthprobabilites = new List<Birthprobability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        public Form1()
        {
            InitializeComponent();
            population.AddRange(GetPopulation(@"C:\temp\nép-teszt.csv"));
            Birthprobabilites = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

            for (int year = 2005; year <= 2024; year++)
            {
                for (int i = 0; i < population.Count; i++)
                {
                    SimStep(year, population[i]);
                }

                var nbrofMales = (from x in population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();

                var nbrofFemales = (from x in population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();

                Console.WriteLine(string.Format(
                    "Év: {0}\t" +
                    "Férfiak száma: {1}\t" +
                    "nők száma: {2}",
                    year,
                    nbrofFemales,
                    nbrofMales
                    ));
            }
        }

        private void SimStep(int year, Person person)
        {
            // Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
    if (!person.IsAlive) return;

            // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
            byte age = (byte)(year - person.BirthYear);

            // Halál kezelése
            // Halálozási valószínűség kikeresése
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.p).FirstOrDefault();
            // Meghal a személy?
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in Birthprobabilites
                                 where x.Age == age
                                 select x.p).FirstOrDefault();
                //Születik gyermek?
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrofChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    population.Add(újszülött);
                }
            }
        }

        public List<Person> GetPopulation(string csvpath)
        {
            var population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrofChildren = byte.Parse(line[2]),
                    });
                }
            }

            return population;
        }

        public List<Birthprobability> GetBirthProbabilities(string csvpath)
        {
            var birthProbabilities = new List<Birthprobability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthProbabilities.Add(new Birthprobability()
                    {
                        Age = int.Parse(line[0]),
                        NbrofChildren = byte.Parse(line[1]),
                        p = double.Parse(line[2]),

                    });
                }
            }

            return birthProbabilities;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            var deathProbabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathProbabilities.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        p = double.Parse(line[2]),

                    });
                }
            }

            return deathProbabilities;
        }
    }
}
