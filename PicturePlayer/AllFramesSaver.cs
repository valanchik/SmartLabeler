using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class AllFramesSaver
    {
        private IPlayer frameSelector;

        public AllFramesSaver(IPlayer frameSelector)
        {
            this.frameSelector = frameSelector;
        }

        public async Task SaveAllFramesAsync()
        {
            if (frameSelector.IsReady())
            {
                using (ProgressDialog p = new ProgressDialog())
                {
                    p.Shown += ProgressDialog_Shown;

                    p.ShowDialog();
                }
            }
        }
        private async void ProgressDialog_Shown(object sender, EventArgs e)
        {
            if (frameSelector.IsReady())
            {
                int index = frameSelector.GetCurrentFrameIndex();

                ProgressDialog p = sender as ProgressDialog;

                p.Owner = frameSelector.GetCurrentWindow();
                var countFrame = frameSelector.GetFramesCount();
                while (frameSelector.ShowNextFrame())
                {
                    using (Bitmap clonedImage = (Bitmap)frameSelector.GetCurrentFrame().Clone())
                    {
                        await frameSelector.GetFrameSaver().SaveFrameAsync(clonedImage, index);
                        p.UpdateProgress(index, countFrame);
                        index++;
                    }
                    if (p.DialogResult == DialogResult.Cancel)
                    {
                        break;
                    }
                }
                p.Close();
            }
        }


    }

}
