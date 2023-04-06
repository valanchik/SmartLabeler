using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotkeys
{
    public class HotkeyEventArgs : EventArgs
    {
        public Hotkey Hotkey { get; set; }

        public HotkeyEventArgs(Hotkey hotkey)
        {
            Hotkey = hotkey;
        }
    }
}
