' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System.Collections.Generic
Imports Opc.UaFx

Namespace MethodNode
    ''' <content>
    ''' Defines some command types used by the <see cref="SampleNodeManager"/>.
    ''' </content>
    Partial Friend Class SampleNodeManager
        Public Class AddCommand
            Implements IOpcMethodCommand

            Public Function CanExecute(context As OpcContext) As Boolean Implements IOpcMethodCommand.CanExecute
                Return True
            End Function

            Public Function CanUserExecute(context As OpcContext) As Boolean Implements IOpcMethodCommand.CanUserExecute
                Return True
            End Function

            Public Sub Execute(context As OpcMethodContext, inputArguments As IList(Of Object), outputArguments As IList(Of Object)) Implements IOpcMethodCommand.Execute
                ' result = a + b
                outputArguments(0) = CInt(inputArguments(0)) + CInt(inputArguments(1))
            End Sub
        End Class

        Public Class MultiplyCommand
            Implements IOpcMethodCommand

            Public Function CanExecute(context As OpcContext) As Boolean Implements IOpcMethodCommand.CanExecute
                Return True
            End Function

            Public Function CanUserExecute(context As OpcContext) As Boolean Implements IOpcMethodCommand.CanUserExecute
                Return True
            End Function

            Public Sub Execute(context As OpcMethodContext, inputArguments As IList(Of Object), outputArguments As IList(Of Object)) Implements IOpcMethodCommand.Execute
                ' result = a * b
                outputArguments(0) = CInt(inputArguments(0)) * CInt(inputArguments(1))
            End Sub
        End Class
    End Class
End Namespace
