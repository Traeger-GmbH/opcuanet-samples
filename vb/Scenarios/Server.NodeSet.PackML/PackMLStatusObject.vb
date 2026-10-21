' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeSet
    ''' <summary>
    ''' https://reference.opcfoundation.org/v104/PackML/v101/docs/6.3.3
    ''' </summary>
    Friend Class PackMLStatusObjectNode
        Inherits OpcObjectNode

        '---------- Public constructors ----------

        Public Sub New(parent As IOpcNode, name As OpcName)
            MyBase.New(parent, name)
            ' TODO: Implement according to companion specification.
        End Sub

        '---------- Protected properties ----------

        Protected Overrides ReadOnly Property DefaultTypeDefinitionId As OpcNodeId
            Get
                Return PackML.StatusObjectId
            End Get
        End Property
    End Class
End Namespace
