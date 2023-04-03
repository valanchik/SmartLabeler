using InputControllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class FolderPlayer : Player, IPlayer
    {
        private  IFrameSaver _frameSaver;
        private  Dictionary<int, Bitmap> _frameCache;
        public FolderPlayer(PlayerInputHandler inputs, IFrameSaver frameSaver)
            : base(inputs)
        {
            _frameSaver = frameSaver;
            _frameCache = new Dictionary<int, Bitmap>();
        }

        public  override IPlayer SetSource(PlaySource source)
        {
            currentPath = source.Path;

            return this;
        }

        public override void Init()
        {
            if (Directory.Exists(currentPath))
            {
                frameCount = Directory.GetFiles(currentPath, "frame_*.jpg").Length;
                _frameCache.Clear();
                if (UpdatePictureBox())
                {
                    RaiseOnReady();
                }
                
            }
            inputsHandler.Init();
        }
        public override bool IsReady()
        {
            return Directory.Exists(currentPath) && _frameSaver != null && frameCount > 0;
        }

        public override Bitmap GetCurrentFrame()
        {
            if (!_frameCache.TryGetValue(currentFrameIndex, out var frame))
            {
                string framePath = Path.Combine(currentPath, $"frame_{currentFrameIndex}.jpg");
                if (File.Exists(framePath))
                {
                    //todo разобраться с этой дичью
                    using (var image = Image.FromFile(framePath))
                    using (frame = new Bitmap(image))
                    {
                        frame = new Bitmap(image);
                        _frameCache[currentFrameIndex] = frame;
                    }
                    return frame;
                }
            } else
            {
                return _frameCache[currentFrameIndex];
            }

            return null;
        }

        public override async Task SaveAllFramesAsync()
        {

        }

        public override IFrameSaver GetFrameSaver()
        {
            return _frameSaver;
        }
       

    }
}
