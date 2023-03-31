namespace PicturePlayer
{
    public enum PLayerResourceType
    {
        VideoFile,
        Directory
    }
    public class PlayResource
    {
        public string Path;
        public PLayerResourceType ResourceType;
    }
}
