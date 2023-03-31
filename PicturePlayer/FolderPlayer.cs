using InputControllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class FolderPlayer : Player, IPlayer
    {
        
        private IFrameSaver _frameSaver;
        private string _folderPath;
        private int _currentFrameIndex = 0;
        private Dictionary<int, Image> _frameCache;
        

        public FolderPlayer(PictureBox pictureBox, IInputPlayerController inputs, IFrameSaver frameSaver)
            : base(pictureBox, inputs)
        {
            _frameSaver = frameSaver;
            _frameCache = new Dictionary<int, Image>();
            inputsHandler = new PlayerInputHandler(inputs, this);
        }


        public void SetResource(PlayResource resource)
        {
            _folderPath = resource.Path;
            UpdatePictureBox();
        }

        public int GetFramesCount()
        {
            if (Directory.Exists(_folderPath))
            {
                return Directory.GetFiles(_folderPath, "frame_*.jpg").Length;
            }

            return -1;
        }

        public bool ShowNextFrame()
        {
            int nextFrameIndex = _currentFrameIndex + 1;
            if (nextFrameIndex < GetFramesCount())
            {
                _currentFrameIndex = nextFrameIndex;
                UpdatePictureBox();
                return true;
            }
            return false;
        }

        public bool ShowPreviousFrame()
        {
            int previousFrameIndex = _currentFrameIndex - 1;
            if (previousFrameIndex >= 0)
            {
                _currentFrameIndex = previousFrameIndex;
                UpdatePictureBox();
                return true;
            }

            return false;
        }

        public bool ShowFrameByIndex(int index)
        {
            if (index >= 0 && index < GetFramesCount())
            {
                _currentFrameIndex = index;
                UpdatePictureBox();
                return true;
            }

            return false;
        }

        public async Task SaveAllFramesAsync()
        {
            
        }

        private void UpdatePictureBox()
        {
            Image frame;
            if (!_frameCache.TryGetValue(_currentFrameIndex, out frame))
            {
                string framePath = Path.Combine(_folderPath, $"frame_{_currentFrameIndex}.jpg");
                if (File.Exists(framePath))
                {
                    using (var image = Image.FromFile(framePath))
                    {
                        frame = new Bitmap(image);
                        _frameCache[_currentFrameIndex] = frame;
                    }
                }
            }

            if (frame != null)
            {
                _pictureBox.Image = frame;
            }
        }

        public bool IsReady()
        {
            return Directory.Exists(_folderPath) && _frameSaver != null;
        }

        public Image GetCurrentFrame()
        {
            if (_frameCache.TryGetValue(_currentFrameIndex, out var frame))
            {
                return frame;
            }

            return null;
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
            return _currentFrameIndex;
        }
        protected override void OnPlaybackTimerTick(object sender, EventArgs e)
        {
            if (!ShowNextFrame())
            {
                Pause();
            }
        }

    }
}
