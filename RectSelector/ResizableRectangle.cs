using System.Drawing;
using System.Windows.Forms;

namespace RectSelector
{
    public class ResizableRectangle
    {
        private Rectangle _rect;
        private const int ResizeHandleSize = 8;
        Rectangle zoomedRect;

        public ResizableRectangle()
        {
            _rect = new Rectangle();
        }
        

        public void SetLocation(Point location)
        {
            _rect.Location = location;
        }

        public void SetSize(Size size)
        {
            _rect.Size = size;
        }

        public Rectangle GetRectangle()
        {
            return _rect;
        }

        public bool Contains(Point point)
        {
            return _rect.Contains(point);
        }

        public int GetSelectedHandle(Point location)
        {
            Rectangle[] handles = GetHandles();

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
            int deltaY = (int)((endPoint.Y - startPoint.Y) );

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
                zoomedRect = new Rectangle(
                    (int)(_rect.X),
                    (int)(_rect.Y),
                    (int)(_rect.Width ),
                    (int)(_rect.Height )
                );

                graphics.DrawRectangle(pen, zoomedRect);

                foreach (Rectangle handle in GetHandles())
                {
                    graphics.FillRectangle(Brushes.White, handle);
                    graphics.DrawRectangle(Pens.Black, handle);
                }
            }
        }
        public bool IsMouseInsideRectangle(Point location)
        {
            Point adjustedLocation = new Point((int)(location.X), (int)(location.Y));
            return zoomedRect.Contains(adjustedLocation);
        }

        public void MoveRectangle(Point startPoint, Point endPoint)
        {
            int deltaX = (int)((endPoint.X - startPoint.X) );
            int deltaY = (int)((endPoint.Y - startPoint.Y) );

            _rect.X += deltaX;
            _rect.Y += deltaY;
        }

        public Rectangle[] GetHandles()
        {
            return new[]
            {
        new Rectangle((int)((_rect.Left - ResizeHandleSize / 2) ), (int)((_rect.Top - ResizeHandleSize / 2) ), (int)(ResizeHandleSize ), (int)(ResizeHandleSize )),
        new Rectangle((int)((_rect.Left + _rect.Width / 2 - ResizeHandleSize / 2) ), (int)((_rect.Top - ResizeHandleSize / 2) ), (int)(ResizeHandleSize ), (int)(ResizeHandleSize )),
        new Rectangle((int)((_rect.Right - ResizeHandleSize / 2)), (int)((_rect.Top - ResizeHandleSize / 2) ), (int)(ResizeHandleSize ), (int)(ResizeHandleSize )),
        new Rectangle((int)((_rect.Right - ResizeHandleSize / 2) ), (int)((_rect.Top + _rect.Height / 2 - ResizeHandleSize / 2) ), (int)(ResizeHandleSize ), (int)(ResizeHandleSize )),
        new Rectangle((int)((_rect.Right - ResizeHandleSize / 2) ), (int)((_rect.Bottom - ResizeHandleSize / 2) ), (int)(ResizeHandleSize ), (int)(ResizeHandleSize )),
        new Rectangle((int)((_rect.Left + _rect.Width / 2 - ResizeHandleSize / 2) ), (int)((_rect.Bottom - ResizeHandleSize / 2) ), (int)(ResizeHandleSize ), (int)(ResizeHandleSize )),
        new Rectangle((int)((_rect.Left - ResizeHandleSize / 2) ), (int)((_rect.Bottom - ResizeHandleSize / 2) ), (int)(ResizeHandleSize ), (int)(ResizeHandleSize )),
        new Rectangle((int)((_rect.Left - ResizeHandleSize / 2) ), (int)((_rect.Top + _rect.Height / 2 - ResizeHandleSize / 2) ), (int)(ResizeHandleSize ), (int)(ResizeHandleSize )),
    };
        }

    }


}

        

