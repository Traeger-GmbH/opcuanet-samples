
using Opc.UaFx;
using Opc.UaFx.Server;

public class Program
{
    public static void Main()
    {
        var shopfloorNode = new OpcFolderNode("Shopfloor");

        var mac01Node = new OpcObjectNode(
                shopfloorNode,
                name: "MAC01",
                new OpcDataVariableNode<string>("Job", "JOB123"),
                new OpcDataVariableNode<double>("Duration", value: 24000),
                new OpcDataVariableNode<int>("Quantity", value: 1200),
                new OpcDataVariableNode<short>("Temperature", value: 987),
                new OpcDataVariableNode<bool>("JobCompleted", value: false),
                new OpcMethodNode("StartJob", StartJob));

        var mac02Node = new OpcObjectNode(
                shopfloorNode,
                name: "MAC02",
                new OpcDataVariableNode<string>("Job", "JOB124"),
                new OpcDataVariableNode<double>("Duration", value: 23000),
                new OpcDataVariableNode<int>("Quantity", value: 1400),
                new OpcDataVariableNode<short>("Temperature", value: 789),
                new OpcDataVariableNode<bool>("JobCompleted", value: false),
                new OpcMethodNode("StartJob", StartJob));

        using var server = new OpcServer("opc.tcp://localhost:4841", shopfloorNode);
        server.Start();

        Console.WriteLine("Server started.");
        Console.ReadLine();
    }

    private static void StartJob(OpcMethodContext context)
    {
        var machineNode = context.Node.Parent;
        var temperatureNode = (OpcDataVariableNode)machineNode.Child("Temperature");

        var jobNode = (OpcDataVariableNode)machineNode.Child("Job");
        var jobCompletedNode = (OpcDataVariableNode)machineNode.Child("JobCompleted");

        jobCompletedNode.Value = false;
        jobCompletedNode.ApplyChanges(context);

        Console.WriteLine("Starting Job ...");

        var random = Random.Shared.Next(5000);
        var jobName = $"JOB{random}";

        jobNode.Value = jobName;
        jobNode.ApplyChanges(context);

        // Do some stuff.
        Thread.Sleep(random);

        temperatureNode.Value = random;
        jobCompletedNode.Value = true;

        machineNode.ApplyChanges(context, recursive: true);
        Console.WriteLine($"Job '{jobName}' completed.");
    }
}
