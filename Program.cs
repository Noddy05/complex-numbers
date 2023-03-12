using System;

namespace ComplexNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            ComplexNumber z = new ComplexNumber(1, 1);
            ComplexNumber zSquared = z * z;
            Console.WriteLine($"{z}^2 = {zSquared}\n");

            ComplexNumber z2 = new ComplexNumber(2, 1);
            Console.WriteLine($"{z} / {z2} = {z / z2}");
            Console.WriteLine($"{z}^3.5 = {z.Pow(3.5f)}\n");

            ComplexNumber zNeg = -z;
            Console.WriteLine($"-z = {zNeg}\n");
            ComplexNumber eToThePIi = ComplexNumber.Pow(MathF.E, new ComplexNumber(0, MathF.PI));
            Console.WriteLine($"e^(pi*i) = {eToThePIi}");
            ComplexNumber eToThePIi2 = ComplexNumber.Pow(MathF.E, new ComplexNumber(2, MathF.PI));
            Console.WriteLine($"e^(2 + pi*i) = {eToThePIi2}\n");

            ComplexNumber zNormalized = ComplexNumber.Normalize(z);
            Console.WriteLine($"z normalized: {zNormalized}");
            ComplexNumber.Normalize(ref z);
            Console.WriteLine($"z renormalized: {z}\n");

            ComplexNumber z3 = (ComplexNumber)(1f, MathF.PI / 2);
            Console.WriteLine($"Polar to complex number: {z3}");

            while (true) ;
        }
    }
}
