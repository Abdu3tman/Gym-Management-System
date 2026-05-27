using System;
using System.Windows.Forms;

namespace GYM2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Load saved data before showing any form
            GymData.Load();

            Application.Run(new Form1());
        }
    }
}
