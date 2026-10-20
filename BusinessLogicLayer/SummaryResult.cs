using System;
using System.Collections.Generic;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer
{
    internal class SummaryResult
    {
        // Stores the total number of animals in the system.
        public int TotalAnimals { get; set; }
        // Stores the average age of the animals.
        public decimal? AverageAge { get; set; }
        // Stores the average recovery score of the animals.
        public decimal? AverageRecoveryScore { get; set; }
        // Stores the number of animals recorded under each status.
        public Dictionary<string, int> StatusCounts { get; set; }
    }
}
