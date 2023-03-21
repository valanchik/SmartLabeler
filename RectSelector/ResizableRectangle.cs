using System.Drawing;
using System.Windows.Forms;

namespace RectSelector
{
    public class ResizableRectangle
    {
        private Rectangle _rect = new Rectangle();
        private const int ResizeHandleSize = 8;
        Rectangle zoomedRect;
        Rectangle[] handles;
        public float ScaleFactor { get; set; } = 1.0f;

        public ResizableRectangle()
        {
            UpdateHandles();
        }
        public void SetScaleFactor(float scaleFactor)
        {
            if (scaleFactor > 0)
            {
                ScaleFactor = scaleFactor;
                UpdateHandles();
            }
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


        public int GetSelectedHandle(Point location)
        {
            location = new Point((int)(location.X / ScaleFactor), (int)(location.Y / ScaleFactor));
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
            
            int deltaX = (int)((endPoint.X - startPoint.X));
            int deltaY = (int)((endPoint.Y - startPoint.Y));

            int minWidth = (int)(10); // Minimum rectangle width
            int minHeight = (int)(10); // Minimum rectangle height
            
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
            
            {
                DrawOtherFunc(graphics);
                graphics.DrawRectangle(pen, GetRectangle());

                foreach (Rectangle handle in handles)
                {
                    graphics.FillRectangle(Brushes.White, handle);
                    graphics.DrawRectangle(Pens.Black, handle);
                }
            }
        }
        public void DrawOtherFunc(Graphics graphics)
        {
            using (Pen penGreen = new Pen(Color.Green, 2))
            {
                graphics.DrawRectangle(penGreen, _rect);
            }
        }
        public bool IsMouseInsideRectangle(Point location)
        {
            Point adjustedLocation = new Point((int)(location.X * ScaleFactor), (int)(location.Y * ScaleFactor));
            return zoomedRect.Contains(adjustedLocation);
        }

        public void MoveRectangle(Point startPoint, Point endPoint)
        {
            int deltaX = (int)((endPoint.X - startPoint.X));
            int deltaY = (int)((endPoint.Y - startPoint.Y) );

            _rect.X += deltaX;
            _rect.Y += deltaY;
        }

        public void UpdateHandles()
        {
            
            handles = new[]
            {
                new Rectangle((int)((_rect.Left - ResizeHandleSize / 2) * ScaleFactor), (int)((_rect.Top - ResizeHandleSize / 2) * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor)),
                new Rectangle((int)((_rect.Left + _rect.Width / 2 - ResizeHandleSize / 2) * ScaleFactor), (int)((_rect.Top - ResizeHandleSize / 2) * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor)),
                new Rectangle((int)((_rect.Right - ResizeHandleSize / 2) * ScaleFactor), (int)((_rect.Top - ResizeHandleSize / 2) * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor)),
                new Rectangle((int)((_rect.Right - ResizeHandleSize / 2) * ScaleFactor), (int)((_rect.Top + _rect.Height / 2 - ResizeHandleSize / 2) * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor)),
                new Rectangle((int)((_rect.Right - ResizeHandleSize / 2) * ScaleFactor), (int)((_rect.Bottom - ResizeHandleSize / 2) * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor)),
                new Rectangle((int)((_rect.Left + _rect.Width / 2 - ResizeHandleSize / 2) * ScaleFactor), (int)((_rect.Bottom - ResizeHandleSize / 2) * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor)),
                new Rectangle((int)((_rect.Left - ResizeHandleSize / 2) * ScaleFactor), (int)((_rect.Bottom - ResizeHandleSize / 2) * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor)),
                new Rectangle((int)((_rect.Left - ResizeHandleSize / 2) * ScaleFactor), (int)((_rect.Top + _rect.Height / 2 - ResizeHandleSize / 2) * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor), (int)(ResizeHandleSize * ScaleFactor)),
            };
        }
    }


}

        

