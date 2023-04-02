using System.Windows.Forms;

namespace PicturePlayer
{
    public class TimelineHandler
    {
        private  ProgressBar timeLineBar;
        private  IPlayer player;

        public TimelineHandler(ProgressBar timeLineBar, IPlayer player)
        {
            this.timeLineBar = timeLineBar;
            this.player = player;
            Hendling();
            UpdateProgressBar();
        }

        private void Hendling()
        {
            player.OnReady += Player_OnReady;
            timeLineBar.MouseClick -= TimeLineBar_MouseClick;
            timeLineBar.MouseClick += TimeLineBar_MouseClick;
            player.OnTick -= Player_OnTick;
            player.OnTick += Player_OnTick;
        }

        private void Player_OnReady()
        {
            UpdateMaximum();
        }

        public void UpdateMaximum()
        {
            var count = player.GetFramesCount();
            if (count>0)
            {
                timeLineBar.Maximum = count-1;
            }
            
        }
        private void TimeLineBar_MouseClick(object sender, MouseEventArgs e)
        {
            if (player.GetFramesCount() <= 0) return;
            
            player.Pause();
            
            timeLineBar.Maximum = player.GetFramesCount() - 1;
            float relativeX = (float)e.X / timeLineBar.Width;
            int frameIndex = (int)(relativeX * timeLineBar.Maximum);
            player.ShowFrameByIndex(frameIndex);
            UpdateProgressBar();
        }

        private void Player_OnTick()
        {
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
