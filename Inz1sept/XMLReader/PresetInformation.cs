using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace XMLReaderTest
{
    /// <summary>
    /// Holds information about preset - list of series. Serie includes opacity and color function loaded from xml file. 
    /// </summary>
    public class PresetInformation
    {
        /// <summary>
        /// List of each series
        /// </summary>
        public List<ElementOfSeries> Series { set; get; }
      
        /// <summary>
        /// Creates preset information.
        /// </summary>
        public PresetInformation()
        {
            this.Series = new List<ElementOfSeries>();
        }

        /// <summary>
        /// Add new serie to the preset information.
        /// </summary>
        /// <param name="opacityFunction">Opacity function - holds intensity and it correspond opacity in format { value of hu, opacity }</param>
        /// <param name="colorFunction">Color function - holds intensity and it correspond color in format { value of hu, color }</param>
        public void AddSerie(Dictionary<float, float> opacityFunction, Dictionary<float, Color[]> colorFunction)
        {
            this.Series.Add( new ElementOfSeries(opacityFunction, colorFunction));
        }
    }
}
