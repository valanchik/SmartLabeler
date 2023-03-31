namespace InputControllers
{
    public interface IInputController
    {
        void SetActiveElement(InputElementType type, bool status);
    }
}