using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLReaderTest
{
    public class Color
    {
        public float Red { set; get; }
        public float Green { set; get; }
        public float Blue { set; get; }

        public Color(float R, float G, float B)
        {
            this.Red = R;
            this.Green = G;
            this.Blue = B;
        }
    }

    public class PresetInformation
    {
        private List<Dictionary<float, float>> series = new List<Dictionary<float, float>>();
        public Dictionary<float, Color[]> ColorFuction { set; get; }
        public List<Dictionary<float, float>> Series
        {
            get { return this.series; }
        }

        public PresetInformation()
        {
            this.ColorFuction = new Dictionary<float,Color[]>();
        }

        public void AddSerie(Dictionary<float, float> opacityFunction)
        {
            this.series.Add(opacityFunction);
        }
    }
}
