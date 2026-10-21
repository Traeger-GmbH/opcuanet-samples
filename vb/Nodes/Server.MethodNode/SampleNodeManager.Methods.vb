' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Text
Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace MethodNode
    ''' <content>
    ''' Defines some methods used by the <see cref="SampleNodeManager"/>.
    ''' </content>
    Partial Friend Class SampleNodeManager
        '---------- Private static methods ----------

        Private Shared Function AddByDelegate(
                context As OpcMethodContext,
                a As Integer,
                b As Integer) As Integer
            ' result = a + b
            Return a + b
        End Function

        Private Shared Sub AddByCommand(
                context As OpcMethodContext,
                inputArguments As IList(Of Object),
                outputArguments As IList(Of Object))
            ' result = a + b
            outputArguments(0) = CInt(inputArguments(0)) + CInt(inputArguments(1))
        End Sub



        Private Shared Function MultiplyByDelegate(
                a As Integer,
                b As Integer) As Integer
            ' result = a * b
            Return a * b
        End Function

        Private Shared Sub MultiplyByCommand(
                context As OpcMethodContext,
                inputArguments As IList(Of Object),
                outputArguments As IList(Of Object))
            ' result = a * b
            outputArguments(0) = CInt(inputArguments(0)) * CInt(inputArguments(1))
        End Sub




        Private Shared Sub ProcessInputs(count As Integer, value As String)
            If String.IsNullOrEmpty(value) Then
                value = "Hello World!"
            End If

            For index = 0 To count - 1
                Console.WriteLine(value)
            Next
        End Sub

        Private Shared Sub ProcessInputsWithMetadata(
                <OpcArgument("COUNT", Description:="Number of Console.WriteLines executed.")>
                count As Integer,
                <OpcArgument("value", Description:="Value to be printed to the console.")>
                value As String)
            ProcessInputs(count, value)
        End Sub



        ' VB.NET has no 'out' keyword: an output argument is a ByRef parameter marked with the
        ' OutAttribute. A ByRef parameter without it is published as input and output argument.
        Private Shared Sub ProcessOutputs(<Out> ByRef value As String)
            value = "Hello World!"
        End Sub

        Private Shared Sub ProcessOutputsWithMetadata(
                <OpcArgument("value", Description:="The value provided."), Out>
                ByRef value As String)
            value = "Hello World!"
        End Sub



        Private Shared Function ProcessMultipleOutputs(
                <Out> ByRef value As String) As String
            value = "Hello World via Parameter!"
            Return "Hello World via Return!"
        End Function

        Private Shared Function ProcessMultipleOutputsWithMetadata(
                <OpcArgument("value1", Description:="The value1 provided."), Out>
                ByRef value As String) As <OpcArgument("value2", Description:="The value2 provided.")> String
            Return ProcessMultipleOutputs(value)
        End Function



        Private Shared Sub ProcessInOutputs(count As Integer, <Out> ByRef value As String)
            If count = 0 Then
                value = "Hello World"
            Else
                Dim builder = New StringBuilder()

                For index = 0 To count - 1
                    builder.AppendFormat("{0}. ", index + 1)
                Next

                builder.AppendFormat(" -- Hello again!")
                value = builder.ToString()
            End If
        End Sub

        Private Shared Sub ProcessInOutputsWithMetadata(
                <OpcArgument("COUNT", Description:="Countdown to hello.")>
                count As Integer,
                <OpcArgument("value", Description:="The countdown."), Out>
                ByRef value As String)
            ProcessInOutputs(count, value)
        End Sub



        Private Shared Sub ProcessMultipleInOutputs(
                first As String,
                second As String,
                <Out> ByRef result As String,
                <Out> ByRef resultLength As Integer)
            result = String.Join(", ", first, second)
            resultLength = result.Length
        End Sub

        Private Shared Sub ProcessMultipleInOutputsWithMetadata(
                <OpcArgument("First", Description:="The value joined first.")>
                first As String,
                <OpcArgument("Second", Description:="The value joined second.")>
                second As String,
                <OpcArgument("Result", Description:="The result value."), Out>
                ByRef result As String,
                <OpcArgument("Result Length", Description:="The length of the result."), Out>
                ByRef resultLength As Integer)
            ProcessMultipleInOutputs(first, second, result, resultLength)
        End Sub



        Public Shared Sub ProcessWithContext(context As OpcMethodContext)
            Console.WriteLine("Just called from session '{0}'!", context.SessionId)
        End Sub

        Public Shared Sub ProcessWithoutContext()
            Console.WriteLine("Just called!")
        End Sub
    End Class
End Namespace
