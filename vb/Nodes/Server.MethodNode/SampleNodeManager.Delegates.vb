' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System.Runtime.InteropServices
Imports Opc.UaFx

Namespace MethodNode
    ''' <content>
    ''' Defines some delegate types used by the <see cref="SampleNodeManager"/>.
    ''' </content>
    Partial Friend Class SampleNodeManager
        Public Delegate Function AddDelegate(context As OpcMethodContext, a As Integer, b As Integer) As Integer
        Public Delegate Function MultiplyDelegate(a As Integer, b As Integer) As Integer

        Public Delegate Sub ProcessOutputsDelegate(<Out> ByRef value As String)
        Public Delegate Function ProcessMultipleOutputsDelegate(<Out> ByRef value As String) As String

        Public Delegate Sub ProcessInOutputsDelegate(count As Integer, <Out> ByRef value As String)
        Public Delegate Sub ProcessMultipleInOutputsDelegate(
                first As String,
                second As String,
                <Out> ByRef result As String,
                <Out> ByRef resultLength As Integer)
    End Class
End Namespace
