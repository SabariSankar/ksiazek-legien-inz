using System;

namespace XMLReaderTest
{
    class Program
    {
        static void Main()
        {
            XmlPresetReader reader = new XmlPresetReader();
            PresetInformation pi = reader.ReadXmlFile("Abd_shaded_A.xml");
            Console.ReadKey();
            Console.WriteLine(pi.Series[0].ToString());
        }
    }
}
