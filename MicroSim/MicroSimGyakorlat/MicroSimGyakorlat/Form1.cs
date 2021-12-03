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
using MicroSimGyakorlat.Entities;

namespace MicroSimGyakorlat
{
    public partial class Form1 : Form
    {
        Random rng = new Random(1234);

        List<Person> Population = null;
        List<BirthProbability> BirthProbabilities = null;
        List<DeathProbability> DeathProbabilities = null;
        public Form1()
        {
            InitializeComponent();

            BirthProbabilities = GetBirthProbabilities("C:\temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities("C:\temp\halál.csv");

            StartSimulation();

            
        }

        private void StartSimulation()
        {
            Population = GetPopulation("C:\temp\nép-teszt.csv");


            for (int year = 2005; year <= 2024; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {

                    SimStep(year, Population[i]);
                }

                int NbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();

                int NbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();

                //txtMain.Text += string.Format(

                Console.WriteLine(string.Format(
                    "Év: {0}\nFiúk: {1}\nLányok: {2}\n",
                    year,
                    NbrOfMales,
                    NbrOfFemales));

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
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.P).FirstOrDefault();
            // Meghal a személy?
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbabilities
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
                    var p = new Person();
                    p.BirthYear = int.Parse(line[0]);
                    p.Gender = (Gender)Enum.Parse(typeof(Gender), line[1]);
                    p.NbrOfChildren = int.Parse(line[2]);
                    population.Add(p);
                
                
                }
            
            }

                return population;
        
        }

        public List<BirthProbability> GetBirthProbabilities(string csvPath)
        {
            List<BirthProbability> birthProbabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    var b = new BirthProbability();
                    b.Age = int.Parse(line[0]);
                    b.NbrOfChildren = int.Parse(line[1]);
                    P = double.Parse(line[2]);
                    
                    birthProbabilities.Add(b);


                }

            }

            return birthProbabilities;

        }
        public List<DeathProbability> GetDeathProbabilities(string csvPath)
        {
            List<DeathProbability> deathProbabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    var d = new DeathProbability();
                    d.Gender = (Gender)Enum.Parse(typeof(Gender), line[0]);
                    d.Age = int.Parse(line[1]);
                    P = double.Parse(line[2].Replace);

                    deathProbabilities.Add(d);


                }

            }

            return deathProbabilities;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartSimulation((int)nudYear.Value, txtPath.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            txtPath.Text = ofd.FileName;
        }
    }

}
