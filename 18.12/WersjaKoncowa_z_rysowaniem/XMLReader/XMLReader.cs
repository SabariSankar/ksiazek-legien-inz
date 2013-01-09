using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Drawing;

namespace XMLReaderModule
{
    /// <summary>
    /// Open and reads xml file with preset
    /// </summary>
    public class XmlPresetReader
    {
        /// <summary>
        /// Handler to the open xml file
        /// </summary>
        private XmlTextReader _reader;

        /// <summary>
        /// Reads opacity function from the open xml
        /// </summary>
        /// <param name="colorFunction">Color function which will be appply to loaded opacity function</param>
        /// <param name="fileName">Name of open xml</param>
        /// <returns>Information about the preset, which includes series of color and opacity function</returns>
        private PresetInformation ReadOpacityFunction(String fileName, Dictionary<float, Color[]> colorFunction)
        {
            PresetInformation presetInformation = new PresetInformation();
            _reader = new XmlTextReader(@"..\..\presety\" + fileName);

            while (_reader.Read())
            {
                if ((_reader.NodeType == XmlNodeType.Element) && _reader.Name.Equals("PointList"))
                {
                    Console.WriteLine("<PointList>"); ;
                    Dictionary<float, float> opacityFunction = new Dictionary<float, float>();
                    while (_reader.Read())
                    {
                        if ((_reader.NodeType == XmlNodeType.Element) && _reader.Name.Equals("First"))
                        {
                            float first = float.Parse(_reader.ReadElementContentAsString().Replace('.', ','));
                            _reader.ReadToFollowing("Second");
                            float second = float.Parse(_reader.ReadElementContentAsString().Replace('.', ','));
                            opacityFunction.Add(first, second);
                        }
                        if ((_reader.NodeType == XmlNodeType.EndElement) && _reader.Name.Equals("PointList"))
                        {
                            Console.WriteLine("</PointList>");
                            presetInformation.AddSerie(opacityFunction,colorFunction);
                            break;
                        }
                    }
                }
                if ((_reader.NodeType == XmlNodeType.EndElement) && _reader.Name.Equals("OpacityFunctions"))
                {
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
            _reader.ReadToFollowing("ROT");
            float r = float.Parse(_reader.ReadElementContentAsString().Replace('.', ','));
            _reader.ReadToFollowing("GRUEN");
            float g = float.Parse(_reader.ReadElementContentAsString().Replace('.', ','));
            _reader.ReadToFollowing("BLAU");
            float b = float.Parse(_reader.ReadElementContentAsString().Replace('.', ','));
            return Color.FromArgb((int)r*255, (int)g*255, (int)b*255);
        }

        /// <summary>
        /// Reads color function from the open xml. 
        /// </summary>
        /// <returns>Color function.</returns>
        private Dictionary<float, Color[]> ReadColorFunction()
        {
            Dictionary<float, Color[]> colorFunction = new Dictionary<float, Color[]>();

            if (_reader.ReadToFollowing("ColorLut"))

            while (_reader.Read())
            {
                if ((_reader.NodeType == XmlNodeType.Element) && _reader.Name.Equals("PointList"))
                {
                    while (_reader.Read())
                    {
                        if ((_reader.NodeType == XmlNodeType.Element) && _reader.Name.Equals("First"))
                        {
                            Color[] colorArray = new Color[2];
                            float intensity = float.Parse(_reader.ReadElementContentAsString().Replace('.', ','));
                            //Left Color
                            _reader.ReadToFollowing("LeftColor");
                            colorArray[0] = ReadColor();
                            //Right Color
                            _reader.ReadToFollowing("RightColor");
                            colorArray[1] = ReadColor();
                            colorFunction.Add(intensity, colorArray);
                        }
                        if ((_reader.NodeType == XmlNodeType.EndElement) && _reader.Name.Equals("PointList"))
                        {
                            break;
                        }
                    }
                }
                if ((_reader.NodeType == XmlNodeType.EndElement) && _reader.Name.Equals("ColorLut"))
                {
                    break;
                }
            }
            return colorFunction;
        }


        /// <summary>
        /// Reads opacity and color function from the open xml. 
        /// </summary>
        /// <returns>Information about the preset, which includes series of color and opacity function.</returns>
        private PresetInformation ReadOpacityAndColorFunction()
        {
            PresetInformation presetInformation = new PresetInformation();

            if (_reader.ReadToFollowing("ColoredFunctions"))

            while (_reader.Read())
            {
                if ((_reader.NodeType == XmlNodeType.Element) && _reader.Name.Equals("PointList"))
                {
                    Dictionary<float, float> opacityFunction = new Dictionary<float, float>();
                    Dictionary<float, Color[]> colorFunction = new Dictionary<float, Color[]>();
                    while (_reader.Read())
                    {
                        if ((_reader.NodeType == XmlNodeType.Element) && _reader.Name.Equals("First"))
                        {
                            Color[] colorArray = new Color[2];
                            float first = float.Parse(_reader.ReadElementContentAsString().Replace('.', ','));
                            _reader.ReadToFollowing("LeftColor");
                            colorArray[0] = ReadColor();
                            _reader.ReadToFollowing("RightColor");
                            colorArray[1] = ReadColor();
                            _reader.ReadToFollowing("Opacity");
                            float opacity = float.Parse(_reader.ReadElementContentAsString().Replace('.', ','));

                            opacityFunction.Add(first, opacity);
                            colorFunction.Add(first, colorArray);
                        }               
                        
                        if ((_reader.NodeType == XmlNodeType.EndElement) && _reader.Name.Equals("PointList"))
                        {
                            presetInformation.AddSerie(opacityFunction, colorFunction);
                            break;
                        }
                    }
                }

                if ((_reader.NodeType == XmlNodeType.EndElement) && _reader.Name.Equals("ColoredFunctions"))
                {
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
        public PresetInformation ReadXmlFile(String fileName)
        {
            _reader = new XmlTextReader(Path.Combine(@"..\..\presety\",fileName));
            _reader.ReadToFollowing("IndependentColor");
            Boolean isIndependent = _reader.ReadElementContentAsBoolean();

            if (isIndependent)
            {
                return ReadOpacityFunction(fileName, ReadColorFunction());
            }
            return ReadOpacityAndColorFunction();
        }
    }

}
