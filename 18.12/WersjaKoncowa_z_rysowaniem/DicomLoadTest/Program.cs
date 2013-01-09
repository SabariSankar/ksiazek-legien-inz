using System;
using System.Windows.Forms;
using Kitware.VTK;

namespace MainWindow
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //VTK Logging
            var fileOutput = vtkFileOutputWindow.New();
            fileOutput.AppendOff();
            fileOutput.SetFileName("log.txt");
            vtkOutputWindow.SetInstance(fileOutput);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
