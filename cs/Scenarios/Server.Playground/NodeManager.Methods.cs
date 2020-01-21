// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Server.Playground
{
    using System;
    using Opc.UaFx;

    internal partial class NodeManager
    {
        #region ---------- Private methods ----------

        [return: OpcArgument("sum", Description = "The result of the addition.")]
        private int Add(
                [OpcArgument("a (summand)", Description = "The first summand.")]
                int a,
                [OpcArgument("b (summand)", Description = "The second summand.")]
                int b)
        {
            return a + b;
        }

        [return: OpcArgument("quotient", Description = "The result of the division.")]
        private int Divide(
                [OpcArgument("a (dividend)", Description = "The number which is divided.")]
                int a,
                [OpcArgument("b (divisor)", Description = "The number which divides the dividend.")]
                int b)
        {
            return a / b;
        }

        [return: OpcArgument("product", Description = "The result of the multiplication.")]
        private int Multiply(
                [OpcArgument("a (multiplicand)", Description = "The number which is multiplied (= the first factor).")]
                int a,
                [OpcArgument("b (multiplier)", Description = "The number of times the multiplicant is multiplied (= the second factor).")]
                int b)
        {
            return a * b;
        }

        [return: OpcArgument("power", Description = "The result of the exponentiation.")]
        private int Power(
                [OpcArgument("a (base)", Description = "The number which is the base.")]
                int a,
                [OpcArgument("b (exponent)", Description = "The number of times the base is powered.")]
                int b)
        {
            return unchecked((int)Math.Pow(a, b));
        }

        [return: OpcArgument("difference", Description = "The result of the substraction.")]
        private int Substract(
                [OpcArgument("a (minuend)", Description = "The number which is decreased.")]
                int a,
                [OpcArgument("b (subtrahend)", Description = "The number which is substracted.")]
                int b)
        {
            return a - b;
        }

        #endregion
    }
}
