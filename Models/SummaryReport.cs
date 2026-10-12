using System.Collections.Generic;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Models
{
    
    /// Holds the calculated values for the summary report (Feature 5).
    
    public class SummaryReport
    {
      
      /// Total number of animal records.

        public int TotalAnimals { get; set; }

       ///Average age across all records (0 when there is no data).</summary>
        public double AverageAge { get; set; }

      ///Average recovery score across all records (0 when there is no data).</summary>
        public double AverageRecoveryScore { get; set; }

        /// Number of animals in each status, keyed by status name.</summary>
        public Dictionary<string, int> StatusCounts { get; private set; }

        ///False when animals.txt has no records.</summary>
        public bool HasData { get; set; }

       
        /// Creates an empty report with a status count for every known status.
        
        public SummaryReport()
        {
            StatusCounts = new Dictionary<string, int>();
        }
    }
}
