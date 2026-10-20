using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Datalayer
{
    internal class DataHandler
    {
        string filepath = @"animals.txt";
        //Format and add animal to textfile
        public void Add(Animal a)
        {
            using (StreamWriter sw = new StreamWriter(filepath, true))
            {
                sw.WriteLine($"{a.AnimalID} | {a.Name} | {a.Species} | {a.Age} | {a.RecoveryScore} | {a.Status} | {a.HousingUnit}");
            }
        }

        //finds an animal by ID and returns its details, or null if not found
        public Animal FindByID(string animalID)
        {
            if (!File.Exists(filepath))
            {
                return null;
            }

            string[] lines = File.ReadAllLines(filepath);

            foreach (string line in lines)
            {
                string[] details = line.Split('|');

                if (details.Length == 7 &&
                    details[0].Trim().Equals(
                        animalID.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    Animal a = new Animal(
                        details[0].Trim(),
                        details[1].Trim(),
                        details[2].Trim(),
                        int.Parse(details[3].Trim()),
                        int.Parse(details[4].Trim()),
                        details[5].Trim(),
                        details[6].Trim()
                    );

                    return a;
                }
            }

            return null;

        }

        //updates the selected animals record in the text file
        public void Update(Animal a)
        {
            string[] lines = File.ReadAllLines(filepath);

            bool found = false;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split('|');

                if (data[0].Trim().Equals(
                    a.AnimalID,
                    StringComparison.OrdinalIgnoreCase))
                {
                    lines[i] = $"{a.AnimalID} | {a.Name} | {a.Species} | {a.Age} | {a.RecoveryScore} | {a.Status} | {a.HousingUnit}";

                    found = true;
                    break;
                }
            }

            if (!found)
            {
                throw new Exception("Animal ID not found.");
            }

            File.WriteAllLines(filepath, lines);
        }

        //Get animal IDs to check for uniqueness
        public List<string> GetAnimalIDs()
        {
            List<string> animalIDs = new List<string>();
            if (File.Exists(filepath))
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        animalIDs.Add(line.Substring(0, line.IndexOf('|') - 1));
                    }
                }
            }
            return animalIDs;
        }

        /*Loads every well-formed record and reports how many lines had to be
      skipped. Skipped lines are not returned, so they never reach the grid,
      and they are dropped whenever the file is next rewritten.*/
        public (List<Animal>, int) GetAll()
        {
            List<Animal> animals = new List<Animal>();

            int skippedLines = 0;

            if (File.Exists(filepath))
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        Animal a;

                        if (TryParseLine(line, out a))
                        {
                            animals.Add(a);
                        }
                        else
                        {
                            skippedLines++;
                        }
                    }
                }
            }

            return (animals, skippedLines);
        }

        private bool TryParseLine(string line, out Animal animal)
        {
            animal = null;

            if (string.IsNullOrWhiteSpace(line))
            {
                return false;
            }

            string[] details = line.Split('|');
            int age;
            int recoveryScore;
            int idNumber;
            string animalID = details.Length > 0 ? details[0].Trim().ToUpperInvariant() : string.Empty;

            if (details.Length != 7 ||
                animalID.Length != 7 ||
                animalID.Substring(0, 3) != "WR-" ||
                !int.TryParse(animalID.Substring(3, 4), NumberStyles.None, CultureInfo.InvariantCulture, out idNumber) ||
                !int.TryParse(details[3].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out age) ||
                !int.TryParse(details[4].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out recoveryScore))
            {
                return false;
            }

            animal = new Animal(
                details[0].Trim(),
                details[1].Trim(),
                details[2].Trim(),
                age,
                recoveryScore,
                details[5].Trim(),
                details[6].Trim());

            return true;
        }

        // Deletes one animal record from animals.txt using its ID and returns false if the file does not exist or the animal is not found.
        public bool Delete(string animalID)
        {

            if (!File.Exists(filepath))
            {
                return false;
            }
            List<string> lines = File.ReadAllLines(filepath).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] data = lines[i].Split('|');

                if (data.Length == 7 && data[0].Trim().Equals(animalID.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    // Remove only the matching animal record.
                    lines.RemoveAt(i);
                    File.WriteAllLines(filepath, lines);

                    // Save the remaining records back to the file.
                    return true;
                }
            }
            // if no matching animal was found nothing is deleted
            return false;
        }
    }
}