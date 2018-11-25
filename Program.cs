using System;
using System.Collections.Generic;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            Console.WriteLine("This is a test version of this programm. If you will make a mistake, programm will crash. In the next version I will add error handler and normal user interface.");
            Console.WriteLine("Enter math expression to evaluate:");
            Console.WriteLine(calculator.Evaluate(Console.ReadLine()));
        }
    }
}
