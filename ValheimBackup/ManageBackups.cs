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
        public String type;
        public String folderURL;
        public List<String> lstURLs;
        String[] saves;
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

            DialogResult confirm = MessageBox.Show("Are you sure you want to Load " + lstNames.SelectedItem.ToString() + " saved at " + lstSaves.SelectedItem.ToString() + " ?\nCurrent save file WILL be lost.\nIf you prefer to have a copy of the current save,\nplease go back to the main menu and back up " + lstNames.SelectedItem.ToString() + " to ensure the data won't be lost.", "Confirm Recovery", MessageBoxButtons.YesNo);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            //DateTime time = DateTime.Now;

            /*// Create 'Backups' Directory if it doesn't exist
            if (!Directory.Exists(folderURL + "/Backups"))
            {
                Directory.CreateDirectory(folderURL + "/Backups");
            }*/



            String path = lstURLs[lstNames.SelectedIndex]/*.Split("\\")[lstURLs[lstSaves.SelectedIndex].Split("\\").Length - 3] + "\\"*/;
            String nameFWL = path/* + lstURLs[lstSaves.SelectedIndex].Split("\\")[lstURLs[lstSaves.SelectedIndex].Split("\\").Length - 1]*/;
            String nameDB = nameFWL.Split(".")[0] + ".db";


            //foreach (String save in saves)
            for (int i = 0; i < saves.Length; i++)
            {
                String[] saveName = lstSaves.SelectedItem.ToString().Split(" ");
                if (saves[i].Contains(saveName[0].Replace(":", "-") + " " + saveName[1]))
                {
                    //MessageBox.Show(saves[i]);
                    if (saves[i].Contains(".db"))
                    {
                        
                        
                        File.Copy(saves[i], nameDB, true);
                    }
                    else
                    {
                        File.Copy(saves[i], nameFWL, true);
                    }
                }
            }


            MessageBox.Show(lstSaves.SelectedItem.ToString() + " Has Been Recovered Sucessfully");



            // Copy files to a main folder
            //File.Copy(path, nameFWL, true);
            //File.Copy(".db", true);

            //MessageBox.Show(lstWorld.SelectedItem.ToString() + " has been Backed Up.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //saves = Directory.GetFiles(folderURL + "/Backups/" + lstNames.SelectedItem.ToString());

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this?", "Confirm Deletion", MessageBoxButtons.YesNo);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            if (lstSaves.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a Save to Delete.");
                return;
            }

            foreach (String save in saves)
            {
                String[] saveName = lstSaves.SelectedItem.ToString().Split(" ");
                //MessageBox.Show(save);
                if (save.Contains(saveName[0].Replace(":", "-") + " " + saveName[1]))
                {
                    File.Delete(save);
                }
            }


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
