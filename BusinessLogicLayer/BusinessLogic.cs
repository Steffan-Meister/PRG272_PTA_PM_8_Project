using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Datalayer;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer
{
    internal class BusinessLogic
    {
        DataHandler handler = new DataHandler();

        //Calculate animal status and housing unit based on recovery score
        public (string status, string housingUnit) ClassifyAnimal(int recoveryScore)
        {
            if (recoveryScore >= 0 && recoveryScore <= 19)
            {
                return ("Critical", "Intensive Care Unit");
            }
            else if (recoveryScore >= 20 && recoveryScore <= 39)
            {
                return ("Serious", "High-Dependency Ward");
            }
            else if (recoveryScore >= 40 && recoveryScore <= 59)
            {
                return ("Stable", "Recovery Ward");
            }
            else if (recoveryScore >= 60 && recoveryScore <= 79)
            {
                return ("Recovering", "Outdoor Enclosure");
            }
            else
            {
                return ("Release-Ready", "Pre-Release Camp");
            }
        }

        /*Create new animal object based on passed user input and 
          hand it off to the data layer for writing to disk*/
        public void AddAnimal(string animalID, string name, string species, int age, int recoveryScore, string status, string housingUnit)
        {
            Animal animal = new Animal(animalID, name, species, age, recoveryScore, status, housingUnit);
            handler.Add(animal);
        }

        /*Gets animal IDs from data layer and verifies that the ID of the animal
          that the user is attempting to add is unique*/
        public Boolean CheckIDUniqueness(string animalID)
        {
            List<string> animalIDs = handler.GetAnimalIDs();

            if (animalIDs.Count > 0)
            {
                foreach (string ID in animalIDs)
                {
                    if (ID == animalID)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Animal FindAnimal(string animalID)
        {
            return handler.FindByID(animalID);
        }

        public void UpdateAnimal(string animalID, string name, string species, int age, int recoveryScore)
        {
            var result = ClassifyAnimal(recoveryScore);

            Animal a = new Animal(animalID, name, species, age, recoveryScore, result.status, result.housingUnit);

            handler.Update(a);
        }

        public List<Animal> GetAllAnimals()
        {
            return handler.GetAll();
        }


        public SummaryResult GetSummary()
        {
            SummaryCalculator calculator = new SummaryCalculator();

            return calculator.Calculate(GetAllAnimals());

        }
        public string GenerateSummaryReport()
        {
            SummaryResult result = GetSummary();

            StringBuilder report = new StringBuilder();


            report.AppendLine("BAOBAB RIDGE WILDLIFE REHABILITATION CENTRE");
            report.AppendLine("ANIMAL SUMMARY REPORT");
            report.AppendLine("Generate:" + DateTime.Now.ToString("yyyy - MM - dd HH: mm"));
            report.AppendLine("----------------------------------------------------------------");
            report.AppendLine("Total animals:" + result.TotalAnimals);

            report.AppendLine("Average age (years):" + (result.AverageAge.HasValue ? result.AverageAge.Value.ToString("F2") : "N/A"));
            report.AppendLine("-------------------------------------");
            report.AppendLine("Animals per status");

            foreach (KeyValuePair< string, int> status in result.StatusCounts)
            {
                report.AppendLine(status.Key + ": " + status.Value);

            }
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "summary.txt");

            System.IO.File.WriteAllText(path, report.ToString());

            return report.ToString();
        }

        // Deletes an animal by passing its ID to the data handler and returns true if the animal was deleted, or false if it was not found.
        public bool DeleteAnimal(string animalID)
       {
            return handler.Delete(animalID);
       }

    }
}