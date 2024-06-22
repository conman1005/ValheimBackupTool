/*
 *              Authour: Conner Cullity
 *               GitHub: @conman1005
 *            Repo Link: https://github.com/conman1005/ValheimBackupTool
 *    Time of Last Edit: 2024-06-21 11:14 PM
 * 
 *     Form Description: This Form provides a User interface that allows
 *                       users to select a Character or World to recover.
 *                       There are Buttons to Delete and to Load Saves.
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValheimBackup
{
    public partial class ManageBackups : Form
    {
        // Initialize Management Variables
        public String type;
        public String folderURL;
        public List<String> lstURLs;
        String[] saves;

        /**
         * <param name="type"/>
         * <param name="folderURL"/>
         * <param name="lstNames"/>
         * <param name="lstURLs"/>
         * 
         * 
         * <summary>The <b>ManageBackups</b> cunstructor is used for determining the backup type (Character or World),<br/> and to get World/Character Names/URLs from the Main Form.</summary>
         * 
         */
        public ManageBackups(String type, String folderURL, ListBox.ObjectCollection lstNames, List<String> lstURLs)
        {
            InitializeComponent();

            // Set type and folder urls
            this.type = type;
            this.folderURL = folderURL;
            this.lstURLs = lstURLs;
            lblNames.Text = type;

            // Set names to the ListBox
            foreach (String name in lstNames)
            {
                this.lstNames.Items.Add(name);
            }
        }

        private void lstNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset Lists when list is selected with no items selected
            if (lstNames.SelectedItems.Count > 0)
            {
                ResetSaves();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            // Check if anyting is selected before continuing
            if (lstSaves.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a World to Recover.");
                return;
            }

            // Show Confirmation of Recovery Dialog
            DialogResult confirm = MessageBox.Show("Are you sure you want to Load " + lstNames.SelectedItem.ToString() + " saved at " + lstSaves.SelectedItem.ToString() + " ?\nCurrent save file WILL be lost.\nIf you prefer to have a copy of the current save,\nplease go back to the main menu and back up " + lstNames.SelectedItem.ToString() + " to ensure the data won't be lost.", "Confirm Recovery", MessageBoxButtons.YesNo);

            // Stop recovery of user does not select Yes
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            // Set FWL (or FCH) file and corrosponding DB file names
            String nameFWL = lstURLs[lstNames.SelectedIndex];
            String nameDB = nameFWL.Split(".")[0] + ".db";


            // Loop through saves
            for (int i = 0; i < saves.Length; i++)
            {
                // Run if save is found
                String[] saveName = lstSaves.SelectedItem.ToString().Split(" ");
                if (saves[i].Contains(saveName[0].Replace(":", "-") + " " + saveName[1]))
                {
                    // Recover DB file when found
                    if (saves[i].Contains(".db"))
                    {
                        File.Copy(saves[i], nameDB, true);
                    }
                    else
                    {
                        // Otherwise recocover fch or fwl
                        File.Copy(saves[i], nameFWL, true);
                    }
                }
            }

            // Let User know that the recovery was Sucessful
            MessageBox.Show(lstSaves.SelectedItem.ToString() + " Has Been Recovered Sucessfully");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Show confirmatiom of deletion dialog
            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo);

            // Stop if user selects anything other than yes
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            // Make sure a save is selected, return if not
            if (lstSaves.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a Save to Delete.");
                return;
            }

            // Loop through saves
            foreach (String save in saves)
            {
                // Delete save if it matches the selected save
                String[] saveName = lstSaves.SelectedItem.ToString().Split(" ");
                if (save.Contains(saveName[0].Replace(":", "-") + " " + saveName[1]))
                {
                    File.Delete(save);
                }
            }

            // Let user know save was deleted sucessfully
            MessageBox.Show(lstSaves.SelectedItem.ToString() + " Has Been Deleted Sucessfully");
            ResetSaves();
        }

        /**
         * <summary><b>ResetSaves()</b> Refreshes <b>lstSave.Items</b> for updating the list upon loading or after deleting.</summary>
         */
        private void ResetSaves()
        {
            // Clear list before refresh
            lstSaves.Items.Clear();

            // Get saves from directory
            saves = Directory.GetFiles(folderURL + "/Backups/" + lstNames.SelectedItem.ToString());

            // loop through save files
            foreach (String name in saves)
            {
                // Add to list if file extension  equals '.fwl' or '.fch'
                if (name.Split('.')[1] == "fwl" || name.Split('.')[1] == "fch")
                {
                    //MessageBox.Show(name.Split('.')[0].Split("\\")[name.Split('.')[0].Split("\\").Length - 1].Split("_")[1]);
                    // Add save time to saves list
                    String[] saveDateTime = name.Split('.')[0].Split("\\")[name.Split('.')[0].Split("\\").Length - 1].Split("_")[1].Split(" ");
                    lstSaves.Items.Add(saveDateTime[0].Replace("-", ":") + " " + saveDateTime[1]);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
