' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System
Imports System.Collections.Generic
Imports System.Threading

Imports Opc.UaFx
Imports Opc.UaFx.Server

Namespace Server.Playground
    Partial Friend Class NodeManager
        Inherits OpcNodeManager

        '---------- Private readonly fields ----------

        Private ReadOnly indexRandom As Random
        Private ReadOnly valueRandom As Random

        Private ReadOnly tasks As List(Of Action)

        '---------- Public constructors ----------

        Public Sub New()
            MyBase.New("http://sampleserver/")

            Me.indexRandom = New Random()
            Me.valueRandom = New Random()

            Me.tasks = New List(Of Action)()
        End Sub

        '---------- Public methods ----------

        Public Sub Simulate(semaphore As SemaphoreSlim)
            While Not semaphore.Wait(TimeSpan.FromSeconds(3))
                For Each task In Me.tasks
                    task.Invoke()
                Next
            End While
        End Sub

        '---------- Protected methods ----------

        Protected Overrides Iterator Function CreateNodes(references As OpcNodeReferenceCollection) As IEnumerable(Of IOpcNode)
            Dim nodes As New OpcFolderNode(Me.DefaultNamespace.GetName("Nodes"))
            references.Add(nodes, OpcObjectTypes.ObjectsFolder)

            Yield nodes

            Me.CreateStaticNodes(nodes)
            Me.CreateDynamicNodes(nodes)
            Me.CreateMethodNodes(nodes)

            Yield New OpcDataTypeNode(Of MyEnum)()
            Yield New OpcDataTypeNode(Of MyEnumFlags)()
        End Function

        '---------- Private methods ----------

        Private Sub CreateDynamicNodes(nodes As OpcFolderNode)
            Dim dynamicNodes As New OpcFolderNode(nodes, "Dynamic")

            Me.CreateNode(Of Byte)(dynamicNodes, 1, Function(old, random) CByte(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))
            Me.CreateArrayNode(Of Byte)(dynamicNodes, 1, Function(old, random) CByte(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))

            Me.CreateNode(Of SByte)(dynamicNodes, 2, Function(old, random) CSByte(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))
            Me.CreateArrayNode(Of SByte)(dynamicNodes, 2, Function(old, random) CSByte(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))

            Me.CreateNode(Of Short)(dynamicNodes, 3, Function(old, random) CShort(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))
            Me.CreateArrayNode(Of Short)(dynamicNodes, 3, Function(old, random) CShort(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))

            Me.CreateNode(Of UShort)(dynamicNodes, 4, Function(old, random) CUShort(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))
            Me.CreateArrayNode(Of UShort)(dynamicNodes, 4, Function(old, random) CUShort(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))

            Me.CreateNode(Of Integer)(dynamicNodes, 5, Function(old, random) DateTime.Now.Second * random \ Math.Max(old, 1))
            Me.CreateArrayNode(Of Integer)(dynamicNodes, 5, Function(old, random) DateTime.Now.Second * random \ Math.Max(old, 1))

            Me.CreateNode(Of UInteger)(dynamicNodes, 6UI, Function(old, random) CUInt(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))
            Me.CreateArrayNode(Of UInteger)(dynamicNodes, 6UI, Function(old, random) CUInt(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))

            Me.CreateNode(Of Long)(dynamicNodes, 7L, Function(old, random) CLng(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))
            Me.CreateArrayNode(Of Long)(dynamicNodes, 7L, Function(old, random) CLng(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))

            Me.CreateNode(Of ULong)(dynamicNodes, 8UL, Function(old, random) CULng(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))
            Me.CreateArrayNode(Of ULong)(dynamicNodes, 8UL, Function(old, random) CULng(DateTime.Now.Second * random \ Math.Max(CInt(old), 1)))

            Me.CreateNode(Of Single)(dynamicNodes, 9.1F, Function(old, random) CSng(DateTime.Now.Second * random) / Math.Max(CInt(Fix(old)), 1))
            Me.CreateArrayNode(Of Single)(dynamicNodes, 9.1F, Function(old, random) CSng(DateTime.Now.Second * random \ Math.Max(CInt(Fix(old)), 1)))

            Me.CreateNode(Of Double)(dynamicNodes, 11.12, Function(old, random) CDbl(DateTime.Now.Second * random \ Math.Max(CInt(Fix(old)), 1)))
            Me.CreateArrayNode(Of Double)(dynamicNodes, 11.12, Function(old, random) CDbl(DateTime.Now.Second * random \ Math.Max(CInt(Fix(old)), 1)))

            Dim stringValue = Me.RandomString()

            Me.CreateNode(Of String)(dynamicNodes, stringValue, AddressOf Me.RandomString)
            Me.CreateArrayNode(Of String)(dynamicNodes, stringValue, AddressOf Me.RandomString)

            Me.CreateNode(Of MyEnum)(dynamicNodes, MyEnum.Starting, AddressOf Me.RandomEnum)
            'Me.CreateArrayNode(Of MyEnum)(dynamicNodes, MyEnum.Maintenance, AddressOf Me.RandomEnum)

            Me.CreateNode(Of MyEnumFlags)(dynamicNodes, MyEnumFlags.CanCut Or MyEnumFlags.CanLaminate, AddressOf Me.RandomEnum)
            'Me.CreateArrayNode(Of MyEnumFlags)(dynamicNodes, MyEnumFlags.CanFold Or MyEnumFlags.CanPrint, AddressOf Me.RandomEnum)
        End Sub

        Private Sub CreateMethodNodes(nodes As OpcFolderNode)
            Dim methodNodes As New OpcFolderNode(nodes, "Methods")

            Dim addNode As New OpcMethodNode(methodNodes, "Add", New Func(Of Integer, Integer, Integer)(AddressOf Me.Add))
            Dim divideNode As New OpcMethodNode(methodNodes, "Divide", New Func(Of Integer, Integer, Integer)(AddressOf Me.Divide))
            Dim multiplyNode As New OpcMethodNode(methodNodes, "Multiply", New Func(Of Integer, Integer, Integer)(AddressOf Me.Multiply))
            Dim powerNode As New OpcMethodNode(methodNodes, "Power", New Func(Of Integer, Integer, Integer)(AddressOf Me.Power))
            Dim subtractNode As New OpcMethodNode(methodNodes, "Subtract", New Func(Of Integer, Integer, Integer)(AddressOf Me.Substract))
        End Sub

        Private Sub CreateStaticNodes(nodes As OpcFolderNode)
            Dim staticNodes As New OpcFolderNode(nodes, "Static")

            Me.CreateNode(Of Byte)(staticNodes, 1)
            Me.CreateArrayNode(Of Byte)(staticNodes, 1)

            Me.CreateNode(Of SByte)(staticNodes, 2)
            Me.CreateArrayNode(Of SByte)(staticNodes, 2)

            Me.CreateNode(Of Short)(staticNodes, 3)
            Me.CreateArrayNode(Of Short)(staticNodes, 3)

            Me.CreateNode(Of UShort)(staticNodes, 4)
            Me.CreateArrayNode(Of UShort)(staticNodes, 4)

            Me.CreateNode(Of Integer)(staticNodes, 5)
            Me.CreateArrayNode(Of Integer)(staticNodes, 5)

            Me.CreateNode(Of UInteger)(staticNodes, 6UI)
            Me.CreateArrayNode(Of UInteger)(staticNodes, 6UI)

            Me.CreateNode(Of Long)(staticNodes, 7L)
            Me.CreateArrayNode(Of Long)(staticNodes, 7L)

            Me.CreateNode(Of ULong)(staticNodes, 8UL)
            Me.CreateArrayNode(Of ULong)(staticNodes, 8UL)

            Me.CreateNode(Of Single)(staticNodes, 9.1F)
            Me.CreateArrayNode(Of Single)(staticNodes, 9.1F)

            Me.CreateNode(Of Double)(staticNodes, 11.12)
            Me.CreateArrayNode(Of Double)(staticNodes, 11.12)

            Dim stringValue = Me.RandomString()

            Me.CreateNode(Of String)(staticNodes, stringValue)
            Me.CreateArrayNode(Of String)(staticNodes, stringValue)

            Me.CreateNode(Of MyEnum)(staticNodes, MyEnum.Maintenance)
            'Me.CreateArrayNode(Of MyEnum)(staticNodes, MyEnum.Starting)

            Me.CreateNode(Of MyEnumFlags)(staticNodes, MyEnumFlags.CanCut Or MyEnumFlags.CanFold)
            'Me.CreateArrayNode(Of MyEnumFlags)(staticNodes, MyEnumFlags.CanPrint Or MyEnumFlags.CanCorrugate)
        End Sub
    End Class
End Namespace
