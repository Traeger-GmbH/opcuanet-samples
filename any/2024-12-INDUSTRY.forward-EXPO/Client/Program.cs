
using Opc.UaFx;
using Opc.UaFx.Client;

public class Program
{
    public static void Main()
    {
        using var client = new OpcClient("opc.tcp://localhost:4841");

        client.Connect();
        Console.WriteLine("Connected.");

        var shopfloorNode = client.BrowseNode(client.TranslatePath("Objects/Shopfloor"));

        foreach (var machineNode in shopfloorNode.Children()) {
            var jobCompletedNode = machineNode.Child("JobCompleted");

            var subscription = client.SubscribeDataChange(
                    jobCompletedNode.NodeId, HandleJobCompleted);

            subscription.Tag = machineNode;
        }

        Console.WriteLine("Subscribed.");
        Console.ReadLine();
    }

    private static void HandleJobCompleted(object sender, OpcDataChangeReceivedEventArgs e)
    {
        var jobCompleted = e.Item.Value.As<bool>();

        if (jobCompleted) {
            var machineNode = (OpcNodeInfo)e.MonitoredItem.Subscription.Tag;

            var values = from child in machineNode.Children()
                         where child is OpcVariableNodeInfo
                         let value = child.AttributeValue(OpcAttribute.Value)
                         select value?.ToString();

            Console.WriteLine(string.Join("\t", values));
        }
    }
}