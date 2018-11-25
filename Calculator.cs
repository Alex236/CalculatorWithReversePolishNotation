using System;
using System.Collections.Generic;

namespace Calculator
{
    class Calculator
    {
        private Queue<string> PostfixNotation = new Queue<string>();
        private List<string> IntfixNotation = new List<string>();
        private Stack<string> Digits = new Stack<string>();
        public string Evaluate(string str)
        {
            string error;
            error = Spliter(str);
            if(error != "") return error;
            IntfixToPostfixNotation();
            EvaluationOfPostfixExpression();
            return Math.Round(Double.Parse(Digits.Pop()), 10).ToString();
        }
        private void EvaluationOfPostfixExpression()
        {
            foreach(var str in PostfixNotation)
            {
                if(Double.TryParse(str, out double temp))
                {
                    Digits.Push(str);
                }
                else
                {
                    double operand1 = double.Parse(Digits.Pop());
                    double operand2 = double.Parse(Digits.Pop());
                    switch(str)
                    {
                        case "+":
                        Digits.Push((operand2 + operand1).ToString());
                        break;
                        case "-":
                        Digits.Push((operand2 - operand1).ToString());
                        break;
                        case "*":
                        Digits.Push((operand2 * operand1).ToString());
                        break;
                        case "/":
                        Digits.Push((operand2 / operand1).ToString());
                        break;
                        case "^":
                        Digits.Push(Math.Pow(operand2, operand1).ToString());
                        break;
                    }
                }
            }
        }
        private void IntfixToPostfixNotation()
        {
            foreach(var str in IntfixNotation)
            {
                if(Double.TryParse(str, out double temp)) PostfixNotation.Enqueue(str);
                else
                {
                    if(str == "(")
                    {
                        if(Digits.Count == 0)
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "(")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "+" || Digits.Peek() == "-")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "*" || Digits.Peek() == "/")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "^")
                        {
                            Digits.Push(str);
                            continue;
                        }
                    }
                    if(str == "+" || str == "-")
                    {
                        if(Digits.Count == 0)
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "(")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "+" || Digits.Peek() == "-")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "*" || Digits.Peek() == "/")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "^")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                    }
                    if(str == "*" || str == "/")
                    {
                        if(Digits.Count == 0)
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "(")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "+" || Digits.Peek() == "-")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "*" || Digits.Peek() == "/")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "^")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                    }
                    if(str == "^")
                    {
                        if(Digits.Count == 0)
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "(")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "+" || Digits.Peek() == "-")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "*" || Digits.Peek() == "/")
                        {
                            Digits.Push(str);
                            continue;
                        }
                        if(Digits.Peek() == "^")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                            Digits.Push(str);
                            continue;
                        }
                    }
                    if(str == ")")
                    {
                        while(Digits.Peek() != "(")
                        {
                            PostfixNotation.Enqueue(Digits.Pop());
                        }
                        Digits.Pop();
                    }
                }
            }
            while(Digits.Count != 0)
            {
                PostfixNotation.Enqueue(Digits.Pop());
            }
        }
        private string Spliter(string str)
        {
            str = str.Replace(" ", "");
            string number = "";
            bool lastElementIsDigit = false;
            bool wasNumber = false;
            for (int i = 0; i < str.Length; i++)
            {
                if(str[i] == '-' && !lastElementIsDigit)
                {
                    number += str[i].ToString();
                    continue;
                }
                if(Char.IsDigit(str[i]) || str[i] == '.' || str[i] == ',')
                {
                    if(str[i] == ',') number += '.';
                    else
                    {
                        number += str[i].ToString();
                        lastElementIsDigit = true;
                    }
                    wasNumber = true;
                    if(i == str.Length - 1 && wasNumber) IntfixNotation.Add(number);
                    continue;
                }
                if(wasNumber)
                {
                    IntfixNotation.Add(number);
                    number = "";
                    wasNumber = false;
                    lastElementIsDigit = false;
                    IntfixNotation.Add(str[i].ToString());
                }
                else
                {
                    lastElementIsDigit = false;
                    IntfixNotation.Add(str[i].ToString());
                }
            }
            return UserInputChecker();
        }
        private string UserInputChecker()
        {
            for(int i = 1; i < IntfixNotation.Count; i++)
            {
                if((IntfixNotation[i-1] == "+" || IntfixNotation[i-1] == "-" || IntfixNotation[i-1] == "*" || IntfixNotation[i-1] == "/"
                || IntfixNotation[i-1] == "^") && (IntfixNotation[i] == "+" || IntfixNotation[i] == "-" || IntfixNotation[i] == "*"
                || IntfixNotation[i] == "/" || IntfixNotation[i] == "^"))
                {
                    return "Incorrect user input";
                }
            }
            return BreaketsCheckedAdnRight() ? "" : "Incorrect breakets position";
        }
        private bool BreaketsCheckedAdnRight()
        {
            int counter = 0;
            foreach(var e in IntfixNotation)
            {
                if(e == "(") counter++;
                if(e == ")") counter--;
            }
            return counter == 0 ? true : false;
        }
    }
}