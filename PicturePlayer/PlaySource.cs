namespace PicturePlayer
{
    public enum PlaySourceType
    {
        Video,
        FolderImages
    }
    public class PlaySource
    {
        public string Path;
        public PlaySourceType Type;
    }
}
