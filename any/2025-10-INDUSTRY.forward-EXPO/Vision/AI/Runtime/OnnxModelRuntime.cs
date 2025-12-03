namespace App.AI
{
    using Microsoft.ML.OnnxRuntime;

    public class OnnxModelRuntime : IDisposable
    {
        #region ---------- Private readonly fields ----------

        private readonly InferenceSession session;
        private readonly List<NamedOnnxValue> inputs;
        private readonly List<string> outputs;

        #endregion

        #region ---------- Private fields ----------

        private bool isDisposed;

        #endregion

        #region ---------- Private constructors ----------

        private OnnxModelRuntime(string modelPath)
            : base()
        {
            this.session = new InferenceSession(modelPath);

            this.inputs = new List<NamedOnnxValue>();
            this.outputs = new List<string>(this.session.OutputMetadata.Keys);
        }

        #endregion

        #region ---------- Public static methods ----------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelPath"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static OnnxModelRuntime WithModel(string modelPath)
        {
            var path = Path.GetFullPath(modelPath);

            if (!File.Exists(path))
                throw new FileNotFoundException("Model not found", path);

            return new OnnxModelRuntime(path);
        }

        #endregion

        #region ---------- Public methods ----------

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<OnnxModelResults<T>> Execute<T>(Func<DisposableNamedOnnxValue, IEnumerable<T>> selector)
        {
            using var results = this.session.Run(this.inputs, this.outputs);
            var selectedResults = new List<T>();

            foreach (var result in results)
                yield return new OnnxModelResults<T>(selector(result));
        }

        public OnnxModelRuntime Inputs(Func<string, NamedOnnxValue> retrieval)
        {
            this.inputs.Clear();

            foreach (var key in session.InputMetadata.Keys)
                this.inputs.Add(retrieval(key));

            return this;
        }


        #endregion

        #region ---------- Protected methods ----------

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed) {
                if (disposing)
                    this.session.Dispose();

                this.isDisposed = true;
            }
        }

        #endregion
    }
}
