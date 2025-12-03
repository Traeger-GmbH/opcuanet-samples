namespace App.AI
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    using Microsoft.ML.OnnxRuntime.Tensors;

    public class Preprocessor
    {
        #region ---------- Private readonly fields ----------

        private readonly PreprocessorOptions options;

        #endregion

        #region ---------- Private constructors ----------

        private Preprocessor(PreprocessorOptions options)
            : base()
        {
            this.options = options;
        }

        #endregion

        #region ---------- Public static methods ----------

        public static Preprocessor WithOptions(string optionsPath)
        {
            var options = PreprocessorOptions.Load(optionsPath);
            return new Preprocessor(options);
        }

        #endregion

        #region ---------- Public methods ----------

        // ---- Vorverarbeitung: Resize (Bilinear), Rescale, Normalize, CHW, Batch-Dim ----
        public DenseTensor<float> Process(string imagePath)
        {
            using var image = new Bitmap(imagePath);
            return this.Process(image);
        }

        public DenseTensor<float> Process(Bitmap image)
        {
            int width = this.options.Size.Width;
            int height = this.options.Size.Height;

            using var resized = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            using (var graphics = Graphics.FromImage(resized)) {
                graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                graphics.DrawImage(image, new Rectangle(0, 0, width, height));
            }

            // Tensor [1,3,H,W]
            var tensor = new DenseTensor<float>(new[] { 1, 3, height, width });

            // Normalisierungs-Parameter
            float rescale = this.options.UseRescale ? this.options.RescaleFactor : 1f;
            float[] mean = this.options.UseNormalize ? this.options.ImageMean : [0f, 0f, 0f];
            float[] std = this.options.UseNormalize ? this.options.ImageStandard : [1f, 1f, 1f];

            var data = resized.LockBits(
                    new Rectangle(0, 0, width, height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format24bppRgb);

            try {
                unsafe {
                    byte* scan0 = (byte*)data.Scan0;
                    int stride = data.Stride;

                    for (int y = 0; y < height; y++) {
                        byte* row = scan0 + y * stride;

                        for (int x = 0; x < width; x++) {
                            // 24bppRgb: B,G,R
                            byte b = row[x * 3 + 0];
                            byte g = row[x * 3 + 1];
                            byte r = row[x * 3 + 2];

                            // RGB -> float
                            float rf = (r * rescale - mean[0]) / std[0];
                            float gf = (g * rescale - mean[1]) / std[1];
                            float bf = (b * rescale - mean[2]) / std[2];

                            // CHW: [1, C, H, W]
                            int hwIndex = y * width + x;
                            tensor[0, 0, y, x] = rf; // R
                            tensor[0, 1, y, x] = gf; // G
                            tensor[0, 2, y, x] = bf; // B
                        }
                    }
                }
            }
            finally {
                resized.UnlockBits(data);
            }

            return tensor;
        }

        #endregion
    }
}
