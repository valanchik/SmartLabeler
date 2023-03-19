using System.Drawing;
using System.Windows.Forms;

namespace RectSelector
{
    public interface IResizableRectangle
    {
        bool Contains(Point point);
        void DrawRectangleAndHandles(Graphics graphics, float zoomFactor, bool isDrawing);
        Cursor GetCursorForHandle(int handleIndex);
        Rectangle[] GetHandles(float zoomFactor);
        Rectangle GetRectangle();
        int GetSelectedHandle(Point location);
        bool IsMouseInsideRectangle(Point location);
        void MoveRectangle(Point startPoint, Point endPoint);
        void ResizeRectangle(int handleIndex, Point startPoint, Point endPoint);
        void SetLocation(Point location);
        void SetSize(Size size);
        void SetZoomFactor(float zoomFactor);
    }
}