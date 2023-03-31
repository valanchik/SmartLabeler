namespace PicturePlayer
{
    public enum PLayerResourceType
    {
        VideoFile,
        Directory
    }
    public class PlaySource
    {
        public string Path;
        public PLayerResourceType ResourceType;
    }
}
