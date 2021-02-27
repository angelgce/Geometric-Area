using System;


namespace Calculator_StrategyPattern
{
    interface AreaInterface // creating an interface for all our mathematical formulas
    {
        public void calculateArea(); // we use this to program the form
        public String showResult(); //we use this to show a mssg
 
    }
}
