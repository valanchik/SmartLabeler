using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InputControllers
{
    public class InputController : IInputController
    {
        private readonly Dictionary<InputsControllerType, Control> dict = new Dictionary<InputsControllerType, Control>();

        public void SetElement(InputsControllerType type, Control element)
        {
            dict[type] = element;
        }
        public Control GetElement(InputsControllerType type)
        {
            if (dict.TryGetValue(type, out Control element))
            {
                return element;
            }

            return null;
        }

        public void SetDisableElement(InputsControllerType type)
        {
            SetElementStatus(type, false);
        }
        public void SetEnableElement(InputsControllerType type)
        {
            SetElementStatus(type, true);
        }
        protected void SetElementStatus(InputsControllerType type, bool status)
        {
            if (dict.TryGetValue(type, out Control element))
            {
                element.Enabled = status;
            }
        }
        public IEnumerator<KeyValuePair<InputsControllerType, Control>> GetEnumerator()
        {
            return dict.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }
    }
}
