using System.Drawing;

namespace PicturePlayer
{
    public interface IFrameSaver
    {
        void SaveFrame(Bitmap frame, int index);
    }
}