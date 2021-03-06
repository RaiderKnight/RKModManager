using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Globalization;

namespace RKLauncher
{
    public partial class frmMain : Form
    {
        private Dictionary<string, CMod> m_ModsAvailable = new Dictionary<string, CMod>();
        private Dictionary<string, CMod> m_ModsInstalled = new Dictionary<string, CMod>();

        private const string MOD_SUBFOLDER = @"LifePlay\\Content\\Modules\\";
        private const string BASE_REPO_URL = "https://raw.githubusercontent.com/RaiderKnight/LPMods/main/";
        private string rKLUrlPathX = "https://raw.githubusercontent.com/RaiderKnight/RKModManager/main/rkCL.xml";
        private string m_sPath = "";
        private string rkLauncherVersionCust = "1.4.6";
        private string m_sLifePlayVersionInstalled = "";
        private string m_sLifePlayVersionAvailable = "";
        private string m_sLauncherVersionAvailable = "";
        private string rkLauncherVersionAvailable = "";

        private Point m_ptDndStart;

        public frmMain()
        {
            InitializeComponent();

            int cnt = 0;
            // FileInfo fi = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
            // m_sPath = fi.DirectoryName;

            foreach (string arg in Environment.GetCommandLineArgs())
            {
                switch(cnt++)
                {
                    case 1: // argument 1 is alternative installation path
                        string path = arg;
                        path = path.Trim('"');
                        if (!path.EndsWith("\\"))
                            path += "\\";

                        if (Directory.Exists(path))
                            m_sPath = path;
                        else
                            MessageBox.Show("The path <" + path + "> does not exist. Sorry for that!", "Something wrong!");
                        
                        break;

                    case 0: // own path
                        m_sPath = Path.GetDirectoryName(arg);
                        m_sPath += "\\";
                        break;
                }
            }  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetLifePlayVersion();
            RefreshLocalMods();
            RefreshRepoMods();
            GetUpdateRKLVersion();

            this.Text = "RK Mod Launcher " + rkLauncherVersionCust.ToString();
        }

        private void GetUpdateRKLVersion()
        {
            try
            {
                XmlDocument GetRKLVersion = new XmlDocument();
                GetRKLVersion.Load(rKLUrlPathX);
                XmlNode root = GetRKLVersion.DocumentElement.SelectSingleNode("/RKLDefinition");
                foreach (XmlNode modXml in root.ChildNodes)
                {
                    string ver = modXml.Attributes["name"].InnerText;
                    rkLauncherVersionAvailable = ver;

                    if (rkLauncherVersionAvailable != rkLauncherVersionCust)
                    {
                        DialogResult dr = MessageBox.Show("There is a new version of the launcher available.\nWould you like to download the latest version?", "RKLauncher is out dated", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            Process.Start(Path.Combine("https://github.com/RaiderKnight/RKModManager/releases/tag/" + rkLauncherVersionCust));
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void GetLifePlayVersion()
        {
            try
            {
                /** string pathToExe = m_sPath + "lifeplay.exe";
                if(File.Exists(pathToExe))
                {
                    var versionInfo = FileVersionInfo.GetVersionInfo(pathToExe); Version
                    m_sLifePlayVersionInstalled = versionInfo.ProductVersion;
                } */

                // Read the change log to get the current version - Fline
                string pathToLog = m_sPath + "Docs\\change_logs.txt";
                if (File.Exists(pathToLog))
                {
                    using (StreamReader sr = File.OpenText(pathToLog))
                    {
                        string version = sr.ReadLine();
                        if (version.Contains(" Beta "))
                        { 
                            version = version.Replace(" Beta ", "."); 
                        }
                        
                        version = version.Trim();
                        version = version.TrimEnd(':');
                        /*int idx = version.LastIndexOf(' ');
                        if (idx > 1)
                            version = version.Substring(idx + 1); */

                        m_sLifePlayVersionInstalled = version;
                        lblLPVersion.Text = version;
                        
                    }
                }
            }
            catch (Exception) { }
        }

        private void RenderInstModList()
        {
            // Sort modules by index
            List<CMod> addList = m_ModsInstalled.Values.ToList();
            addList = addList.OrderBy(o => o.getIndex()).ToList();

            lbInst.Items.Clear();
            foreach (CMod mod in addList)
            {
                lbInst.Items.Add(mod);
            }
        }

        private void RenderRepoModList()
        {
            lbAvail.Items.Clear();

            // We keep two lists for sorting them in the UI
            List<CMod> updatedMods = new List<CMod>();
            List<CMod> normalMods = new List<CMod>();

            foreach (CMod mod in m_ModsAvailable.Values)
            {
                // Check if this module is installed locally
                CMod instMod = findInstalledModWithId(mod.getId());
                if (instMod != null)
                {
                    if (instMod.getVersion() != mod.getVersion())
                    {
                        // Looks updated
                        mod.setUpdated(true);
                        updatedMods.Add(mod);
                    }
                    else
                        normalMods.Add(mod);
                }
                else
                    normalMods.Add(mod);
            }

            normalMods.Sort((x, y) => x.ToString().CompareTo(y.ToString()));
            updatedMods.Sort((x, y) => x.ToString().CompareTo(y.ToString()));

            lbAvail.Items.AddRange(updatedMods.ToArray());
            lbAvail.Items.AddRange(normalMods.ToArray());
        }

        private void GetInstalledMods()
        {
            m_ModsInstalled.Clear();

            try
            {
                DirectoryInfo directory = new DirectoryInfo(m_sPath + MOD_SUBFOLDER);
                DirectoryInfo[] directories = directory.GetDirectories();

                foreach (DirectoryInfo folder in directories)
                {
                    bool wasDisabled = false;

                    // Test if the folder contains a required info file
                    FileInfo modFileInfo = null;
                    if (folder.GetFiles("*.lpmod").Length>0)
                    {
                        modFileInfo = folder.GetFiles("*.lpmod")[0];
                    }
                    else if (folder.GetFiles("*.lpaddon").Length>0)
                    {
                        modFileInfo = folder.GetFiles("*.lpaddon")[0];
                    }
                    else if (folder.GetFiles("*.disabled").Length > 0)
                    {
                        modFileInfo = folder.GetFiles("*.disabled")[0];
                        
                        // reset old disabled state to new one
                        string newName = modFileInfo.FullName.Replace(".disabled", "");
                        modFileInfo.MoveTo(newName);
                        wasDisabled = true;
                    }

                    if (modFileInfo != null)
                    {
                        try
                        {
                            string modFile = folder.Name;
                            string modPath = modFileInfo.DirectoryName;

                            CMod instMod = new CMod(modFile, modPath);
                            if (wasDisabled)
                                instMod.setEnabled(!wasDisabled);

                            instMod.setFileName(modFileInfo.FullName);
                            instMod.readModInfo();

                            m_ModsInstalled.Add(instMod.getId(), instMod);
                        }
                        catch (Exception) { }
                    }
                }

                lblInstalledModules.Text = "You have " + m_ModsInstalled.Count.ToString() + " modules installed (checked: " + DateTime.Now.ToString() + ")";
            }
            catch(Exception ex)
            {
                lblInstalledModules.Text = "I am unable to display your installed mods... :-(";
            }
        }

        private void UpdateRepo()
        {
            bool canContinue = true;

            m_ModsAvailable.Clear();

            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (WebClient c = new WebClient())
                {
                    string sUrl = BASE_REPO_URL + "repo.xml";
                    c.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                    c.Headers.Add("Cache-Control", "no-cache");
                    c.DownloadFile(sUrl, "lprepo.xml");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Cannot download mod repository.\nCheck your connection and/or if a newer version of the mod manager is avalilable.\n\nError message is:\n"+ex.Message, "Connect 404!");
                canContinue = false;
            }

            XmlDocument repo = new XmlDocument();
            if(canContinue)
            {
                try
                {
                    repo.Load("lprepo.xml");
                    XmlNode root = repo.DocumentElement.SelectSingleNode("/Repo");
                    foreach (XmlNode modXml in root.ChildNodes)
                    {
                        if (modXml.LocalName == "#comment")
                        {
                            string val = modXml.Value;
                            string searchVer = "#Current LifePlay Version:";
                            
                            if (val.Contains(searchVer))
                            {
                                val = val.Trim();
                                m_sLifePlayVersionAvailable = val.Substring(searchVer.Length + 1);
                            }

                            searchVer = "#Current Launcher Version:";
                            if (val.Contains(searchVer))
                            {
                                val = val.Trim();
                                m_sLauncherVersionAvailable = val.Substring(searchVer.Length + 1);
                            }
                            continue;
                        }

                        string name = modXml.Attributes["Name"].InnerText;
                        string ver = modXml.Attributes["Version"].InnerText;
                        string id = modXml.Attributes["Id"].InnerText;
                        string path = modXml.Attributes["Path"].InnerText;
                        bool isPatch = modXml.Attributes["isPatch"] != null ? modXml.Attributes["isPatch"].InnerText == "1" : false;

                        if ( !(path.StartsWith("http://") || path.StartsWith("https://")) )
                            path = BASE_REPO_URL + path;

                        CMod curMod = new CMod(id, name, ver, path);

                        // Test for optional message
                        string devMsg = null;
                        if( modXml.Attributes.GetNamedItem("Msg") != null )
                            devMsg = modXml.Attributes["Msg"].InnerText;
                        curMod.setDevMessage(devMsg);

                        // Optional description
                        string descr = null;
                        if (modXml.Attributes.GetNamedItem("Description") != null)
                            descr = modXml.Attributes["Description"].InnerText;
                        curMod.setDescription(descr);

                        // Optional dependencies
                        string deps = null;
                        if (modXml.Attributes.GetNamedItem("Depends") != null)
                            deps = modXml.Attributes["Depends"].InnerText;
                        curMod.setDependencies(deps);

                        // Patch flag
                        curMod.setIsPatch(isPatch);

                        try
                        {
                            m_ModsAvailable.Add(id, curMod);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Something is wrong with the repository!\nError code: " + ex.Message);
                        }
                        
                    }

                    lblAvailModules.Text = m_ModsAvailable.Count.ToString() + " modules available (checked: " + DateTime.Now.ToString() + ")";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(this, "The repo could not be downloaded.\nError code:\n" + ex.Message + "\nPlease check if a newer version of the mod manager is avalilable.", "Ooops!");
                    canContinue = false;

                    lblAvailModules.Text = "Unable to fetch the repository. :-(";
                }
            }

            if (File.Exists("lprepo.xml"))
                File.Delete("lprepo.xml");

            try
            {
                
                string[] aInst = m_sLifePlayVersionInstalled.Split('.');
                string [] aAvail = m_sLifePlayVersionAvailable.Split('.');
                
                int majorInst = int.Parse(aInst[0]);
                int majorAvail = int.Parse(aAvail[0]);
                int minorInst = int.Parse(aInst[1]);
                int minorAvail = int.Parse(aAvail[1]);

                if (majorInst <= majorAvail)
                {
                    if (minorInst < minorAvail)
                    {
                        DialogResult dr = MessageBox.Show("Looks like you are running an old version of LifePlay.\nWould you like to download the latest version?", "LifePlay is out dated", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            Process.Start("https://www.patreon.com/vinfamy/posts?filters%5Btag%5D=release");
                        }
                    }
                }
            }
            catch (Exception) { }


            /*if (m_sLauncherVersionAvailable.Length > 0 && m_sLauncherVersionAvailable != Application.ProductVersion.ToString())
            {
                DialogResult dr = MessageBox.Show("There is a new version of the launcher available.\nWould you like to download the latest version?", "RKLauncher is out dated", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start("https://github.com/RaiderKnight/RKModManager/releases");
                }
            }*/
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if ( File.Exists( m_sPath + "lifeplay.exe") )
            {
                WriteModControlFile();

                if(CNameLists.hasCustomNameList(m_sPath))
                {
                    CNameLists nameLists = new CNameLists(m_sPath);
                    if( nameLists.loadNameLists() )
                        nameLists.writeLifePlayNameLists(m_sPath);
                }

                if (CClothLists.hasCustomClothLists(m_sPath, CClothLists.ClothType.ctCasual))
                {
                    CClothLists clothList = new CClothLists(m_sPath, CClothLists.ClothType.ctCasual);
                    if (clothList.loadClothList())
                        clothList.writeLifePlayClothList(m_sPath);
                }

                if (CClothLists.hasCustomClothLists(m_sPath, CClothLists.ClothType.ctWork))
                {
                    CClothLists clothList = new CClothLists(m_sPath, CClothLists.ClothType.ctWork);
                    if (clothList.loadClothList())
                        clothList.writeLifePlayClothList(m_sPath);
                }

                if (CClothLists.hasCustomClothLists(m_sPath, CClothLists.ClothType.ctSport))
                {
                    CClothLists clothList = new CClothLists(m_sPath, CClothLists.ClothType.ctSport);
                    if (clothList.loadClothList())
                        clothList.writeLifePlayClothList(m_sPath);
                }

#if !DEBUG
                Process.Start(m_sPath + "lifeplay.exe");
#endif
            }
            else
            {
                MessageBox.Show("I could not find the 'LifePlay.exe' in the same directory as 'RKLauncher.exe'.\n\nPlease make sure you extracted the RKLauncher into your LifePlay game directory.\n\nCheck the readme.md file for further information. Thanks...", "LifePlay not found :-(");
            }

        }

        private void WriteModControlFile()
        {
            if (lbInst.Items.Count > 0 && Directory.Exists(m_sPath))
            {
                string sPath = m_sPath + @"LifePlay\Content\Modules\ModLauncher.config";
                try
                {
                    // Delete exiting file
                    if (File.Exists(sPath))
                        File.Delete(sPath);

                    using (StreamWriter sw = new StreamWriter(File.OpenWrite(sPath)))
                    {
                        sw.WriteLine("UseConfigIgnoreAppData:true");

                        // The mod control file needs to be in reverse order ... :(
                        List<CMod> revList = lbInst.Items.Cast<CMod>().ToList();
                        revList.Reverse();

                        foreach (CMod mod in revList)
                        {
                            // if( !mod.isBaseMod() && !mod.isAddon() )
                            sw.WriteLine(mod.getId() + ":" + (mod.isEnabled() ? "true" : "false"));
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error while writing the mod control file!\nError code:\n\n" + ex.Message);
                }

            }
        }

        private bool ReadModControlFile()
        {
            string sPath = m_sPath + @"LifePlay\Content\Modules\ModLauncher.config";

            if (!File.Exists(sPath))
                return false;
            try
            {
                using (StreamReader sr = File.OpenText(sPath))
                {
                    int idx = 0;
                    List<String> itemList = new List<string>();

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (!line.StartsWith("UseConfigIgnoreAppData") && line.IndexOf(':') > 1)
                        {
                            // Test if the item already exists
                            if (!itemList.Contains(line))
                                itemList.Add(line);
                            else if(!line.StartsWith("vin_Base"))
                                Debug.Print("Duplicate mod item in control file: " + line);
                        }
                    }

                    // Test if we need to reverse the mod control file
                    if( !itemList[0].StartsWith("vin_Base"))
                        itemList.Reverse(); // The mod control file is in reversed order

                    foreach( string line in itemList )
                    {
                        string[] kvp = line.Split(':');

                        string sId = kvp[0];
                        bool enabled = (kvp[1].ToLower() == "true");
                        CMod mod = findInstalledModWithId(sId);
                        if (mod != null)
                        {
                            mod.setEnabled(enabled);
                            mod.setIndex(idx++);
                        }
                    }
                }
            }
            catch (Exception ex)
            { }

            return true;
        }

        private void lbInst_DoubleClick(object sender, EventArgs e)
        {
            if (lbInst.SelectedItem != null)
            {
                CMod selMod = (CMod)lbInst.SelectedItem;

                if (!selMod.isBaseMod() && !selMod.isAddon())
                {
                    selMod.toggleActive();

                    int idx = lbInst.SelectedIndex;
                    lbInst.Items.Remove(selMod);
                    lbInst.Items.Insert(idx, selMod);
                    lbInst.SelectedIndex = idx;
                }
                else
                {
                    if( selMod.isAddon() )
                        MessageBox.Show("You cannot disable addons like character packs or room presets.", "Delete them from the root...");
                    else
                        MessageBox.Show("Disabling the base game files will break the game...\nI'll keep them enabled for you.", "Better like that!");
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDelMod_Click(object sender, EventArgs e)
        {
            if (lbInst.SelectedItem != null)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete this mod?","Delete mod",MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    CMod mod = (CMod)lbInst.SelectedItem;
                    if (!mod.isBaseMod())
                    {
                        bool success = mod.delete();

                        if (success)
                        {
                            lbInst.Items.Remove(mod);
                            m_ModsInstalled.Remove(mod.getId());
                        }
                    }
                }
            }
        }

        private void lbInst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbInst.SelectedItem != null)
            {
                CMod mod = (CMod)lbInst.SelectedItem;

                if (!mod.isBaseMod())
                    btnDelMod.Enabled = true;
                else
                    btnDelMod.Enabled = false;

                // Select the same module in the repo if available
                int oldSel = lbAvail.SelectedIndex;
                for (int idx = 0; idx < lbAvail.Items.Count; idx++)
                {
                    CMod itm = (CMod)lbAvail.Items[idx];
                    if (itm.getId() == mod.getId())
                    {
                        lbAvail.SelectedIndex = idx;
                        break;
                    }
                }
            }
            else
                btnDelMod.Enabled = false;
        }

        private void lbAvail_DoubleClick(object sender, EventArgs e)
        {
            if (lbAvail.SelectedItem != null)
            {
                CMod mod = (CMod)lbAvail.SelectedItem;
                
                CMod newMod = InstallMod(mod, true);

                if(newMod != null)
                {
                    // Test if the mod was already installed (only updated)
                    CMod instMod = findInstalledModWithId(newMod.getId());
                    if (instMod == null)
                    {
                        RefreshLocalMods();
                    }
                }
            }
        }

        private CMod InstallMod(CMod mod, bool askToReplace = false)
        {
            if( mod.getDependencies() != null && mod.getDependencies().Length>0 )
            {
                // Test if the dependency is met
                CMod dep = findInstalledModWithId(mod.getDependencies());
                if(dep==null)
                {
                    DialogResult dr = System.Windows.Forms.DialogResult.Cancel;
                    dep = findRepoModWithId(mod.getDependencies());

                    if (dep != null)
                    {
                        dr = MessageBox.Show("The mod '" + mod.getName() + "' requires the mod '" + mod.getDependencies() + "' to be installed.\n\nCurrently I cannot find that module in your game but I have it in my repository.\n\nDo you want to install it?", mod.getDisplayName() + ": missing dependency!", MessageBoxButtons.YesNoCancel);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            InstallMod(dep, true);
                        }
                        else if (dr == System.Windows.Forms.DialogResult.Cancel)
                            return null;
                    }
                    else
                    {
                        dr = MessageBox.Show("The mod '" + mod.getName() + "' requires the mod '" + mod.getDependencies() + "' to be installed.\n\nCurrently I cannot find that module in your game and in my repository. Please consider to report this on RK forums...\n\nDo you want to continue anyways?", mod.getDisplayName() + ": missing dependency!", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.No)
                            return null;
                    }
                }
            }

            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.DownloadFile(mod.getFileName(), "lpmgr.tmp");

                    if (Directory.Exists("mmTemp"))
                        Directory.Delete("mmTemp", true);

                    Directory.CreateDirectory("mmTemp");

                    System.IO.Compression.ZipFile.ExtractToDirectory("lpmgr.tmp", "mmTemp");
                    
                    string[] subFolders = Directory.GetDirectories("mmTemp");
                    if (subFolders.Length == 1)    // Single mod
                    {
                        string name = Path.GetFileName(subFolders[0]);

                        // Test if already installed
                        bool install = true;
                        string dest = m_sPath + MOD_SUBFOLDER + name;
                        if (!mod.isPatch() && Directory.Exists(dest))       // patches always override
                        {
                            DialogResult dr = System.Windows.Forms.DialogResult.Yes;
                            if(askToReplace)
                            {
                                dr = MessageBox.Show("Replace existing mod?", mod.getDisplayName() + ": already installed!", MessageBoxButtons.YesNo);
                            }

                            if (dr == System.Windows.Forms.DialogResult.Yes)
                                Directory.Delete(dest, true);
                            else
                                install = false;
                        }

                        if (install)
                        {
                            if (mod.getDevMessage() != null)
                            {
                                MessageBox.Show(mod.getDevMessage(), "Message from mod " + mod.getName());
                            }

                            if (mod.isPatch())
                            {
                                DirectoryCopy(subFolders[0], dest, true);
                            }
                            else
                            {
                                Directory.Move(subFolders[0], dest);
                            }

                            // Test if the mod file exists
                            DirectoryInfo folder = new DirectoryInfo(dest);
                            FileInfo modFileInfo = null;
                            if (folder.GetFiles("*.lpmod").Length > 0)
                            {
                                modFileInfo = folder.GetFiles("*.lpmod")[0];
                            }
                            else if (folder.GetFiles("*.lpaddon").Length > 0)
                            {
                                modFileInfo = folder.GetFiles("*.lpaddon")[0];
                            }

                            if (modFileInfo != null)
                            {
                                CMod newMod = new CMod(mod.getName(), dest);
                                newMod.setFileName(modFileInfo.FullName);
                                newMod.readModInfo();
                                newMod.setIndex(lbInst.Items.Count);

                                if (Directory.Exists("mmTemp"))
                                    Directory.Delete("mmTemp", true);
                                File.Delete("lpmgr.tmp");

                                return newMod;
                            }
                        }
                    }
                    else if(subFolders.Length > 1)
                    {
                        if (mod.getDevMessage() != null)
                        {
                            MessageBox.Show(mod.getDevMessage(), "Message from mod " + mod.getName());
                        }

                        DialogResult dr = System.Windows.Forms.DialogResult.Yes;
                        dr = MessageBox.Show("The package contains multiple folders.\n\nIt may replace / modify several existing mods (which is OK for patches or bundles).\n\nProceed with installation?\n\n(If unsure check the mod description again...)", mod.getDisplayName() + ": Install?", MessageBoxButtons.YesNo);
                        
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            CMod retMod = null;
                            foreach (string sf in subFolders)
                            {
                                string name = Path.GetFileName(sf);
                                string dest = m_sPath + MOD_SUBFOLDER + name;

                                DirectoryCopy(sf, dest, true);
                            }
                        }
                    }

                    if (Directory.Exists("mmTemp"))
                        Directory.Delete("mmTemp", true);

                    File.Delete("lpmgr.tmp");

                    RefreshLocalMods();
                    RefreshRepoMods();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem during the installation of the mod " + mod.ToString() + ":\n" + ex.Message, "Ooops...");
                }

                return null;
            }
        }

        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            // Compare avail with installed
            List<CMod> modsToUpdate = new List<CMod>();
            string sModsToUpdate = "";

            foreach( CMod modAvail in m_ModsAvailable.Values )
            {
                CMod found = findInstalledModWithId(modAvail.getId());
                if( found != null )
                {
                    if( found.getVersion() != modAvail.getVersion() )
                    {
                        // Looks like changed
                        modsToUpdate.Add(modAvail);
                        sModsToUpdate += modAvail.ToString() + "\n";
                    }
                }
            }

            if (modsToUpdate.Count > 0)
            {
                DialogResult dr = MessageBox.Show("The following updates are available:\n\n"+sModsToUpdate +"\nDo you want to install them now?", "Updates found", MessageBoxButtons.YesNo);
                if( dr == System.Windows.Forms.DialogResult.Yes )
                {
                    foreach(CMod modToUpdate in modsToUpdate)
                    {
                        bool currentState = true;
                        CMod installedVersion = findInstalledModWithId(modToUpdate.getId());
                        currentState = installedVersion.isEnabled();
                    
                        InstallMod(modToUpdate);

                        // After installation all mods are enabled.
                        // Set the previous state
                        if( !currentState )
                        {
                            // Disable again
                            installedVersion = findInstalledModWithId(modToUpdate.getId());
                            installedVersion.toggleActive();
                        }
                    }

                    RefreshLocalMods();
                    RefreshRepoMods();
                }
            }
            else
                MessageBox.Show("All your modules are up to date.", "No updates found");

        }

        private CMod findRepoModWithId(string id)
        {
            if (m_ModsAvailable.ContainsKey(id))
            {
                return m_ModsAvailable[id];
            }
            return null;
        }

        private CMod findInstalledModWithId(string id)
        {
            if(m_ModsInstalled.ContainsKey(id))
            {
                return m_ModsInstalled[id];
            }
            return null;
        }

        private void lbAvail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbAvail.SelectedItem != null)
            {
                CMod selMod = (CMod)lbAvail.SelectedItem;

                if (selMod.getDescription() == null)
                {
                    if( selMod.getDependencies() != null )
                        lblModInfo.Text = "Requires: " + selMod.getDependencies();
                    else
                        lblModInfo.Text = "Double click a module to install / update it.";
                }
                else
                {
                    lblModInfo.Text = selMod.getDescription();
                    if (selMod.getDependencies() != null)
                        lblModInfo.Text += " [Requires: " + selMod.getDependencies() + "]";
                }

                // Select the same module in the local repo if installed
                int oldSel = lbInst.SelectedIndex;
                for( int idx = 0; idx < lbInst.Items.Count; idx++ )
                {
                    CMod itm = (CMod)lbInst.Items[idx];
                    if (itm.getId() == selMod.getId())
                    {
                        lbInst.SelectedIndex = idx;
                        break;
                    }
                }
            }
            else
                lblModInfo.Text = "Double click a module to install / update it.";
        }

        private void pbRefreshLocal_Click(object sender, EventArgs e)
        {
            RefreshLocalMods();
        }

        private void RefreshLocalMods()
        {
            GetInstalledMods();
            ReadModControlFile();
            RenderInstModList();
        }

        private void pbRefreshRepo_Click(object sender, EventArgs e)
        {
            RefreshRepoMods();
        }

        private void RefreshRepoMods()
        {
            UpdateRepo();
            RenderRepoModList();
        }

        private void lbInst_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lbInst_DragDrop(object sender, DragEventArgs e)
        {
            Point point = lbInst.PointToClient(new Point(e.X, e.Y));
            int index = lbInst.IndexFromPoint(point);

            if (index < 0) 
                index = lbInst.Items.Count - 1;
            
            CMod data = (CMod)e.Data.GetData(typeof(CMod));
            lbInst.Items.Remove(data);
            lbInst.Items.Insert(index, data);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteModControlFile();
            Application.Exit();
        }

        private void lbInst_MouseMove(object sender, MouseEventArgs e)
        {
            if (lbInst.SelectedItem == null || e.Button == System.Windows.Forms.MouseButtons.None)
                return;

            if( e.X != m_ptDndStart.X || e.Y != m_ptDndStart.Y )
                lbInst.DoDragDrop(lbInst.SelectedItem, DragDropEffects.Move);
        }

        private void lbInst_MouseDown(object sender, MouseEventArgs e)
        {
            m_ptDndStart = e.Location;
        }

        private void cmAdvanced_Opening(object sender, CancelEventArgs e)
        {

        }

        private void editLifePlayNameListsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditList editorWnd = new frmEditList(frmEditList.EditorType.editNames, m_sPath);
            DialogResult dr = editorWnd.ShowDialog(this);
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {

            Point ptLowerLeft = new Point(0, btnAdvanced.Height);
            cmAdvanced.Show(btnAdvanced, ptLowerLeft);
        }

        private void editLifePlaycasualOutfitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditList editorWnd = new frmEditList(frmEditList.EditorType.editClothesCasual, m_sPath);
            DialogResult dr = editorWnd.ShowDialog(this);
        }

        private void editLifePlayworkOutfitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditList editorWnd = new frmEditList(frmEditList.EditorType.editClothesWork, m_sPath);
            DialogResult dr = editorWnd.ShowDialog(this);
        }

        private void editLifePlaysportsOutfitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditList editorWnd = new frmEditList(frmEditList.EditorType.editClothesSport, m_sPath);
            DialogResult dr = editorWnd.ShowDialog(this);
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://lifeplay.site");
        }
    }
}
