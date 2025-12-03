namespace App.AI
{
    using System;
    using System.Drawing;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class PreprocessorOptions
    {
        #region ---------- Private constructors ----------

        public PreprocessorOptions()
            : base()
        {
            this.ImageMean = [0, 0, 0];
            this.ImageStandard = [1, 1, 1];
        }

        #endregion

        #region ---------- Public static methods ----------

        public static PreprocessorOptions Load(string optionsPath)
        {
            var path = Path.GetFullPath(optionsPath);

            if (!File.Exists(path))
                throw new FileNotFoundException("Options not found", path);

            var content = File.ReadAllText(optionsPath);
            var options = JsonSerializer.Deserialize<PreprocessorOptions>(content);

            if (options is null)
                throw new InvalidOperationException("Options are empty or of a not supported format");

            return options;
        }

        #endregion

        #region ---------- Public properties ----------

        [JsonPropertyName("size")]
        [JsonConverter(typeof(JsonSizeConverter))]
        public Size Size
        {
            get;
            set;
        }

        [JsonPropertyName("do_rescale")]
        public bool UseRescale
        {
            get;
            set;
        }

        [JsonPropertyName("rescale_factor")]
        public float RescaleFactor
        {
            get;
            set;
        }

        [JsonPropertyName("do_normalize")]
        public bool UseNormalize
        {
            get;
            set;
        }

        [JsonPropertyName("image_mean")]
        public float[] ImageMean
        {
            get;
            set;
        }

        [JsonPropertyName("image_std")]
        public float[] ImageStandard
        {
            get;
            set;
        }

        #endregion
    }
}
