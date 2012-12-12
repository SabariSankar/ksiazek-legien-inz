using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Globalization;

namespace DicomLoadTest
{
    public class PresetMapper
    {
        private delegate Dictionary<int, float[]> function();
        private List<String> functionMapper = new List<String>();

        public List<String> Presets
        {
            get { return this.functionMapper; }
        }

        public PresetMapper()
        {
            string[] filePaths = Directory.GetFiles(@"presety", "*.xml");

            //foreach (string dir in filePaths)
           // {
                //functionMapper[new FileInfo(dir).Name] = Abd_shaded_A;
                functionMapper.Add("Carotid_sha_A_kopia.xml");
           // }
        }

        public Dictionary<float, float> changeOpacityFunction(String nameOfPreset)
        {
            //Load xml
            XmlDocument doc = new XmlDocument();
            doc.Load("..//..//presety//Carotid_sha_A_kopia.xml");

            Dictionary<float, float> dict = new Dictionary<float, float>();

            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";

            //Run query
            XmlNodeList elemList = doc.GetElementsByTagName("Value");
            for (int i = 0; i < elemList.Count; i++)
            {
                float first = float.Parse(elemList[i].Attributes["First"].Value, NumberStyles.Any, ci);
                float second = float.Parse(elemList[i].Attributes["Second"].Value, NumberStyles.Any, ci);

                dict[first] = second;
            }

            return dict;
        }

        public Dictionary<int, float[]> changeColorFunction(String nameOfPreset)
        {
            //Load xml
            XmlDocument doc = new XmlDocument();
            doc.Load("..//..//presety//Carotid_sha_A_kopia.xml");

            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";

            Dictionary<int, float[]> dict = new Dictionary<int, float[]>();

            //Run query
            XmlNodeList elemList = doc.GetElementsByTagName("Node");
            for (int i = 0; i < elemList.Count; i++)
            {
                int intensity = int.Parse(elemList[i].Attributes["Val"].Value, NumberStyles.Any, ci);
                float R = float.Parse(elemList[i].Attributes["ROT"].Value, NumberStyles.Any, ci);
                float G = float.Parse(elemList[i].Attributes["GRUEN"].Value, NumberStyles.Any, ci);
                float B = float.Parse(elemList[i].Attributes["BLUA"].Value, NumberStyles.Any, ci);

                float[] RGB = { R, G, B };
                dict[intensity] = RGB;
            }

            return dict;
        }
    }
}
