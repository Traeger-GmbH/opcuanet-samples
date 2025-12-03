namespace App.AI
{
    public class OnnxModelResults<T>
    {
        #region ---------- Private readonly fields ----------

        private readonly IEnumerable<T> data;

        #endregion

        #region ---------- Public constructors ----------

        public OnnxModelResults(IEnumerable<T> data)
            : base()
        {
            this.data = data;
        }

        #endregion

        #region ---------- Public methods ----------

        public T[] ToArray()
        {
            return this.data.ToArray();
        }

        #endregion
    }
}
