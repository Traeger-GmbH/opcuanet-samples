namespace App.AI
{
    public static class OnnxModelResultsExtensions
    {
        #region ---------- Public static methods ----------

        public static OnnxModelResults<float> Softmax(this OnnxModelResults<float> output)
        {
            // numerisch stabil
            var logits = output.ToArray();

            float max = logits.Max();
            double sum = 0.0;

            var exps = new double[logits.Length];

            for (int i = 0; i < logits.Length; i++) {
                double e = Math.Exp(logits[i] - max);
                exps[i] = e;
                sum += e;
            }

            var probs = new float[logits.Length];

            for (int i = 0; i < logits.Length; i++)
                probs[i] = (float)(exps[i] / sum);

            return new OnnxModelResults<float>(probs);
        }

        public static OnnxModelResult<int> ArgMax(this OnnxModelResults<float> output)
        {
            var array = output.ToArray();

            int index = 0;
            float best = array[0];

            for (int i = 1; i < array.Length; i++) {
                if (array[i] > best) {
                    best = array[i];
                    index = i;
                }
            }

            return new OnnxModelResult<int>(index);
        }

        #endregion
    }
}
