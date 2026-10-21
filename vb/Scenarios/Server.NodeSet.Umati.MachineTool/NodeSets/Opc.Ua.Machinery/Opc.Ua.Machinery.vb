' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeSet.Umati
    Friend Module Machinery
        '---------- Public const fields ----------

        Public Const NamespaceUri As String = "http://opcfoundation.org/UA/Machinery/"

        Public Const MachinesId As Integer = 1001

        '---------- Public static readonly fields ----------

        Public ReadOnly [Namespace] As OpcNamespace = OpcNamespace.Create(NamespaceUri)

        '---------- Public static properties (object identifiers) ----------

        Public ReadOnly Property Machines As OpcNodeId
            Get
                Return [Namespace].GetId(MachinesId)
            End Get
        End Property
    End Module
End Namespace
