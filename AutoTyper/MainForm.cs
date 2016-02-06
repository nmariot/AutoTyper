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

namespace AutoTyper
{
    /// <summary>
    /// MainForm
    /// </summary>
    public partial class MainForm : Form
    {        
        #region Declarations
        private AutoTyper _typer;
        #endregion
        public MainForm()
        {
            InitializeComponent();
        }


        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnabled.Checked)
            {
                _typer = new AutoTyper();
                _typer.StartTyping(txtTextToType.Text);                
            }
            else
            {
                _typer.StopTyping();                
            }
        }

        /// <summary>
        /// get the scan code relative to the char 'c'
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static byte GetScanCode(char c)
        {
            int keyCode = (int)c;

            // Letters
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
            {
                return (byte)keyCode;
            }

            return (byte)keyCode;
        }
    }
}
