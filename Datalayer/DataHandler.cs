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

        /*Tries to turn one raw file line into an Animal.
          Returns false when the line is malformed: wrong number of fields,
          non-numeric age or recovery score, or an ID that is not in the
          WR-0000 format. Malformed lines are skipped, never crash the program.*/
        private bool TryParseLine(string line, out Animal animal)
        {
            animal = null;

            if (string.IsNullOrWhiteSpace(line))
            {
                return false;
            }

            string[] data = line.Split('|');

            if (data.Length != 7)
            {
                return false;
            }

            int age;
            int recoveryScore;

            if (!int.TryParse(data[3].Trim(), out age) ||
                !int.TryParse(data[4].Trim(), out recoveryScore))
            {
                return false;
            }

            string animalID = data[0].Trim();

            if (!IsValidAnimalID(animalID))
            {
                return false;
            }

            animal = new Animal(
                animalID,
                data[1].Trim(),
                data[2].Trim(),
                age,
                recoveryScore,
                data[5].Trim(),
                data[6].Trim()
            );

            return true;
        }

        //An ID is valid when it looks like WR-0000 (same rule the form uses).
        private bool IsValidAnimalID(string animalID)
        {
            if (animalID.Length != 7 ||
                animalID.Substring(0, 3) != "WR-")
            {
                return false;
            }

            int numericPart;

            return int.TryParse(animalID.Substring(3, 4), out numericPart);
        }

        public Animal FindByID(string animalID)
        {
            if (!File.Exists(filepath))
            {
                return null;
            }

            string[] lines = File.ReadAllLines(filepath);

            foreach (string line in lines)
            {
                Animal a;

                //Malformed lines are skipped while searching.
                if (TryParseLine(line, out a) &&
                    a.AnimalID.Equals(
                        animalID.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    return a;
                }
            }

            return null;

        }

        public void Update(Animal a)
        {
            string[] lines = File.ReadAllLines(filepath);

            bool found = false;

            for (int i = 0; i < lines.Length; i++)
            {
                Animal existing;

                //Malformed lines never match, so they are left untouched.
                if (TryParseLine(lines[i], out existing) &&
                    existing.AnimalID.Equals(
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

        //Get animal IDs to check for uniqueness (malformed lines are skipped)
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
                        Animal a;

                        if (TryParseLine(line, out a))
                        {
                            animalIDs.Add(a.AnimalID);
                        }
                    }
                }
            }
            return animalIDs;
        }

        public List<Animal> GetAll()
        {
            int skippedLines;
            return GetAll(out skippedLines);
        }

        /*Loads every well-formed record and reports how many lines had to be
          skipped. Skipped lines are not returned, so they never reach the grid,
          and they are dropped whenever the file is next rewritten.*/
        public List<Animal> GetAll(out int skippedLines)
        {
            List<Animal> animals = new List<Animal>();

            skippedLines = 0;

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
                Animal existing;

                //Only well-formed records can be deleted.
                if (TryParseLine(lines[i], out existing) &&
                    existing.AnimalID.Equals(animalID.Trim(), StringComparison.OrdinalIgnoreCase))
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