using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class AllFramesSaver
    {
        private readonly IPlayer player;

        public AllFramesSaver(IPlayer player)
        {
            this.player = player;
        }
        private async void ProgressDialog_Shown(object sender, EventArgs e)
        {
            if (player.IsReady())
            {
                int index = player.GetCurrentFrameIndex();

                ProgressDialog p = sender as ProgressDialog;

                p.Owner = player.GetCurrentWindow();
                var countFrame = player.GetFramesCount();
                while (player.ShowNextFrame())
                {
                    using (Bitmap clonedImage = (Bitmap)player.GetCurrentFrame().Clone())
                    {
                        await player.GetFrameSaver().SaveFrameAsync(clonedImage, index);
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
        public async Task SaveAllFramesAsync()
        {
            if (player.IsReady())
            {
                using (ProgressDialog p = new ProgressDialog())
                {
                    p.Shown += ProgressDialog_Shown;

                    p.ShowDialog();
                }
            }
        }



    }

}
