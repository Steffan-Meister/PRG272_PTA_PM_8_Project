using Baobab_Ridge_Wildlife_Rehabilitation_Records_System.BusinessLogicLayer;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Lifetime;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System
{
    public partial class frmMain : Form

    {
        public frmMain()
        {
            InitializeComponent();

            btnSaveChanges.Enabled = false;

            RefreshAnimalGrid();

            
        }

        
        BusinessLogic bl = new BusinessLogic();

        private void InitializeComponent()
        {
            this.label5 = new System.Windows.Forms.Label();
            this.txtAnimalID = new System.Windows.Forms.TextBox();
            this.txtSpecies = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRecoveryScore = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvAnimals = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnSummary = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimals)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Recovery Score";
            // 
            // txtAnimalID
            // 
            this.txtAnimalID.Location = new System.Drawing.Point(123, 14);
            this.txtAnimalID.Name = "txtAnimalID";
            this.txtAnimalID.Size = new System.Drawing.Size(150, 22);
            this.txtAnimalID.TabIndex = 5;
            // 
            // txtSpecies
            // 
            this.txtSpecies.Location = new System.Drawing.Point(126, 123);
            this.txtSpecies.Name = "txtSpecies";
            this.txtSpecies.Size = new System.Drawing.Size(150, 22);
            this.txtSpecies.TabIndex = 7;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(126, 183);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(150, 22);
            this.txtAge.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Animal ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Species";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Age";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtRecoveryScore
            // 
            this.txtRecoveryScore.Location = new System.Drawing.Point(126, 232);
            this.txtRecoveryScore.Name = "txtRecoveryScore";
            this.txtRecoveryScore.Size = new System.Drawing.Size(150, 22);
            this.txtRecoveryScore.TabIndex = 9;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(126, 65);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 22);
            this.txtName.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.txtRecoveryScore);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtAge);
            this.panel1.Controls.Add(this.txtSpecies);
            this.panel1.Controls.Add(this.txtAnimalID);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(11, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(286, 280);
            this.panel1.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(55, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(666, 46);
            this.label6.TabIndex = 6;
            this.label6.Text = "Baobab Ridge Wildlife Rehabilitation";
            // 
            // dgvAnimals
            // 
            this.dgvAnimals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnimals.Location = new System.Drawing.Point(303, 119);
            this.dgvAnimals.Name = "dgvAnimals";
            this.dgvAnimals.RowHeadersWidth = 51;
            this.dgvAnimals.RowTemplate.Height = 24;
            this.dgvAnimals.Size = new System.Drawing.Size(440, 236);
            this.dgvAnimals.TabIndex = 7;
            this.dgvAnimals.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnimals_CellClick);
            this.dgvAnimals.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnimals_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.btnSaveChanges);
            this.panel2.Controls.Add(this.btnSummary);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Location = new System.Drawing.Point(11, 362);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(732, 76);
            this.panel2.TabIndex = 8;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(606, 40);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Location = new System.Drawing.Point(606, 14);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(104, 23);
            this.btnSaveChanges.TabIndex = 5;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnSummary
            // 
            this.btnSummary.Location = new System.Drawing.Point(333, 14);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(246, 50);
            this.btnSummary.TabIndex = 4;
            this.btnSummary.Text = "Display Summary";
            this.btnSummary.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(235, 14);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 50);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(123, 14);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 50);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(17, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 50);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.btnSearch);
            this.panel3.Controls.Add(this.txtSearch);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Location = new System.Drawing.Point(303, 75);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(440, 36);
            this.panel3.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(331, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(152, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(166, 22);
            this.txtSearch.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Search by Animal ID";
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(755, 446);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgvAnimals);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.Text = "Main";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimals)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string animalID = txtAnimalID.Text.Trim().ToUpper();
            string name = txtName.Text.Trim();
            string species = txtSpecies.Text.Trim();
            string age = txtAge.Text.Trim();
            string recoveryScore = txtRecoveryScore.Text.Trim();

            if (ValidateAnimal())
            {
                if (bl.CheckIDUniqueness(animalID))
                {
                    (string status, string housingUnit) = bl.ClassifyAnimal(int.Parse(txtRecoveryScore.Text));
                    bl.AddAnimal(animalID, name, species, int.Parse(age), int.Parse(recoveryScore), status, housingUnit);
                    MessageBox.Show("Animal added successfully.\n" +
                                    $"Status: {status}\n" +
                                    $"Housing Unit: {housingUnit}");
                    ClearAnimalInputs();
                }
                else
                {
                    MessageBox.Show("Animal ID must be unique. Please try again.");
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
      //Deletes the selected animal after asking the user to confirm.
        {
            // Check that the user has selected an animal from the table.
            if (dgvAnimals.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an animal you want to delete.");
                return;
            }
            DataGridViewRow row = dgvAnimals.SelectedRows[0];

            string animalID = row.Cells["AnimalID"].Value.ToString();
            string animalName = row.Cells["Name"].Value.ToString();

            DialogResult answer = MessageBox.Show("" + animalName + "(ID:" + animalID + ")?\n\n" + "This cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (answer == DialogResult.No)
            {
                return;
            }

   
            
            if (bl.DeleteAnimal(animalID))
            {
                // Reload the table so the deleted animal no longer appears.
                RefreshAnimalGrid();

                // Clear the form only if it was displaying the deleted animal.
                if (txtAnimalID.Text.Trim().Equals(animalID, StringComparison.OrdinalIgnoreCase))
                {
                    ClearAnimalForm();
                }

                MessageBox.Show("Animal deleted successfully.");

            }
            else
            {
                MessageBox.Show("The animal could not be found");
                RefreshAnimalGrid();
            }
            
        }

        private Boolean ValidateAnimal()
        {
            string animalID = txtAnimalID.Text.Trim().ToUpper();
            string name = txtName.Text.Trim();
            string species = txtSpecies.Text.Trim();
            string age = txtAge.Text.Trim();
            string recoveryScore = txtRecoveryScore.Text.Trim();

            //check that animalID numeric part, age and recovery score are of type integer
            int result;

            if (animalID.Length != 7 ||
                animalID.Substring(0, 3) != "WR-" || //Check character part of animal ID
                !int.TryParse(animalID.Substring(3, 4), out result))  //Validate animalID numeric part

            {
                MessageBox.Show("Animal ID must be in format WR-0000.");
                txtAnimalID.Focus();
                return false;
            }
            else if (name.Length < 1 ||
                    name.Length > 30 ||
                    name.Contains("|"))
            {
                MessageBox.Show("Animal name must be between 1 " +
                    "and 30 characters and my not contain the pipe (|) character.");
                txtName.Focus();
                return false;
            }
            else if (species.Length < 1 ||
                    species.Length > 30 ||
                    species.Contains("|"))
            {
                MessageBox.Show("Animal species must be between 1 " +
                    "and 30 characters and my not contain the pipe (|) character.");
                txtSpecies.Focus();
                return false;
            }
            else if (!int.TryParse(age, out result) ||
                    result < 0 ||
                    result > 100)
            {
                MessageBox.Show("Enter animal age 0-100.");
                txtAge.Focus();
                return false;
            }
            else if (!int.TryParse(recoveryScore, out result) ||
                    result < 0 ||
                    result > 100)
            {
                MessageBox.Show("Enter recovery score 0-100.");
                txtRecoveryScore.Focus();
                return false;
            }

            return true;
        }

        private string editingAnimalID = null;

        private void LoadAnimalEditing(string animalID)
        {
             Animal animal = bl.FindAnimal(animalID);
             if (animal == null)
             {
                 MessageBox.Show("Animal ID not found.");
                 return;
             }

             editingAnimalID = animal.AnimalID;

             txtAnimalID.Text = animal.AnimalID;
             txtName.Text = animal.Name;
             txtSpecies.Text = animal.Species;
             txtAge.Text = animal.Age.ToString();
             txtRecoveryScore.Text = animal.RecoveryScore.ToString();

             txtAnimalID.ReadOnly = true;
             btnAdd.Enabled = false;
             btnSaveChanges.Enabled = true;
            

            
        }
        private void ClearAnimalInputs()
        {
            txtAnimalID.Clear();
            txtName.Clear();
            txtSpecies.Clear();
            txtAge.Clear();
            txtRecoveryScore.Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchID = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchID))
            {
                MessageBox.Show("Please enter an Animal ID.");
                return;
            }

            LoadAnimalEditing(searchID);
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(editingAnimalID))
            {
                MessageBox.Show("Please select an animal to update.");
                return;
            }


            if (!ValidateAnimal())
                return;

            try
            {
                bl.UpdateAnimal(
                    editingAnimalID,
                    txtName.Text.Trim(),
                    txtSpecies.Text.Trim(),
                    int.Parse(txtAge.Text),
                    int.Parse(txtRecoveryScore.Text));

                MessageBox.Show("Animal updated successfully!");

                RefreshAnimalGrid();
                ClearAnimalForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating animal: " + ex.Message);
            }
            
            

            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAnimalForm();
        }

        private void ClearAnimalForm()
        {
            editingAnimalID = null;

            txtAnimalID.Clear();
            txtName.Clear();
            txtSpecies.Clear();
            txtAge.Clear();
            txtRecoveryScore.Clear();
            txtSearch.Clear();

            txtAnimalID.ReadOnly = false;

            btnAdd.Enabled = true;
            btnSaveChanges.Enabled = false;

            dgvAnimals.ClearSelection();

            txtAnimalID.Focus();
        }

        private void RefreshAnimalGrid()
        {
            dgvAnimals.DataSource = null;
            dgvAnimals.DataSource = bl.GetAllAnimals();

            dgvAnimals.ReadOnly = true;
            dgvAnimals.AllowUserToAddRows = false;
            dgvAnimals.AllowUserToDeleteRows = false;
            dgvAnimals.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvAnimals.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvAnimals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string animalID = dgvAnimals
                .Rows[e.RowIndex]
                .Cells[0]
                .Value.ToString();

            LoadAnimalEditing(animalID);
        }

        private void dgvAnimals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string animalID =
                dgvAnimals.Rows[e.RowIndex].Cells[0].Value.ToString();

            MessageBox.Show("Selected Animal ID: " + animalID);

            LoadAnimalEditing(animalID);
        }
    }
}