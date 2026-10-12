namespace Baobab_Ridge_Wildlife_Rehabilitation_Records_System
{
    partial class SummaryForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblEmptyNote = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.valTotal = new System.Windows.Forms.Label();
            this.lblAvgAge = new System.Windows.Forms.Label();
            this.valAvgAge = new System.Windows.Forms.Label();
            this.lblAvgScore = new System.Windows.Forms.Label();
            this.valAvgScore = new System.Windows.Forms.Label();
            this.lblCountsHeading = new System.Windows.Forms.Label();
            this.pnlStatusCounts = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(16, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(160, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Summary Report";
            // 
            // lblEmptyNote
            // 
            this.lblEmptyNote.AutoSize = true;
            this.lblEmptyNote.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmptyNote.Location = new System.Drawing.Point(20, 52);
            this.lblEmptyNote.Name = "lblEmptyNote";
            this.lblEmptyNote.Size = new System.Drawing.Size(210, 17);
            this.lblEmptyNote.TabIndex = 1;
            this.lblEmptyNote.Text = "No animal records are available.";
            this.lblEmptyNote.Visible = false;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(20, 84);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(95, 17);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "Total animals:";
            // 
            // valTotal
            // 
            this.valTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valTotal.Location = new System.Drawing.Point(200, 81);
            this.valTotal.Name = "valTotal";
            this.valTotal.Size = new System.Drawing.Size(200, 22);
            this.valTotal.TabIndex = 3;
            this.valTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAvgAge
            // 
            this.lblAvgAge.AutoSize = true;
            this.lblAvgAge.Location = new System.Drawing.Point(20, 114);
            this.lblAvgAge.Name = "lblAvgAge";
            this.lblAvgAge.Size = new System.Drawing.Size(90, 17);
            this.lblAvgAge.TabIndex = 4;
            this.lblAvgAge.Text = "Average age:";
            // 
            // valAvgAge
            // 
            this.valAvgAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valAvgAge.Location = new System.Drawing.Point(200, 111);
            this.valAvgAge.Name = "valAvgAge";
            this.valAvgAge.Size = new System.Drawing.Size(200, 22);
            this.valAvgAge.TabIndex = 5;
            this.valAvgAge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAvgScore
            // 
            this.lblAvgScore.AutoSize = true;
            this.lblAvgScore.Location = new System.Drawing.Point(20, 144);
            this.lblAvgScore.Name = "lblAvgScore";
            this.lblAvgScore.Size = new System.Drawing.Size(155, 17);
            this.lblAvgScore.TabIndex = 6;
            this.lblAvgScore.Text = "Average recovery score:";
            // 
            // valAvgScore
            // 
            this.valAvgScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valAvgScore.Location = new System.Drawing.Point(200, 141);
            this.valAvgScore.Name = "valAvgScore";
            this.valAvgScore.Size = new System.Drawing.Size(200, 22);
            this.valAvgScore.TabIndex = 7;
            this.valAvgScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCountsHeading
            // 
            this.lblCountsHeading.AutoSize = true;
            this.lblCountsHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCountsHeading.Location = new System.Drawing.Point(20, 184);
            this.lblCountsHeading.Name = "lblCountsHeading";
            this.lblCountsHeading.Size = new System.Drawing.Size(140, 17);
            this.lblCountsHeading.TabIndex = 8;
            this.lblCountsHeading.Text = "Animals per status:";
            // 
            // pnlStatusCounts
            // 
            this.pnlStatusCounts.Location = new System.Drawing.Point(20, 208);
            this.pnlStatusCounts.Name = "pnlStatusCounts";
            this.pnlStatusCounts.Size = new System.Drawing.Size(380, 160);
            this.pnlStatusCounts.TabIndex = 9;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(310, 380);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 30);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 426);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlStatusCounts);
            this.Controls.Add(this.lblCountsHeading);
            this.Controls.Add(this.valAvgScore);
            this.Controls.Add(this.lblAvgScore);
            this.Controls.Add(this.valAvgAge);
            this.Controls.Add(this.lblAvgAge);
            this.Controls.Add(this.valTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblEmptyNote);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SummaryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Summary Report";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblEmptyNote;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label valTotal;
        private System.Windows.Forms.Label lblAvgAge;
        private System.Windows.Forms.Label valAvgAge;
        private System.Windows.Forms.Label lblAvgScore;
        private System.Windows.Forms.Label valAvgScore;
        private System.Windows.Forms.Label lblCountsHeading;
        private System.Windows.Forms.Panel pnlStatusCounts;
        private System.Windows.Forms.Button btnClose;
    }
}
