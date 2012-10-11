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

    public class ElementOfSeries
    {
        //opacity - {wartosc hvc, intensywnosc}
        public Dictionary<float, float> OpacityFunction { get; set;  }
        //color - {wartosc, kolor}
        public Dictionary<float, Color[]> ColorFuction { get; set;  }

        public ElementOfSeries( Dictionary<float, float> opacityFunction, Dictionary<float, Color[]> colorFuction )
        {
            this.OpacityFunction = opacityFunction;
            this.ColorFuction = colorFuction;
        }
    }

    public class PresetInformation
    {
        //seria = lista elementow {funkcja opacity, funkcja koloru}
        public List<ElementOfSeries> Series { set; get; }
      
        public PresetInformation()
        {
            this.Series = new List<ElementOfSeries>();
        }

        public void AddSerie(Dictionary<float, float> opacityFunction, Dictionary<float, Color[]> colorFunction)
        {
            this.Series.Add( new ElementOfSeries(opacityFunction, colorFunction));
        }
    }
}
