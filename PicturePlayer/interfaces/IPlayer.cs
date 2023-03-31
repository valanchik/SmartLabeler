
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PicturePlayer
{
    public interface IPlayer : IPlayable, IAllFramesSaver
    {
        event Action OnTick;

        bool ShowFrameByIndex(int index);
        bool ShowNextFrame();
        bool ShowPreviousFrame();
        bool IsReady();

        Image GetCurrentFrame();
        int GetCurrentFrameIndex();
        IFrameSaver GetFrameSaver();
        Form GetCurrentWindow();
        int GetFramesCount();
        void SetSource(PlaySource resource);
    }
}