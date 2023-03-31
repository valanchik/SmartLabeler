﻿using InputControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public class Player : IPlayable
    {
        protected PictureBox _pictureBox;
        protected Timer _playbackTimer;
        protected int _playbackSpeed = 1;
        protected PlayerInputHandler inputsHandler;

        public Player(PictureBox pictureBox, IInputPlayerController inputs)
        {
            _pictureBox = pictureBox;
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
                inputsHandler.TogglePlayAndPause();
            }
        }

        public void Pause()
        {
            if (IsPlaying())
            {
                _playbackTimer.Stop();
                inputsHandler.TogglePlayAndPause();
            }
        }

        public bool IsPlaying()
        {
            return _playbackTimer.Enabled;
        }

        protected virtual void OnPlaybackTimerTick(object sender, EventArgs e) { }
    }

}