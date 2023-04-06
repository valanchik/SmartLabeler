using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotkeys
{
    public class HotkeyManager
    {
        private static HotkeyManager _instance;
        public static HotkeyManager Instance => _instance ??= new HotkeyManager();

        private Dictionary<Hotkey, Action> _hotkeyActions;

        private HotkeyManager()
        {
            _hotkeyActions = new Dictionary<Hotkey, Action>();
        }

        public void AddHotkey(Hotkey hotkey, Action action)
        {
            _hotkeyActions.Add(hotkey, action);
        }

        public void RemoveHotkey(Hotkey hotkey)
        {
            _hotkeyActions.Remove(hotkey);
        }

        public void ProcessHotkeys(Keys keyData)
        {
            foreach (var hotkey in _hotkeyActions.Keys)
            {
                if (hotkey.Matches(keyData))
                {
                    _hotkeyActions[hotkey].Invoke();
                    break;
                }
            }
        }
    }


}
