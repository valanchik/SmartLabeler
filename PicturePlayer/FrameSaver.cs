
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

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

        public async Task SaveFrameAsync(Bitmap frame, int index)
        {
            if (frame != null)
            {
                string fileName = $"frame_{index}.jpg";
                string filePath = Path.Combine(_outputFolderPath, fileName);

                await Task.Run(() =>
                {
                    frame.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                });
            }
        }
    }
}