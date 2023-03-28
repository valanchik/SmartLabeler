﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    class VideoPlayer: IPlayer, IAllFramesSaver
    {
       
        private PictureBox _pictureBox;
        private VideoLoader _videoLoader;
        private IFrameSaver _frameSaver;
        private AllFramesSaver _allFramesSaver;
        private PlayResource resource;

        public VideoPlayer(PictureBox pictureBox, IFrameSaver frameSaver)
        {
            _pictureBox = pictureBox;
            _frameSaver = frameSaver;
            _allFramesSaver = new AllFramesSaver(this);
        }
        public void SetResource(PlayResource resource)
        {
            this.resource = resource;
            _videoLoader = new VideoLoader(resource.Path);
            UpdatePictureBox();
        }
        public int GetFramesCount()
        {
            if (_videoLoader == null) return - 1;
            return _videoLoader.FrameCount;
        }

        public bool ShowNextFrame()
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
