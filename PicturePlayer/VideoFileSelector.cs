using InputControllers;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class VideoFileSelector: SelectorBase
    {
        private readonly IInputPlayerController playerControls;

        public VideoFileSelector(Button openVideoButton, IInputPlayerController playerControls)
        {
            this.player = player;
            openVideoButton.Click += OpenVideoButton_Click;
            this.playerControls = playerControls;
        }

        private void OpenVideoButton_Click(object sender, System.EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Video files|*.mp4;*.avi;*.mkv;*.mjpeg;*.webm;*.wmv;*.mov;*.flv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = openFileDialog.FileName;
                    
                    InitPlayer(new PlaySource { Path = openFileDialog.FileName});
                }
            }
        }
        private void InitPlayer(PlaySource source)
        {
            IFrameSaver frameSaver = new FrameSaver(GetRandomeDir());
            var player = new VideoPlayer(playerControls, frameSaver);
            player.SetSource(source);
        }
    }
}