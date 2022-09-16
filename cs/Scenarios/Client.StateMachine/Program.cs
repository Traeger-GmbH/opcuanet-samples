namespace Client.StateMachine
{
    using System;
    using Opc.UaFx.Client;

    public class Program
    {
        public static void Main()
        {
            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.Connect();
                Console.WriteLine("Connected");

                client.SubscribeDataChange("ns=2;s=SampleStateMachine/CurrentState", HandleDataChanged);

                Console.WriteLine("Perform state changes now!");
                Console.ReadLine();
            }
        }

        private static void HandleDataChanged(object sender,OpcDataChangeReceivedEventArgs e)
        {
            Console.WriteLine("State changed to: {0}", e.Item.Value);
        }
    }
}
