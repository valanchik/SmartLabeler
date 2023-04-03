using System;

namespace PicturePlayer
{
    public interface IPlayable
    {
        int PlaybackSpeed { get; set; }
        bool IsPlaying();
        void Pause();
        void Stop();
        void Play();
    }
}