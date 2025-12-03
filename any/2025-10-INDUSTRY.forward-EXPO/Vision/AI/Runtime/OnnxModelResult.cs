namespace App.AI
{
    public class OnnxModelResult<T>
    {
        #region ---------- Private readonly fields ----------

        private readonly T value;

        #endregion

        #region ---------- Public constructors ----------

        public OnnxModelResult(T value)
            : base()
        {
            this.value = value;
        }

        #endregion

        #region ---------- Public methods ----------

        public TValue Map<TValue>(IDictionary<T, TValue> dictionary, Func<T, TValue> fallback)
        {
            if (dictionary.TryGetValue(this.value, out var value))
                return value;

            return fallback(this.value);
        }

        #endregion
    }
}
