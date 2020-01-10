namespace Client
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    public class Program
    {
        public static void Main()
        {
            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.Connect();
                Console.WriteLine("Connected!");

                Console.WriteLine("Press any key to add a new 'Speed' node with history to the 'Machine' node.");
                Console.ReadLine();

                var result = client.AddNode(new OpcAddDataVariableNode<int>(
                        name: "Speed",
                        nodeId: OpcNodeId.Null, // the server shall define it
                        parentNodeId: "ns=2;s=Machine") {
                    //IsHistorizing = true,
                    Value = 1000,
                    AccessLevel = OpcAccessLevel.CurrentReadOrWrite | OpcAccessLevel.HistoryReadOrWrite,
                    UserAccessLevel = OpcAccessLevel.CurrentReadOrWrite | OpcAccessLevel.HistoryReadOrWrite,
                });

                if (result.IsBad) {
                    Console.WriteLine(result.Description);
                }
                else {
                    Console.WriteLine(
                            "Enter a number and press enter to write it as the new speed value "
                            + "which is also added to the history.");

                    while (int.TryParse(Console.ReadLine(), out var speed))
                        client.WriteNode(result.NodeId, new OpcValue(speed, DateTime.UtcNow));
                }
            }
        }
    }
}
