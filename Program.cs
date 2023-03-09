using System;

namespace Complex_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            ComplexNumber z = new ComplexNumber(1, 1);
            ComplexNumber zSquared = z * z;
            Console.WriteLine($"{z}^2 = {zSquared}");

            ComplexNumber z2 = new ComplexNumber(2, 1);
            Console.WriteLine($"{z} / {z2} = {z / z2}");
            Console.WriteLine($"{z}^3.5 = {z.Pow(3.5f)}");

            while (true) ;
        }
    }
}
