using System.Collections.Generic;
using System.Windows.Forms;

namespace InputControllers
{
    public interface IInputPlayerController: IEnumerable<KeyValuePair<InputPlayerControllerType, Control>>
    {
        Control GetElement(InputPlayerControllerType type);
        void SetDisableElement(InputPlayerControllerType type);
        void SetElement(InputPlayerControllerType type, Control element);
        void SetEnableElement(InputPlayerControllerType type);
    }
}