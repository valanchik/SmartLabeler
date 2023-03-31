using System;

namespace PicturePlayer
{
    public interface IPlayable
    {
        event Action OnTick;
        event Action OnPause;
        event Action OnEndPlaying;
        int PlaybackSpeed { get; set; }

        bool IsPlaying();
        void Pause();
        void Play();
        void RaiseOnTick();
        public void RaiseOnPause();
        public void RaiseOnEndPlaying();

    }
}