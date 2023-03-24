using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturePlayer
{
    class Player
    {
        private IFrameSelector _frameSelector;
        private PictureBox _pictureBox;

        public Player(IFrameSelector frameSelector, PictureBox pictureBox)
        {
            _frameSelector = frameSelector;
            _pictureBox = pictureBox;
        }
    }
}
