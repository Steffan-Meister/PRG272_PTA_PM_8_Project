using System;
using System.Collections.Generic;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer
{
    internal class SummaryCalculator
    {
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
            if (animals.Count == 0)
            {

                result.TotalAnimals = 0;
                result.AverageAge = null;
                result.AverageRecoveryScore = null;

                return result;

            }
            result.TotalAnimals = animals.Count;
            decimal totalAge = 0;
            decimal totalScore = 0;

            foreach (Animal animal in animals)
            {

                totalAge += animal.Age;
                totalScore += animal.RecoveryScore;

                if (result.StatusCounts.ContainsKey(animal.Status))
                {
                    result.StatusCounts[animal.Status]++;
                }
            }
            result.AverageAge = totalAge / animals.Count;
            result.AverageRecoveryScore = totalScore / animals.Count;

            return result;
        }
    }
}