using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator_StrategyPattern
{
    class Circle : AreaInterface
    {
        private List<double> values = new List<double>();
        private double result;
        public Circle(List<double> values) => this.values = values;
        public void calculateArea() => result = values.Aggregate((x, y) =>  Math.Pow(x ,2)*y);
        public String showResult() => "The Area of this Circle is: " + result + " cm2";

    }
}
