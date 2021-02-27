using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator_StrategyPattern
{
    class Polygon : AreaInterface
    {
        private List<double> values = new List<double>();
        private double result;
        public Polygon(List<double> values) => this.values = values;
        public void calculateArea()
        {
            result = values.Aggregate((x, y) => x * y)/2;
   
        }



        public String showResult() => "The Area of this Polygon is: " + result +" cm2";
    }
}
