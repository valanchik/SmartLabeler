using System.Drawing;

namespace RectSelector
{
    public interface IRectangleSelector
    {
        bool IsAnyProcess();
        bool IsMouseInsideRectangle(Point location);
        void SetZoomFactor(float zoomFactor);
    }
}