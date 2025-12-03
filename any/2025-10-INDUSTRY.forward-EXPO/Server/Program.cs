namespace App
{
    using Opc.UaFx.MachineVision;

    public class Program
    {
        public static void Main()
        {
            var manager = new VisionNodeManager();

            using var server = new OpcMachineVisionServer(manager);
            server.Start();

            manager.AddResult(content: "happy");
            manager.AddResult(content: "angry");
            manager.AddResult(content: "fear");

            Console.WriteLine("Started.");
            Console.ReadLine();
        }
    }
}
