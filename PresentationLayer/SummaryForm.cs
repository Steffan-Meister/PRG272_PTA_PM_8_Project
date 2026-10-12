using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Models;
using Bll = Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer.BusinessLogicLayer;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System
{
   
    /// Feature 5: displays the calculated summary values in read-only labels.

    public partial class SummaryForm : Form
    {
        
        /// Creates the form and fills it with the supplied report.
        
        public SummaryForm(SummaryReport report)
        {
            InitializeComponent();
            Populate(report);
        }

        
        /// Writes the report values into the labels and builds one row per status.
       
        private void Populate(SummaryReport report)
        {
            if (report == null)
                report = new SummaryReport();

            valTotal.Text = report.TotalAnimals.ToString(CultureInfo.InvariantCulture);

            if (report.HasData)
            {
                valAvgAge.Text = report.AverageAge.ToString("F2", CultureInfo.InvariantCulture);
                valAvgScore.Text = report.AverageRecoveryScore.ToString("F2", CultureInfo.InvariantCulture);
            }
            else
            {
                valAvgAge.Text = "N/A";
                valAvgScore.Text = "N/A";
            }

            lblEmptyNote.Visible = !report.HasData;

            // Build the per-status rows from the single list of statuses.
            int y = 4;
            foreach (string status in Bll.Statuses)
            {
                int count = report.StatusCounts.ContainsKey(status) ? report.StatusCounts[status] : 0;

                Label nameLabel = new Label();
                nameLabel.Text = status + ":";
                nameLabel.Location = new Point(4, y);
                nameLabel.AutoSize = true;

                Label valueLabel = new Label();
                valueLabel.Text = count.ToString(CultureInfo.InvariantCulture);
                valueLabel.Location = new Point(200, y);
                valueLabel.AutoSize = true;
                valueLabel.Font = new Font(valueLabel.Font, FontStyle.Bold);

                pnlStatusCounts.Controls.Add(nameLabel);
                pnlStatusCounts.Controls.Add(valueLabel);

                y += 26;
            }
        }

        /// Closes the summary form.
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
