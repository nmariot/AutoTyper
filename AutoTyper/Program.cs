using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTyper
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string scenario = "AutoTyper.xml";
            if (args.Length >= 1)
            {
                scenario = args[0];
            }
            Application.Run(new MainForm(scenario));            
        }
    }
}
