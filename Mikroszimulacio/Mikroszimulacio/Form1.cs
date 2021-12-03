using Mikroszimulacio.Entities;
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

namespace Mikroszimulacio
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();

        List<Person> Population = null;
        List<BirthProbablity> BirthProbablities = new List<BirthProbablity>();
        List<DeathProbablity> DeathProbablities = new List<DeathProbablity>();
        public Form1()
        {
            InitializeComponent();

            Population = GetPopulation(@"C:\Temp\nép-teszt.csv");
            BirthProbablities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbablities = GetDeathProbabilities(@"C:\Temp\halál.csv");
            StartSimulation();
        }
        
        private void StartSimulation(int endYear, string csvPath)
            {

            
            Population=GetPopulation(csvPath);

            for (int year = 2005; year <= 2024; year++)
            {
                // Végigmegyünk az összes személyen
                for (int i = 0; i < Population.Count; i++)
                {
                    // Ide jön a szimulációs lépés
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                richTextBox1.Text +=
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}\n", year, nbrOfMales, nbrOfFemales);
            }


            }

        private void SimStep(int year, Person person)
        {
            //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
            if (!person.IsAlive) return;

            // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
            byte age = (byte)(year - person.BirthYear);

            // Halál kezelése
            // Halálozási valószínűség kikeresése
            double pDeath = (from x in DeathProbablities
                             where x.Gender == person.Gender && x.Age == age
                             select x.P).FirstOrDefault();
            // Meghal a személy?
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbablities
                                 where x.Age == age
                                 select x.P).FirstOrDefault();
                //Születik gyermek?
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }

        public List<Person> GetPopulation(string csvPath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');

                    /*var p = new Person();
                      p.BirthYear = int.Pase...*/
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }


            return null;
        }

        public List<BirthProbablity> GetBirthProbabilities (string csvPath)
        {
            List<BirthProbablity> birthProbabilities = new List<BirthProbablity>();

            using (StreamReader sr = new StreamReader(csvPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');

                    /*var p = new Person();
                      p.BirthYear = int.Pase...*/
                    birthProbabilities.Add(new BirthProbablity()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        P = double.Parse(line[2])
                        
                    });
                }
            }


            return null;
        }

        public List<DeathProbablity> GetDeathProbabilities(string csvPath)
        {
            List<DeathProbablity> deathhProbabilities = new List<DeathProbablity>();

            using (StreamReader sr = new StreamReader(csvPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');

                    /*var p = new Person();
                      p.BirthYear = int.Pase...*/
                    deathhProbabilities.Add(new DeathProbablity()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        P = double.Parse(line[2])

                    });
                }
            }


            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartSimulation((int)nudYear.Value, txtPath.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.FileName=txtPath.Text;

            if (ofd.ShowDialog() !=DialogResult.OK)
	{
                return;
	}
            txtPath.Text=ofd.FileName;
        }
    }
}
