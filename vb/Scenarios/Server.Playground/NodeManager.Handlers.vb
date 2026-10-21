' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports Opc.UaFx

Namespace Server.Playground
    Partial Friend Class NodeManager
        '---------- Private methods ----------

        Private Function HandleReadVariableValue(
                context As OpcReadVariableValueContext,
                value As OpcVariableValue(Of Object)) As OpcVariableValue(Of Object)
            If context.Identity IsNot Nothing Then
                Console.WriteLine(
                        vbTab & "[{0} (SID='{1}')] Read: {2}.Value = '{3}'",
                        context.Identity?.DisplayName,
                        context.SessionId?.Value,
                        context.Node.Name,
                        value.Value)
            End If

            Return value
        End Function

        Private Function HandleWriteVariableValue(
                context As OpcWriteVariableValueContext,
                value As OpcVariableValue(Of Object)) As OpcVariableValue(Of Object)
            If context.Identity IsNot Nothing Then
                Console.WriteLine(
                        vbTab & "[{0} (SID='{1}')] Write: {2}.Value = '{3}'",
                        context.Identity?.DisplayName,
                        context.SessionId?.Value,
                        context.Node.Name,
                        value.Value)
            End If

            Return value
        End Function
    End Class
End Namespace
