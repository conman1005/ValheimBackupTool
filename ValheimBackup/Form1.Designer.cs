namespace ValheimBackup
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            lblCharacter = new Label();
            label1 = new Label();
            lstCharacter = new ListBox();
            lstWorld = new ListBox();
            btnCharacter = new Button();
            btnWorld = new Button();
            btnMngChars = new Button();
            btnMngWorlds = new Button();
            SuspendLayout();
            // 
            // lblCharacter
            // 
            lblCharacter.AutoSize = true;
            lblCharacter.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            lblCharacter.Location = new Point(12, 25);
            lblCharacter.Name = "lblCharacter";
            lblCharacter.Size = new Size(175, 30);
            lblCharacter.TabIndex = 0;
            lblCharacter.Text = "Backup Character";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(265, 25);
            label1.Name = "label1";
            label1.Size = new Size(141, 30);
            label1.TabIndex = 4;
            label1.Text = "Backup World";
            // 
            // lstCharacter
            // 
            lstCharacter.FormattingEnabled = true;
            lstCharacter.ItemHeight = 15;
            lstCharacter.Location = new Point(12, 58);
            lstCharacter.Name = "lstCharacter";
            lstCharacter.Size = new Size(175, 199);
            lstCharacter.TabIndex = 1;
            // 
            // lstWorld
            // 
            lstWorld.FormattingEnabled = true;
            lstWorld.ItemHeight = 15;
            lstWorld.Location = new Point(231, 58);
            lstWorld.Name = "lstWorld";
            lstWorld.RightToLeft = RightToLeft.Yes;
            lstWorld.Size = new Size(175, 199);
            lstWorld.TabIndex = 5;
            // 
            // btnCharacter
            // 
            btnCharacter.Location = new Point(12, 263);
            btnCharacter.Name = "btnCharacter";
            btnCharacter.Size = new Size(75, 39);
            btnCharacter.TabIndex = 2;
            btnCharacter.Text = "Back Up";
            btnCharacter.UseVisualStyleBackColor = true;
            btnCharacter.Click += btnCharacter_Click;
            // 
            // btnWorld
            // 
            btnWorld.Location = new Point(231, 263);
            btnWorld.Name = "btnWorld";
            btnWorld.Size = new Size(75, 39);
            btnWorld.TabIndex = 6;
            btnWorld.Text = "Back Up";
            btnWorld.UseVisualStyleBackColor = true;
            btnWorld.Click += btnWorld_Click;
            // 
            // btnMngChars
            // 
            btnMngChars.Location = new Point(112, 263);
            btnMngChars.Name = "btnMngChars";
            btnMngChars.Size = new Size(75, 39);
            btnMngChars.TabIndex = 3;
            btnMngChars.Text = "Manage Backups";
            btnMngChars.UseVisualStyleBackColor = true;
            btnMngChars.Click += btnMngChars_Click;
            // 
            // btnMngWorlds
            // 
            btnMngWorlds.Location = new Point(331, 263);
            btnMngWorlds.Name = "btnMngWorlds";
            btnMngWorlds.Size = new Size(75, 39);
            btnMngWorlds.TabIndex = 7;
            btnMngWorlds.Text = "Manage Backups";
            btnMngWorlds.UseVisualStyleBackColor = true;
            btnMngWorlds.Click += btnMngWorlds_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(418, 334);
            Controls.Add(btnMngWorlds);
            Controls.Add(btnMngChars);
            Controls.Add(btnWorld);
            Controls.Add(btnCharacter);
            Controls.Add(lstWorld);
            Controls.Add(lstCharacter);
            Controls.Add(label1);
            Controls.Add(lblCharacter);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Valheim Backup Tool";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCharacter;
        private Label label1;
        private ListBox lstCharacter;
        private ListBox lstWorld;
        private Button btnCharacter;
        private Button btnWorld;
        private Button btnMngChars;
        private Button btnMngWorlds;
    }
}