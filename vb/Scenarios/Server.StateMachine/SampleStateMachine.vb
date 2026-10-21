' Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

Imports Opc.UaFx

Namespace Server.StateMachine
    <OpcStateMachineInitialState(SampleStates.Stopped)>
    <OpcStateMachineStates(GetType(SampleStates))>
    <OpcStateMachineTransitions(GetType(SampleStates))>
    Friend Class SampleStateMachineNode
        Inherits OpcFiniteStateMachineNode

        '---------- Public constructors ----------

        Public Sub New(name As OpcName)
            MyBase.New(name)
        End Sub
    End Class
End Namespace
