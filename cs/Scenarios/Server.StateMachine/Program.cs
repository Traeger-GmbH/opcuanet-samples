namespace Server.StateMachine
{
    using System;

    using Opc.UaFx.Server;

    /// <summary>
    /// This use case demonstrates how to implement a finite state machine node with custom states
    /// and how the server changes its state.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            var sampleStateMachineNode = new SampleStateMachineNode("SampleStateMachine");

            using (var server = new OpcServer("opc.tcp://localhost:4840/", sampleStateMachineNode)) {
                server.Start();

                Console.WriteLine("Started.");

                while (true) {

                    Console.WriteLine("Enter <enter> to change state.");
                    Console.ReadLine();

                    sampleStateMachineNode.ChangeStateTo(server.SystemContext, SampleStates.Starting);

                    Console.WriteLine("Enter <enter> to change state.");
                    Console.ReadLine();

                    sampleStateMachineNode.ChangeStateTo(server.SystemContext, SampleStates.Started);

                    Console.WriteLine("Enter <enter> to change state.");
                    Console.ReadLine();

                    sampleStateMachineNode.ChangeStateTo(server.SystemContext, SampleStates.Stopping);

                    Console.WriteLine("Enter <enter> to change state.");
                    Console.ReadLine();

                    sampleStateMachineNode.ChangeStateTo(server.SystemContext, SampleStates.Stopped);
                }
            }
        }
    }
}