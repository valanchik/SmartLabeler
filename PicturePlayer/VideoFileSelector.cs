
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class VideoFileSelector : IFrameSelector
    {
        private VideoLoader _videoLoader;
        private TextBox _textBox;
        private PictureBox _pictureBox;

        private IFrameSaver _frameSaver;
        private AllFramesSaver _allFramesSaver;

        public VideoFileSelector(TextBox textBox, PictureBox pictureBox, Button openVideoButton, IFrameSaver frameSaver)
        {
            _textBox = textBox;
            _pictureBox = pictureBox;
            openVideoButton.Click += OpenVideoButton_Click;
            _frameSaver = frameSaver;
            _allFramesSaver = new AllFramesSaver(this);
        }

        private void OpenVideoButton_Click(object sender, System.EventArgs e)
        {
            OpenVideoFile();
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

        public int GetFramesCount()
        {
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
    }
}