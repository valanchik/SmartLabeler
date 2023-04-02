
using InputControllers;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class VideoPlayer : Player
    {
        private VideoLoader _videoLoader;
        private  IFrameSaver _frameSaver;
        private  AllFramesSaver _allFramesSaver;

        public VideoPlayer(PlayerInputHandler inputs, IFrameSaver frameSaver) :
            base(inputs)
        {
            _frameSaver = frameSaver;
            _allFramesSaver = new AllFramesSaver(this);
        }
        public override IPlayer SetSource(PlaySource resource)
        {
            currentPath = resource.Path;
            return this;
        }

        public override void Init()
        {
            _videoLoader = new VideoLoader(currentPath);
            frameCount = _videoLoader.FrameCount;
            if (UpdatePictureBox())
            {
                RaiseOnReady();
            }
            inputsHandler.Init();
        }
        public override bool IsReady()
        {
            return _videoLoader != null && _frameSaver != null;
        }

        public override Bitmap GetCurrentFrame()
        {
            if (_videoLoader != null)
            {
                return _videoLoader.GetCurrentFrame();
            }
            return null;
        }

        public override bool ShowNextFrame()
        {
            if (_videoLoader != null )
            {
                var nextFrame = _videoLoader.CurrentFrameIndex + 1;
                if (nextFrame<frameCount)
                {
                    _videoLoader.CurrentFrameIndex = nextFrame;
                    SetCurrentFrameIndex(nextFrame);
                    return UpdatePictureBox();
                }
            }
            return false;
        }
        public override bool ShowFrameByIndex(int index)
        {
            if (_videoLoader != null && IndexIsValid(index))
            {
                _videoLoader.CurrentFrameIndex = index;
                SetCurrentFrameIndex(index);
                return UpdatePictureBox();
            }
            return false;
        }
        public override bool ShowPreviousFrame()
        {
            if (_videoLoader != null)
            {
                var prevFrame = _videoLoader.CurrentFrameIndex - 1;
                if (IndexIsValid(prevFrame))
                {
                    _videoLoader.CurrentFrameIndex = prevFrame;
                    SetCurrentFrameIndex(prevFrame);
                    return UpdatePictureBox();
                    
                }
            }
            return false;
        }


        public override async Task SaveAllFramesAsync()
        {
            if (_allFramesSaver != null)
            {
                await _allFramesSaver.SaveAllFramesAsync();
            }
        }

        public override IFrameSaver GetFrameSaver()
        {
            return _frameSaver;
        }

    }
}
