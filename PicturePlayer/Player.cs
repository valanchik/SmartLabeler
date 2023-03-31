using InputControllers;
using System;
using System.Windows.Forms;

namespace PicturePlayer
{
    public abstract class Player : IPlayable
    {
        public event Action OnTick;
        public event Action OnPause;
        public event Action OnEndPlaying;

        protected PictureBox _pictureBox;
        protected Timer _playbackTimer;
        protected int _playbackSpeed = 1;
        protected PlayerInputHandler inputsHandler;

        public Player(IInputPlayerController inputs)
        {
            _pictureBox = (PictureBox)inputs.GetElement(InputPlayerControllerType.PictureBox);
            _playbackTimer = new Timer();
            _playbackTimer.Tick += OnPlaybackTimerTick;
        }

        public int PlaybackSpeed
        {
            get { return _playbackSpeed; }
            set
            {
                _playbackSpeed = value;
                _playbackTimer.Interval = 1000 / value;
            }
        }

        public void Play()
        {
            if (!IsPlaying())
            {
                _playbackTimer.Start();
            }
            
        }

        public void Pause()
        {
            if (IsPlaying())
            {
                _playbackTimer.Stop();
            }
        }

        public bool IsPlaying()
        {
            return _playbackTimer.Enabled;
        }
        public abstract bool ShowNextFrame();
        
        protected virtual void OnPlaybackTimerTick(object sender, EventArgs e) {
            if (!ShowNextFrame())
            {
                Pause();
                RaiseOnEndPlaying();
            }
        }

        public void RaiseOnTick() => OnTick?.Invoke();
        public void RaiseOnPause() => OnPause?.Invoke();
        public void RaiseOnEndPlaying() => OnEndPlaying?.Invoke();

    }

}
