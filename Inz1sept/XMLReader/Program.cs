using System;
using System.Collections.Generic;
using System.Text;

namespace XMLReaderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLPresetReader reader = new XMLPresetReader();
            PresetInformation pi = reader.ReadXMLFile("Abd_shaded_A.xml");
            Console.ReadKey();
            Console.WriteLine(pi.Series[0].ToString());
        }
    }
}
