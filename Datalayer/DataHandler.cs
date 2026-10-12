using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Models;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Datalayer
{
   
    /// Reads and writes animal records in animals.txt, and writes summary.txt.
    /// animals.txt is pipe delimited with a header row and lives in the
    /// application folder:
    /// AnimalID|Name|Species|Age|RecoveryScore|Status|HousingUnit
    ///
    /// Every file operation is wrapped so that missing files/folders, files in
    /// use (IOException) and permission problems (UnauthorizedAccessException)
    /// surface as a friendly <see cref="DataFileException"/> the UI can show.
    
    public class DataHandler
    {
        private const string Delimiter = "|";
        private const string Header = "AnimalID|Name|Species|Age|RecoveryScore|Status|HousingUnit";
        private const string AnimalsFileName = "animals.txt";
        private const string SummaryFileName = "summary.txt";

        private readonly string _filePath;
        private readonly string _summaryPath;

        
        /// Creates a handler that uses animals.txt in the application folder.
        
        public DataHandler()
            : this(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AnimalsFileName))
        {
        }

        /// Creates a handler that uses the given animals.txt path. The summary
        /// file is written next to it.
        
        public DataHandler(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("A file path is required.", "filePath");

            _filePath = filePath;

            string folder = Path.GetDirectoryName(filePath);
            _summaryPath = string.IsNullOrEmpty(folder)
                ? SummaryFileName
                : Path.Combine(folder, SummaryFileName);
        }

        ///Full path of the animals.txt file in use.</summary>
        public string FilePath
        {
            get { return _filePath; }
        }


        /// Creates animals.txt (with its header row) if it does not exist yet.

        public void EnsureFileExists()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, Header + Environment.NewLine);
            }
        }

        
        /// Returns every valid animal currently stored in the file, in file order.

        public List<Animal> GetAllAnimals()
        {
            return Execute(delegate
            {
                List<Animal> animals = new List<Animal>();
                EnsureFileExists();

                foreach (string line in File.ReadAllLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(new[] { Delimiter }, StringSplitOptions.None);
                    if (parts.Length < 7)
                        continue;

                    // Skip the header row (it appears once, but tolerate it anywhere).
                    if (string.Equals(parts[0].Trim(), "AnimalID", StringComparison.OrdinalIgnoreCase))
                        continue;

                    int age;
                    int recoveryScore;
                    int.TryParse(parts[3].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out age);
                    int.TryParse(parts[4].Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out recoveryScore);

                    animals.Add(new Animal(
                        parts[0].Trim(),
                        parts[1].Trim(),
                        parts[2].Trim(),
                        age,
                        recoveryScore,
                        parts[5].Trim(),
                        parts[6].Trim()));
                }

                return animals;
            }, "loading the animal records");
        }


        /// Returns true when an animal with the given ID already exists.
        
        public bool AnimalIdExists(string animalId)
        {
            if (string.IsNullOrWhiteSpace(animalId))
                return false;

            string target = animalId.Trim();
            foreach (Animal animal in GetAllAnimals())
            {
                if (string.Equals(animal.AnimalID, target, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        
        /// Appends a single new line to animals.txt, leaving existing lines untouched.
        
        public void AppendAnimal(Animal animal)
        {
            if (animal == null)
                throw new ArgumentNullException("animal");

            Execute(delegate
            {
                EnsureFileExists();
                File.AppendAllText(_filePath, FormatLine(animal) + Environment.NewLine);
            }, "saving the animal record");
        }

        
        /// Rewrites animals.txt, replacing only the record with
        /// <paramref name="originalAnimalId"/>. All other lines are kept exactly as-is.
        
        public void UpdateAnimal(string originalAnimalId, Animal updatedAnimal)
        {
            if (updatedAnimal == null)
                throw new ArgumentNullException("updatedAnimal");

            Execute(delegate
            {
                EnsureFileExists();

                List<string> output = new List<string>();
                bool replaced = false;

                foreach (string line in File.ReadAllLines(_filePath))
                {
                    if (!replaced && IsRecordFor(line, originalAnimalId))
                    {
                        output.Add(FormatLine(updatedAnimal));
                        replaced = true;
                    }
                    else
                    {
                        output.Add(line);
                    }
                }

                if (!replaced)
                    throw new DataFileException(
                        string.Format("Animal ID '{0}' could not be found, so it was not updated.", originalAnimalId));

                File.WriteAllLines(_filePath, output);
            }, "updating the animal record");
        }

        /// Rewrites animals.txt, removing only the record with the given ID.
        /// All other lines are kept exactly as-is.
        
        public void DeleteAnimal(string animalId)
        {
            Execute(delegate
            {
                EnsureFileExists();

                List<string> output = new List<string>();
                bool removed = false;

                foreach (string line in File.ReadAllLines(_filePath))
                {
                    if (!removed && IsRecordFor(line, animalId))
                    {
                        removed = true;
                        continue;
                    }

                    output.Add(line);
                }

                if (!removed)
                    throw new DataFileException(
                        string.Format("Animal ID '{0}' could not be found, so nothing was deleted.", animalId));

                File.WriteAllLines(_filePath, output);
            }, "deleting the animal record");
        }

        /// <summary>
        /// Writes the summary report text to summary.txt in the application folder.
        /// </summary>
        public void WriteSummary(string content)
        {
            Execute(delegate
            {
                File.WriteAllText(_summaryPath, content ?? string.Empty);
            }, "writing the summary report");
        }

        
        /// Returns true when the given raw file line belongs to the given Animal ID.
        
        private static bool IsRecordFor(string line, string animalId)
        {
            if (string.IsNullOrWhiteSpace(line) || string.IsNullOrWhiteSpace(animalId))
                return false;

            string[] parts = line.Split(new[] { Delimiter }, StringSplitOptions.None);
            if (parts.Length < 7)
                return false;

            string first = parts[0].Trim();
            if (string.Equals(first, "AnimalID", StringComparison.OrdinalIgnoreCase))
                return false;

            return string.Equals(first, animalId.Trim(), StringComparison.OrdinalIgnoreCase);
        }


        /// Formats an animal as one pipe-delimited line.

        private static string FormatLine(Animal animal)
        {
            return string.Join(Delimiter, new[]
            {
                Sanitize(animal.AnimalID),
                Sanitize(animal.Name),
                Sanitize(animal.Species),
                animal.Age.ToString(CultureInfo.InvariantCulture),
                animal.RecoveryScore.ToString(CultureInfo.InvariantCulture),
                Sanitize(animal.Status),
                Sanitize(animal.HousingUnit)
            });
        }

        
        /// Removes characters that would break the pipe-delimited format.
        
        private static string Sanitize(string value)
        {
            if (value == null)
                return string.Empty;

            return value
                .Replace(Delimiter, "/")
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Trim();
        }

        /// Runs a file operation and converts any failure into a friendly
        /// <see cref="DataFileException"/>.
      
        private void Execute(Action operation, string description)
        {
            try
            {
                operation();
            }
            catch (DataFileException)
            {
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new DataFileException(
                    "Permission was denied while " + description + ". The file may be read-only or in use by another user. Close it or check its permissions and try again.", ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DataFileException(
                    "The folder for the data file was not found. Ensure the application folder exists and try again.", ex);
            }
            catch (FileNotFoundException ex)
            {
                throw new DataFileException(
                    "The data file could not be found: " + _filePath, ex);
            }
            catch (IOException ex)
            {
                throw new DataFileException(
                    "The data file is in use by another program. Close it and try again.", ex);
            }
            catch (Exception ex)
            {
                throw new DataFileException(
                    "An unexpected error occurred while " + description + ": " + ex.Message, ex);
            }
        }

        /// Runs a file operation that returns a value and converts any failure
        /// into a friendly <see cref="DataFileException"/>.
        
        private T Execute<T>(Func<T> operation, string description)
        {
            try
            {
                return operation();
            }
            catch (DataFileException)
            {
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new DataFileException(
                    "Permission was denied while " + description + ". The file may be read-only or in use by another user. Close it or check its permissions and try again.", ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DataFileException(
                    "The folder for the data file was not found. Ensure the application folder exists and try again.", ex);
            }
            catch (FileNotFoundException ex)
            {
                throw new DataFileException(
                    "The data file could not be found: " + _filePath, ex);
            }
            catch (IOException ex)
            {
                throw new DataFileException(
                    "The data file is in use by another program. Close it and try again.", ex);
            }
            catch (Exception ex)
            {
                throw new DataFileException(
                    "An unexpected error occurred while " + description + ": " + ex.Message, ex);
            }
        }
    }
}
