using System;
using System.Globalization;
using System.Windows.Forms;
using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer;
using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Datalayer;
using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.Models;
using Bll = Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer.BusinessLogicLayer;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System
{
    public partial class Form1 : Form
    {
        private readonly Bll _businessLogic = new Bll();

        // True while an existing record is loaded for editing (Feature 3).
        private bool _isEditMode;

        // The ID of the record being edited; used to rewrite only that record.
        private string _originalAnimalId;

        /// <summary>
        /// Creates the form and wires up the designer controls.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the records and sets the form into Add mode on startup.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            ClearInputs();

            try
            {
                RefreshGrid();
            }
            catch (DataFileException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ShowError("An unexpected error occurred while loading the animal records: " + ex.Message);
            }
        }

        // ---------------------------------------------------------------- Add

        /// <summary>
        /// Feature 1: validates the form, and when valid classifies and appends
        /// a new animal, then confirms and refreshes.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationResult result = _businessLogic.Validate(
                    txtAnimalId.Text,
                    txtName.Text,
                    txtSpecies.Text,
                    txtAge.Text,
                    txtRecoveryScore.Text);

                if (!result.IsValid)
                {
                    ShowWarning(result.Message);
                    FocusField(result.Field);
                    return;
                }

                int age = int.Parse(txtAge.Text.Trim(), CultureInfo.InvariantCulture);
                int recoveryScore = int.Parse(txtRecoveryScore.Text.Trim(), CultureInfo.InvariantCulture);

                Animal animal = _businessLogic.AddAnimal(
                    txtAnimalId.Text,
                    txtName.Text,
                    txtSpecies.Text,
                    age,
                    recoveryScore);

                MessageBox.Show(
                    this,
                    BuildClassificationMessage("Animal added successfully.", animal),
                    "Animal Added",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearInputs();
                RefreshGrid();
            }
            catch (DataFileException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ShowError("An unexpected error occurred while adding the animal: " + ex.Message);
            }
        }

        // ------------------------------------------------------- View / select

        /// <summary>
        /// Feature 3: clicking a grid row loads that record into the input boxes
        /// for editing.
        /// </summary>
        private void dgvAnimals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Animal animal = dgvAnimals.Rows[e.RowIndex].DataBoundItem as Animal;
            if (animal != null)
                LoadAnimal(animal);
        }

        // --------------------------------------------------------------- Search

        /// <summary>
        /// Feature 3: finds a record by exact (case-insensitive) Animal ID and
        /// loads it for editing, or tells the user it was not found.
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchId = (txtSearchAnimalId.Text ?? string.Empty).Trim();

            if (searchId.Length == 0)
            {
                ShowWarning("Enter an Animal ID to search for.");
                txtSearchAnimalId.Focus();
                return;
            }

            try
            {
                Animal animal = _businessLogic.FindById(searchId);

                if (animal == null)
                {
                    MessageBox.Show(
                        this,
                        string.Format("No animal was found with Animal ID '{0}'.", searchId),
                        "Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                LoadAnimal(animal);
            }
            catch (DataFileException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ShowError("An unexpected error occurred while searching: " + ex.Message);
            }
        }

        // --------------------------------------------------------------- Update

        /// <summary>
        /// Feature 3: saves the loaded record. Uses the same validation as Add
        /// (without the uniqueness check) and always recalculates the status and
        /// housing unit.
        /// </summary>
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
            {
                ShowWarning("Load a record before saving changes.");
                return;
            }

            try
            {
                ValidationResult result = _businessLogic.ValidateForUpdate(
                    txtAnimalId.Text,
                    txtName.Text,
                    txtSpecies.Text,
                    txtAge.Text,
                    txtRecoveryScore.Text);

                if (!result.IsValid)
                {
                    ShowWarning(result.Message);
                    FocusField(result.Field);
                    return;
                }

                int age = int.Parse(txtAge.Text.Trim(), CultureInfo.InvariantCulture);
                int recoveryScore = int.Parse(txtRecoveryScore.Text.Trim(), CultureInfo.InvariantCulture);

                Animal animal = _businessLogic.UpdateAnimal(
                    _originalAnimalId,
                    txtAnimalId.Text,
                    txtName.Text,
                    txtSpecies.Text,
                    age,
                    recoveryScore);

                MessageBox.Show(
                    this,
                    BuildClassificationMessage("Changes saved successfully.", animal),
                    "Changes Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ClearInputs();
                RefreshGrid();
            }
            catch (DataFileException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ShowError("An unexpected error occurred while saving changes: " + ex.Message);
            }
        }

        // --------------------------------------------------------------- Delete

        /// <summary>
        /// Feature 4: deletes the selected record after a Yes/No confirmation.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Animal selected = GetSelectedAnimal();

            if (selected == null)
            {
                ShowWarning("Please select an animal in the grid to delete.");
                return;
            }

            DialogResult confirmation = MessageBox.Show(
                this,
                string.Format("Delete {0} ({1})? This cannot be undone.", selected.AnimalID, selected.Name),
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmation != DialogResult.Yes)
                return;

            try
            {
                _businessLogic.DeleteAnimal(selected.AnimalID);

                // If the deleted animal was loaded in the form, return to Add mode.
                if (_isEditMode && string.Equals(_originalAnimalId, selected.AnimalID, StringComparison.OrdinalIgnoreCase))
                    ClearInputs();

                RefreshGrid();
            }
            catch (DataFileException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ShowError("An unexpected error occurred while deleting the animal: " + ex.Message);
            }
        }

        // --------------------------------------------------------------- Clear

        /// <summary>
        /// Returns the form to Add mode with empty boxes (Feature 3).
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        // -------------------------------------------------------- Summary report

        /// <summary>
        /// Feature 5: calculates the summary, saves summary.txt, and shows the
        /// results on the summary form.
        /// </summary>
        private void btnGenerateSummary_Click(object sender, EventArgs e)
        {
            try
            {
                SummaryReport report = _businessLogic.GenerateSummary();

                try
                {
                    _businessLogic.SaveSummaryReport(report);
                }
                catch (DataFileException ex)
                {
                    ShowError(ex.Message);
                }

                using (SummaryForm summaryForm = new SummaryForm(report))
                {
                    summaryForm.ShowDialog(this);
                }
            }
            catch (DataFileException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ShowError("An unexpected error occurred while generating the summary: " + ex.Message);
            }
        }

        // --------------------------------------------------------- Grid helpers

        /// <summary>
        /// Reloads the grid from animals.txt. Setting the data source fresh each
        /// time avoids duplicate rows.
        /// </summary>
        private void RefreshGrid()
        {
            dgvAnimals.DataSource = null;
            dgvAnimals.DataSource = _businessLogic.GetAllAnimals();
            dgvAnimals.ClearSelection();
        }

        /// <summary>
        /// Returns the animal in the selected grid row, or null when none is selected.
        /// </summary>
        private Animal GetSelectedAnimal()
        {
            if (dgvAnimals.SelectedRows.Count == 0)
                return null;

            return dgvAnimals.SelectedRows[0].DataBoundItem as Animal;
        }

        // ------------------------------------------------------- Form helpers

        /// <summary>
        /// Loads an animal into the input boxes and switches the form to edit mode.
        /// </summary>
        private void LoadAnimal(Animal animal)
        {
            txtAnimalId.Text = animal.AnimalID;
            txtName.Text = animal.Name;
            txtSpecies.Text = animal.Species;
            txtAge.Text = animal.Age.ToString(CultureInfo.InvariantCulture);
            txtRecoveryScore.Text = animal.RecoveryScore.ToString(CultureInfo.InvariantCulture);

            _isEditMode = true;
            _originalAnimalId = animal.AnimalID;
            UpdateButtonStates();

            txtName.Focus();
        }

        /// <summary>
        /// Clears all boxes and returns the form to Add mode.
        /// </summary>
        private void ClearInputs()
        {
            txtAnimalId.Clear();
            txtName.Clear();
            txtSpecies.Clear();
            txtAge.Clear();
            txtRecoveryScore.Clear();
            txtSearchAnimalId.Clear();

            _isEditMode = false;
            _originalAnimalId = null;
            UpdateButtonStates();

            txtAnimalId.Focus();
        }

        /// <summary>
        /// Applies the Add/edit mode rules to the buttons and the ID box.
        /// </summary>
        private void UpdateButtonStates()
        {
            txtAnimalId.ReadOnly = _isEditMode;
            btnAdd.Enabled = !_isEditMode;
            btnSaveChanges.Enabled = _isEditMode;
        }

        /// <summary>
        /// Puts the cursor back in the field named by a validation result.
        /// </summary>
        private void FocusField(AnimalField field)
        {
            switch (field)
            {
                case AnimalField.AnimalID:
                    txtAnimalId.Focus();
                    break;
                case AnimalField.Name:
                    txtName.Focus();
                    break;
                case AnimalField.Species:
                    txtSpecies.Focus();
                    break;
                case AnimalField.Age:
                    txtAge.Focus();
                    break;
                case AnimalField.RecoveryScore:
                    txtRecoveryScore.Focus();
                    break;
            }
        }

        /// <summary>
        /// Builds the confirmation text that always shows the status and housing unit.
        /// </summary>
        private static string BuildClassificationMessage(string heading, Animal animal)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0}{1}{1}Status: {2}{1}Housing Unit: {3}",
                heading,
                Environment.NewLine,
                animal.Status,
                animal.HousingUnit);
        }

        /// <summary>Shows a friendly warning message.</summary>
        private void ShowWarning(string message)
        {
            MessageBox.Show(this, message, "Please Check Your Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>Shows a friendly error message (never a stack trace).</summary>
        private void ShowError(string message)
        {
            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
