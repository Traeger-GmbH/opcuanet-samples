namespace App.AI
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class PostprocessorOptions
    {
        #region ---------- Private constructors ----------

        public PostprocessorOptions()
            : base()
        {
            this.Id2LabelMapping = new Dictionary<string, string>();
        }

        #endregion

        #region ---------- Public static methods ----------

        public static PostprocessorOptions Load(string optionsPath)
        {
            var path = Path.GetFullPath(optionsPath);

            if (!File.Exists(path))
                throw new FileNotFoundException("Options not found", path);

            var content = File.ReadAllText(optionsPath);
            var options = JsonSerializer.Deserialize<PostprocessorOptions>(content);

            if (options is null)
                throw new InvalidOperationException("Options are empty or of a not supported format");

            return options;
        }

        #endregion

        #region ---------- Public properties ----------

        [JsonPropertyName("id2label")]
        public Dictionary<string, string> Id2LabelMapping
        {
            get;
            set;
        }

        #endregion
    }
}
