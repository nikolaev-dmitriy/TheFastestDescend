using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoohMathParser;


     

namespace TheFastestDescend
    {
          class Form1 
        {
          
        

            public delegate double functor(double alpha);

          

            public static void Main(string[] args)
            {
            Console.WriteLine("Enter the expression of function F(x,y):");
                MathExpression expr = new MathExpression(Console.ReadLine());
            Console.WriteLine("Enter the start X coordinate:");
            double x1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the start Y coordinate:");
            double x2 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the epsilon (precision):");
            double E = Convert.ToDouble(Console.ReadLine());

            double ModulOfGradient = E + 1;
                int counter = 1;
                
                while (ModulOfGradient >= E)
                {
                    Console.WriteLine("Iteration №" + counter);
                    Console.WriteLine( "Point A"+ counter + " = (" + Math.Round(x1,4) + ", " + Math.Round(x2,4) + ")");
                    Console.WriteLine("F(A" + counter + ") = " + Math.Round(expr.Calculate(x1, x2),4));
                    double der1 = GradientX1(expr, x1, x2);
                    double der2 = GradientX2(expr, x1, x2);
                    double[] gradient = { der1, der2 };
                    Console.WriteLine("Derivative of function F(x,y) by X in point(" + Math.Round(x1, 4) + ";"+ Math.Round(x2, 4) + ") = "+der1.ToString());
                    Console.WriteLine("Derivative of function F(x,y) by Y in point(" + Math.Round(x1, 4) + ";" + Math.Round(x2, 4) + ") = " + der2.ToString());

                    ModulOfGradient = Math.Sqrt(Math.Pow(der1, 2) + Math.Pow(der2, 2));
                    Console.WriteLine("Norma of gradient = " + Math.Round(ModulOfGradient,4));

                    functor g = a =>
                    {
                        double first = x1 - a * der1;
                        double second = x2 - a * der2;
                        return expr.Calculate(first, second);
                    };

                    double lyambda = GoldenRush(0, 50, E / 10, g);

                    x1 = x1 - lyambda * der1;
                    x2 = x2 - lyambda * der2;
                    Console.WriteLine("_____________________________________________________________");
                    counter++;
                }
                Console.WriteLine("Answer: x* = (" + Math.Round(x1, 4) + "; " + Math.Round(x2, 4) + ")");
            Console.WriteLine("F(" + Math.Round(x1, 4) + ";" + Math.Round(x2, 4) + ") = " + Math.Round(expr.Calculate(x1, x2)),4);

            Console.ReadKey();
            }


            public static double GoldenRush(double a, double b, double E, functor expr)
            {
                double fi = (1 + Math.Sqrt(5)) / 2;
                double x1, x2;
                do
                {
                    x1 = b - (b - a) / fi;
                    x2 = a + (b - a) / fi;
                    if (expr(x1) >= expr(x2)) a = x1;
                    b = x2;
                } while (Math.Abs(b - a) > E);
                return (a + b) / 2;
            }



            public static double GradientX1(MathExpression expr, double x0, double y0)
            {
                double result;

                double dx = 10e-10;
                result = (expr.Calculate(x0 + dx, y0) - expr.Calculate(x0, y0)) / dx;

                return result;
            }

            public static double GradientX2(MathExpression expr, double x0, double y0)
            {
                double result;

                double dx = 10e-10;
                result = (expr.Calculate(x0, y0 + dx) - expr.Calculate(x0, y0)) / dx;

                return result;
            }


        }

    }



