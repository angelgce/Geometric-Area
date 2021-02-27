using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator_StrategyPattern
{
    class Rectangle : AreaInterface
    {
        private List<double> values = new List<double>();
        private double result;
        public Rectangle(List<double> values) => this.values = values;
        public void calculateArea() => result = values.Aggregate((x, y) => x * y);
        public String showResult() =>  "The Area of this Rectangle is: " + result + " cm2";
    }
}
