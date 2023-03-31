using System.Windows.Forms;

namespace PicturePlayer
{
    public class VideoFileSelector
    {
        private readonly IPlayer player;
        private string selectedFile;

        public VideoFileSelector(Button openVideoButton, IPlayer player)
        {
            this.player = player;
            openVideoButton.Click += OpenVideoButton_Click;
        }

        private void OpenVideoButton_Click(object sender, System.EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Video files|*.mp4;*.avi;*.mkv;*.mjpeg;*.webm;*.wmv;*.mov;*.flv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFile = openFileDialog.FileName;

                    var resource = new PlaySource { Path = openFileDialog.FileName, ResourceType = PLayerResourceType.VideoFile };
                    player.SetSource(resource);
                }
            }
        }
    }
}