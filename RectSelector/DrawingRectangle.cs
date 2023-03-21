using RectSelector;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcScan.RectSelector
{
    public class DrawingRectangle
    {
        public bool IsDrawing;
        private Point _startPoint;
        private  ResizableRectangle _resizableRect;
        private float _scaleFactor = 1F;

        public DrawingRectangle()
        {

        }

        public void SetResizebleRectangle(ResizableRectangle  resizableRectangle)
        {
            _resizableRect = resizableRectangle;
        }

        public void SetScaleFactor(float scaleFactor)
        {
            _scaleFactor = scaleFactor;
        }

        public void StartDrawing(Point startPoint)
        {
            IsDrawing = true;
            _startPoint = startPoint.Divide(_scaleFactor);
            _resizableRect.SetLocationAndSize(_startPoint, new Size());
            _resizableRect.UpdateHandles();
        }

        public void StopDrawing()
        {
            IsDrawing = false;
        }

        public void UpdateRectangleSize(Point endPoint)
        {
            endPoint = endPoint.Divide(_scaleFactor);
            int x = Math.Min(_startPoint.X, endPoint.X);
            int y = Math.Min(_startPoint.Y, endPoint.Y);
            int width = Math.Abs(_startPoint.X - endPoint.X);
            int height = Math.Abs(_startPoint.Y - endPoint.Y);
            _resizableRect.SetLocationAndSize(new Point(x, y), new Size(width, height));
            _resizableRect.UpdateHandles();
        }

        public void DrawRectangleAndHandles(Graphics g)
        {
            _resizableRect.DrawRectangleAndHandles(g);
        }

        public Rectangle GetRectangle()
        {
            return _resizableRect.GetRectangle();
        }

        public bool IsMouseInsideRectangle(Point location)
        {
            return _resizableRect.IsMouseInsideRectangle(location);
        }
    }
}
