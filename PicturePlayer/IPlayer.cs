
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    public interface IPlayer: IPlayable, IAllFramesSaver
    {
        bool ShowFrameByIndex(int index);
        bool ShowNextFrame();
        bool ShowPreviousFrame();
        bool IsReady();
        
        Image GetCurrentFrame();
        int GetCurrentFrameIndex();
        IFrameSaver GetFrameSaver();
        Form GetCurrentWindow();
        int GetFramesCount();
        void SetResource(PlayResource resource);
        Task SaveAllFramesAsync();
    }
}