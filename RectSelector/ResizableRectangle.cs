using System;
using System.Drawing;
using System.Windows.Forms;

namespace RectSelector
{
    public class ResizableRectangle : IScalible
    {
        private string _title = "None";
        public  int Index;
        private Rectangle _rect = new Rectangle();
        private const int ResizeHandleSize = 6;
        Rectangle[] handles;
        private readonly int minSize = ResizeHandleSize * 2;

        public double ScaleFactor { get; set; } = 1.0f;
        private bool _drawHandleStatus = false;
        private PictureBox pictureBox;

        public ResizableRectangle(int index, PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            Index = index;
            UpdateHandles();
        }
        public ResizableRectangle Clone()
        {
            ResizableRectangle cloned = new ResizableRectangle(Index, pictureBox)
            {
                ScaleFactor = ScaleFactor,
                _title = _title,
                _drawHandleStatus = _drawHandleStatus,
                _rect = new Rectangle(_rect.Location, _rect.Size),
            };
            cloned.UpdateHandles();
            return cloned;
        }
        public void SetTitle(string title)
        {
            _title = title;
        }
        private void DrawTitle(Graphics graphics)
        {
            if (!string.IsNullOrEmpty(_title))
            {
                using (Font font = new Font("Arial", 12))
                using (Brush brush = new SolidBrush(Color.White))
                {
                    SizeF titleSize = graphics.MeasureString(_title, font);
                    float availableWidth = _rect.Width * (float)ScaleFactor;
                    string titleToDraw = _title;
                    if (titleSize.Width > availableWidth)
                    {
                        titleToDraw = TruncateTitle(_title, font, availableWidth, graphics);
                    }
                    float x = (_rect.Left * (float)ScaleFactor) + (availableWidth - titleSize.Width) / 2;
                    float y = (_rect.Top * (float)ScaleFactor) - titleSize.Height;
                    graphics.DrawString(titleToDraw, font, brush, x, y);
                }
            }
        }

        private string TruncateTitle(string title, Font font, float availableWidth, Graphics graphics)
        {
            string ellipsis = "...";
            float ellipsisWidth = graphics.MeasureString(ellipsis, font).Width;
            int titleLength = title.Length;

            while (titleLength > 0 && graphics.MeasureString(title.Substring(0, titleLength) + ellipsis, font).Width > availableWidth)
            {
                titleLength--;
            }

            return title.Substring(0, titleLength) + ellipsis;
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
            var newrect = new Rectangle(location, size);
            if (newrect.IsRectangleInsidePictureBox(pictureBox, ScaleFactor))
            {
                _rect = newrect;
            } else
            {
                _rect = newrect.ClipRectangleToPictureBox(pictureBox, ScaleFactor);
            }
           
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

            int deltaX = (endPoint.X - startPoint.X);
            int deltaY = (endPoint.Y - startPoint.Y);

            int minWidth = minSize;
            int minHeight = minSize; 

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
                Rectangle newRect = new Rectangle(_rect.Location, new Size(newWidth, newHeight));

                if (handleIndex == 0 || handleIndex == 6 || handleIndex == 7)
                    newRect.X += deltaX;
                if (handleIndex == 0 || handleIndex == 1 || handleIndex == 2)
                    newRect.Y += deltaY;

                if (newRect.IsRectangleInsidePictureBox(pictureBox,ScaleFactor))
                {
                    _rect = newRect;
                } else
                {
                    _rect = newRect.ClipRectangleToPictureBox(pictureBox, ScaleFactor);
                }
            }
        }


        public void DrawRectangleAndHandles(Graphics graphics)
        {
            DrawTitle(graphics); 

            using (Pen pen = new Pen(Color.Red, 2))
            using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.Blue)))
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
            int deltaX = (endPoint.X - startPoint.X);
            int deltaY = (endPoint.Y - startPoint.Y);

            Rectangle newRect = new Rectangle(_rect.X + deltaX, _rect.Y + deltaY, _rect.Width, _rect.Height);

            if (newRect.IsRectangleInsidePictureBox(pictureBox,ScaleFactor))
            {
                _rect = newRect;
            }
            else
            {
                _rect = newRect.AlignRectangleToPictureBoxEdges(pictureBox, ScaleFactor);
            }
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



