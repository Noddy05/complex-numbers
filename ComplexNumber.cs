using System;

namespace ComplexNumbers
{
    struct ComplexNumber
    {
        public static readonly ComplexNumber i = new ComplexNumber(0, 1);

        /// <summary>
        /// The component 'a' (Sometimes called 'x') is the real part of the complex number.<br></br>
        /// </summary>
        public float a;
        /// <summary>
        /// The component 'b' (Sometimes called 'y') is the imaginary part of the complex number.
        /// </summary>
        public float b;

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
        /// Makes a complex number with a distance from center of 1, and an angle of t.<br></br><br></br>
        /// Formula:<br></br>
        /// e^(it) = cos(t) + i * sin(t).
        /// </summary>
        public ComplexNumber(float t)
        {
            a = MathF.Cos(t);
            b = MathF.Sin(t);
        }

        /// <summary>
        /// Returns a float representing the complex numbers distance from the origin.<br></br><br></br>
        /// Formula: <br></br>
        /// √a²+b².
        /// </summary>
        public float Magnitude() => MathF.Sqrt(a * a + b * b);
        /// <summary>
        /// Returns a float representing the angle of the complex number.<br></br><br></br>
        /// Formula: <br></br>
        /// arctan(y / x).
        /// </summary>
        public float Angle() => MathF.Atan2(b, a);

        /// <summary>
        /// Returns the distance between the origin and the complex number (r = √a²+b²).
        /// Also returns the angle of the complex number (theta = Atan2(b, a)).
        /// </summary>
        public (float r, float theta) ToPolar()
        {
            float r = Magnitude();
            float theta = Angle();

            return (r, theta);
        }
        /// <summary>
        /// Normalized a complex number, so its distance from the origin is 1.<br><br></br></br>
        /// Formula:<br></br>
        /// z / z.Magnitude().
        /// </summary>
        public ComplexNumber Normalize()
        {
            float mag = Magnitude();
            if (mag == 0)
                throw new DivideByZeroException();

            return new ComplexNumber(a, b) / mag;
        }
        /// <summary>
        /// Normalized a complex number, so its distance from the origin is 1.<br><br></br></br>
        /// Formula:<br></br>
        /// z / z.Magnitude().
        /// </summary>
        public static void Normalize(ref ComplexNumber z)
        {
            float mag = z.Magnitude();
            if (mag == 0)
                throw new DivideByZeroException();

            z /= mag;
        }
        /// <summary>
        /// Normalized a complex number, so its distance from the origin is 1.<br><br></br></br>
        /// Formula:<br></br>
        /// z / z.Magnitude().
        /// </summary>
        public static ComplexNumber Normalize(ComplexNumber z)
        {
            float mag = z.Magnitude();
            if (mag == 0)
                throw new DivideByZeroException();

            return z / mag;
        }

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
        /// Raises a real number to a complex power.
        /// </summary>
        public static ComplexNumber Pow(float c, ComplexNumber complexPower)
        {
            float newA = MathF.Cos(complexPower.b * MathF.Log(c));
            float newB = MathF.Sin(complexPower.b * MathF.Log(c));
            return MathF.Pow(c, complexPower.a) * new ComplexNumber(newA, newB);
        }

        /// <summary>
        /// Raises a real number to a complex power.
        /// </summary>
        public static ComplexNumber Pow(int c, ComplexNumber complexPower)
        {
            float newA = MathF.Cos(complexPower.b * MathF.Log(c));
            float newB = MathF.Sin(complexPower.b * MathF.Log(c));
            return MathF.Pow(c, complexPower.a) * new ComplexNumber(newA, newB);
        }

        /// <summary>
        /// Raises a complex number to a complex power.<br></br><br></br>
        /// Formula:<br></br>
        /// (r₁ * e^(it₁))^(r₂ * e^(it₂)) = r₁^(r₂ * e^(it₂)) * e^(it₁ * r₂ * e^(it₂))<br></br>
        /// Multiplying e^(it₂) by i is the same as taking its conjugate and then swapping a and b.
        /// </summary>
        public static ComplexNumber Pow(ComplexNumber z, ComplexNumber complexPower)
        {
            (float r1, float t1) = z.ToPolar();
            (float r2, float t2) = complexPower.ToPolar();

            ComplexNumber rPart = Pow(r1, r2 * new ComplexNumber(t2));
            ComplexNumber ePart = Pow(MathF.E, t1 * r2 * new ComplexNumber(t2).Conj().Swapped());
            return rPart * ePart;
        }
        /// <summary>
        /// Raises a complex number to a complex power.<br></br><br></br>
        /// Formula:<br></br>
        /// (r₁ * e^(it₁))^(r₂ * e^(it₂)) = r₁^(r₂ * e^(it₂)) * e^(it₁ * r₂ * e^(it₂))
        /// </summary>
        public ComplexNumber Pow(ComplexNumber complexPower)
        {
            (float r1, float t1) = ToPolar();
            (float r2, float t2) = complexPower.ToPolar();

            ComplexNumber rPart = Pow(r1, r2 * new ComplexNumber(t2));
            // Multiplying e^(it₂) by i is the same as taking its conjugate and then swapping a and b.
            ComplexNumber ePart = Pow(MathF.E, t1 * r2 * new ComplexNumber(t2).Conj().Swapped());
            return rPart * ePart;
        }

        /// <summary>
        /// Returns the sine of a complex number.<br></br><br></br>
        /// Formula:<br></br>
        /// 1 / 2i * (e^(iz) - e^(-iz)).
        /// </summary>
        public static ComplexNumber Sin(ComplexNumber z)
        {
            return -0.5f * i * (Pow(MathF.E, z.Conj().Swapped()) - Pow(MathF.E, -z.Conj().Swapped()));
        }

        /// <summary>
        /// Returns the cosine of a complex number.<br></br><br></br>
        /// Formula:<br></br>
        /// 1 / 2 * (e^(i(z)) + e^(-i(z))).
        /// </summary>
        public static ComplexNumber Cos(ComplexNumber z)
        {
            return 0.5f * (Pow(MathF.E, z.Conj().Swapped()) + Pow(MathF.E, -z.Conj().Swapped()));
        }

        /// <summary>
        /// Returns the cosine of a complex number.<br></br><br></br>
        /// Formula:<br></br>
        /// sin(z) / cos(z).
        /// </summary>
        public static ComplexNumber Tan(ComplexNumber z)
        {
            return Sin(z) / Cos(z);
        }

        /// <summary>
        /// Returns natural logarithm of a complex number.<br></br><br></br>
        /// Formula:<br></br>
        /// ln(r) + it.
        /// </summary>
        public static ComplexNumber Ln(ComplexNumber z)
        {
            float r = z.Magnitude();
            float theta = z.Angle();

            return new ComplexNumber(MathF.Log(r), theta);
        }

        /// <summary>
        /// Returns logarithm (with a real base) of a complex number.<br></br><br></br>
        /// Formula:<br></br>
        /// (ln(r) + it) / ln(base).
        /// </summary>
        public static ComplexNumber Log(float realBase, ComplexNumber z)
        {
            float r = z.Magnitude();
            float theta = z.Angle();

            return new ComplexNumber(MathF.Log(r), theta) / MathF.Log(realBase);
        }

        /// <summary>
        /// Returns logarithm (with a complex base) of a real number.<br></br><br></br>
        /// Formula:<br></br>
        /// ln(c) / ln(r) + it).
        /// </summary>
        public static ComplexNumber Log(ComplexNumber complexBase, float c)
        {
            float r = complexBase.Magnitude();
            float theta = complexBase.Angle();

            return MathF.Log(c) / new ComplexNumber(MathF.Log(r), theta);
        }

        /// <summary>
        /// Returns logarithm (with a complex base) of a complex number.<br></br><br></br>
        /// Formula:<br></br>
        /// (ln(r) + it) / ln(base).
        /// </summary>
        public static ComplexNumber Log(ComplexNumber complexBase, ComplexNumber z)
        {
            float r1 = complexBase.Magnitude();
            float theta1 = complexBase.Angle();
            float r2 = z.Magnitude();
            float theta2 = z.Angle();

            return new ComplexNumber(MathF.Log(r2), theta2) / new ComplexNumber(MathF.Log(r1), theta1);
        }

        /// <summary>
        /// Returns inverted value of a complex number.<br></br><br></br>
        /// </summary>
        public static ComplexNumber CircleInversion(ComplexNumber z)
        {
            return z / (z.a * z.a + z.b * z.b);
        }

        /// <summary>
        /// Returns inverted value of a complex number.<br></br><br></br>
        /// </summary>
        public static ComplexNumber CircleInversion(ComplexNumber z, ComplexNumber circleCenter)
        {
            ComplexNumber centerToZ = z - circleCenter;
            return centerToZ / (centerToZ.a * centerToZ.a + centerToZ.b * centerToZ.b);
        }

        /// <summary>
        /// Returns inverted value of a complex number.<br></br><br></br>
        /// </summary>
        public static ComplexNumber CircleInversion(ComplexNumber z, ComplexNumber circleCenter, float radius)
        {
            ComplexNumber centerToZ = z - circleCenter;
            return centerToZ * radius * radius / (centerToZ.a * centerToZ.a + centerToZ.b * centerToZ.b);
        }

        /// <summary>
        /// Returns the conjugate of a complex number.<br></br>
        /// (Multiplies the imaginary component by (-1))
        /// </summary>
        public ComplexNumber Conj()
        {
            return new ComplexNumber(a, -b);
        }

        /// <summary>
        /// Swaps the values of a and b in a complex number.
        /// </summary>
        public ComplexNumber Swapped()
        {
            return new ComplexNumber(b, a);
        }

        /// <summary>
        /// Returns a complex number as a string.
        /// Format: (a + bi)
        /// </summary>
        public override string ToString()
        {
            string outputString = "(";
            float roundedA = MathF.Round(a * 100000) / 100000;
            float roundedB = MathF.Round(b * 100000) / 100000;
            outputString += roundedA;

            if (roundedB == 0)
                return outputString + ")";

            if (roundedB >= 0)
                outputString += $" + {roundedB}i)";
            else
                outputString += $" - {-roundedB}i)";

            if (roundedA != 0)
                return outputString;


            if (roundedB >= 0)
                outputString = $"({roundedB}i)";
            else
                outputString = $"({-roundedB}i)";

            return outputString;
        }

        #region Operators
        public static ComplexNumber operator +(ComplexNumber z, float c) => new ComplexNumber(z.a + c, z.b);
        public static ComplexNumber operator +(float c, ComplexNumber z) => new ComplexNumber(z.a + c, z.b);
        public static ComplexNumber operator +(ComplexNumber z, int c) => new ComplexNumber(z.a + c, z.b);
        public static ComplexNumber operator +(int c, ComplexNumber z) => new ComplexNumber(z.a + c, z.b);
        public static ComplexNumber operator +(ComplexNumber z1, ComplexNumber z2) => new ComplexNumber(z1.a + z2.a, z1.b + z2.b);
        public static ComplexNumber operator -(ComplexNumber z) => new ComplexNumber(-z.a, -z.b);
        public static ComplexNumber operator -(ComplexNumber z, float c) => new ComplexNumber(z.a - c, z.b);
        public static ComplexNumber operator -(float c, ComplexNumber z) => new ComplexNumber(z.a - c, z.b);
        public static ComplexNumber operator -(ComplexNumber z, int c) => new ComplexNumber(z.a - c, z.b);
        public static ComplexNumber operator -(int c, ComplexNumber z) => new ComplexNumber(z.a - c, z.b);
        public static ComplexNumber operator -(ComplexNumber z1, ComplexNumber z2) => new ComplexNumber(z1.a - z2.a, z1.b - z2.b);

        public static ComplexNumber operator *(ComplexNumber z, float c) => new ComplexNumber(z.a * c, z.b * c);
        public static ComplexNumber operator *(float c, ComplexNumber z) => new ComplexNumber(z.a * c, z.b * c);
        public static ComplexNumber operator *(ComplexNumber z, int c) => new ComplexNumber(z.a * c, z.b * c);
        public static ComplexNumber operator *(int c, ComplexNumber z) => new ComplexNumber(z.a * c, z.b * c);
        public static ComplexNumber operator *(ComplexNumber z1, ComplexNumber z2)
        {
            (float r1, float theta1) = z1.ToPolar();
            (float r2, float theta2) = z2.ToPolar();

            float a = r1 * r2 * MathF.Cos(theta1 + theta2);
            float b = r1 * r2 * MathF.Sin(theta1 + theta2);

            return new ComplexNumber(a, b);
        }

        public static ComplexNumber operator /(ComplexNumber z, float c) => new ComplexNumber(z.a / c, z.b / c);
        public static ComplexNumber operator /(float c, ComplexNumber z) => c * z.Pow(-1);
        public static ComplexNumber operator /(ComplexNumber z, int c) => new ComplexNumber(z.a / c, z.b / c);
        public static ComplexNumber operator /(int c, ComplexNumber z) => c * z.Pow(-1);
        public static ComplexNumber operator /(ComplexNumber z1, ComplexNumber z2)
        {
            (float r1, float theta1) = z1.ToPolar();
            (float r2, float theta2) = z2.ToPolar();

            float a = r1 / r2 * MathF.Cos(theta1 - theta2);
            float b = r1 / r2 * MathF.Sin(theta1 - theta2);

            return new ComplexNumber(a, b);
        }

        public static ComplexNumber operator %(ComplexNumber z, float c) => new ComplexNumber(z.a % c, z.b % c);
        public static ComplexNumber operator %(float c, ComplexNumber z) => new ComplexNumber(z.a % c, z.b % c);
        public static ComplexNumber operator %(ComplexNumber z, int c) => new ComplexNumber(z.a % c, z.b % c);
        public static ComplexNumber operator %(int c, ComplexNumber z) => new ComplexNumber(z.a % c, z.b % c);
        public static ComplexNumber operator %(ComplexNumber z1, ComplexNumber z2) => new ComplexNumber(z1.a % z2.a, z1.b % z2.b);

        public static ComplexNumber operator ++(ComplexNumber z1) => new ComplexNumber(z1.a++, z1.b++);
        public static ComplexNumber operator --(ComplexNumber z1) => new ComplexNumber(z1.a--, z1.b--);

        public static explicit operator ComplexNumber((float r, float theta)polar) 
            => new ComplexNumber(polar.r * MathF.Cos(polar.theta), polar.r * MathF.Sin(polar.theta));
        #endregion
    }
}
