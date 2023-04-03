using InputControllers;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class PlayerInputHandler
    {
        private  IPlayer player;
        private  IInputController inputs;
        private TimelineHandler timelineHandler;
        public PlayerInputHandler(IInputController inputs)
        {
            this.inputs = inputs;
            ConfigurePlayAndPauseBtn(
                inputs.GetElement(InputsControllerType.Play),
                inputs.GetElement(InputsControllerType.Pause)
            );
            Init();
        }

        public void Init()
        {
            

            ((Button)inputs.GetElement(InputsControllerType.Play)).Click -= PlayClick;
            ((Button)inputs.GetElement(InputsControllerType.Pause)).Click -= PauseClick;
            ((Button)inputs.GetElement(InputsControllerType.Stop)).Click -= StopClick;
            ((Button)inputs.GetElement(InputsControllerType.NextFrame)).Click -= NextFrameClick;
            ((Button)inputs.GetElement(InputsControllerType.PrevFrame)).Click -= PrevFrameClick;

            ((Button)inputs.GetElement(InputsControllerType.Play)).Click += PlayClick;
            ((Button)inputs.GetElement(InputsControllerType.Pause)).Click += PauseClick;
            ((Button)inputs.GetElement(InputsControllerType.Stop)).Click += StopClick;
            ((Button)inputs.GetElement(InputsControllerType.NextFrame)).Click += NextFrameClick;
            ((Button)inputs.GetElement(InputsControllerType.PrevFrame)).Click += PrevFrameClick;
            ((NumericUpDown)inputs.GetElement(InputsControllerType.SpeedPlayback)).ValueChanged += PlayerInputHandler_ValueChanged; ;
            
        }
        public IInputController GetInputsController()
        {
            return inputs;
        }
        public void SetPlayer(IPlayer player)
        {
            this.player = player;
            player.OnPlay -= Player_OnPlay;
            player.OnPlay += Player_OnPlay;
            player.OnPause += Player_OnPause;
            player.OnPause += Player_OnPause;
            player.OnEndPlaying -= Player_OnEndPlaying;
            player.OnEndPlaying += Player_OnEndPlaying;
            timelineHandler = new TimelineHandler((ProgressBar)inputs.GetElement(InputsControllerType.TimelineBar), player);
        }

        private void Player_OnPlay()
        {
            SetPlayingStatus(true);
        }

        private void Player_OnPause()
        {
            SetPlayingStatus(false);
        }

        private void PlayerInputHandler_ValueChanged(object sender, EventArgs e)
        {
            if(player!=null)
                 player.PlaybackSpeed = (int)((NumericUpDown)sender).Value;
        }

        private void Player_OnEndPlaying()
        {
            SetPlayingStatus(false);
        }

        private void ConfigurePlayAndPauseBtn(Control play, Control pause)
        {
            pause.Location = play.Location;
            pause.Size = play.Size;
            pause.Visible = false;
            play.Visible = true;
        }
        
        private void SetPlayingStatus(bool playing)
        {
            var play = inputs.GetElement(InputsControllerType.Play);
            var pause = inputs.GetElement(InputsControllerType.Pause);
            play.Visible = !playing;
            pause.Visible = playing;
        }

        private void PrevFrameClick(object sender, EventArgs e)
        {
            player?.ShowPreviousFrame();
        }

        private void NextFrameClick(object sender, EventArgs e)
        {
            player?.ShowNextFrame();
        }

        private void StopClick(object sender, EventArgs e)
        {
            player?.Stop();
        }

        private void PauseClick(object sender, EventArgs e)
        {
            player?.Pause();
            SetPlayingStatus(false);
        }

        private void PlayClick(object sender, EventArgs e)
        {
            player?.Play();
        }
    }
}
