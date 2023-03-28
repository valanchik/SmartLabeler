using System.Windows.Forms;

namespace PicturePlayer
{
    public class VideoFileSelector
    {
        private TextBox _textBox;
        private IPlayer player;

        public VideoFileSelector(TextBox textBox,Button openVideoButton, IPlayer player)
        {
            this.player = player;
            _textBox = textBox;
            openVideoButton.Click += OpenVideoButton_Click;
        }

        private void OpenVideoButton_Click(object sender, System.EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Video files|*.mp4;*.avi;*.mkv;*.mjpeg;*.webm;*.wmv;*.mov;*.flv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _textBox.Text = openFileDialog.FileName;

                    var resource = new PlayResource { Path = openFileDialog.FileName, ResourceType = PLayerResourceType.VideoFile };
                    player.SetResource(resource);
                }
            }
        }
    }
}