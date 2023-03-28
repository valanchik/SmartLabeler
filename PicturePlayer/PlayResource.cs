using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
