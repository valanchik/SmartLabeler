using RectSelector;
using System;

namespace InputControllers
{
    public interface IInputController
    {
         void SetActiveElement(InputElementType type, bool status);
    }
}