using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputControllers
{
    public class InputRectController : IInputController
    {
        private readonly Button _createNewRect;
        public event EventHandler<EventArgs> OnClickNewRect;

        public InputRectController(Button creaNewRect)
        {
            _createNewRect = creaNewRect;
            _createNewRect.Click += CreateNewRectClickHandler;
        }
        public void SetActiveElement(InputElementType type, bool status)
        {
            switch (type)
            {
                case InputElementType.CreateNewRectBtn:
                    _createNewRect.Enabled = status;
                    break;
            }

        }
        private void CreateNewRectClickHandler(object sender, EventArgs e)
        {
            OnClickNewRect?.Invoke(sender, e);
        }
    }
}
