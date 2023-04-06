using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotkeys
{
    public class Hotkey
    {
        public Keys Key { get; set; }
        public bool Control { get; set; }
        public bool Alt { get; set; }
        public bool Shift { get; set; }

        public Hotkey(Keys key, bool control = false, bool alt = false, bool shift = false)
        {
            Key = key;
            Control = control;
            Alt = alt;
            Shift = shift;
        }

        public bool Matches(Keys keyData)
        {
            return Key == keyData &&
                   Control == keyData.HasFlag(Keys.Control) &&
                   Alt == keyData.HasFlag(Keys.Alt) &&
                   Shift == keyData.HasFlag(Keys.Shift);
        }
    }

}
