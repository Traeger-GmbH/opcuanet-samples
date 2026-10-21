' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeSet.Umati
    Friend Module Di
        '---------- Public const fields ----------

        Public Const NamespaceUri As String = "http://opcfoundation.org/UA/DI/"

        '---------- Public static readonly fields ----------

        Public ReadOnly [Namespace] As OpcNamespace = OpcNamespace.Create(NamespaceUri)
    End Module
End Namespace
