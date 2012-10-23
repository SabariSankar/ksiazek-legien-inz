using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Kitware.VTK;

namespace DicomLoadTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Logging
            var fileOutput = vtkFileOutputWindow.New();
            fileOutput.AppendOff();
            fileOutput.SetFileName("log.txt");
            vtkOutputWindow.SetInstance(fileOutput);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
