namespace App.Vision
{
    using System.Drawing;

    using OpenCvSharp;
    using OpenCvSharp.Extensions;

    public static class Camera
    {
        private static VideoCapture camera;


        public static Bitmap CaptureSnapshot()
        {
            if (camera is null) {
                camera = new VideoCapture();

                // 1) Kamera explizit öffnen – mit Backend-Fallbacks
                bool opened =
                    camera.Open(0, VideoCaptureAPIs.MSMF) ||  // Media Foundation (Windows)
                    camera.Open(0, VideoCaptureAPIs.DSHOW) || // DirectShow (ältere Treiber)
                    camera.Open(0, VideoCaptureAPIs.ANY);

                if (!opened || !camera.IsOpened())
                    throw new InvalidOperationException("Kamera konnte nicht geöffnet werden (MSMF/DSHOW/ANY).");

                // 2) Optional: gewünschte Auflösung setzen (nur Best Effort)
                camera.Set(VideoCaptureProperties.FrameWidth, 1024);
                camera.Set(VideoCaptureProperties.FrameHeight, 1024);

                // 3) Warm-up: ein paar Frames „graben“, einige Kameras liefern initial leere Frames
                for (int i = 0; i < 5; i++)
                    camera.Grab();
            }

            // 4) Frame lesen
            using var frame = new Mat();

            if (!camera.Read(frame) || frame.Empty()) {
                Thread.Sleep(1000);
                //throw new InvalidOperationException("Kein gültiger Frame gelesen (frame.Empty).");
            }

            // 5) Als Bitmap zurückgeben
            return BitmapConverter.ToBitmap(frame); // eigene Kopie; cap/frame sind via using bereits entsorgt
        }
    }
}
