namespace RKLauncher
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lbAvail = new System.Windows.Forms.ListBox();
            this.lbInst = new System.Windows.Forms.ListBox();
            this.lblModHint = new System.Windows.Forms.Label();
            this.lblModInfo = new System.Windows.Forms.Label();
            this.splitContainerContent = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelLeft = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelBottomLeft = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.btnUpdateAll = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAvailModules = new System.Windows.Forms.Label();
            this.pbRefreshRepo = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelBottomRight = new System.Windows.Forms.TableLayoutPanel();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnDelMod = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblInstalledModules = new System.Windows.Forms.Label();
            this.pbRefreshLocal = new System.Windows.Forms.PictureBox();
            this.splitContainerHeader = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblLPVersion = new System.Windows.Forms.Label();
            this.cmAdvanced = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editLifePlayNameListsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editLifePlaysportsOutfitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editLifePlayworkOutfitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editLifePlaycasualOutfitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerContent)).BeginInit();
            this.splitContainerContent.Panel1.SuspendLayout();
            this.splitContainerContent.Panel2.SuspendLayout();
            this.splitContainerContent.SuspendLayout();
            this.tableLayoutPanelLeft.SuspendLayout();
            this.tableLayoutPanelBottomLeft.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefreshRepo)).BeginInit();
            this.tableLayoutPanelRight.SuspendLayout();
            this.tableLayoutPanelBottomRight.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefreshLocal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHeader)).BeginInit();
            this.splitContainerHeader.Panel1.SuspendLayout();
            this.splitContainerHeader.Panel2.SuspendLayout();
            this.splitContainerHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.cmAdvanced.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbAvail
            // 
            this.lbAvail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.lbAvail, "lbAvail");
            this.lbAvail.ForeColor = System.Drawing.Color.White;
            this.lbAvail.Name = "lbAvail";
            this.lbAvail.SelectedIndexChanged += new System.EventHandler(this.lbAvail_SelectedIndexChanged);
            this.lbAvail.DoubleClick += new System.EventHandler(this.lbAvail_DoubleClick);
            // 
            // lbInst
            // 
            this.lbInst.AllowDrop = true;
            this.lbInst.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.lbInst, "lbInst");
            this.lbInst.ForeColor = System.Drawing.Color.White;
            this.lbInst.Name = "lbInst";
            this.lbInst.SelectedIndexChanged += new System.EventHandler(this.lbInst_SelectedIndexChanged);
            this.lbInst.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbInst_DragDrop);
            this.lbInst.DragOver += new System.Windows.Forms.DragEventHandler(this.lbInst_DragOver);
            this.lbInst.DoubleClick += new System.EventHandler(this.lbInst_DoubleClick);
            this.lbInst.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbInst_MouseDown);
            this.lbInst.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbInst_MouseMove);
            // 
            // lblModHint
            // 
            this.lblModHint.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lblModHint, "lblModHint");
            this.lblModHint.ForeColor = System.Drawing.Color.BlanchedAlmond;
            this.lblModHint.Name = "lblModHint";
            // 
            // lblModInfo
            // 
            this.lblModInfo.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lblModInfo, "lblModInfo");
            this.lblModInfo.ForeColor = System.Drawing.Color.BlanchedAlmond;
            this.lblModInfo.Name = "lblModInfo";
            // 
            // splitContainerContent
            // 
            resources.ApplyResources(this.splitContainerContent, "splitContainerContent");
            this.splitContainerContent.Name = "splitContainerContent";
            // 
            // splitContainerContent.Panel1
            // 
            this.splitContainerContent.Panel1.Controls.Add(this.tableLayoutPanelLeft);
            // 
            // splitContainerContent.Panel2
            // 
            this.splitContainerContent.Panel2.Controls.Add(this.tableLayoutPanelRight);
            // 
            // tableLayoutPanelLeft
            // 
            resources.ApplyResources(this.tableLayoutPanelLeft, "tableLayoutPanelLeft");
            this.tableLayoutPanelLeft.Controls.Add(this.lbAvail, 0, 1);
            this.tableLayoutPanelLeft.Controls.Add(this.lblModInfo, 0, 2);
            this.tableLayoutPanelLeft.Controls.Add(this.tableLayoutPanelBottomLeft, 0, 3);
            this.tableLayoutPanelLeft.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            // 
            // tableLayoutPanelBottomLeft
            // 
            resources.ApplyResources(this.tableLayoutPanelBottomLeft, "tableLayoutPanelBottomLeft");
            this.tableLayoutPanelBottomLeft.Controls.Add(this.btnAdvanced, 0, 0);
            this.tableLayoutPanelBottomLeft.Controls.Add(this.btnUpdateAll, 0, 0);
            this.tableLayoutPanelBottomLeft.Name = "tableLayoutPanelBottomLeft";
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.BackColor = System.Drawing.Color.Blue;
            this.btnAdvanced.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnAdvanced, "btnAdvanced");
            this.btnAdvanced.ForeColor = System.Drawing.Color.White;
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.UseVisualStyleBackColor = false;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // btnUpdateAll
            // 
            this.btnUpdateAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnUpdateAll.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnUpdateAll, "btnUpdateAll");
            this.btnUpdateAll.ForeColor = System.Drawing.Color.White;
            this.btnUpdateAll.Name = "btnUpdateAll";
            this.btnUpdateAll.UseVisualStyleBackColor = false;
            this.btnUpdateAll.Click += new System.EventHandler(this.btnUpdateAll_Click);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.lblAvailModules, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pbRefreshRepo, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // lblAvailModules
            // 
            resources.ApplyResources(this.lblAvailModules, "lblAvailModules");
            this.lblAvailModules.BackColor = System.Drawing.Color.Black;
            this.lblAvailModules.ForeColor = System.Drawing.Color.White;
            this.lblAvailModules.Name = "lblAvailModules";
            // 
            // pbRefreshRepo
            // 
            this.pbRefreshRepo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRefreshRepo.Image = global::RKLauncher.Properties.Resources.arrow_refresh_small;
            resources.ApplyResources(this.pbRefreshRepo, "pbRefreshRepo");
            this.pbRefreshRepo.Name = "pbRefreshRepo";
            this.pbRefreshRepo.TabStop = false;
            this.pbRefreshRepo.Click += new System.EventHandler(this.pbRefreshRepo_Click);
            // 
            // tableLayoutPanelRight
            // 
            resources.ApplyResources(this.tableLayoutPanelRight, "tableLayoutPanelRight");
            this.tableLayoutPanelRight.Controls.Add(this.lbInst, 0, 1);
            this.tableLayoutPanelRight.Controls.Add(this.lblModHint, 0, 2);
            this.tableLayoutPanelRight.Controls.Add(this.tableLayoutPanelBottomRight, 0, 3);
            this.tableLayoutPanelRight.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            // 
            // tableLayoutPanelBottomRight
            // 
            resources.ApplyResources(this.tableLayoutPanelBottomRight, "tableLayoutPanelBottomRight");
            this.tableLayoutPanelBottomRight.Controls.Add(this.btnLaunch, 0, 0);
            this.tableLayoutPanelBottomRight.Controls.Add(this.btnDelMod, 0, 0);
            this.tableLayoutPanelBottomRight.Name = "tableLayoutPanelBottomRight";
            // 
            // btnLaunch
            // 
            this.btnLaunch.BackColor = System.Drawing.Color.Green;
            this.btnLaunch.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnLaunch, "btnLaunch");
            this.btnLaunch.ForeColor = System.Drawing.Color.White;
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.UseVisualStyleBackColor = false;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnDelMod
            // 
            this.btnDelMod.BackColor = System.Drawing.Color.Red;
            this.btnDelMod.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btnDelMod, "btnDelMod");
            this.btnDelMod.ForeColor = System.Drawing.Color.White;
            this.btnDelMod.Name = "btnDelMod";
            this.btnDelMod.UseVisualStyleBackColor = false;
            this.btnDelMod.Click += new System.EventHandler(this.btnDelMod_Click);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblInstalledModules, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbRefreshLocal, 1, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lblInstalledModules
            // 
            resources.ApplyResources(this.lblInstalledModules, "lblInstalledModules");
            this.lblInstalledModules.BackColor = System.Drawing.Color.Black;
            this.lblInstalledModules.ForeColor = System.Drawing.Color.White;
            this.lblInstalledModules.Name = "lblInstalledModules";
            // 
            // pbRefreshLocal
            // 
            this.pbRefreshLocal.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pbRefreshLocal, "pbRefreshLocal");
            this.pbRefreshLocal.Image = global::RKLauncher.Properties.Resources.arrow_refresh_small;
            this.pbRefreshLocal.Name = "pbRefreshLocal";
            this.pbRefreshLocal.TabStop = false;
            this.pbRefreshLocal.Click += new System.EventHandler(this.pbRefreshLocal_Click);
            // 
            // splitContainerHeader
            // 
            resources.ApplyResources(this.splitContainerHeader, "splitContainerHeader");
            this.splitContainerHeader.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerHeader.Name = "splitContainerHeader";
            // 
            // splitContainerHeader.Panel1
            // 
            this.splitContainerHeader.Panel1.Controls.Add(this.label1);
            this.splitContainerHeader.Panel1.Controls.Add(this.pictureBox2);
            this.splitContainerHeader.Panel1.Controls.Add(this.lblLPVersion);
            // 
            // splitContainerHeader.Panel2
            // 
            this.splitContainerHeader.Panel2.Controls.Add(this.splitContainerContent);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label1.Name = "label1";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // lblLPVersion
            // 
            this.lblLPVersion.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lblLPVersion, "lblLPVersion");
            this.lblLPVersion.ForeColor = System.Drawing.Color.White;
            this.lblLPVersion.Name = "lblLPVersion";
            // 
            // cmAdvanced
            // 
            this.cmAdvanced.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editLifePlayNameListsToolStripMenuItem,
            this.editLifePlaysportsOutfitsToolStripMenuItem,
            this.editLifePlayworkOutfitsToolStripMenuItem,
            this.editLifePlaycasualOutfitsToolStripMenuItem});
            this.cmAdvanced.Name = "cmAdvanced";
            resources.ApplyResources(this.cmAdvanced, "cmAdvanced");
            this.cmAdvanced.Opening += new System.ComponentModel.CancelEventHandler(this.cmAdvanced_Opening);
            // 
            // editLifePlayNameListsToolStripMenuItem
            // 
            this.editLifePlayNameListsToolStripMenuItem.Name = "editLifePlayNameListsToolStripMenuItem";
            resources.ApplyResources(this.editLifePlayNameListsToolStripMenuItem, "editLifePlayNameListsToolStripMenuItem");
            this.editLifePlayNameListsToolStripMenuItem.Click += new System.EventHandler(this.editLifePlayNameListsToolStripMenuItem_Click);
            // 
            // editLifePlaysportsOutfitsToolStripMenuItem
            // 
            this.editLifePlaysportsOutfitsToolStripMenuItem.Name = "editLifePlaysportsOutfitsToolStripMenuItem";
            resources.ApplyResources(this.editLifePlaysportsOutfitsToolStripMenuItem, "editLifePlaysportsOutfitsToolStripMenuItem");
            this.editLifePlaysportsOutfitsToolStripMenuItem.Click += new System.EventHandler(this.editLifePlaysportsOutfitsToolStripMenuItem_Click);
            // 
            // editLifePlayworkOutfitsToolStripMenuItem
            // 
            this.editLifePlayworkOutfitsToolStripMenuItem.Name = "editLifePlayworkOutfitsToolStripMenuItem";
            resources.ApplyResources(this.editLifePlayworkOutfitsToolStripMenuItem, "editLifePlayworkOutfitsToolStripMenuItem");
            this.editLifePlayworkOutfitsToolStripMenuItem.Click += new System.EventHandler(this.editLifePlayworkOutfitsToolStripMenuItem_Click);
            // 
            // editLifePlaycasualOutfitsToolStripMenuItem
            // 
            this.editLifePlaycasualOutfitsToolStripMenuItem.Name = "editLifePlaycasualOutfitsToolStripMenuItem";
            resources.ApplyResources(this.editLifePlaycasualOutfitsToolStripMenuItem, "editLifePlaycasualOutfitsToolStripMenuItem");
            this.editLifePlaycasualOutfitsToolStripMenuItem.Click += new System.EventHandler(this.editLifePlaycasualOutfitsToolStripMenuItem_Click);
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel1.LinkColor = System.Drawing.Color.PaleTurquoise;
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.White;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkLabel1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitContainerHeader);
            this.DoubleBuffered = true;
            this.Name = "frmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainerContent.Panel1.ResumeLayout(false);
            this.splitContainerContent.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerContent)).EndInit();
            this.splitContainerContent.ResumeLayout(false);
            this.tableLayoutPanelLeft.ResumeLayout(false);
            this.tableLayoutPanelBottomLeft.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefreshRepo)).EndInit();
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.tableLayoutPanelBottomRight.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefreshLocal)).EndInit();
            this.splitContainerHeader.Panel1.ResumeLayout(false);
            this.splitContainerHeader.Panel1.PerformLayout();
            this.splitContainerHeader.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHeader)).EndInit();
            this.splitContainerHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.cmAdvanced.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbAvail;
        private System.Windows.Forms.ListBox lbInst;
        private System.Windows.Forms.Label lblModHint;
        private System.Windows.Forms.Label lblModInfo;
        private System.Windows.Forms.SplitContainer splitContainerContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLeft;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.SplitContainer splitContainerHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottomLeft;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBottomRight;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnDelMod;
        private System.Windows.Forms.Button btnUpdateAll;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblAvailModules;
        private System.Windows.Forms.PictureBox pbRefreshRepo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblInstalledModules;
        private System.Windows.Forms.PictureBox pbRefreshLocal;
        private System.Windows.Forms.Label lblLPVersion;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.ContextMenuStrip cmAdvanced;
        private System.Windows.Forms.ToolStripMenuItem editLifePlayNameListsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLifePlaysportsOutfitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLifePlayworkOutfitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLifePlaycasualOutfitsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel panel1;
    }
}

