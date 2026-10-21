' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeSet.Umati
    Friend Module MachineTool
        '---------- Public const fields ----------

        Public Const NamespaceUri As String = "http://opcfoundation.org/UA/MachineTool/"

        '---------- Public static readonly fields ----------

        Public ReadOnly [Namespace] As OpcNamespace = OpcNamespace.Create(NamespaceUri)

        Public Const NotificationTypeId As Integer = 7
        Public Const MachineToolTypeId As Integer = 13
        Public Const AlertTypeId As Integer = 39

        '---------- Public static properties (object type identifiers) ----------

        Public ReadOnly Property NotificationType As OpcNodeId
            Get
                Return [Namespace].GetId(NotificationTypeId)
            End Get
        End Property

        Public ReadOnly Property MachineToolType As OpcNodeId
            Get
                Return [Namespace].GetId(MachineToolTypeId)
            End Get
        End Property

        Public ReadOnly Property AlertType As OpcNodeId
            Get
                Return [Namespace].GetId(AlertTypeId)
            End Get
        End Property

        '---------- Public static methods ----------

        Public Function GetId(value As Integer) As OpcNodeId
            Return [Namespace].GetId(value)
        End Function

        Public Function GetName(name As String) As OpcName
            Return [Namespace].GetName(name)
        End Function
    End Module
End Namespace
