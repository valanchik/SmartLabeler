using System.Collections.Generic;
using System.Windows.Forms;

namespace InputControllers
{
    public interface IInputPlayerController : IEnumerable<KeyValuePair<InputsPlayerControllerType, Control>>
    {
        Control GetElement(InputsPlayerControllerType type);
        void SetDisableElement(InputsPlayerControllerType type);
        void SetElement(InputsPlayerControllerType type, Control element);
        void SetEnableElement(InputsPlayerControllerType type);
    }
}