using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InputControllers
{
    public class InputPlayerController : IInputPlayerController
    {
        private readonly Dictionary<InputsPlayerControllerType, Control> dict = new Dictionary<InputsPlayerControllerType, Control>();

        public void SetElement(InputsPlayerControllerType type, Control element)
        {
            dict[type] = element;
        }
        public Control GetElement(InputsPlayerControllerType type)
        {
            if (dict.TryGetValue(type, out Control element))
            {
                return element;
            }

            return null;
        }

        public void SetDisableElement(InputsPlayerControllerType type)
        {
            SetElementStatus(type, false);
        }
        public void SetEnableElement(InputsPlayerControllerType type)
        {
            SetElementStatus(type, true);
        }
        protected void SetElementStatus(InputsPlayerControllerType type, bool status)
        {
            if (dict.TryGetValue(type, out Control element))
            {
                element.Enabled = status;
            }
        }
        public IEnumerator<KeyValuePair<InputsPlayerControllerType, Control>> GetEnumerator()
        {
            return dict.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }
    }
}
