using System.Windows.Forms;

namespace PicturePlayer
{
    public class FolderImagesSelector
    {
        private IPlayer player;
        private string selectedFolder;

        public FolderImagesSelector(Button openFolderButton, IPlayer player)
        {
            this.player = player;
            openFolderButton.Click += OpenImagesFolder_Click;
        }

        private void OpenImagesFolder_Click(object sender, System.EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Выберите папку с видеофайлами";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolder = folderBrowserDialog.SelectedPath;
                    var resource = new PlayResource { Path = selectedFolder, ResourceType = PLayerResourceType.Directory };
                    player.SetResource(resource);
                }
            }
        }

    }
}