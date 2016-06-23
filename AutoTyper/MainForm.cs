using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Xml.Linq;
using System.Resources;
using AutoTyper.Properties;
using System.Reflection;

namespace AutoTyper
{
    /// <summary>
    /// MainForm
    /// </summary>
    public partial class MainForm : Form
    {
        #region Declarations
        private AutoTyper _typer;
        private string[] _autoTypedText;
        private const string MSGBOX_TITLE = "AutoTyper";
        private string _scenario;
        #endregion

        public MainForm(string scenario)
        {
            InitializeComponent();

            this.Text = $"AutoTyper (V{Assembly.GetExecutingAssembly().GetName().Version})";
            _scenario = scenario;
        }

        static readonly List<string> FUNCTION_KEYS = new List<string> { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" };

        private bool InitializeAutoTyper(string file)
        {
            try
            {
                _typer_Stopped(null, null);

                // Dispose old autotyper first
                if (_typer != null) _typer.Dispose();

                XDocument doc = XDocument.Load(file);
                _autoTypedText = new string[FUNCTION_KEYS.Count];
                foreach (var eltKey in doc.Root.Elements("Key"))
                {
                    string key = eltKey.Attribute("value").Value;
                    int numKey = FUNCTION_KEYS.IndexOf(key);
                    if (numKey >= 0 && numKey <= 11)
                    {
                        // Reading CDATA information
                        var cdata = (from n in eltKey.Nodes() where n is XCData select n).FirstOrDefault();
                        if (cdata != null) _autoTypedText[numKey] = (cdata as XCData).Value.Replace("\n", "\r\n");
                    }
                }
                cboKey.SelectedIndex = 0;
                _typer = new AutoTyper(_autoTypedText);
                _typer.Started += _typer_Started;
                _typer.Stopped += _typer_Stopped;
                _typer.KeyStroke += _typer_KeyStroke;
                _typer.NbOfLettersTypedChanged += _typer_NbOfLettersTypedChanged;
                RemoveSelectionColor();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while reading config file '{file}'\r\n{ex.Message}", MSGBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        private void _typer_NbOfLettersTypedChanged(object sender, int e)
        {
            txtNbLettersTyped.Value = e;
        }

        private void _typer_KeyStroke(object sender, int e)
        {
            rtbTextToType.Select(0, e);
            rtbTextToType.SelectionBackColor = Color.Yellow;
        }

        private void _typer_Stopped(object sender, EventArgs e)
        {
            lblInfo.Text = "AutoTyper stopped.Start scenario using Ctrl + Shift + function key(F1 - F12)";
            niTaskBar.Text = $"AutoTyper - Stopped";
            niTaskBar.Icon = Resources.AutoTyper;
            cboKey.Enabled = true;
        }

        private void _typer_Started(object sender, int e)
        {
            lblInfo.Text = "AutoTyper started. Change scenario using Ctrl + Shift + function key(F1 - F12)";            
            cboKey.SelectedIndex = e;
            cboKey.Enabled = false;
            rtbTextToType.SelectAll();
            rtbTextToType.SelectionBackColor = rtbTextToType.BackColor;
            string firstChars = rtbTextToType.Text.Length > 20 ? rtbTextToType.Text.Substring(0, 20) + "..." : rtbTextToType.Text;
            niTaskBar.Text = $"AutoTyper - Started using F{(e + 1).ToString()}\n{firstChars}";
            switch (e)
            {
                case 0:
                    niTaskBar.Icon = Resources.F1;
                    break;
                case 1:
                    niTaskBar.Icon = Resources.F2;
                    break;
                case 2:
                    niTaskBar.Icon = Resources.F3;
                    break;
                case 3:
                    niTaskBar.Icon = Resources.F4;
                    break;
                case 4:
                    niTaskBar.Icon = Resources.F5;
                    break;
                case 5:
                    niTaskBar.Icon = Resources.F6;
                    break;
                case 6:
                    niTaskBar.Icon = Resources.F7;
                    break;
                case 7:
                    niTaskBar.Icon = Resources.F8;
                    break;
                case 8:
                    niTaskBar.Icon = Resources.F9;
                    break;
                case 9:
                    niTaskBar.Icon = Resources.F10;
                    break;
                case 10:
                    niTaskBar.Icon = Resources.F11;
                    break;
                case 11:
                    niTaskBar.Icon = Resources.F12;
                    break;
            }

        }

        private void cboKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveSelectionColor();
            rtbTextToType.Text = _autoTypedText[cboKey.SelectedIndex];
        }

        private void RemoveSelectionColor()
        {
            rtbTextToType.SelectAll();
            rtbTextToType.SelectionBackColor = rtbTextToType.BackColor;
        }

        private void btnLoadScenarii_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;
                dlg.Title = "Choose file containing autotyper scenarii";
                dlg.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    InitializeAutoTyper(dlg.FileName);
                }
            }
        }

        private void niTaskBar_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Debug.WriteLine("MainForm_Resize : FormWindowState = " + this.WindowState);
            switch (this.WindowState)
            {
                case FormWindowState.Minimized:
                    this.ShowInTaskbar = false;
                    break;
                case FormWindowState.Normal:
                    this.ShowInTaskbar = true;
                    break;
            }
        }

        private void mnuQuit_Click(object sender, EventArgs e)
        {
            _typer.Dispose();
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!InitializeAutoTyper(_scenario)) this.Close();
        }

        private void mnuOpenWindow_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void txtNbLettersTyped_ValueChanged(object sender, EventArgs e)
        {
            _typer.NbOfLettersTyped = (int)txtNbLettersTyped.Value;
        }
    }
}
