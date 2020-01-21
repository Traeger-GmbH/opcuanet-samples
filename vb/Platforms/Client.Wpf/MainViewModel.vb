Imports System
Imports Opc.UaFx
Imports Opc.UaFx.Client

Namespace Client.Wpf
    Public Class MainViewModel
        Inherits ViewModel

        '---------- Private fields ----------

        Private _address As Uri
        Private _addressStatus As String
        Private _client As OpcClient
        Private _subscription As OpcSubscription
        Private _nodeId As OpcNodeId
        Private _nodeStatus As String
        Private _nodeValue As Object

        '---------- Public constructors ----------

        Public Sub New()
            MyBase.New()

            Me._address = New Uri("opc.tcp://localhost:4840")
            Me._addressStatus = "Enter address of server."

            Me._nodeId = "ns=2;s=MyNode"
            Me._nodeStatus = "Enter a node identifier and click 'Subscribe'."

            Me.SubscribeCommand = New DelegateCommand(
                    execute:=Sub(__) Me.Subscribe(),
                    canExecute:=Function(__) Me.AddressIsValid AndAlso Me.NodeIdIsValid)
        End Sub

        '---------- Public properties ----------

        Public Property Address As String
            Get
                Return Me._address?.ToString()
            End Get
            Set(ByVal value As String)
                Me.RaisePropertyChanging(NameOf(Me.Address))
                Me.RaisePropertyChanging(NameOf(Me.AddressIsValid))

                Dim result As Uri = Nothing

                If Uri.TryCreate(value, UriKind.Absolute, result) _
                        AndAlso (result.Scheme = "opc.tcp" OrElse result.Scheme = "https") Then
                    Me._subscription?.Unsubscribe()
                    Me._client?.Disconnect()
                    Me._client = Nothing

                    Me._address = result

                    Me.RaisePropertyChanged(NameOf(Me.Address))
                    Me.RaisePropertyChanged(NameOf(Me.AddressIsValid))

                    Me.SubscribeCommand.RaiseCanExecuteChanged()
                Else
                    Me.AddressStatus = "Invalid address uri."
                End If
            End Set
        End Property

        Public ReadOnly Property AddressIsValid As Boolean
            Get
                Return Me.address IsNot Nothing
            End Get
        End Property

        Public Property AddressStatus As String
            Get
                Return Me._addressStatus
            End Get
            Set(ByVal value As String)
                Me.RaisePropertyChanging(NameOf(Me.AddressStatus))
                Me._addressStatus = value
                Me.RaisePropertyChanged(NameOf(Me.AddressStatus))
            End Set
        End Property

        Public Property NodeId As String
            Get
                Return Me._nodeId?.ToString()
            End Get
            Set(ByVal value As String)
                Me.RaisePropertyChanging(NameOf(Me.NodeId))
                Me.RaisePropertyChanging(NameOf(Me.NodeIdIsValid))

                Dim result = Nothing

                If OpcNodeId.TryParse(value, result) Then
                    Me._nodeId = result

                    Me.RaisePropertyChanged(NameOf(Me.NodeId))
                    Me.RaisePropertyChanged(NameOf(Me.NodeIdIsValid))

                    Me.SubscribeCommand.RaiseCanExecuteChanged()
                Else
                    Me.NodeStatus = "Invalid node identifier."
                End If
            End Set
        End Property

        Public ReadOnly Property NodeIdIsValid As Boolean
            Get
                Return Me._nodeId IsNot Nothing AndAlso Me._nodeId <> OpcNodeId.Null
            End Get
        End Property

        Public Property NodeStatus As String
            Get
                Return Me._nodeStatus
            End Get
            Set(ByVal value As String)
                Me.RaisePropertyChanging(NameOf(Me.NodeStatus))
                Me._nodeStatus = value
                Me.RaisePropertyChanged(NameOf(Me.NodeStatus))
            End Set
        End Property

        Public Property NodeValue As Object
            Get
                Return Me._nodeValue
            End Get
            Set(ByVal value As Object)
                Me.RaisePropertyChanging(NameOf(Me.NodeValue))
                Me._nodeValue = value
                Me.RaisePropertyChanged(NameOf(Me.NodeValue))
            End Set
        End Property

        Public ReadOnly Property SubscribeCommand As DelegateCommand

        '---------- Public methods ----------

        Public Sub Subscribe()
            Try
                If Me._client Is Nothing Then
                    Me._client = New OpcClient(Me.Address)
                    Me._client.Connect()
                End If

                If Me._subscription IsNot Nothing Then Me._subscription.Unsubscribe()

                Me._subscription = Me._client.SubscribeDataChange(
                        Me.NodeId,
                        AddressOf Me.HandleDataChangeReceived)

                Dim monitoredItem = Me._subscription.MonitoredItems(0)
                Me.NodeStatus = monitoredItem.Status.[Error].Description
            Catch ex As Exception
                Me.NodeStatus = ex.Message
            End Try
        End Sub

        '---------- Private methods ----------

        Private Sub HandleDataChangeReceived(ByVal sender As Object, ByVal e As OpcDataChangeReceivedEventArgs)
            Dim value = e.Item.Value

            Me.NodeStatus = value.Status.Description
            Me.NodeValue = $"{value.Value} ({e.MonitoredItem.NodeId})"
        End Sub
    End Class
End Namespace
