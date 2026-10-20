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

        //reads all animal records from the text file and returns as a list
        public List<Animal> GetAll()
        {
            List<Animal> animals = new List<Animal>();

            if (File.Exists(filepath))
            {
                using (StreamReader sr = new StreamReader(filepath))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split('|');

                        if (data.Length == 7)
                        {
                            Animal a = new Animal(
                                data[0].Trim(),
                                data[1].Trim(),
                                data[2].Trim(),
                                int.Parse(data[3]),
                                int.Parse(data[4]),
                                data[5].Trim(),
                                data[6].Trim()
                            );

                            animals.Add(a);
                        }
                    }
                }
            }

            return animals;
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