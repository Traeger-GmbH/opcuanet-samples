' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx

Namespace Server.Playground
    Partial Friend Class NodeManager
        '---------- Private methods ----------

        Private Function Add(
                <OpcArgument("a (summand)", Description:="The first summand.")>
                a As Integer,
                <OpcArgument("b (summand)", Description:="The second summand.")>
                b As Integer) As <OpcArgument("sum", Description:="The result of the addition.")> Integer
            Return a + b
        End Function

        Private Function Divide(
                <OpcArgument("a (dividend)", Description:="The number which is divided.")>
                a As Integer,
                <OpcArgument("b (divisor)", Description:="The number which divides the dividend.")>
                b As Integer) As <OpcArgument("quotient", Description:="The result of the division.")> Integer
            Return a \ b
        End Function

        Private Function Multiply(
                <OpcArgument("a (multiplicand)", Description:="The number which is multiplied (= the first factor).")>
                a As Integer,
                <OpcArgument("b (multiplier)", Description:="The number of times the multiplicant is multiplied (= the second factor).")>
                b As Integer) As <OpcArgument("product", Description:="The result of the multiplication.")> Integer
            Return a * b
        End Function

        Private Function Power(
                <OpcArgument("a (base)", Description:="The number which is the base.")>
                a As Integer,
                <OpcArgument("b (exponent)", Description:="The number of times the base is powered.")>
                b As Integer) As <OpcArgument("power", Description:="The result of the exponentiation.")> Integer
            Return CInt(Fix(Math.Pow(a, b)))
        End Function

        Private Function Substract(
                <OpcArgument("a (minuend)", Description:="The number which is decreased.")>
                a As Integer,
                <OpcArgument("b (subtrahend)", Description:="The number which is substracted.")>
                b As Integer) As <OpcArgument("difference", Description:="The result of the substraction.")> Integer
            Return a - b
        End Function
    End Class
End Namespace
