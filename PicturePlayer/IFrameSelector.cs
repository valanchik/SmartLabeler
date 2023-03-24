namespace PicturePlayer
{
    public interface IFrameSelector
    {
        bool ShowFrameByIndex(int index);
        bool ShowNextFrame();
        bool ShowPreviousFrame();
    }
}