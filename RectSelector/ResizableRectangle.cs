using System;
using System.Drawing;
using System.Windows.Forms;

namespace RectSelector
{
    public class ResizableRectangle : IScalible
    {
        public readonly int Index;
        private Rectangle _rect = new Rectangle();
        private const int ResizeHandleSize = 6;
        Rectangle[] handles;
        private int minSize = ResizeHandleSize * 2;

        public double ScaleFactor { get; set; } = 1.0f;
        private bool _drawHandleStatus = false;

        public ResizableRectangle(int index)
        {
            Index = index;
            UpdateHandles();
        }
        public void SetScaleFactor(double scaleFactor)
        {
            if (scaleFactor > 0)
            {
                ScaleFactor = scaleFactor;
                UpdateHandles();
            }
        }
        public void SetDrawHandleStatus(bool status)
        {
            _drawHandleStatus = status;
        }
        public void SetLocationAndSize(Point location, Size size)
        {
            _rect.Location = location;
            _rect.Size = size;
        }

        public Rectangle GetRectangle()
        {
            var scaledLocation = new Point((int)(_rect.Location.X * ScaleFactor), (int)(_rect.Location.Y * ScaleFactor));
            var scaledSize = new Size((int)(_rect.Size.Width * ScaleFactor), (int)(_rect.Size.Height * ScaleFactor));
            var scaledRect = new Rectangle(scaledLocation, scaledSize);
            return scaledRect;
        }


        public int GetSelectedHandle(Point location) // надо базовый
        {

            if (handles == null) return -1;
            for (int i = 0; i < handles.Length; i++)
            {
                if (handles[i].Contains(location))
                {
                    return i;
                }
            }
            return -1;
        }

        public Cursor GetCursorForHandle(int handleIndex)
        {
            switch (handleIndex)
            {
                case 0:
                case 4:
                    return Cursors.SizeNWSE;
                case 1:
                case 5:
                    return Cursors.SizeNS;
                case 2:
                case 6:
                    return Cursors.SizeNESW;
                case 3:
                case 7:
                    return Cursors.SizeWE;
                default:
                    return Cursors.Default;
            }
        }

        public void ResizeRectangle(int handleIndex, Point startPoint, Point endPoint)
        {
            startPoint = startPoint.Divide(ScaleFactor);
            endPoint = endPoint.Divide(ScaleFactor);

            int deltaX = (int)((endPoint.X - startPoint.X));
            int deltaY = (int)((endPoint.Y - startPoint.Y));

            int minWidth = minSize; // Minimum rectangle width
            int minHeight = minSize; // Minimum rectangle height

            int newWidth = _rect.Width;
            int newHeight = _rect.Height;

            switch (handleIndex)
            {
                case 0: newWidth -= deltaX; newHeight -= deltaY; break;
                case 1: newHeight -= deltaY; break;
                case 2: newWidth += deltaX; newHeight -= deltaY; break;
                case 3: newWidth += deltaX; break;
                case 4: newWidth += deltaX; newHeight += deltaY; break;
                case 5: newHeight += deltaY; break;
                case 6: newWidth -= deltaX; newHeight += deltaY; break;
                case 7: newWidth -= deltaX; break;
            }

            if (newWidth >= minWidth && newHeight >= minHeight)
            {
                if (handleIndex == 0 || handleIndex == 6 || handleIndex == 7)
                    _rect.X += deltaX;
                if (handleIndex == 0 || handleIndex == 1 || handleIndex == 2)
                    _rect.Y += deltaY;

                _rect.Width = newWidth;
                _rect.Height = newHeight;
            }
        }

        public void DrawRectangleAndHandles(Graphics graphics)
        {
            using (Pen pen = new Pen(Color.Red, 2))
            using (Brush brush = new SolidBrush(Color.FromArgb(128, Color.Blue)))
            {
                graphics.FillRectangle(brush, GetRectangle());
                graphics.DrawRectangle(pen, GetRectangle());

                if (_drawHandleStatus) DrawHandles(graphics);
            }
        }
        private void DrawHandles(Graphics graphics)
        {
            foreach (Rectangle handle in handles)
            {
                graphics.FillRectangle(Brushes.White, handle);
                graphics.DrawRectangle(Pens.Black, handle);
            }
        }
        public bool IsMouseInsideRectangle(Point location)
        {
            return GetRectangle().Contains(location);
        }

        public void MoveRectangle(Point startPoint, Point endPoint)
        {
            startPoint = startPoint.Divide(ScaleFactor);
            endPoint = endPoint.Divide(ScaleFactor);
            int deltaX = (int)((endPoint.X - startPoint.X));
            int deltaY = (int)((endPoint.Y - startPoint.Y));

            _rect.X += deltaX;
            _rect.Y += deltaY;
        }

        public void UpdateHandles()
        {
            Point location = new Point((int)Math.Round(_rect.Left * ScaleFactor - ResizeHandleSize / 2.0), (int)Math.Round(_rect.Top * ScaleFactor - ResizeHandleSize / 2.0));
            Size size = new Size(ResizeHandleSize, ResizeHandleSize);
            handles = new[]
            {
                new Rectangle(location, size),
                new Rectangle(new Point((int)Math.Round((_rect.Left + _rect.Width / 2.0) * ScaleFactor - ResizeHandleSize / 2.0), (int)Math.Round(_rect.Top * ScaleFactor - ResizeHandleSize / 2.0)), size),
                new Rectangle(new Point((int)Math.Round((_rect.Right) * ScaleFactor - ResizeHandleSize / 2.0), (int)Math.Round(_rect.Top * ScaleFactor - ResizeHandleSize / 2.0)), size),
                new Rectangle(new Point((int)Math.Round((_rect.Right) * ScaleFactor - ResizeHandleSize / 2.0), (int)Math.Round((_rect.Top + _rect.Height / 2.0) * ScaleFactor - ResizeHandleSize / 2.0)), size),
                new Rectangle(new Point((int)Math.Round((_rect.Right) * ScaleFactor - ResizeHandleSize / 2.0), (int)Math.Round((_rect.Bottom) * ScaleFactor - ResizeHandleSize / 2.0)), size),
                new Rectangle(new Point((int)Math.Round((_rect.Left + _rect.Width / 2.0) * ScaleFactor - ResizeHandleSize / 2.0), (int)Math.Round((_rect.Bottom) * ScaleFactor - ResizeHandleSize / 2.0)), size),
                new Rectangle(new Point((int)Math.Round(_rect.Left * ScaleFactor - ResizeHandleSize / 2.0), (int)Math.Round((_rect.Bottom) * ScaleFactor - ResizeHandleSize / 2.0)), size),
                new Rectangle(new Point((int)Math.Round(_rect.Left * ScaleFactor - ResizeHandleSize / 2.0), (int)Math.Round((_rect.Top + _rect.Height / 2.0) * ScaleFactor - ResizeHandleSize / 2.0)), size),
            };
        }
    }


}



