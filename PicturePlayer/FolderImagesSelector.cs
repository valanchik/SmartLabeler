using InputControllers;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class FolderImagesSelector : SelectorBase
    {
        private readonly IInputPlayerController playerControls;

        public delegate void PlayerDelegate(IPlayer player);
        public event PlayerDelegate OnPlayer;
        public FolderImagesSelector(Button openFolderButton, IInputPlayerController playerControls)
        {
            openFolderButton.Click += OpenImagesFolder_Click;
            this.playerControls = playerControls;
        }

        private void OpenImagesFolder_Click(object sender, System.EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Выберите папку с видеофайлами";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = folderBrowserDialog.SelectedPath;
                    InitPlayer(new PlaySource { Path = folderBrowserDialog.SelectedPath });
                }
            }
        }
        private void InitPlayer(PlaySource source)
        {
            IFrameSaver frameSaver = new FrameSaver(GetRandomeDir());
            var player = new FolderPlayer(playerControls, frameSaver);
            player.SetSource(source);
        }

    }
}