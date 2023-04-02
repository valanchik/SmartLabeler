using InputControllers;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class FolderImagesSelector : SelectorBase
    {
        private readonly IInputPlayerController playerControls;
        public FolderImagesSelector(Button openFolderButton)
        {
            openFolderButton.Click += OpenImagesFolder_Click;
        }

        private void OpenImagesFolder_Click(object sender, System.EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Выберите папку с видеофайлами";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedPath = folderBrowserDialog.SelectedPath;
                    RaiseOnSource(new PlaySource { Path = folderBrowserDialog.SelectedPath, Type = PlaySourceType.FolderImages });
                }
            }
        }

    }
}