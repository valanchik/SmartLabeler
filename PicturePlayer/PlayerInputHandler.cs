using InputControllers;
using PicturePlayer;
using System;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class PlayerInputHandler
    {
        private IPlayer player;
        public PlayerInputHandler(IInputPlayerController inputs, IPlayer player)
        {
            this.player = player;
            ((Button)inputs.GetElement(InputPlayerControllerType.Play)).Click += PlayClick;
            ((Button)inputs.GetElement(InputPlayerControllerType.Pause)).Click += PauseClick;
            ((Button)inputs.GetElement(InputPlayerControllerType.Stop)).Click += StopClick;
            ((Button)inputs.GetElement(InputPlayerControllerType.NextFrame)).Click += NextFrameClick;
            ((Button)inputs.GetElement(InputPlayerControllerType.PrevFrame)).Click += PrevFrameClick;
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
