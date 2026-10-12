using System;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Models
{
    /// <summary>
    /// Identifies which input field a validation message refers to so that the
    /// presentation layer can return the cursor to the offending control.
    /// </summary>
    public enum AnimalField
    {
        AnimalID,
        Name,
        Species,
        Age,
        RecoveryScore
    }

    /// <summary>
    /// Represents a single animal record stored in animals.txt.
    /// </summary>
    public class Animal
    {
        public string AnimalID { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public int RecoveryScore { get; set; }
        public string Status { get; set; }
        public string HousingUnit { get; set; }

        public Animal()
        {
        }

        /// <summary>
        /// Creates an animal with all of its values set.
        /// </summary>
        public Animal(string animalId, string name, string species, int age, int recoveryScore, string status, string housingUnit)
        {
            AnimalID = animalId;
            Name = name;
            Species = species;
            Age = age;
            RecoveryScore = recoveryScore;
            Status = status;
            HousingUnit = housingUnit;
        }
    }
}
