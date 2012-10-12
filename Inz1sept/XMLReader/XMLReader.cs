using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;

namespace XMLReaderTest
{
    /// <summary>
    /// Open and reads xml file with preset
    /// </summary>
    public class XMLPresetReader
    {
        /// <summary>
        /// Handler to the open xml file
        /// </summary>
        private XmlTextReader reader;

        /// <summary>
        /// Reads opacity function from the open xml
        /// </summary>
        /// <param name="colorFunction">Color function which will be appply to loaded opacity function</param>
        /// <param name="fileName">Name of open xml</param>
        /// <returns>Information about the preset, which includes series of color and opacity function</returns>
        public PresetInformation ReadOpacityFunction(String fileName, Dictionary<float, Color[]> colorFunction)
        {
            PresetInformation presetInformation = new PresetInformation();
            reader = new XmlTextReader(@"..\..\presety\" + fileName);

            if (reader.ReadToFollowing("OpacityFunctions"))
                Console.WriteLine("<Opacity Function>");

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && reader.Name.Equals("PointList"))
                {
                    Console.WriteLine("<PointList>"); ;
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
                            Console.WriteLine("</PointList>");
                            presetInformation.AddSerie(opacityFunction,colorFunction);
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
            return presetInformation;
        }

        /// <summary>
        /// Helper method. Reads the color block from xml file.
        /// </summary>
        /// <returns> Loaded color. </returns>
        private Color ReadColor()
        {
            reader.ReadToFollowing("ROT");
            float R = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));
            reader.ReadToFollowing("GRUEN");
            float G = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));
            reader.ReadToFollowing("BLAU");
            float B = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));
            //Console.WriteLine(R + " " + G + " " + B);
            return Color.FromArgb((int)R, (int)G, (int)B);
        }

        /// <summary>
        /// Reads color function from the open xml. 
        /// </summary>
        /// <returns>Color function.</returns>
        public Dictionary<float, Color[]> ReadColorFunction()
        {
            Dictionary<float, Color[]> colorFunction = new Dictionary<float, Color[]>();

            if (reader.ReadToFollowing("ColorLut"))
                //Console.WriteLine("<ColorLut>");

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && reader.Name.Equals("PointList"))
                {
                    //Console.WriteLine("<PointList>"); 
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && reader.Name.Equals("First"))
                        {
                            Color[] colorArray = new Color[2];
                            float intensity = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));
                            //Left Color
                            reader.ReadToFollowing("LeftColor");
                            colorArray[0] = ReadColor();
                            //Right Color
                            reader.ReadToFollowing("RightColor");
                            colorArray[1] = ReadColor();
                            colorFunction.Add(intensity, colorArray);
                        }
                        if ((reader.NodeType == XmlNodeType.EndElement) && reader.Name.Equals("PointList"))
                        {
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
            return colorFunction;
        }


        /// <summary>
        /// Reads opacity and color function from the open xml. 
        /// </summary>
        /// <returns>Information about the preset, which includes series of color and opacity function.</returns>
        public PresetInformation ReadOpacityAndColorFunction()
        {
            PresetInformation presetInformation = new PresetInformation();

            if (reader.ReadToFollowing("ColoredFunctions"))
                //Console.WriteLine("<ColoredFunctions>");

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && reader.Name.Equals("PointList"))
                {
                    //Console.WriteLine("<PointList>");
                    Dictionary<float, float> opacityFunction = new Dictionary<float, float>();
                    Dictionary<float, Color[]> colorFunction = new Dictionary<float, Color[]>();
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element) && reader.Name.Equals("First"))
                        {
                            Color[] colorArray = new Color[2];
                            float first = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));
                            reader.ReadToFollowing("LeftColor");
                            colorArray[0] = ReadColor();
                            reader.ReadToFollowing("RightColor");
                            colorArray[1] = ReadColor();
                            reader.ReadToFollowing("Opacity");
                            float opacity = float.Parse(reader.ReadElementContentAsString().Replace('.', ','));

                            opacityFunction.Add(first, opacity);
                            colorFunction.Add(first, colorArray);
                        }               
                        
                        if ((reader.NodeType == XmlNodeType.EndElement) && reader.Name.Equals("PointList"))
                        {
                            //Console.WriteLine("</PointList>");
                            presetInformation.AddSerie(opacityFunction, colorFunction);
                            break;
                        }
                    }
                }

                if ((reader.NodeType == XmlNodeType.EndElement) && reader.Name.Equals("ColoredFunctions"))
                {
                    //Console.WriteLine("</ColoredFunctions>"); ;
                    break;
                }
            }
            return presetInformation;
        }


        /// <summary>
        /// Determinates which version of the xml if this current file, and read color and opacity function from it. 
        /// </summary>
        /// <param name="fileName">Name of the preset</param>
        /// <returns>Information about the preset, which includes series of color and opacity function</returns>
        public PresetInformation ReadXMLFile(String fileName)
        {
            reader = new XmlTextReader(@"..\..\presety\" + fileName);
            reader.ReadToFollowing("IndependentColor");
            Boolean isIndependent = reader.ReadElementContentAsBoolean();

            if (isIndependent)
            {
                return ReadOpacityFunction(fileName, ReadColorFunction());
            }
            else
            {
                return ReadOpacityAndColorFunction();
            }
        }
    }

}
