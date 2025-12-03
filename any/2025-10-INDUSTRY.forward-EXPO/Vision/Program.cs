namespace App
{
    using App.AI;
    using App.Vision;

    using Microsoft.ML.OnnxRuntime;

    public class Program
    {
        public static void Main()
        {
            // https://www.ultralytics.com/
            // https://adamharley.com/nn_vis/cnn/2d.html

            var preprocessor = Preprocessor.WithOptions(@"./AI/preprocessor_config.json");
            var runtime = OnnxModelRuntime.WithModel(@"./AI/model.onnx");
            var postprocessor = Postprocessor.WithOptions(@"./AI/config.json");

            while (true) {
                var snapshot = Camera.CaptureSnapshot();

                var tensor = preprocessor.Process(snapshot);

                var results = runtime
                        .Inputs(name => NamedOnnxValue.CreateFromTensor(name, tensor))
                        .Execute(result => result.AsEnumerable<float>());

                var emotion = postprocessor.Process(results.First());

                Console.Clear();
                Console.WriteLine($"{DateTime.Now.TimeOfDay}: {emotion}");

                Thread.Sleep(100);
            }
        }
    }
}
