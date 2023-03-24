namespace PicturePlayer
{
    public interface IFrameSelector
    {
        void ShowFrameByIndex(int index);
        void ShowNextFrame();
        void ShowPreviousFrame();
    }
}