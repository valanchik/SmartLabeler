namespace PicturePlayer
{
    public interface IPlayable
    {
        int PlaybackSpeed { get; set; }

        bool IsPlaying();
        void Pause();
        void Play();
    }
}