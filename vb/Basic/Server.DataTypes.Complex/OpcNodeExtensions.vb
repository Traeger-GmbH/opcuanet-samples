' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Linq
Imports System.Runtime.CompilerServices

Imports Opc.UaFx

Namespace DataTypes.Complex
    ''' <summary>
    '''
    ''' </summary>
    Public Module OpcNodeExtensions
        '---------- Public static methods ----------

        <Extension>
        Public Function CreateFieldNode(Of T)(
                parent As IOpcNode,
                name As OpcName,
                getter As Func(Of T),
                setter As Action(Of T)) As OpcDataVariableNode(Of T)
            Return CreateFieldNode(Of OpcDataVariableNode(Of T), T)(
                    Function() New OpcDataVariableNode(Of T)(parent, name),
                    getter,
                    setter)
        End Function

        <Extension>
        Public Function CreateFieldNode(Of TNode As OpcDataVariableNode(Of T), T As New)(
                parent As IOpcNode,
                name As OpcName,
                getter As Func(Of T()),
                setter As Action(Of T()),
                itemConstructor As Func(Of IOpcNode, OpcName, T, TNode)) As OpcDataVariableNode(Of T())
            Dim fieldNode = New OpcDataVariableNode(Of T())(parent, name)

            fieldNode.ReadVariableValueCallback = AddressOf ReadFieldValue(Of T())
            fieldNode.WriteVariableValueCallback = AddressOf WriteFieldValue(Of T())

            Dim value = getter()

            For index = 0 To value.Length - 1
                Dim itemIndex = index

                CreateFieldNode(Of TNode, T)(
                        Function() itemConstructor(fieldNode, $"[{itemIndex}]", value(itemIndex)),
                        Function() getter()(itemIndex),
                        Sub(itemValue) getter()(itemIndex) = itemValue)
            Next

            fieldNode.Tag = (getter, setter)
            Return fieldNode
        End Function

        '---------- Private static methods ----------

        Private Function CreateFieldNode(Of TNode As OpcDataVariableNode(Of T), T)(
                constructor As Func(Of TNode),
                getter As Func(Of T),
                setter As Action(Of T)) As TNode
            Dim fieldNode = constructor()

            fieldNode.ReadVariableValueCallback = AddressOf ReadFieldValue(Of T)
            fieldNode.WriteVariableValueCallback = AddressOf WriteFieldValue(Of T)

            fieldNode.Tag = (getter, setter)
            Return fieldNode
        End Function

        Private Function ReadFieldValue(Of T)(
                context As OpcReadVariableValueContext,
                value As OpcVariableValue(Of Object)) As OpcVariableValue(Of Object)
            If TypeOf context.Node.Tag Is ValueTuple(Of Func(Of T), Action(Of T)) Then
                Dim [property] = DirectCast(context.Node.Tag, ValueTuple(Of Func(Of T), Action(Of T)))
                value = New OpcVariableValue(Of Object)([property].Item1.Invoke())
            End If

            Return value
        End Function

        Private Function WriteFieldValue(Of T)(
                context As OpcWriteVariableValueContext,
                value As OpcVariableValue(Of Object)) As OpcVariableValue(Of Object)
            If TypeOf context.Node.Tag Is ValueTuple(Of Func(Of T), Action(Of T)) Then
                Dim [property] = DirectCast(context.Node.Tag, ValueTuple(Of Func(Of T), Action(Of T)))
                [property].Item2.Invoke(DirectCast(value.Value, T))
                DirectCast(context.Node.Parent, OpcNode).UpdateChanges(context, OpcNodeChanges.Value)
            End If

            Return value
        End Function
    End Module
End Namespace
