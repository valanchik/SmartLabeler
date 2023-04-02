using InputControllers;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class VideoFileSelector: SelectorBase
    {
        private readonly IInputPlayerController playerControls;

        public VideoFileSelector(Button openVideoButton)
        {
            openVideoButton.Click += OpenVideoButton_Click;
        }

        private void OpenVideoButton_Click(object sender, System.EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Video files|*.mp4;*.avi;*.mkv;*.mjpeg;*.webm;*.wmv;*.mov;*.flv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = openFileDialog.FileName;

                    RaiseOnSource(new PlaySource { Path = openFileDialog.FileName, Type = PlaySourceType.Video});
                }
            }
        }
        
    }
}