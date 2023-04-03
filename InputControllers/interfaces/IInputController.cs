using System.Collections.Generic;
using System.Windows.Forms;

namespace InputControllers
{
    public interface IInputController : IEnumerable<KeyValuePair<InputsControllerType, Control>>
    {
        Control GetElement(InputsControllerType type);
        void SetDisableElement(InputsControllerType type);
        void SetElement(InputsControllerType type, Control element);
        void SetEnableElement(InputsControllerType type);
    }
}