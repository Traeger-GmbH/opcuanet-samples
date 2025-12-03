namespace App.Emotion
{
    using App.AI;
    using App.Vision;

    using Microsoft.ML.OnnxRuntime;

    public class EmotionObserver : IDisposable
    {
        private volatile bool disposed;
        private string? previousEmotion;
        private readonly Action<string> callback;


        private EmotionObserver(Action<string> changedCallack)
            : base()
        {
            this.callback = changedCallack;
        }


        public static EmotionObserver StartNew(Action<string> callback)
        {
            var instance = new EmotionObserver(callback);

            instance.Start();

            return instance;
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                this.disposed = true;
        }


        private void Start()
        {
            var preprocessor = Preprocessor.WithOptions(@"./AI/preprocessor_config.json");
            var runtime = OnnxModelRuntime.WithModel(@"./AI/model.onnx");
            var postprocessor = Postprocessor.WithOptions(@"./AI/config.json");

            while (!this.disposed) {
                var snapshot = Camera.CaptureSnapshot();

                var tensor = preprocessor.Process(snapshot);

                var results = runtime
                        .Inputs(name => NamedOnnxValue.CreateFromTensor(name, tensor))
                        .Execute(result => result.AsEnumerable<float>());

                var label = postprocessor.Process(results.First());

                if (this.previousEmotion is null || !string.Equals(label, this.previousEmotion)) {
                    this.previousEmotion = label;
                    this.callback(label);
                }

                Thread.Sleep(100);
            }
        }
    }
}
