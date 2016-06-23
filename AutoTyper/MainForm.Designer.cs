namespace AutoTyper
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblInfo = new System.Windows.Forms.Label();
            this.cboKey = new System.Windows.Forms.ComboBox();
            this.btnLoadScenarii = new System.Windows.Forms.Button();
            this.rtbTextToType = new System.Windows.Forms.RichTextBox();
            this.niTaskBar = new System.Windows.Forms.NotifyIcon(this.components);
            this.mnuNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuOpenWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoadScenarii = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.txtNbLettersTyped = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuNotifyIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNbLettersTyped)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(13, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(539, 17);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "AutoTyper stopped. Start scenario using Ctrl+Shift+function key (F1-F12)";
            // 
            // cboKey
            // 
            this.cboKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKey.FormattingEnabled = true;
            this.cboKey.Items.AddRange(new object[] {
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11",
            "F12"});
            this.cboKey.Location = new System.Drawing.Point(16, 30);
            this.cboKey.MaxDropDownItems = 12;
            this.cboKey.Name = "cboKey";
            this.cboKey.Size = new System.Drawing.Size(164, 24);
            this.cboKey.TabIndex = 0;
            this.cboKey.SelectedIndexChanged += new System.EventHandler(this.cboKey_SelectedIndexChanged);
            // 
            // btnLoadScenarii
            // 
            this.btnLoadScenarii.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadScenarii.Location = new System.Drawing.Point(398, 29);
            this.btnLoadScenarii.Name = "btnLoadScenarii";
            this.btnLoadScenarii.Size = new System.Drawing.Size(164, 24);
            this.btnLoadScenarii.TabIndex = 2;
            this.btnLoadScenarii.Text = "Load scenarii";
            this.btnLoadScenarii.UseVisualStyleBackColor = true;
            this.btnLoadScenarii.Click += new System.EventHandler(this.btnLoadScenarii_Click);
            // 
            // rtbTextToType
            // 
            this.rtbTextToType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbTextToType.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbTextToType.Location = new System.Drawing.Point(16, 60);
            this.rtbTextToType.Name = "rtbTextToType";
            this.rtbTextToType.ReadOnly = true;
            this.rtbTextToType.Size = new System.Drawing.Size(546, 212);
            this.rtbTextToType.TabIndex = 3;
            this.rtbTextToType.Text = "";
            // 
            // niTaskBar
            // 
            this.niTaskBar.ContextMenuStrip = this.mnuNotifyIcon;
            this.niTaskBar.Icon = ((System.Drawing.Icon)(resources.GetObject("niTaskBar.Icon")));
            this.niTaskBar.Text = "AutoTyper - Stopped";
            this.niTaskBar.Visible = true;
            this.niTaskBar.DoubleClick += new System.EventHandler(this.niTaskBar_DoubleClick);
            // 
            // mnuNotifyIcon
            // 
            this.mnuNotifyIcon.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenWindow,
            this.mnuLoadScenarii,
            this.mnuQuit});
            this.mnuNotifyIcon.Name = "mnuNotifyIcon";
            this.mnuNotifyIcon.Size = new System.Drawing.Size(177, 82);
            // 
            // mnuOpenWindow
            // 
            this.mnuOpenWindow.Name = "mnuOpenWindow";
            this.mnuOpenWindow.Size = new System.Drawing.Size(176, 26);
            this.mnuOpenWindow.Text = "Open window";
            this.mnuOpenWindow.Click += new System.EventHandler(this.mnuOpenWindow_Click);
            // 
            // mnuLoadScenarii
            // 
            this.mnuLoadScenarii.Name = "mnuLoadScenarii";
            this.mnuLoadScenarii.Size = new System.Drawing.Size(176, 26);
            this.mnuLoadScenarii.Text = "Load scenarii";
            this.mnuLoadScenarii.Click += new System.EventHandler(this.btnLoadScenarii_Click);
            // 
            // mnuQuit
            // 
            this.mnuQuit.Name = "mnuQuit";
            this.mnuQuit.Size = new System.Drawing.Size(176, 26);
            this.mnuQuit.Text = "Quit";
            this.mnuQuit.Click += new System.EventHandler(this.mnuQuit_Click);
            // 
            // txtNbLettersTyped
            // 
            this.txtNbLettersTyped.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNbLettersTyped.Location = new System.Drawing.Point(315, 29);
            this.txtNbLettersTyped.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtNbLettersTyped.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNbLettersTyped.Name = "txtNbLettersTyped";
            this.txtNbLettersTyped.Size = new System.Drawing.Size(77, 22);
            this.txtNbLettersTyped.TabIndex = 1;
            this.txtNbLettersTyped.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNbLettersTyped.ValueChanged += new System.EventHandler(this.txtNbLettersTyped_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nb of letters :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 284);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNbLettersTyped);
            this.Controls.Add(this.rtbTextToType);
            this.Controls.Add(this.btnLoadScenarii);
            this.Controls.Add(this.cboKey);
            this.Controls.Add(this.lblInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoTyper";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.mnuNotifyIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNbLettersTyped)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ComboBox cboKey;
        private System.Windows.Forms.Button btnLoadScenarii;
        private System.Windows.Forms.RichTextBox rtbTextToType;
        private System.Windows.Forms.NotifyIcon niTaskBar;
        private System.Windows.Forms.ContextMenuStrip mnuNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem mnuQuit;
        private System.Windows.Forms.ToolStripMenuItem mnuLoadScenarii;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenWindow;
        private System.Windows.Forms.NumericUpDown txtNbLettersTyped;
        private System.Windows.Forms.Label label1;
    }
}

