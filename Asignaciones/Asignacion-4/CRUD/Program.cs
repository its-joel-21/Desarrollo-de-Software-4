using System;
using System.Windows.Forms;
using CrudWindowsForms.Forms;

namespace CrudWindowsForms
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Interface_1());
        }
    }
}