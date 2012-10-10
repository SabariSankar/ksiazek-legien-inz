using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace XMLReaderTest
{
    public class XMLPresetReader
    {
        private PresetInformation presetInformation;
        public List<String> Presets { get; set; }


        public XMLPresetReader()
        {
            this.Presets = new List<string>();
            string[] filePaths = Directory.GetFiles(@"..\..\presety", "*.xml");

            foreach (string dir in filePaths)
            {
                //functionMapper[new FileInfo(dir).Name] = Abd_shaded_A;
                Presets.Add(new FileInfo(dir).Name);
            }
            return;
        }

        public void ReadOpacityFunction(String fileName)
        {
            //  Load the XML file
            XmlTextReader reader = new XmlTextReader(@"..\..\presety\" + fileName);
   
            if (reader.ReadToFollowing("OpacityFunctions"))
                //Console.WriteLine("<Opacity Function>");

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && reader.Name.Equals("PointList"))
                {
                    //Console.WriteLine("<PointList>"); ;
                    Dictionary<float, float> opacityFunction = new Dictionary<float, float>();
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && reader.Name.Equals("First"))
                        {
                            float first = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));
                            reader.ReadToFollowing("Second");
                            float second = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));
                            opacityFunction.Add(first, second);
                            //Console.WriteLine(first + " " + second);
                        }
                        if ((reader.NodeType == XmlNodeType.EndElement) && reader.Name.Equals("PointList"))
                        {
                            //Console.WriteLine("</PointList>");
                            presetInformation.AddSerie(opacityFunction);
                            //Console.WriteLine(presetInformation.Series.Count.ToString());
                            break;
                        }
                    }
                }
                if ((reader.NodeType == XmlNodeType.EndElement) && reader.Name.Equals("OpacityFunctions"))
                {
                    //Console.WriteLine("</OpacityFunctions>"); ;
                    break;
                }
            }
        }

        private Color ReadColor(XmlReader reader)
        {
            reader.ReadToFollowing("ROT");
            float R = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));
            reader.ReadToFollowing("GRUEN");
            float G = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));
            reader.ReadToFollowing("BLAU");
            float B = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));
            //Console.WriteLine(R + " " + G + " " + B);
            return new Color(R, G, B);
        }

        public void ReadColorFunction(String fileName)
        {
            //  Load the XML file
            XmlTextReader reader = new XmlTextReader(@"..\..\presety\" + fileName);

            if (reader.ReadToFollowing("ColorLut"))
                //Console.WriteLine("<ColorLut>");

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && reader.Name.Equals("PointList"))
                {
                    //Console.WriteLine("<PointList>"); ;
                    Dictionary<float, float> opacityFunction = new Dictionary<float, float>();
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && reader.Name.Equals("First"))
                        {
                            Color[] colorArray = new Color[2];
                            float intensity = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));
                            //Left Color
                            reader.ReadToFollowing("LeftColor");
                            colorArray[0] = ReadColor(reader);
                            //Right Color
                            reader.ReadToFollowing("RightColor");
                            colorArray[1] = ReadColor(reader);
                            presetInformation.ColorFuction.Add(intensity, colorArray);
                        }
                        if ((reader.NodeType == XmlNodeType.EndElement) && reader.Name.Equals("PointList"))
                        {
                            //Console.WriteLine("</PointList>");
                            presetInformation.AddSerie(opacityFunction);
                            break;
                        }
                    }
                }
                if ((reader.NodeType == XmlNodeType.EndElement) && reader.Name.Equals("ColorLut"))
                {
                    //Console.WriteLine("</ColorLut>"); ;
                    break;
                }
            }
        }

        public PresetInformation ReadXMLFile(String fileName)
        {
            presetInformation = new PresetInformation();
            ReadOpacityFunction(fileName);
            ReadColorFunction(fileName);

            return presetInformation;
        }
    }

}
