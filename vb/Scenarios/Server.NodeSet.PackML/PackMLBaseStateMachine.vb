' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace NodeSet
    ''' <summary>
    ''' https://reference.opcfoundation.org/v104/PackML/v101/docs/6.3.6
    ''' </summary>
    Friend Class PackMLBaseStateMachineNode
        Inherits OpcObjectNode

        '---------- Public constructors ----------

        Public Sub New(parent As IOpcNode, name As OpcName)
            MyBase.New(parent, name)
            ' TODO: Implement according to companion specification.
        End Sub

        '---------- Protected properties ----------

        Protected Overrides ReadOnly Property DefaultTypeDefinitionId As OpcNodeId
            Get
                Return PackML.BaseStateMachineId
            End Get
        End Property
    End Class
End Namespace
