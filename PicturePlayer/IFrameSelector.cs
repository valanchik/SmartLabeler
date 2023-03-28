using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public interface IFrameSelector
    {
        bool ShowFrameByIndex(int index);
        bool ShowNextFrame();
        bool ShowPreviousFrame();
        bool IsReady();
        Task SaveAllFramesAsync();
        Image GetCurrentFrame();
        IFrameSaver GetFrameSaver();
        Form GetCurrentWindow();
        int GetFramesCount();
    }
}