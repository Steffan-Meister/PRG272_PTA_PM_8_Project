using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Datalayer;
using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Models;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer
{


    
    public class BusinessLogicLayer
    {
        public const int MinAge = 0;
        public const int MaxAge = 100;
        public const int MinRecoveryScore = 0;
        public const int MaxRecoveryScore = 100;


        public static readonly string[] Statuses =
        {
            "Critical",
            "Stable",
            "Recovering",
            "Improving",
            "Ready for Release"
        };

        private readonly DataHandler _dataHandler;




        public BusinessLogicLayer()
            : this(new DataHandler())
        {
        }

        /// Creates the business layer with the supplied data handler (used for testing).
        
        public BusinessLogicLayer(DataHandler dataHandler)
        {
            if (dataHandler == null)
                throw new ArgumentNullException("dataHandler");

            _dataHandler = dataHandler;
        }


        /// recovery score and the housing unit from the status and species.
        
        public static void Classify(int recoveryScore, string species, out string status, out string housingUnit)
        {
            if (recoveryScore < MinRecoveryScore || recoveryScore > MaxRecoveryScore)
                throw new ArgumentOutOfRangeException(
                    "recoveryScore",
                    string.Format("Recovery Score must be between {0} and {1}.", MinRecoveryScore, MaxRecoveryScore));

            if (recoveryScore <= 20)
                status = "Critical";
            else if (recoveryScore <= 40)
                status = "Stable";
            else if (recoveryScore <= 60)
                status = "Recovering";
            else if (recoveryScore <= 80)
                status = "Improving";
            else
                status = "Ready for Release";

            if (status == "Critical")
                housingUnit = "Intensive Care Unit";
            else if (IsBird(species))
                housingUnit = "Aviary";
            else if (IsReptile(species))
                housingUnit = "Reptile Habitat";
            else if (IsMammal(species))
                housingUnit = "Mammal Enclosure";
            else
                housingUnit = "General Enclosure";
        }

        
        /// Validates the Add Animal form (including ID uniqueness).
       
        public ValidationResult Validate(string animalId, string name, string species, string ageText, string recoveryScoreText)
        {
            return Validate(animalId, name, species, ageText, recoveryScoreText, true);
        }


        /// Validates an edit. Same rules as Add, but the ID is not re-checked for
        /// uniqueness because it cannot change.
        
        public ValidationResult ValidateForUpdate(string animalId, string name, string species, string ageText, string recoveryScoreText)
        {
            return Validate(animalId, name, species, ageText, recoveryScoreText, false);
        }


        /// Shared validation used by Add and Update.
        
        private ValidationResult Validate(string animalId, string name, string species, string ageText, string recoveryScoreText, bool checkUniqueness)
        {
            string id = (animalId ?? string.Empty).Trim();
            if (id.Length == 0)
                return ValidationResult.Invalid(AnimalField.AnimalID, "Animal ID is required.");
            if (checkUniqueness && _dataHandler.AnimalIdExists(id))
                return ValidationResult.Invalid(AnimalField.AnimalID, "Animal ID must be unique.");

            if (string.IsNullOrWhiteSpace(name))
                return ValidationResult.Invalid(AnimalField.Name, "Name is required.");

            if (string.IsNullOrWhiteSpace(species))
                return ValidationResult.Invalid(AnimalField.Species, "Species is required.");

            int age;
            if (!TryParseWholeNumber(ageText, out age) || age < MinAge || age > MaxAge)
                return ValidationResult.Invalid(
                    AnimalField.Age,
                    string.Format("Age must be a whole number between {0} and {1}.", MinAge, MaxAge));

            int recoveryScore;
            if (!TryParseWholeNumber(recoveryScoreText, out recoveryScore)
                || recoveryScore < MinRecoveryScore
                || recoveryScore > MaxRecoveryScore)
                return ValidationResult.Invalid(
                    AnimalField.RecoveryScore,
                    string.Format("Recovery Score must be a whole number between {0} and {1}.", MinRecoveryScore, MaxRecoveryScore));

            return ValidationResult.Valid();
        }


        /// Classifies a new animal and appends it to animals.txt.
        
        public Animal AddAnimal(string animalId, string name, string species, int age, int recoveryScore)
        {
            string status;
            string housingUnit;
            Classify(recoveryScore, species, out status, out housingUnit);

            Animal animal = new Animal(
                (animalId ?? string.Empty).Trim(),
                (name ?? string.Empty).Trim(),
                (species ?? string.Empty).Trim(),
                age,
                recoveryScore,
                status,
                housingUnit);

            _dataHandler.AppendAnimal(animal);
            return animal;
        }

        
        /// Reclassifies an edited animal and rewrites its line in animals.txt.
        
        public Animal UpdateAnimal(string originalAnimalId, string animalId, string name, string species, int age, int recoveryScore)
        {
            string status;
            string housingUnit;
            Classify(recoveryScore, species, out status, out housingUnit);

            Animal animal = new Animal(
                (animalId ?? string.Empty).Trim(),
                (name ?? string.Empty).Trim(),
                (species ?? string.Empty).Trim(),
                age,
                recoveryScore,
                status,
                housingUnit);

            _dataHandler.UpdateAnimal(originalAnimalId, animal);
            return animal;
        }

       
        /// Deletes the record with the given ID from animals.txt.
        
        public void DeleteAnimal(string animalId)
        {
            _dataHandler.DeleteAnimal(animalId);
        }

       
        /// Finds an animal by exact (case-insensitive) ID, or null when not found.
        
        public Animal FindById(string animalId)
        {
            if (string.IsNullOrWhiteSpace(animalId))
                return null;

            string target = animalId.Trim();
            foreach (Animal animal in _dataHandler.GetAllAnimals())
            {
                if (string.Equals(animal.AnimalID, target, StringComparison.OrdinalIgnoreCase))
                    return animal;
            }

            return null;
        }

        
        /// Returns every stored animal so the grid can be refreshed.
        
        public List<Animal> GetAllAnimals()
        {
            return _dataHandler.GetAllAnimals();
        }

        
        /// Calculates the summary values from the records currently in animals.txt.
        /// Handles the empty-data case without crashing.
        
        public SummaryReport GenerateSummary()
        {
            List<Animal> animals = _dataHandler.GetAllAnimals();

            SummaryReport report = new SummaryReport();
            report.TotalAnimals = animals.Count;
            report.HasData = animals.Count > 0;

            foreach (string status in Statuses)
                report.StatusCounts[status] = 0;

            if (animals.Count > 0)
            {
                long ageTotal = 0;
                long scoreTotal = 0;

                foreach (Animal animal in animals)
                {
                    ageTotal += animal.Age;
                    scoreTotal += animal.RecoveryScore;

                    if (report.StatusCounts.ContainsKey(animal.Status))
                        report.StatusCounts[animal.Status]++;
                }

                report.AverageAge = (double)ageTotal / animals.Count;
                report.AverageRecoveryScore = (double)scoreTotal / animals.Count;
            }

            return report;
        }

       
        /// Saves the summary report to summary.txt.
        
        public void SaveSummaryReport(SummaryReport report)
        {
            _dataHandler.WriteSummary(FormatSummaryText(report));
        }

        
        /// Builds the text layout for summary.txt (assumed layout, Section 5.2).
        
        public static string FormatSummaryText(SummaryReport report)
        {
            if (report == null)
                throw new ArgumentNullException("report");

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Baobab Ridge Wildlife Rehabilitation Records System");
            builder.AppendLine("Summary Report");
            builder.AppendLine("Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            builder.AppendLine();
            builder.AppendLine("Total animals: " + report.TotalAnimals.ToString(CultureInfo.InvariantCulture));

            if (report.HasData)
            {
                builder.AppendLine("Average age: " + report.AverageAge.ToString("F2", CultureInfo.InvariantCulture));
                builder.AppendLine("Average recovery score: " + report.AverageRecoveryScore.ToString("F2", CultureInfo.InvariantCulture));
            }
            else
            {
                builder.AppendLine("Average age: N/A");
                builder.AppendLine("Average recovery score: N/A");
            }

            builder.AppendLine();
            builder.AppendLine("Animals per status:");

            foreach (string status in Statuses)
            {
                int count = report.StatusCounts.ContainsKey(status) ? report.StatusCounts[status] : 0;
                builder.AppendLine(status + ": " + count.ToString(CultureInfo.InvariantCulture));
            }

            return builder.ToString();
        }

        
        /// Parses a whole number, rejecting blanks, decimals, signs and letters.
        
        private static bool TryParseWholeNumber(string text, out int value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(text))
                return false;

            int parsed;
            if (!int.TryParse(
                    text.Trim(),
                    NumberStyles.None,
                    CultureInfo.InvariantCulture,
                    out parsed))
                return false;

            value = parsed;
            return true;
        }

        /// Returns true when the species looks like a bird.</summary>
        private static bool IsBird(string species)
        {
            return ContainsAny(species, "bird", "avian", "owl", "eagle", "hawk", "falcon", "kite",
                "vulture", "parrot", "duck", "goose", "heron", "stork", "ibis", "crow", "raven",
                "sparrow", "weaver", "barbet", "hornbill", "guineafowl", "francolin", "bustard");
        }

        /// Returns true when the species looks like a reptile.</summary>
        private static bool IsReptile(string species)
        {
            return ContainsAny(species, "reptile", "snake", "lizard", "gecko", "chameleon",
                "tortoise", "turtle", "terrapin", "crocodile", "croc", "monitor", "python",
                "cobra", "adder", "skink", "agamid");
        }

        /// Returns true when the species looks like a mammal.</summary>
        private static bool IsMammal(string species)
        {
            return ContainsAny(species, "mammal", "lion", "leopard", "cheetah", "caracal", "serval",
                "elephant", "rhino", "buffalo", "antelope", "buck", "impala", "kudu", "wildebeest",
                "zebra", "giraffe", "hippo", "warthog", "baboon", "monkey", "mongoose", "genet",
                "jackal", "fox", "otter", "hyena", "wild dog", "wilddog", "bat", "squirrel",
                "duiker", "nyala", "sable", "eland", "blesbok", "springbok", "aardvark", "pangolin",
                "porcupine", "hare", "rabbit", "rat", "mouse", "shrew", "clawless otter");
        }


        /// Returns true when any of the given keywords appears in the value
        /// (case-insensitive).
        
        private static bool ContainsAny(string value, params string[] keywords)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            string lowered = value.ToLowerInvariant();
            foreach (string keyword in keywords)
            {
                if (lowered.Contains(keyword))
                    return true;
            }

            return false;
        }
    }
}
