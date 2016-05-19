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
        #endregion

        public MainForm()
        {
            InitializeComponent();

            InitializeAutoTyper();

            cboKey.SelectedIndex = 0;
        }

        static readonly List<string> FUNCTION_KEYS = new List<string> { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" };

        private void InitializeAutoTyper()
        {
            XDocument doc = XDocument.Load("autotyper.xml");
            _autoTypedText = new string[FUNCTION_KEYS.Count];
            foreach (var elt in doc.Root.Elements("Key"))
            {
                string key = elt.Attribute("value").Value;
                int numKey = FUNCTION_KEYS.IndexOf(key);
                if (numKey >= 0 && numKey <= 11)
                {
                    _autoTypedText[numKey] = (elt.FirstNode as XCData).Value.Replace("\n", "\r\n");
                }                
            }
            _typer = new AutoTyper(_autoTypedText);
            _typer.StartAutoTyping();
        }

        private void cboKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTextToType.Text = _autoTypedText[cboKey.SelectedIndex];
        }
    }
}
