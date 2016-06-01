using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElevatorSaga
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string assemblyPath = "";
            for (int i = 0; i < args.Length; i++)
            {
                if (assemblyPath == "next")
                {
                    assemblyPath = args[i];
                    break;
                }
                if (args[i] == "/dll")
                    assemblyPath = "next";
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.MainForm(assemblyPath));
        }
    }
}
