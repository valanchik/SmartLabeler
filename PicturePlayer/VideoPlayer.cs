
using InputControllers;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class VideoPlayer : Player, IPlayer
    {
        private VideoLoader _videoLoader;
        private readonly IFrameSaver _frameSaver;
        private readonly AllFramesSaver _allFramesSaver;
        private int frameCount;

        public VideoPlayer(IInputPlayerController inputs, IFrameSaver frameSaver) :
            base(inputs)
        {
            _frameSaver = frameSaver;
            _allFramesSaver = new AllFramesSaver(this);
            inputsHandler = new PlayerInputHandler(inputs, this);
        }
        public void SetSource(PlaySource resource)
        {
            _videoLoader = new VideoLoader(resource.Path);
            frameCount = _videoLoader.FrameCount;
            UpdatePictureBox();
        }
        public int GetFramesCount()
        {
            return frameCount;
        }

        public override bool ShowNextFrame()
        {
            if (_videoLoader != null)
            {
                Bitmap frame = _videoLoader.GetNextFrame();

                if (frame != null)
                {
                    _pictureBox.Image = frame;
                    return true;
                }
            }

            return false;
        }

        public bool ShowPreviousFrame()
        {
            if (_videoLoader != null)
            {
                Bitmap frame = _videoLoader.GetPreviousFrame();

                if (frame != null)
                {
                    _pictureBox.Image = frame;
                    return true;
                }
            }

            return false;
        }

        public bool ShowFrameByIndex(int index)
        {
            if (_videoLoader != null)
            {
                Bitmap frame = _videoLoader.GetFrameByIndex(index);

                if (frame != null)
                {
                    _pictureBox.Image = frame;
                    return true;
                }
            }

            return false;
        }
        public async Task SaveAllFramesAsync()
        {
            if (_allFramesSaver != null)
            {
                await _allFramesSaver.SaveAllFramesAsync();
            }
        }

        private void UpdatePictureBox()
        {
            if (_videoLoader != null)
            {
                Bitmap frame = _videoLoader.GetCurrentFrame();

                if (frame != null)
                {
                    _pictureBox.Image = frame;
                    RaiseOnTick();
                }
            }
        }

        public bool IsReady()
        {
            return _videoLoader != null && _frameSaver != null;
        }

        public Image GetCurrentFrame()
        {
            return _pictureBox.Image;
        }

        public IFrameSaver GetFrameSaver()
        {
            return _frameSaver;
        }

        public Form GetCurrentWindow()
        {
            return FindParentForm(_pictureBox);
        }
        private Form FindParentForm(Control control)
        {
            Control parent = control.Parent;

            while (parent != null)
            {
                if (parent is Form)
                {
                    return (Form)parent;
                }

                parent = parent.Parent;
            }

            return null;
        }

        public int GetCurrentFrameIndex()
        {
            return _videoLoader.CurrentFrameIndex;
        }
    }
}
