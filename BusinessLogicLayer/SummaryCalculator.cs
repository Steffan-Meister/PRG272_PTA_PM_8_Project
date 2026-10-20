using System;
using System.Collections.Generic;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer
{
    internal class SummaryCalculator
    {
        // Calculates the summary figures using the list of animals.
        public SummaryResult Calculate(List<Animal> animals)
        {
            SummaryResult result = new SummaryResult();

            result.StatusCounts = new Dictionary<string, int>()
            {
                { "Critical", 0 },
                { "Serious", 0 },
                { "Stable", 0 },
                { "Recovering", 0 },
                { "Release-Ready", 0 }
            };
            // If there are no animals, averages cannot be calculated.
            if (animals.Count == 0)
            {

                result.TotalAnimals = 0;
                result.AverageAge = null;
                result.AverageRecoveryScore = null;

                return result;

            }
            // Store the total number of animals.
            result.TotalAnimals = animals.Count;
            decimal totalAge = 0;
            decimal totalScore = 0;

            // Add up the ages and recovery scores and also count how many animals belong to each status.
            foreach (Animal animal in animals)
            {

                totalAge += animal.Age;
                totalScore += animal.RecoveryScore;
                // Only count statuses that are in our list.
                if (result.StatusCounts.ContainsKey(animal.Status))
                {
                    result.StatusCounts[animal.Status]++;
                }
            }
            // Divide the totals by the number of animals to get the averages.
            result.AverageAge = totalAge / animals.Count;
            result.AverageRecoveryScore = totalScore / animals.Count;

            return result;
        }
    }
}