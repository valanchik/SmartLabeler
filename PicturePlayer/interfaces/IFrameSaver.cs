using System.Drawing;
using System.Threading.Tasks;

namespace PicturePlayer
{
    public interface IFrameSaver
    {
        Task SaveFrameAsync(Bitmap frame, int index);
    }
}