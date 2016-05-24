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
        #endregion

        public MainForm()
        {
            InitializeComponent();

            InitializeAutoTyper("AutoTyper.xml");
        }

        static readonly List<string> FUNCTION_KEYS = new List<string> { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" };

        private void InitializeAutoTyper(string file)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while reading config file '{file}'\r\n{ex.Message}", MSGBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _typer_KeyStroke(object sender, int e)
        {
            rtbTextToType.Select(0, e);
            rtbTextToType.SelectionBackColor = Color.Yellow;
        }

        private void _typer_Stopped(object sender, EventArgs e)
        {
            lblInfo.Text = "AutoTyper stopped. Start scenario using function keys (F1-F12)";            
        }

        private void _typer_Started(object sender, int e)
        {
            lblInfo.Text = "AutoTyper started. Change scenario using function keys (F1-F12)";
            cboKey.SelectedIndex = e;
            rtbTextToType.SelectAll();
            rtbTextToType.SelectionBackColor = rtbTextToType.BackColor;
        }

        private void cboKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbTextToType.Text = _autoTypedText[cboKey.SelectedIndex];
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
    }
}
