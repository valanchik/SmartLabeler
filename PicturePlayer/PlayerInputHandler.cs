using InputControllers;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class PlayerInputHandler
    {
        private  IPlayer player;
        private  IInputPlayerController inputs;
        private TimelineHandler timelineHandler;
        public PlayerInputHandler(IInputPlayerController inputs)
        {
            this.inputs = inputs;
            ConfigurePlayAndPauseBtn(
                inputs.GetElement(InputsPlayerControllerType.Play),
                inputs.GetElement(InputsPlayerControllerType.Pause)
            );
            Init();
        }

        public void Init()
        {
            

            ((Button)inputs.GetElement(InputsPlayerControllerType.Play)).Click -= PlayClick;
            ((Button)inputs.GetElement(InputsPlayerControllerType.Pause)).Click -= PauseClick;
            ((Button)inputs.GetElement(InputsPlayerControllerType.Stop)).Click -= StopClick;
            ((Button)inputs.GetElement(InputsPlayerControllerType.NextFrame)).Click -= NextFrameClick;
            ((Button)inputs.GetElement(InputsPlayerControllerType.PrevFrame)).Click -= PrevFrameClick;

            ((Button)inputs.GetElement(InputsPlayerControllerType.Play)).Click += PlayClick;
            ((Button)inputs.GetElement(InputsPlayerControllerType.Pause)).Click += PauseClick;
            ((Button)inputs.GetElement(InputsPlayerControllerType.Stop)).Click += StopClick;
            ((Button)inputs.GetElement(InputsPlayerControllerType.NextFrame)).Click += NextFrameClick;
            ((Button)inputs.GetElement(InputsPlayerControllerType.PrevFrame)).Click += PrevFrameClick;
            ((NumericUpDown)inputs.GetElement(InputsPlayerControllerType.SpeedPlayback)).ValueChanged += PlayerInputHandler_ValueChanged; ;
            
        }
        public IInputPlayerController GetInputsController()
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
            timelineHandler = new TimelineHandler((ProgressBar)inputs.GetElement(InputsPlayerControllerType.TimelineBar), player);
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
            var play = inputs.GetElement(InputsPlayerControllerType.Play);
            var pause = inputs.GetElement(InputsPlayerControllerType.Pause);
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
            player?.Pause();
            player?.ShowFrameByIndex(0);
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
