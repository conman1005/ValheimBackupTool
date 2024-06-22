namespace ValheimBackup
{
    partial class ManageBackups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageBackups));
            btnDelete = new Button();
            btnLoad = new Button();
            lstSaves = new ListBox();
            lstNames = new ListBox();
            lblSaves = new Label();
            lblNames = new Label();
            btnBack = new Button();
            SuspendLayout();
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(231, 283);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(175, 39);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Delete Save";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(12, 283);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(175, 39);
            btnLoad.TabIndex = 4;
            btnLoad.Text = "Load Save";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // lstSaves
            // 
            lstSaves.FormattingEnabled = true;
            lstSaves.ItemHeight = 15;
            lstSaves.Location = new Point(231, 61);
            lstSaves.Name = "lstSaves";
            lstSaves.RightToLeft = RightToLeft.Yes;
            lstSaves.Size = new Size(175, 199);
            lstSaves.TabIndex = 3;
            // 
            // lstNames
            // 
            lstNames.FormattingEnabled = true;
            lstNames.ItemHeight = 15;
            lstNames.Location = new Point(12, 61);
            lstNames.Name = "lstNames";
            lstNames.Size = new Size(175, 199);
            lstNames.TabIndex = 1;
            lstNames.SelectedIndexChanged += lstNames_SelectedIndexChanged;
            // 
            // lblSaves
            // 
            lblSaves.AutoSize = true;
            lblSaves.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblSaves.Location = new Point(341, 28);
            lblSaves.Name = "lblSaves";
            lblSaves.Size = new Size(65, 30);
            lblSaves.TabIndex = 2;
            lblSaves.Text = "Saves";
            // 
            // lblNames
            // 
            lblNames.AutoSize = true;
            lblNames.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblNames.Location = new Point(12, 28);
            lblNames.Name = "lblNames";
            lblNames.Size = new Size(77, 30);
            lblNames.TabIndex = 0;
            lblNames.Text = "Worlds";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(12, 2);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // ManageBackups
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(418, 334);
            Controls.Add(btnBack);
            Controls.Add(btnDelete);
            Controls.Add(btnLoad);
            Controls.Add(lstSaves);
            Controls.Add(lstNames);
            Controls.Add(lblSaves);
            Controls.Add(lblNames);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ManageBackups";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ManageBackups";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnDelete;
        private Button btnLoad;
        private ListBox lstSaves;
        private ListBox lstNames;
        private Label lblSaves;
        private Label lblNames;
        private Button btnBack;
    }
}