using System.Collections.Generic;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Models
{
    
   
    public class SummaryReport
    {
        public int TotalAnimals { get; set; }
        public bool HasData { get; set; }
        public double AverageAge { get; set; }
        public double AverageRecoveryScore { get; set; }
        public Dictionary<string, int> StatusCounts { get; private set; }

        public SummaryReport()
        {
            StatusCounts = new Dictionary<string, int>();
        }
    }
}
