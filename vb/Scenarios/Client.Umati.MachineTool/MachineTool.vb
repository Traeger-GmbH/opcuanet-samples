' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace Umati
    Friend Module MachineTool
        '---------- Public const fields ----------

        Public Const NamespaceUri As String = "http://opcfoundation.org/UA/MachineTool/"


        '---------- Public static readonly fields ----------

        Public ReadOnly [Namespace] As OpcNamespace = OpcNamespace.Create(NamespaceUri)

        Public Const AlertTypeId As Integer = 39


        '---------- Public static properties (object type identifiers) ----------

        Public ReadOnly Property AlertType As OpcNodeId
            Get
                Return [Namespace].GetId(AlertTypeId)
            End Get
        End Property
    End Module
End Namespace
