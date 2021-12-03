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
        List<Person> Population = null;
        List<BirthProbablity> BirthProbablities = new List<BirthProbablity>();
        List<DeathProbablity> DeathProbablities = new List<DeathProbablity>();
        public Form1()
        {
            InitializeComponent();

            Population = GetPopulation(@"C:\Temp\nép-teszt.csv");
            BirthProbablities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbablities = GetDeathProbabilities(@"C:\Temp\halál.csv");
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
    }
}
