'Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace EventTypes
    <OpcEventType("ns=1;s=MachineJobEventType")>
    Public Class MachineJobEvent
        Inherits OpcEvent

        '---------- Public constructors ----------

        Public Sub New(ByVal dataStore As IOpcReadOnlyNodeDataStore)
            MyBase.New(dataStore)
        End Sub

        '---------- Public properties ----------

        Public ReadOnly Property Job As MachineJob
            Get
                Return Me.DataStore.Get(Of MachineJob)($"1:{NameOf(Me.Job)}")
            End Get
        End Property
    End Class
End Namespace
