using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace XMLReaderTest
{
    /// <summary>
    /// Holds opacity and color function
    /// </summary>
    public class ElementOfSeries
    {
        /// <summary>
        /// Opacity function - holds intensity and it correspond opacity in format { value of hu, opacity }
        /// </summary>
        public Dictionary<float, float> OpacityFunction { get; set; }
        /// <summary>
        ///  Color function - holds intensity and it correspond color in format { value of hu, color }
        /// </summary>
        public Dictionary<float, Color[]> ColorFuction { get; set; }

        /// <summary>
        /// Creates the element of series
        /// </summary>
        /// <param name="opacityFunction"> Opacity function - holds intensity and it correspond opacity in format { value of hu, opacity }</param>
        /// <param name="colorFuction"> Color function - holds intensity and it correspond color in format { value of hu, color }</param>
        public ElementOfSeries(Dictionary<float, float> opacityFunction, Dictionary<float, Color[]> colorFuction)
        {
            this.OpacityFunction = opacityFunction;
            this.ColorFuction = colorFuction;
        }
    }
}
