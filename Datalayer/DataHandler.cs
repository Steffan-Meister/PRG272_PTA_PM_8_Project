using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Models;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Datalayer
{
    /// Handles all file I/O for animals.txt and summary.txt. Wraps IO errors in
    /// DataFileException so the UI can show friendly messages.
   
    public class DataHandler
    {
        private const string AnimalsFileName = "animals.txt";
        private const string SummaryFileName = "summary.txt";

        private readonly string _animalsPath;
        private readonly string _summaryPath;

        public DataHandler()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Environment.CurrentDirectory;
            _animalsPath = Path.Combine(baseDir, AnimalsFileName);
            _summaryPath = Path.Combine(baseDir, SummaryFileName);

            EnsureAnimalsFileExists();
        }

        private void EnsureAnimalsFileExists()
        {
            try
            {
                if (!File.Exists(_animalsPath))
                {
                    
                    string dir = Path.GetDirectoryName(_animalsPath);
                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    // Write header row
                    File.WriteAllText(_animalsPath, "AnimalID|Name|Species|Age|RecoveryScore|Status|HousingUnit" + Environment.NewLine, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                throw new DataFileException("Unable to create the animals data file.", ex);
            }
        }

        
        public void AppendAnimal(Animal animal)
        {
            if (animal == null)
                throw new ArgumentNullException("animal");

            EnsureAnimalsFileExists();

            string line = BuildLine(animal);

            try
            {
                // Append with exclusive lock to reduce risk of concurrent corruption.
                using (FileStream fs = new FileStream(_animalsPath, FileMode.Append, FileAccess.Write, FileShare.None))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(line);
                    sw.Flush();
                    fs.Flush(true);
                }
            }
            catch (Exception ex)
            {
                throw new DataFileException("Unable to append the animal to the data file.", ex);
            }
        }

        public bool AnimalIdExists(string animalId)
        {
            if (string.IsNullOrWhiteSpace(animalId))
                return false;

            try
            {
                var all = GetAllAnimals();
                return all.Any(a => string.Equals(a.AnimalID, animalId.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                throw new DataFileException("Unable to read the data file to check for an existing Animal ID.", ex);
            }
        }

        public List<Animal> GetAllAnimals()
        {
            try
            {
                EnsureAnimalsFileExists();

                var lines = File.ReadAllLines(_animalsPath, Encoding.UTF8);
                var result = new List<Animal>();

                if (lines == null || lines.Length == 0)
                    return result;

                
                int start = 0;
                if (lines.Length > 0 && lines[0].StartsWith("AnimalID"))
                    start = 1;

                for (int i = start; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split('|');
                    if (parts.Length < 7)
                        continue; 

                    int age;
                    int score;
                    if (!int.TryParse(parts[3], NumberStyles.None, CultureInfo.InvariantCulture, out age))
                        continue;
                    if (!int.TryParse(parts[4], NumberStyles.None, CultureInfo.InvariantCulture, out score))
                        continue;

                    var animal = new Animal
                    {
                        AnimalID = parts[0],
                        Name = parts[1],
                        Species = parts[2],
                        Age = age,
                        RecoveryScore = score,
                        Status = parts[5],
                        HousingUnit = parts[6]
                    };

                    result.Add(animal);
                }

                return result;
            }
            catch (DataFileException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataFileException("Unable to read the animals data file.", ex);
            }
        }

        public void UpdateAnimal(string originalAnimalId, Animal updated)
        {
            if (string.IsNullOrWhiteSpace(originalAnimalId))
                throw new ArgumentNullException("originalAnimalId");
            if (updated == null)
                throw new ArgumentNullException("updated");

            try
            {
                EnsureAnimalsFileExists();

                var lines = File.ReadAllLines(_animalsPath, Encoding.UTF8).ToList();
                if (lines.Count == 0)
                    throw new DataFileException("Data file is empty.");

                int startIndex = lines[0].StartsWith("AnimalID") ? 1 : 0;
                bool found = false;

                for (int i = startIndex; i < lines.Count; i++)
                {
                    var cols = lines[i].Split('|');
                    if (cols.Length < 1)
                        continue;
                    if (string.Equals(cols[0].Trim(), originalAnimalId.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        lines[i] = BuildLine(updated);
                        found = true;
                        break;
                    }
                }

                if (!found)
                    throw new DataFileException(string.Format(CultureInfo.InvariantCulture, "Animal ID '{0}' not found.", originalAnimalId));

                WriteLinesAtomically(lines);
            }
            catch (DataFileException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataFileException("Unable to update the animal record.", ex);
            }
        }

        public void DeleteAnimal(string animalId)
        {
            if (string.IsNullOrWhiteSpace(animalId))
                throw new ArgumentNullException("animalId");

            try
            {
                EnsureAnimalsFileExists();

                var lines = File.ReadAllLines(_animalsPath, Encoding.UTF8).ToList();
                if (lines.Count == 0)
                    return;

                int startIndex = lines[0].StartsWith("AnimalID") ? 1 : 0;
                bool removed = false;

                for (int i = startIndex; i < lines.Count; i++)
                {
                    var cols = lines[i].Split('|');
                    if (cols.Length < 1)
                        continue;
                    if (string.Equals(cols[0].Trim(), animalId.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        lines.RemoveAt(i);
                        removed = true;
                        break;
                    }
                }

                if (removed)
                    WriteLinesAtomically(lines);
            }
            catch (DataFileException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DataFileException("Unable to delete the animal record.", ex);
            }
        }

        private void WriteLinesAtomically(List<string> lines)
        {
            string temp = _animalsPath + ".tmp";
            try
            {
                File.WriteAllLines(temp, lines, Encoding.UTF8);

                // Use Replace when possible to preserve metadata; fallback to Move
                if (File.Exists(_animalsPath))
                {
                    File.Replace(temp, _animalsPath, null);
                }
                else
                {
                    File.Move(temp, _animalsPath);
                }

            }
            catch (PlatformNotSupportedException)
            {
              
                if (File.Exists(_animalsPath))
                    File.Delete(_animalsPath);
                File.Move(temp, _animalsPath);
            }
            catch (Exception ex)
            {
                
                try { if (File.Exists(temp)) File.Delete(temp); } catch { }
                throw new DataFileException("Unable to write the data file.", ex);
            }

        }

        private static string BuildLine(Animal a)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                (a.AnimalID ?? string.Empty).Trim(),
                (a.Name ?? string.Empty).Trim(),
                (a.Species ?? string.Empty).Trim(),
                a.Age.ToString(CultureInfo.InvariantCulture),
                a.RecoveryScore.ToString(CultureInfo.InvariantCulture),
                (a.Status ?? string.Empty).Trim(),
                (a.HousingUnit ?? string.Empty).Trim());
        }


        public void WriteSummary(string text)
        {
            try
            {
                string dir = Path.GetDirectoryName(_summaryPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                File.WriteAllText(_summaryPath, text ?? string.Empty, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new DataFileException("Unable to write the summary file.", ex);
            }
        }
    }
}
