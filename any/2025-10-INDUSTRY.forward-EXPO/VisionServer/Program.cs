namespace App
{
    using App.Emotion;

    using Opc.UaFx.MachineVision;

    public class Program
    {
        public static void Main()
        {
            var manager = new VisionNodeManager();

            using var server = new OpcMachineVisionServer(manager);
            server.Start();

            using var observer = EmotionObserver.StartNew(emotion => {
                manager.AddResult(emotion);

                Console.Clear();
                Console.WriteLine($"{DateTime.Now.TimeOfDay}: {emotion}");
            });

            Console.WriteLine("Started.");
            Console.ReadLine();
        }
    }
}
