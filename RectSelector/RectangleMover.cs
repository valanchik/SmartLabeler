using RectSelector;
using System.Drawing;

namespace ProcScan.RectSelector
{
    public class RectangleMover
    {
        private readonly ResizableRectangle _resizableRect;
        private Point _startPoint;
        private bool _isMoving;

        public RectangleMover(ResizableRectangle resizableRect)
        {
            _resizableRect = resizableRect;
        }

        public bool IsMoving => _isMoving;

        public void StartMoving(Point startPoint)
        {
            _isMoving = true;
            _startPoint = startPoint;
        }

        public void StopMoving()
        {
            _isMoving = false;
        }

        public void Move(Point endPoint)
        {
            if (!_isMoving) return;

            _resizableRect.MoveRectangle(_startPoint, endPoint);
            _startPoint = endPoint;
        }
    }

}
