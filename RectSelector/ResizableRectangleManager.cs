using RectSelector;
using System;
using System.Drawing;

namespace ProcScan.RectSelector
{
    public class ResizableRectangleManager
    {
        private readonly ResizableRectangle _resizableRect;
        private int _selectedHandle;
        private Point _startPoint;

        public ResizableRectangleManager(ResizableRectangle resizableRect)
        {
            _resizableRect = resizableRect;
        }

        public bool IsResizing => _selectedHandle != -1;

        public int GetSelectedHandle(Point location)
        {
            return _resizableRect.GetSelectedHandle(location);
        }

        public void StartResizing(int handleIndex, Point startScaledPoint)
        {
            _selectedHandle = handleIndex;
            _startPoint = startScaledPoint;
        }

        public void StopResizing()
        {
            _selectedHandle = -1;
        }

        public void Resize(Point endPoint)
        {
            if (_selectedHandle == -1) return;
            
            _resizableRect.ResizeRectangle(_selectedHandle, _startPoint, endPoint);
            _startPoint = endPoint;
        }
    }
}