using InputControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputControllers
{
    public class InputPlayerController : IInputPlayerController
    {
        private Dictionary<InputPlayerControllerType, Control> dict = new Dictionary<InputPlayerControllerType, Control>();

        public void SetElement(InputPlayerControllerType type, Control element)
        {
            dict[type] = element;
        }
        public Control GetElement(InputPlayerControllerType type)
        {
            if (dict.TryGetValue(type, out Control element))
            {
                return element;
            }

            return null;
        }

        public void SetDisableElement(InputPlayerControllerType type)
        {
            SetElementStatus(type, false);
        }
        public void SetEnableElement(InputPlayerControllerType type)
        {
            SetElementStatus(type, true);
        }
        protected void SetElementStatus(InputPlayerControllerType type, bool status)
        {
            if (dict.TryGetValue(type, out Control element))
            {
                element.Enabled = status;
            }
        }
        public IEnumerator<KeyValuePair<InputPlayerControllerType, Control>> GetEnumerator()
        {
            return dict.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return dict.GetEnumerator();
        }
    }
}
