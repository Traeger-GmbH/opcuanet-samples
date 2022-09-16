namespace Server.StateMachine
{
    using Opc.UaFx;

    [OpcStateMachineInitialState(SampleStates.Stopped)]
    [OpcStateMachineStates(typeof(SampleStates))]
    [OpcStateMachineTransitions(typeof(SampleStates))]
    internal class SampleStateMachineNode : OpcFiniteStateMachineNode
    {
        #region ---------- Public constructors ----------

        public SampleStateMachineNode(OpcName name)
            : base(name)
        {
        }

        #endregion
    }
}
