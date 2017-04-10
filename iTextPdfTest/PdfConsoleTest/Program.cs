using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.pdf");

            if (File.Exists(testFile))
            {
                File.Delete(testFile);
            }

            var output = PdfHelpers.PdfHelper.CreatePdf();
            System.IO.File.WriteAllBytes(testFile, output);
        }
    }
}
