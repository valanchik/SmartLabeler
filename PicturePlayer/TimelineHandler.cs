using System.Windows.Forms;

namespace PicturePlayer
{
    public class TimelineHandler
    {
        private readonly ProgressBar timeLineBar;
        private readonly IPlayer player;

        public TimelineHandler(ProgressBar timeLineBar, IPlayer player)
        {
            this.timeLineBar = timeLineBar;
            this.player = player;
            
            timeLineBar.MouseClick += TimeLineBar_MouseClick;
            player.OnTick += Player_OnTick;
            UpdateProgressBar();
        }


        private void TimeLineBar_MouseClick(object sender, MouseEventArgs e)
        {
            if (player.GetFramesCount() <= 0) return;

            timeLineBar.Maximum = player.GetFramesCount() - 1;
            float relativeX = (float)e.X / timeLineBar.Width;
            int frameIndex = (int)(relativeX * timeLineBar.Maximum);
            player.ShowFrameByIndex(frameIndex);
            UpdateProgressBar();
        }

        private void Player_OnTick()
        {
            timeLineBar.Maximum = player.GetFramesCount() - 1;
            UpdateProgressBar();
        }

        private void UpdateProgressBar()
        {
            if (player.IsReady())
            {
                var val = player.GetCurrentFrameIndex(); 
                timeLineBar.Value = val;
            }
        }
    }
}
