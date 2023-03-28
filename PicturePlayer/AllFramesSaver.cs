    using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class AllFramesSaver
    {
        private IFrameSelector frameSelector;

        public AllFramesSaver(IFrameSelector frameSelector)
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
                int index = 0;

                ProgressDialog p = sender as ProgressDialog;

                p.Owner = frameSelector.GetCurrentWindow();

                while (frameSelector.ShowNextFrame())
                {
                    using (Bitmap clonedImage = (Bitmap)frameSelector.GetCurrentFrame().Clone())
                    {
                        await frameSelector.GetFrameSaver().SaveFrameAsync(clonedImage, index);
                        p.UpdateProgress(index);
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
