using InputControllers;
using PicturePlayer;
using System;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class PlayerInputHandler
    {
        private IPlayer player;
        private IInputPlayerController inputs;
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
        }

        private void ConfigurePlayAndPauseBtn(Control play, Control pause)
        {
            pause.Location = play.Location;
            pause.Size = play.Size;
            pause.Visible = false;
            play.Visible = true;
        }
        public void TogglePlayAndPause()
        {
            inputs.GetElement(InputPlayerControllerType.Play).Visible = !inputs.GetElement(InputPlayerControllerType.Play).Visible;
            inputs.GetElement(InputPlayerControllerType.Pause).Visible = !inputs.GetElement(InputPlayerControllerType.Pause).Visible;
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
        }

        private void PauseClick(object sender, EventArgs e)
        {
            player.Pause();
        }

        private void PlayClick(object sender, EventArgs e)
        {
            player.Play();
        }
    }
}
