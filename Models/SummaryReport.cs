using System.Collections.Generic;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Models
{
    /// <summary>
    /// Holds the calculated values for the summary report (Feature 5).
    /// </summary>
    public class SummaryReport
    {
        /// <summary>Total number of animal records.</summary>
        public int TotalAnimals { get; set; }

        /// <summary>Average age across all records (0 when there is no data).</summary>
        public double AverageAge { get; set; }

        /// <summary>Average recovery score across all records (0 when there is no data).</summary>
        public double AverageRecoveryScore { get; set; }

        /// <summary>Number of animals in each status, keyed by status name.</summary>
        public Dictionary<string, int> StatusCounts { get; private set; }

        /// <summary>False when animals.txt has no records.</summary>
        public bool HasData { get; set; }

        /// <summary>
        /// Creates an empty report with a status count for every known status.
        /// </summary>
        public SummaryReport()
        {
            StatusCounts = new Dictionary<string, int>();
        }
    }
}
