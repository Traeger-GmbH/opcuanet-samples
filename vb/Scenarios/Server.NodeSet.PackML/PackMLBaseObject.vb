' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports System

Imports Opc.UaFx

Namespace NodeSet
    ''' <summary>
    ''' https://reference.opcfoundation.org/v104/PackML/v101/docs/6.3.2
    ''' </summary>
    Friend Class PackMLBaseObjectNode
        Inherits OpcObjectNode

        '---------- Public constructors ----------

        Public Sub New(parent As IOpcNode, name As OpcName, id As OpcNodeId)
            MyBase.New(parent, name, id)

            Me.AdminNode = New PackMLAdminObjectNode(Me, PackML.GetName("Admin"))
            Me.BaseStateMachineNode = New PackMLBaseStateMachineNode(Me, PackML.GetName("BaseStateMachine"))
            Me.StatusNode = New PackMLStatusObjectNode(Me, PackML.GetName("Status"))

            Me.SetMachSpeedNode = New OpcMethodNode(Me, PackML.GetName(NameOf(Me.SetMachSpeed)), New Action(Of Single)(AddressOf Me.SetMachSpeed))
            Me.SetParameterNode = New OpcMethodNode(Me, PackML.GetName(NameOf(Me.SetParameter)), New Action(Of PackMLDescriptorData())(AddressOf Me.SetParameter))
            Me.SetProductNode = New OpcMethodNode(Me, PackML.GetName(NameOf(Me.SetProduct)), New Action(Of PackMLProductData())(AddressOf Me.SetProduct))
            Me.SetUnitModeNode = New OpcMethodNode(Me, PackML.GetName(NameOf(Me.SetUnitMode)), New Action(Of Integer)(AddressOf Me.SetUnitMode))

            Me.RemoteCommandNode = New OpcMethodNode(Me, PackML.GetName(NameOf(Me.RemoteCommand)), New Action(Of PackMLRemoteInterfaceData())(AddressOf Me.RemoteCommand)) ' optional
        End Sub

        '---------- Public properties ----------

        Public ReadOnly Property AdminNode As PackMLAdminObjectNode

        Public ReadOnly Property BaseStateMachineNode As PackMLBaseStateMachineNode

        Public Property PackMLVersionNode As OpcPropertyNode(Of String) ' optional

        Public Property RemoteCommandNode As OpcMethodNode ' optional

        Public Property SetInterlockNode As OpcMethodNode ' optional

        Public ReadOnly Property SetMachSpeedNode As OpcMethodNode

        Public ReadOnly Property SetParameterNode As OpcMethodNode

        Public ReadOnly Property SetProductNode As OpcMethodNode

        Public ReadOnly Property SetUnitModeNode As OpcMethodNode

        Public ReadOnly Property StatusNode As PackMLStatusObjectNode

        Public Property TagID As OpcPropertyNode(Of String) ' optional

        '---------- Protected properties ----------

        Protected Overrides ReadOnly Property DefaultTypeDefinitionId As OpcNodeId
            Get
                Return PackML.BaseObjectId
            End Get
        End Property

        '---------- Public methods ----------

        Public Sub RemoteCommand(
                <OpcArgument("RemoteInterface")>
                remoteInterface As PackMLRemoteInterfaceData())
            Console.WriteLine(New String("-"c, 100))
            Console.WriteLine("Call to 'RemoteCommand'.")

            For itemIndex = 0 To remoteInterface.Length - 1
                Dim item = remoteInterface(itemIndex)

                Console.WriteLine("RemoteInterface[{0}]", itemIndex)
                Console.WriteLine(".Number = {0}", item.Number)
                Console.WriteLine(".ControlCmdNumber = {0}", item.ControlCmdNumber)
                Console.WriteLine(".CmdValue = {0}", item.CmdValue)
                Console.WriteLine(".Parameter")

                For parameterIndex = 0 To item.Parameter.Length - 1
                    Dim parameter = item.Parameter(parameterIndex)

                    Console.WriteLine("..Parameter[{0}]", parameterIndex)
                    Console.WriteLine("...ID = {0}", parameter.ID)
                    Console.WriteLine("...Name = {0}", parameter.Name)
                    Console.WriteLine("...Value = {0} {1}", parameter.Value, parameter.Unit.DisplayName)
                Next
            Next

            Console.WriteLine(New String("-"c, 100))
        End Sub

        Public Sub SetInterlock(
                <OpcArgument("InterlockId")>
                interlockId As Integer,
                <OpcArgument("State")>
                state As Boolean)
            Throw New OpcException(OpcStatusCode.BadNotImplemented)
        End Sub

        Public Sub SetMachSpeed(
                <OpcArgument("RequestedMachineSpeed")>
                requestedMachineSpeed As Single)
            Throw New OpcException(OpcStatusCode.BadNotImplemented)
        End Sub

        Public Sub SetParameter(
                <OpcArgument("Parameter")>
                parameter As PackMLDescriptorData())
            Throw New OpcException(OpcStatusCode.BadNotImplemented)
        End Sub

        Public Sub SetProduct(
                <OpcArgument("Product")>
                product As PackMLProductData())
            Throw New OpcException(OpcStatusCode.BadNotImplemented)
        End Sub

        Public Sub SetUnitMode(
                <OpcArgument("RequestedMode")>
                requestedMode As Integer)
            Throw New OpcException(OpcStatusCode.BadNotImplemented)
        End Sub
    End Class
End Namespace
