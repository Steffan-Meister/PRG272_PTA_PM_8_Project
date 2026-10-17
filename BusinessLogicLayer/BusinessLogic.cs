using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Datalayer;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer
{
    internal class BusinessLogic
    {
        DataHandler handler = new DataHandler();

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
            else if (recoveryScore >= 40 && recoveryScore <= 59)
            {
                return ("Recovering", "Outdoor Enclosure");
            }
            else
            {
                return ("Release-Ready", "Pre-Release Camp");
            }
        }
        public void AddAnimal(string animalID, string name, string species, int age, int recoveryScore, string status, string housingUnit)
        {
            Animal animal = new Animal(animalID, name, species, age, recoveryScore, status, housingUnit);
            handler.Add(animal);
        }

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
    }
}