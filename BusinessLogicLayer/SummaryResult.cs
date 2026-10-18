using System;
using System.Collections.Generic;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer
{
    internal class SummaryResult
    {
        public int TotalAnimals { get; set; }
        public decimal? AverageAge { get; set; }
        public decimal? AverageRecoveryScore { get; set; }
        public Dictionary<string, int> StatusCounts { get; set; }
    }
}

