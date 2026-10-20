using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System.PresentationLayer
{
    public partial class frmSummary : Form
    {
        public frmSummary()
        {
            InitializeComponent();
        }

        //Get summary results and display to the user
        private void frmSummary_Load(object sender, EventArgs e)
        {
            BusinessLogic businessLogic = new BusinessLogic();
            SummaryCalculator calculator = new SummaryCalculator();
            SummaryResult result = calculator.Calculate(businessLogic.GetAllAnimals().Item1);

            lblTotalAnimals.Text = result.TotalAnimals.ToString();
            lblAvgAge.Text = result.AverageAge.ToString();
            lblAvgRecoveryScore.Text = result.AverageRecoveryScore.ToString();
            lblCriticalCount.Text = result.StatusCounts["Critical"].ToString();
            lblSeriousCount.Text = result.StatusCounts["Serious"].ToString();
            lblStableCount.Text = result.StatusCounts["Stable"].ToString();
            lblRecoveringCount.Text = result.StatusCounts["Recovering"].ToString();
            lblReleaseReadyCount.Text = result.StatusCounts["Release-Ready"].ToString();
        }
    }
}
