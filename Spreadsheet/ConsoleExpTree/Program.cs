using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine;
using System.Text.RegularExpressions;

namespace ConsoleExpTree
{
    class Program
    {
        public static void displayMenu(string expression)
        {
            Console.WriteLine("Menu (current expression=\""+expression+ "\")");
            Console.WriteLine("  1 = Enter a new expression");
            Console.WriteLine("  2 = Set a variable value");
            Console.WriteLine("  3 = Evaluate tree");
            Console.WriteLine("  4 = Quit\n");
        }


        static void Main(string[] args)
        {
            ExpTree expTree = new ExpTree("A1+B1+C1");
            string option = "0";
            do
            {
                displayMenu(expTree.Expression);
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.WriteLine("Enter a new expression: ");
                        string exp = Console.ReadLine();
                        expTree = new ExpTree(exp);
                        break;

                    case "2":
                        Console.WriteLine("Enter a variable name: ");
                        string var = Console.ReadLine();
                        Console.WriteLine("Enter a variable value: ");
                        string val = Console.ReadLine();
                        if (double.TryParse(val, out double dVal))
                        {
                            expTree.SetVar(var, dVal);
                        }

                        break;

                    case "3":
                        Console.WriteLine(expTree.Eval());
                        break;

                    default:
                        break;
                }

            } while (option != "4");
            
            

        }
    }
}
