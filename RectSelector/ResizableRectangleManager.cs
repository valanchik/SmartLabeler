using RectSelector;
using System;
using System.Drawing;

namespace ProcScan.RectSelector
{
    public class ResizableRectangleManager : IScalible
    {
        private ResizableRectangle _resizableRect;
        private int _selectedHandle;
        private Point _startPoint;
        private float _scaleFactor = 1f;

        public ResizableRectangleManager()
        {

        }
        public void SetResizableRectangle(ResizableRectangle resizableRectangle)
        {
            _resizableRect = resizableRectangle;
        }
        public void SetScaleFactor(float scaleFactor)
        {
            _scaleFactor = scaleFactor;
        }
        public bool IsResizing => _selectedHandle != -1;

        public int GetSelectedHandle(Point location)
        {
            return _resizableRect.GetSelectedHandle(location);
        }

        public void StartResizing(int handleIndex, Point startScaledPoint)
        {
            _selectedHandle = handleIndex;
            _startPoint = startScaledPoint.Multiply(_scaleFactor);
        }

        public void StopResizing()
        {
            _selectedHandle = -1;
        }

        public void Resize(Point endPoint)
        {
            if (_selectedHandle == -1) return;
            endPoint = endPoint.Multiply(_scaleFactor);
            _resizableRect.ResizeRectangle(_selectedHandle, _startPoint, endPoint);
            _startPoint = endPoint;
        }

        
    }
}