namespace App.AI
{
    public class Postprocessor
    {
        #region ---------- Private readonly fields ----------

        private readonly PostprocessorOptions options;

        #endregion

        #region ---------- Private constructors ----------

        private Postprocessor(PostprocessorOptions options)
            : base()
        {
            this.options = options;
        }

        #endregion

        #region ---------- Public static methods ----------

        public static Postprocessor WithOptions(string optionsPath)
        {
            var options = PostprocessorOptions.Load(optionsPath);
            return new Postprocessor(options);
        }

        #endregion

        #region ---------- Public methods ----------

        public string Process(OnnxModelResults<float> result)
        {
            var map = this.options.Id2LabelMapping.ToDictionary(
                    keySelector: entry => int.Parse(entry.Key),
                    elementSelector: entry => entry.Value);

            return result
                    .Softmax()
                    .ArgMax()
                    .Map(map, value => value.ToString());
        }

        #endregion
    }
}
