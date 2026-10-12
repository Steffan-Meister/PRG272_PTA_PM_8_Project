namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtRecoveryScore = new System.Windows.Forms.TextBox();
            this.lblRecoveryScore = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtSpecies = new System.Windows.Forms.TextBox();
            this.lblSpecies = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtAnimalId = new System.Windows.Forms.TextBox();
            this.lblAnimalId = new System.Windows.Forms.Label();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.btnGenerateSummary = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchAnimalId = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dgvAnimals = new System.Windows.Forms.DataGridView();
            this.colAnimalID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpecies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRecoveryScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHousingUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpDetails.SuspendLayout();
            this.grpActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimals)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDetails
            // 
            this.grpDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDetails.Controls.Add(this.btnAdd);
            this.grpDetails.Controls.Add(this.txtRecoveryScore);
            this.grpDetails.Controls.Add(this.lblRecoveryScore);
            this.grpDetails.Controls.Add(this.txtAge);
            this.grpDetails.Controls.Add(this.lblAge);
            this.grpDetails.Controls.Add(this.txtSpecies);
            this.grpDetails.Controls.Add(this.lblSpecies);
            this.grpDetails.Controls.Add(this.txtName);
            this.grpDetails.Controls.Add(this.lblName);
            this.grpDetails.Controls.Add(this.txtAnimalId);
            this.grpDetails.Controls.Add(this.lblAnimalId);
            this.grpDetails.Location = new System.Drawing.Point(12, 12);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(864, 130);
            this.grpDetails.TabIndex = 0;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Animal Details";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(640, 71);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(150, 30);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtRecoveryScore
            // 
            this.txtRecoveryScore.Location = new System.Drawing.Point(420, 75);
            this.txtRecoveryScore.MaxLength = 3;
            this.txtRecoveryScore.Name = "txtRecoveryScore";
            this.txtRecoveryScore.Size = new System.Drawing.Size(100, 22);
            this.txtRecoveryScore.TabIndex = 9;
            // 
            // lblRecoveryScore
            // 
            this.lblRecoveryScore.AutoSize = true;
            this.lblRecoveryScore.Location = new System.Drawing.Point(300, 78);
            this.lblRecoveryScore.Name = "lblRecoveryScore";
            this.lblRecoveryScore.Size = new System.Drawing.Size(111, 17);
            this.lblRecoveryScore.TabIndex = 8;
            this.lblRecoveryScore.Text = "Recovery Score:";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(110, 75);
            this.txtAge.MaxLength = 3;
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(150, 22);
            this.txtAge.TabIndex = 7;
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(16, 78);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(37, 17);
            this.lblAge.TabIndex = 6;
            this.lblAge.Text = "Age:";
            // 
            // txtSpecies
            // 
            this.txtSpecies.Location = new System.Drawing.Point(640, 31);
            this.txtSpecies.MaxLength = 50;
            this.txtSpecies.Name = "txtSpecies";
            this.txtSpecies.Size = new System.Drawing.Size(150, 22);
            this.txtSpecies.TabIndex = 5;
            // 
            // lblSpecies
            // 
            this.lblSpecies.AutoSize = true;
            this.lblSpecies.Location = new System.Drawing.Point(560, 34);
            this.lblSpecies.Name = "lblSpecies";
            this.lblSpecies.Size = new System.Drawing.Size(62, 17);
            this.lblSpecies.TabIndex = 4;
            this.lblSpecies.Text = "Species:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(370, 31);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(150, 22);
            this.txtName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(300, 34);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 17);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name:";
            // 
            // txtAnimalId
            // 
            this.txtAnimalId.Location = new System.Drawing.Point(110, 31);
            this.txtAnimalId.MaxLength = 20;
            this.txtAnimalId.Name = "txtAnimalId";
            this.txtAnimalId.Size = new System.Drawing.Size(150, 22);
            this.txtAnimalId.TabIndex = 1;
            // 
            // lblAnimalId
            // 
            this.lblAnimalId.AutoSize = true;
            this.lblAnimalId.Location = new System.Drawing.Point(16, 34);
            this.lblAnimalId.Name = "lblAnimalId";
            this.lblAnimalId.Size = new System.Drawing.Size(68, 17);
            this.lblAnimalId.TabIndex = 0;
            this.lblAnimalId.Text = "Animal ID:";
            // 
            // grpActions
            // 
            this.grpActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpActions.Controls.Add(this.btnGenerateSummary);
            this.grpActions.Controls.Add(this.btnDelete);
            this.grpActions.Controls.Add(this.btnClear);
            this.grpActions.Controls.Add(this.btnSaveChanges);
            this.grpActions.Controls.Add(this.btnSearch);
            this.grpActions.Controls.Add(this.txtSearchAnimalId);
            this.grpActions.Controls.Add(this.lblSearch);
            this.grpActions.Location = new System.Drawing.Point(12, 148);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(864, 60);
            this.grpActions.TabIndex = 1;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "Find / Edit / Delete / Report";
            // 
            // btnGenerateSummary
            // 
            this.btnGenerateSummary.Location = new System.Drawing.Point(710, 20);
            this.btnGenerateSummary.Name = "btnGenerateSummary";
            this.btnGenerateSummary.Size = new System.Drawing.Size(140, 28);
            this.btnGenerateSummary.TabIndex = 6;
            this.btnGenerateSummary.Text = "Generate Summary";
            this.btnGenerateSummary.UseVisualStyleBackColor = true;
            this.btnGenerateSummary.Click += new System.EventHandler(this.btnGenerateSummary_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(610, 20);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 28);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(520, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 28);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Location = new System.Drawing.Point(390, 20);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(120, 28);
            this.btnSaveChanges.TabIndex = 3;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(290, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 28);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchAnimalId
            // 
            this.txtSearchAnimalId.Location = new System.Drawing.Point(140, 23);
            this.txtSearchAnimalId.MaxLength = 20;
            this.txtSearchAnimalId.Name = "txtSearchAnimalId";
            this.txtSearchAnimalId.Size = new System.Drawing.Size(140, 22);
            this.txtSearchAnimalId.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(16, 26);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(113, 17);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search Animal ID:";
            // 
            // dgvAnimals
            // 
            this.dgvAnimals.AllowUserToAddRows = false;
            this.dgvAnimals.AllowUserToDeleteRows = false;
            this.dgvAnimals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAnimals.AutoGenerateColumns = false;
            this.dgvAnimals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAnimals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnimals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAnimalID,
            this.colName,
            this.colSpecies,
            this.colAge,
            this.colRecoveryScore,
            this.colStatus,
            this.colHousingUnit});
            this.dgvAnimals.Location = new System.Drawing.Point(12, 216);
            this.dgvAnimals.MultiSelect = false;
            this.dgvAnimals.Name = "dgvAnimals";
            this.dgvAnimals.ReadOnly = true;
            this.dgvAnimals.RowHeadersVisible = false;
            this.dgvAnimals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAnimals.Size = new System.Drawing.Size(864, 390);
            this.dgvAnimals.TabIndex = 2;
            this.dgvAnimals.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnimals_CellClick);
            // 
            // colAnimalID
            // 
            this.colAnimalID.DataPropertyName = "AnimalID";
            this.colAnimalID.HeaderText = "Animal ID";
            this.colAnimalID.Name = "colAnimalID";
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            // 
            // colSpecies
            // 
            this.colSpecies.DataPropertyName = "Species";
            this.colSpecies.HeaderText = "Species";
            this.colSpecies.Name = "colSpecies";
            // 
            // colAge
            // 
            this.colAge.DataPropertyName = "Age";
            this.colAge.HeaderText = "Age";
            this.colAge.Name = "colAge";
            // 
            // colRecoveryScore
            // 
            this.colRecoveryScore.DataPropertyName = "RecoveryScore";
            this.colRecoveryScore.HeaderText = "Recovery Score";
            this.colRecoveryScore.Name = "colRecoveryScore";
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            // 
            // colHousingUnit
            // 
            this.colHousingUnit.DataPropertyName = "HousingUnit";
            this.colHousingUnit.HeaderText = "Housing Unit";
            this.colHousingUnit.Name = "colHousingUnit";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 620);
            this.Controls.Add(this.dgvAnimals);
            this.Controls.Add(this.grpActions);
            this.Controls.Add(this.grpDetails);
            this.MinimumSize = new System.Drawing.Size(760, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Baobab Ridge Wildlife Rehabilitation Records System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.grpActions.ResumeLayout(false);
            this.grpActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimals)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblAnimalId;
        private System.Windows.Forms.TextBox txtAnimalId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblSpecies;
        private System.Windows.Forms.TextBox txtSpecies;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label lblRecoveryScore;
        private System.Windows.Forms.TextBox txtRecoveryScore;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearchAnimalId;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnGenerateSummary;
        private System.Windows.Forms.DataGridView dgvAnimals;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAnimalID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpecies;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecoveryScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHousingUnit;
    }
}
