
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
        event Action OnStop;
        event Action OnReady;
        event Action OnEndPlaying;
        bool ShowFrameByIndex(int index);
        bool ShowNextFrame();
        bool ShowPreviousFrame();
        bool IsReady();
        PictureBox GetScreen();
        Bitmap GetCurrentFrame();
        int GetCurrentFrameIndex();
        IFrameSaver GetFrameSaver();
        Form GetCurrentWindow();
        int GetFramesCount();
        IPlayer SetSource(PlaySource resource);
        void Init();
    }
}