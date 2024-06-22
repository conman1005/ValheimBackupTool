namespace ValheimBackup
{
    public partial class frmMain : Form
    {

        public String urlValheim;

        // World URLs
        public String urlWorld = "C:/Users/conne/AppData/LocalLow/IronGate/Valheim/worlds_local";
        public String urlCharacter = "C:/Users/conne/AppData/LocalLow/IronGate/Valheim/characters_local";

        // Initialize World and Character URLs
        List<String> urlWorlds = new List<String>();
        List<String> urlCharacters = new List<String>();

        // Initialize Character and World save management Forms
        ManageBackups mngWorlds;
        ManageBackups mngCharacters;

        public frmMain()
        {
            InitializeComponent();

            //MessageBox.Show(System.IO.Path.GetDirectoryName(Application.ExecutablePath));

            String ini = "./ValheimBackupTool.ini";
            String defaultLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).Split("Roaming")[0] + "LocalLow\\IronGate\\Valheim";

            do
            {
                // Ask user for Valheim Save folder location if ini file does not exist
                if (File.Exists(ini))
                {
                    //File.Delete(ini); // Test code for handling a file error when reading
                    try
                    {
                        StreamReader readIni = new StreamReader(ini);
                        urlValheim = readIni.ReadLine();
                        readIni.Close();

                        urlWorld = urlValheim + "\\worlds_local";
                        urlCharacter = urlValheim + "\\characters_local";
                    }
                    catch (IOException)
                    {
                        File.Delete(ini);
                        //MessageBox.Show("Error opening Initiation File. Please restart this program");
                    }
                    //File.Delete(ini);
                }
                else
                {
                    StreamWriter outputFile = new StreamWriter(ini, false);

                    if (Directory.Exists(defaultLocation))
                    {
                        //MessageBox.Show("Valheim Save Folder was Found");
                        using (outputFile)
                        {
                            outputFile.WriteLine(defaultLocation);
                            outputFile.Close();
                        }

                    }
                    else
                    {
                        FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                        folderBrowser.Description = "Please select the folder containing the Valheim Save Data";
                        folderBrowser.UseDescriptionForTitle = true;
                        folderBrowser.InitialDirectory = defaultLocation;

                        bool valid = false;

                        while (!valid)
                        {
                            DialogResult result = folderBrowser.ShowDialog();
                            if (result != DialogResult.OK)
                            {
                                MessageBox.Show("You must provide the Valheim Save Locations for this program to work.\nThis program will now close.");
                                Environment.Exit(-1);
                                return;
                            }
                            else
                            {
                                urlWorld = folderBrowser.SelectedPath + "\\worlds_local";
                                urlCharacter = folderBrowser.SelectedPath + "\\characters_local";

                                if (!Directory.Exists(urlWorld) || !Directory.Exists(urlCharacter) || !folderBrowser.SelectedPath.EndsWith("Valheim"))
                                {
                                    MessageBox.Show("This does not seem to be the right folder.\nPlease chose a different folder.\nThe folder should have the name \"Valheim\".");
                                }
                                else
                                {
                                    valid = true;
                                }
                            }
                        }

                        //File.Create(ini);

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
            } while (!File.Exists(ini));

            // Load lists with world and character names

            String[] worldFolder = Directory.GetFiles(urlWorld);
            String[] characterFolder = Directory.GetFiles(urlCharacter);


            foreach (String worldName in worldFolder)
            {
                //MessageBox.Show(worldName);
                Name = worldName.Split("\\")[worldName.Split("\\").Length - 1];
                //Name = worldName;

                // Check for current world files
                if (Name.Split("_").Length < 2 && Name.Split(".").Length < 3 && Name.Split(".")[1].Equals("fwl"))
                {
                    // Add World Name to lstWorlds
                    lstWorld.Items.Add(Name.Split(".")[0]);

                    // Add World URL to urlWorlds
                    urlWorlds.Add(worldName);
                }
            }

            foreach (String characterName in characterFolder)
            {
                Name = characterName.Split("\\")[characterName.Split("\\").Length - 1];
                //Name = characterName;

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

            MessageBox.Show(lstWorld.SelectedItem.ToString() + " has been Backed Up.");
        }

        private void btnMngWorlds_Click(object sender, EventArgs e)
        {
            // Set World save management Forms with type and URL
            mngWorlds = new ManageBackups("Worlds", urlWorld, lstWorld.Items, urlWorlds);

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

            /*if (MessageBox.Show("FYI: Loading Character Backups does not work as the game will not add a character unless it is created in game.", "Loading Will Not Work", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }*/

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

            MessageBox.Show(lstCharacter.SelectedItem.ToString() + " has been Backed Up.");
        }

        private void btnMngChars_Click(object sender, EventArgs e)
        {
            // Set Character save management Forms with type and URL
            mngCharacters = new ManageBackups("Characters", urlCharacter, lstCharacter.Items, urlCharacters);

            mngCharacters.Show();
        }
    }
}