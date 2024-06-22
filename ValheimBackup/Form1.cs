/*
 *              Authour: Conner Cullity
 *               GitHub: @conman1005
 *            Repo Link: https://github.com/conman1005/ValheimBackupTool
 *    Time of Last Edit: 2024-06-21 11:14 PM
 * 
 *     Form Description: This Form provides a User interface that allows
 *                       users to select a Character or World to back up
 *                       from their corrosponding lists.
 *                       Those lists (ListView) have corrosponding
 *                       buttons for backing up and managing backups.
 */


namespace ValheimBackup
{
    public partial class frmMain : Form
    {
        // Initialize Valheim String
        public String urlValheim;

        // Initialize World URLs
        public String urlWorld;
        public String urlCharacter;

        // Initialize World and Character URLs
        List<String> urlWorlds = new List<String>();
        List<String> urlCharacters = new List<String>();

        // Initialize Character and World save management Forms
        ManageBackups mngWorlds;
        ManageBackups mngCharacters;

        public frmMain()
        {
            InitializeComponent();

            //MessageBox.Show(System.IO.Path.GetDirectoryName(Application.ExecutablePath));       // Test code to find where program is saved

            // Initialize Initialization file name
            String ini = "./ValheimBackupTool.ini";

            // Use Environment.SpecialFolder.ApplicationData to set the default Valheim save location String
            String defaultLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Split("Roaming")[0] + "LocalLow\\IronGate\\Valheim";

            // If the ini file happens to be corrupted or deleted during the startup the do while loop will make sure a new ini file is made
            do
            {
                // Ask user for Valheim Save folder location if ini file does not exist
                if (File.Exists(ini))
                {
                    //File.Delete(ini); // Test code for handling a file error when reading

                    // Try to read ini file
                    try
                    {
                        StreamReader readIni = new StreamReader(ini);

                        // Save the First line in the ini file as urlValheim for the save location
                        urlValheim = readIni.ReadLine();

                        // Close File
                        readIni.Close();

                        // Set World/Character folder locations
                        urlWorld = urlValheim + "\\worlds_local";
                        urlCharacter = urlValheim + "\\characters_local";
                    }
                    catch (IOException)
                    {
                        File.Delete(ini);
                        //MessageBox.Show("Error opening Initiation File. Please restart this program");    No more need to restart program with this loop... Right???
                    }
                    //File.Delete(ini);     Testing code to quicly remove ini file
                }
                else
                {
                    // Initialize StreamWriter as outputFile
                    StreamWriter outputFile = new StreamWriter(ini, false);

                    // Check if the default save location for Valheim exists on the computer
                    if (Directory.Exists(defaultLocation))
                    {
                        //MessageBox.Show("Valheim Save Folder was Found");

                        // If default save location exists write it to the ini file
                        using (outputFile)
                        {
                            outputFile.WriteLine(defaultLocation);
                            outputFile.Close();
                        }
                    }
                    else
                    {
                        // If default Valheinm save location is not found then ask user to select the Valheim save location
                        FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                        folderBrowser.Description = "Please select the folder containing the Valheim Save Data";
                        folderBrowser.UseDescriptionForTitle = true;
                        folderBrowser.InitialDirectory = defaultLocation;

                        // Initialize valid Boolean for while loop to run whilw selected location is invalid
                        bool valid = false;

                        // Run while Valheim save location is invalid
                        while (!valid)
                        {
                            // Get folderBrowser Result from showing dialog
                            DialogResult result = folderBrowser.ShowDialog();

                            // Close program if user clicks cancel or 'X'
                            if (result != DialogResult.OK)
                            {
                                MessageBox.Show("You must provide the Valheim Save Locations for this program to work.\nThis program will now close.");
                                Environment.Exit(-1);
                                return;
                            }
                            else
                            {
                                // Set Character and World URLs from the User Selected Path
                                urlWorld = folderBrowser.SelectedPath + "\\worlds_local";
                                urlCharacter = folderBrowser.SelectedPath + "\\characters_local";

                                // Check if World/Character folders exist and if the Root of the Save Folder is named 'Valheim'
                                if (!Directory.Exists(urlWorld) || !Directory.Exists(urlCharacter) || !folderBrowser.SelectedPath.EndsWith("Valheim"))
                                {
                                    // Send MessageBox Error message if criteria is not met
                                    MessageBox.Show("This does not seem to be the right folder.\nPlease chose a different folder.\nThe folder should have the name \"Valheim\".");
                                }
                                else
                                {
                                    // otherwise set valid to true
                                    valid = true;
                                }
                            }
                        }

                        // Write the Valheim save location to the ini file once the location is valid
                        using (outputFile)
                        {
                            outputFile.WriteLine(folderBrowser.SelectedPath);
                            outputFile.Close();
                        }


                        // Bring main window to front
                        this.WindowState = FormWindowState.Minimized;
                        this.Show();
                        this.WindowState = FormWindowState.Normal;
                    }
                }
                // If the ini file happens to be corrupted or deleted during the startup the do while loop will make sure a new ini file is made
            } while (!File.Exists(ini));

            // Load lists with world and character names
            String[] worldFolder = Directory.GetFiles(urlWorld);
            String[] characterFolder = Directory.GetFiles(urlCharacter);

            // Loop through the World Folder
            foreach (String worldName in worldFolder)
            {
                // Get the World Name from the File Name
                String Name = worldName.Split("\\")[worldName.Split("\\").Length - 1];

                // Check for current world files
                if (Name.Split("_").Length < 2 && Name.Split(".").Length < 3 && Name.Split(".")[1].Equals("fwl"))
                {
                    // Add World Name to lstWorlds
                    lstWorld.Items.Add(Name.Split(".")[0]);

                    // Add World URL to urlWorlds
                    urlWorlds.Add(worldName);
                }
            }

            // Loop through Character Folder
            foreach (String characterName in characterFolder)
            {
                // Get Character Name from File Name
                String Name = characterName.Split("\\")[characterName.Split("\\").Length - 1];

                // Check for current world files
                if (Name.Split("_").Length < 3 && Name.Split(".").Length < 3 && Name.Split(".")[1].Equals("fch"))
                {
                    // Add World Name to lstWorlds
                    lstCharacter.Items.Add(Name.Split(".")[0]);

                    // Add World URL to urlWorlds
                    urlCharacters.Add(characterName);
                }
            }
        }

        private void btnWorld_Click(object sender, EventArgs e)
        {
            // Check if anyting is selected before continuing
            if (lstWorld.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a World to Back Up.");
                return;
            }
            DateTime time = DateTime.Now;

            // Create 'Backups' Directory if it doesn't exist
            if (!Directory.Exists(urlWorld + "/Backups"))
            {
                Directory.CreateDirectory(urlWorld + "/Backups");
            }
            // Create World Save Folder if it does not exist
            if (!Directory.Exists(urlWorld + "/Backups/" + lstWorld.SelectedItem)) ;
            {
                Directory.CreateDirectory(urlWorld + "/Backups/" + lstWorld.SelectedItem);
            }

            // Copy world files to a Backup folder
            File.Copy(urlWorlds[lstWorld.SelectedIndex], urlWorld + "/Backups/" + lstWorld.SelectedItem + "/" + lstWorld.SelectedItem + "_" + time.ToString("HH-mm-ss MM-dd-yyyy") + ".fwl", false);
            File.Copy(urlWorlds[lstWorld.SelectedIndex].Split(".")[0] + ".db", urlWorld + "/Backups/" + lstWorld.SelectedItem + "/" + lstWorld.SelectedItem + "_" + time.ToString("HH-mm-ss MM-dd-yyyy") + ".db", false);

            // Let user know backup was sucessfull
            MessageBox.Show(lstWorld.SelectedItem.ToString() + " has been Backed Up.");
        }

        private void btnMngWorlds_Click(object sender, EventArgs e)
        {
            // Set World save management Forms with type and URL
            mngWorlds = new ManageBackups("Worlds", urlWorld, lstWorld.Items, urlWorlds);

            // Show the World Management Form
            mngWorlds.Show();
        }

        private void btnCharacter_Click(object sender, EventArgs e)
        {
            // Check if anyting is selected before continuing
            if (lstCharacter.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a Character to Back Up.");
                return;
            }
            
            // Initialize time as the current time
            DateTime time = DateTime.Now;

            // Create 'Backups' Directory if it doesn't exist
            if (!Directory.Exists(urlCharacter + "/Backups"))
            {
                Directory.CreateDirectory(urlCharacter + "/Backups");
            }
            // Create Character Save Folder if it does nto exist
            if (!Directory.Exists(urlCharacter + "/Backups/" + lstCharacter.SelectedItem))
            {
                Directory.CreateDirectory(urlCharacter + "/Backups/" + lstCharacter.SelectedItem);
            }
            String oldURL = urlCharacters[lstCharacter.SelectedIndex];
            String newURL = urlCharacter + "/Backups/" + lstCharacter.SelectedItem + "/" + lstCharacter.SelectedItem + "_" + time.ToString("HH-mm-ss MM-dd-yyyy") + ".fch";

            //MessageBox.Show(oldURL.Split(".")[0] + ".db\n" + newURL.Split(".")[0] + ".db");

            // Copy world files to a Backup folder
            File.Copy(oldURL, newURL, false);

            // Copy .db file if it Exists
            if (File.Exists(oldURL.Split(".")[0] + ".db"))
            {
                File.Copy(oldURL.Split(".")[0] + ".db", newURL.Split(".")[0] + ".db", false);
            }

            // Let user know backup was sucessfull
            MessageBox.Show(lstCharacter.SelectedItem.ToString() + " has been Backed Up.");
        }

        private void btnMngChars_Click(object sender, EventArgs e)
        {
            // Set Character save management Forms with type and URL
            mngCharacters = new ManageBackups("Characters", urlCharacter, lstCharacter.Items, urlCharacters);

            // Show the Character Management Form
            mngCharacters.Show();
        }
    }
}