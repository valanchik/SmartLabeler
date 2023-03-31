using System.Threading.Tasks;

namespace PicturePlayer
{
    public interface IAllFramesSaver
    {
        Task SaveAllFramesAsync();
    }
}
