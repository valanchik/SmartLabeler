
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PicturePlayer
{
    public interface IPlayer : IPlayable, IAllFramesSaver
    {

        event Action OnTick;
        event Action OnPlay;
        event Action OnPause;
        event Action OnReady;
        event Action OnEndPlaying;
        bool ShowFrameByIndex(int index);
        bool ShowNextFrame();
        bool ShowPreviousFrame();
        bool IsReady();

        Bitmap GetCurrentFrame();
        int GetCurrentFrameIndex();
        IFrameSaver GetFrameSaver();
        Form GetCurrentWindow();
        int GetFramesCount();
        IPlayer SetSource(PlaySource resource);
        void Init();
    }
}