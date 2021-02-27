
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;



namespace Calculator_StrategyPattern
{
    class TriangleArea : AreaInterface
    {
        private double result; //to save the result value
        private List<double> values = new List<double>();//we need a list of values
        public TriangleArea(List<double> values) => this.values = values;
        public void calculateArea() => result = values.Aggregate((x, y) => x * y / 2);// using functional programming to do the math part
        public String showResult() => "The Area of this Triangle is: " + result +" cm2";// using functional programming to do the math part
    }
}
