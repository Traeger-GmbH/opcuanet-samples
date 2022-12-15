namespace Umati
{
    using System;
    using Opc.UaFx.Client;

    public class Program
    {
        public static void Main()
        {
            using (var client = new OpcClient("opc.tcp://localhost:4840")) {
                client.Connect();

                var filter = OpcFilter.Using(client)
                        .FromEvents("ns=2;i=1042")
                        .Select();

                client.SubscribeEvent("ns=3;i=5031", filter, (sender, e) => {
                    if (e.Event is MachineToolAlarmCondition alarm) {
                        Console.WriteLine(new Guid(alarm.EventId.ToArray()).ToString());
                        Console.WriteLine("- AlarmIdentifier: {0}", alarm.AlarmIdentifier);
                        Console.WriteLine("- AuxParameters: {0}", alarm.AuxParameters);
                    }
                });

                Console.WriteLine("Press any key to exit.");
                Console.ReadKey(true);
            }
        }
    }
}
