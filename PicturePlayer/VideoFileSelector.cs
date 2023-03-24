
using System.Drawing;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class VideoFileSelector : IFrameSelector
    {
        private VideoLoader _videoLoader;
        private TextBox _textBox;
        private PictureBox _pictureBox;

        private IFrameSaver _frameSaver;

        public VideoFileSelector(TextBox textBox, PictureBox pictureBox, Button openVideoButton)
        {
            _textBox = textBox;
            _pictureBox = pictureBox;
            openVideoButton.Click += OpenVideoButton_Click;
        }

        private void OpenVideoButton_Click(object sender, System.EventArgs e)
        {
            OpenVideoFile();
        }
        public void SetFrameSaver(IFrameSaver frameSaver)
        {
            _frameSaver = frameSaver;
        }

        public void OpenVideoFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Video files|*.mp4;*.avi;*.mkv;*.mjpeg;*.webm;*.wmv;*.mov;*.flv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _textBox.Text = openFileDialog.FileName;

                    if (_videoLoader != null)
                    {
                        _videoLoader.Dispose();
                    }

                    _videoLoader = new VideoLoader(openFileDialog.FileName);
                    UpdatePictureBox();
                }
            }
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
        public void SaveAllFrames()
        {
            if (_videoLoader != null && _frameSaver != null)
            {
                int index = 0;

                while (ShowNextFrame())
                {
                    _frameSaver.SaveFrame(_pictureBox.Image as Bitmap, index);
                    index++;
                }
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
       
    }
}