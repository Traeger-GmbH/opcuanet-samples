' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Linq

Imports Opc.UaFx

Namespace Server.Playground
    Partial Friend Class NodeManager
        '---------- Private methods ----------

        Private Function CreateArrayNode(Of T)(
                nodes As OpcFolderNode,
                defaultValue As T,
                Optional randomizer As Func(Of T, Integer, T) = Nothing) As OpcDataVariableNode(Of T())
            Dim value = Enumerable.Repeat(defaultValue, 5).ToArray()

            If randomizer Is Nothing Then
                Return Me.CreateNode(nodes, value, randomizer:=Nothing)
            End If

            Return Me.CreateNode(nodes, value,
                Function(oldValue, random)
                    Dim index = Me.indexRandom.Next(0, oldValue.Length - 1)

                    ''''Dim newValue = oldValue

                    ' Array values need to be copied, otherwise there is no
                    ' Value-Changed-Bit set by the node.
                    Dim newValue = New T(oldValue.Length - 1) {}
                    Array.Copy(oldValue, newValue, oldValue.Length)

                    newValue(index) = randomizer(oldValue(index), random)
                    Return newValue
                End Function)
        End Function

        Private Function CreateNode(Of T)(
                nodes As OpcFolderNode,
                defaultValue As T,
                Optional randomizer As Func(Of T, Integer, T) = Nothing) As OpcDataVariableNode(Of T)
            Dim name = GetType(T).Name
            name = Char.ToUpper(name(0)) & name.Substring(1)

            If GetType(T).IsArray Then
                name = GetType(T).GetElementType().Name & "Array"
            End If

            Dim node As New OpcDataVariableNode(Of T)(nodes, name, defaultValue)

            node.ReadVariableValueCallback = AddressOf Me.HandleReadVariableValue
            node.WriteVariableValueCallback = AddressOf Me.HandleWriteVariableValue

            If randomizer IsNot Nothing Then
                Me.tasks.Add(
                    Sub()
                        node.Value = randomizer(node.Value, Me.valueRandom.Next(0, 183))
                        node.ApplyChanges(Me.SystemContext)
                    End Sub)
            End If

            Return node
        End Function
    End Class
End Namespace
