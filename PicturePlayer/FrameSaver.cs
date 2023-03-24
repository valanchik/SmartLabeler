
using System.Drawing;
using System.IO;

namespace PicturePlayer
{
    public class FrameSaver : IFrameSaver
    {
        private readonly string _outputFolderPath;

        public FrameSaver(string outputFolderPath)
        {
            _outputFolderPath = outputFolderPath;

            if (!Directory.Exists(_outputFolderPath))
            {
                Directory.CreateDirectory(_outputFolderPath);
            }
        }

        public void SaveFrame(Bitmap frame, int index)
        {
            if (frame != null)
            {
                string fileName = $"frame_{index}.jpg";
                string filePath = Path.Combine(_outputFolderPath, fileName);
                frame.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}