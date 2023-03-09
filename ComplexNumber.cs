using System;

namespace Complex_Numbers
{
    struct ComplexNumber
    {
        /// <summary>
        /// The component 'a' (Sometimes called 'x') is the real part of the complex number.
        /// The component 'b' (Sometimes called 'y') is the imaginary part of the complex number.
        /// </summary>
        public float a, b;

        /// <summary>
        /// The component 'a' (Sometimes called 'x') is the real part of the complex number.
        /// The component 'b' (Sometimes called 'y') is the imaginary part of the complex number.
        /// </summary>
        public ComplexNumber(float a, float b)
        {
            this.a = a;
            this.b = b;
        }

        /// <summary>
        /// Returns a complex number representing the length and angle of the original complex number
        /// </summary>
        public float Magnitude() => MathF.Sqrt(a * a + b * b);
        public float Angle() => MathF.Atan2(b, a);

        public (float r, float theta) ToPolar()
        {
            float r = Magnitude();
            float theta = Angle();

            return (r, theta);
        }
        public ComplexNumber Normalize() => new ComplexNumber(a, b) / Magnitude();
        public static void Normalize(ComplexNumber z) => z /= z.Magnitude();

        /// <summary>
        /// Raises a complex number to any power.
        /// </summary>
        public ComplexNumber Pow(float power)
        {
            (float r, float theta) = ToPolar();

            float newA = MathF.Pow(r, power) * MathF.Cos(theta * power);
            float newB = MathF.Pow(r, power) * MathF.Sin(theta * power);
            return new ComplexNumber(newA, newB);
        }

        /// <summary>
        /// Returns the conjugate of a complex number.
        /// </summary>
        public ComplexNumber Conj()
        {
            return new ComplexNumber(a, -b);
        }

        /// <summary>
        /// Returns a complex number as a string. 
        /// Format: (a + bi)
        /// </summary>
        public override string ToString()
        {
            string outputString = "(";
            float roundedA = MathF.Round(a * 1000) / 1000;
            float roundedB = MathF.Round(b * 1000) / 1000;
            outputString += roundedA;

            if (roundedB > 0)
                outputString += $" + {roundedB}i)";
            else
                outputString += $" - {-roundedB}i)";

            if (roundedA != 0)
                return outputString;


            if (roundedB > 0)
                outputString = $"({roundedB}i)";
            else
                outputString = $"({-roundedB}i)";

            return outputString;
        }

        #region Operators
        public static ComplexNumber operator +(ComplexNumber z, float c) => new ComplexNumber(z.a + c, z.b);
        public static ComplexNumber operator +(ComplexNumber z, int c) => new ComplexNumber(z.a + c, z.b);
        public static ComplexNumber operator +(ComplexNumber z1, ComplexNumber z2) => new ComplexNumber(z1.a + z2.a, z1.b + z2.b);
        public static ComplexNumber operator -(ComplexNumber z, float c) => new ComplexNumber(z.a - c, z.b);
        public static ComplexNumber operator -(ComplexNumber z, int c) => new ComplexNumber(z.a - c, z.b);
        public static ComplexNumber operator -(ComplexNumber z1, ComplexNumber z2) => new ComplexNumber(z1.a - z2.a, z1.b - z2.b);
        public static ComplexNumber operator *(ComplexNumber z, float c) => new ComplexNumber(z.a * c, z.b * c);
        public static ComplexNumber operator *(ComplexNumber z, int c) => new ComplexNumber(z.a * c, z.b * c);
        public static ComplexNumber operator *(ComplexNumber z1, ComplexNumber z2)
        {
            (float r1, float theta1) = z1.ToPolar();
            (float r2, float theta2) = z2.ToPolar();

            float a = r1 * r2 * MathF.Cos(theta1 + theta2);
            float b = r1 * r2 * MathF.Sin(theta1 + theta2);

            return new ComplexNumber(a, b);
        }
        public static ComplexNumber operator /(ComplexNumber z, float c) => new ComplexNumber(z.a / c, z.b / c);
        public static ComplexNumber operator /(ComplexNumber z, int c) => new ComplexNumber(z.a / c, z.b / c);
        public static ComplexNumber operator /(ComplexNumber z1, ComplexNumber z2)
        {
            (float r1, float theta1) = z1.ToPolar();
            (float r2, float theta2) = z2.ToPolar();

            float a = r1 / r2 * MathF.Cos(theta1 - theta2);
            float b = r1 / r2 * MathF.Sin(theta1 - theta2);

            return new ComplexNumber(a, b);
        }
        public static ComplexNumber operator %(ComplexNumber z, float c) => new ComplexNumber(z.a % c, z.b % c);
        public static ComplexNumber operator %(ComplexNumber z, int c) => new ComplexNumber(z.a % c, z.b % c);
        public static ComplexNumber operator %(ComplexNumber z1, ComplexNumber z2) => new ComplexNumber(z1.a % z2.a, z1.b % z2.b);
        public static ComplexNumber operator ++(ComplexNumber z1) => new ComplexNumber(z1.a++, z1.b++);
        public static ComplexNumber operator --(ComplexNumber z1) => new ComplexNumber(z1.a--, z1.b--);
        #endregion
    }
}
