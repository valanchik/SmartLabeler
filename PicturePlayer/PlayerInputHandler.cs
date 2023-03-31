using InputControllers;
using System;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class PlayerInputHandler
    {
        private readonly IPlayer player;
        private readonly IInputPlayerController inputs;
        public PlayerInputHandler(IInputPlayerController inputs, IPlayer player)
        {
            this.player = player;
            this.inputs = inputs;
            ConfigurePlayAndPauseBtn(
                inputs.GetElement(InputPlayerControllerType.Play),
                inputs.GetElement(InputPlayerControllerType.Pause)
            );
            ((Button)inputs.GetElement(InputPlayerControllerType.Play)).Click += PlayClick;
            ((Button)inputs.GetElement(InputPlayerControllerType.Pause)).Click += PauseClick;
            ((Button)inputs.GetElement(InputPlayerControllerType.Stop)).Click += StopClick;
            ((Button)inputs.GetElement(InputPlayerControllerType.NextFrame)).Click += NextFrameClick;
            ((Button)inputs.GetElement(InputPlayerControllerType.PrevFrame)).Click += PrevFrameClick;
            new TimelineHandler((ProgressBar)inputs.GetElement(InputPlayerControllerType.TimelineBar), player);
        }

        private void ConfigurePlayAndPauseBtn(Control play, Control pause)
        {
            pause.Location = play.Location;
            pause.Size = play.Size;
            pause.Visible = false;
            play.Visible = true;
        }
        
        private void SetPlayAndPauseStatus(bool playing)
        {
            var play = inputs.GetElement(InputPlayerControllerType.Play);
            var pause = inputs.GetElement(InputPlayerControllerType.Pause);
            play.Visible = !playing;
            pause.Visible = playing;
        }

        private void PrevFrameClick(object sender, EventArgs e)
        {
            player.ShowPreviousFrame();
        }

        private void NextFrameClick(object sender, EventArgs e)
        {
            player.ShowNextFrame();
        }

        private void StopClick(object sender, EventArgs e)
        {
            player.Pause();
            player.ShowFrameByIndex(0);
            SetPlayAndPauseStatus(false);
        }

        private void PauseClick(object sender, EventArgs e)
        {
            player.Pause();
            SetPlayAndPauseStatus(false);
        }

        private void PlayClick(object sender, EventArgs e)
        {
            player.Play();
            SetPlayAndPauseStatus(true);
        }
    }
}
