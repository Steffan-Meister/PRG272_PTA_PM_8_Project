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

        

        
        /// Loads the records and sets the form into Add mode on startup.

        private void Form1_Load(object sender, EventArgs e)
        {
            ClearInputs();

            try
            {
                RefreshGrid();
                ConfigureDataGridView();
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


        
        /// Feature 1: validates the form, and when valid classifies and appends
        /// a new animal, then confirms and refreshes.

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
                ConfigureDataGridView();
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

        /// Update

        
        /// Feature 3: saves the loaded record. Uses the same validation as Add
        
        
        
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
                ConfigureDataGridView();
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

        

        /// Builds the confirmation text that always shows the status and housing unit.
        
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

        
        private void ShowWarning(string message)
        {
            MessageBox.Show(this, message, "Please Check Your Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        
        /// Ensures the grid is read-only, selects whole rows, and sets column order
        /// and headers to the project's required layout. Safe to call after every refresh.
        
        private void ConfigureDataGridView()
        {
            try
            {
                if (dgvAnimals == null)
                    return;

                dgvAnimals.ReadOnly = true;
                dgvAnimals.AllowUserToAddRows = false;
                dgvAnimals.AllowUserToDeleteRows = false;
                dgvAnimals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAnimals.MultiSelect = false;

                if (dgvAnimals.DataSource == null)
                    return;

                // Desired order and header text
                var desired = new[]
                {
                    new { Name = "AnimalID", Header = "Animal ID" },
                    new { Name = "Name", Header = "Name" },
                    new { Name = "Species", Header = "Species" },
                    new { Name = "Age", Header = "Age" },
                    new { Name = "RecoveryScore", Header = "Recovery Score" },
                    new { Name = "Status", Header = "Status" },
                    new { Name = "HousingUnit", Header = "Housing Unit" }
                };

                int displayIndex = 0;
                foreach (var def in desired)
                {
                    if (dgvAnimals.Columns.Contains(def.Name))
                    {
                        var col = dgvAnimals.Columns[def.Name];
                        col.HeaderText = def.Header;
                        col.DisplayIndex = displayIndex++;
                    }
                }
            }
            catch
            {
                // Swallow UI configuration errors to avoid interrupting user actions
            }
        }

        /// Returns focus to the offending input field and selects its text where appropriate.
        private void FocusField(AnimalField field)
        {
            switch (field)
            {
                case AnimalField.AnimalID:
                    txtAnimalId.Focus();
                    txtAnimalId.SelectAll();
                    break;
                case AnimalField.Name:
                    txtName.Focus();
                    txtName.SelectAll();
                    break;
                case AnimalField.Species:
                    txtSpecies.Focus();
                    txtSpecies.SelectAll();
                    break;
                case AnimalField.Age:
                    txtAge.Focus();
                    txtAge.SelectAll();
                    break;
                case AnimalField.RecoveryScore:
                    txtRecoveryScore.Focus();
                    txtRecoveryScore.SelectAll();
                    break;
            }
        }
    }
}