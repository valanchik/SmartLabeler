using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public static class RectangleExtensions
{
    public static bool IsRectangleInsidePictureBox(this Rectangle rect, PictureBox pictureBox, double scaleFactor)
    {
        double maxWidth = pictureBox.Width/scaleFactor;
        double maxHeight = pictureBox.Height/scaleFactor;

        return rect.Left >= 0 && rect.Top >= 0 && rect.Right <= maxWidth && rect.Bottom <= maxHeight;
    }
    public static Rectangle ClipRectangleToPictureBox(this Rectangle rect, PictureBox pictureBox, double scaleFactor)
    {
        double maxWidth = pictureBox.Width / scaleFactor;
        double maxHeight = pictureBox.Height / scaleFactor;

        int x = Math.Max(rect.Left, 0);
        int y = Math.Max(rect.Top, 0);
        int width = (int)(Math.Min(rect.Right, maxWidth) - x);
        int height = (int)(Math.Min(rect.Bottom, maxHeight) - y);

        return new Rectangle(x, y, width, height);
    }
    public static Rectangle AlignRectangleToPictureBoxEdges(this Rectangle rect, PictureBox pictureBox, double scaleFactor)
    {
        double maxWidth = pictureBox.Width / scaleFactor;
        double maxHeight = pictureBox.Height / scaleFactor;

        int x = rect.Left;
        int y = rect.Top;
        int width = rect.Width;
        int height = rect.Height;

        if (rect.Left < 0) x = 0;
        if (rect.Top < 0) y = 0;
        if (rect.Right > maxWidth) x = (int)maxWidth - width;
        if (rect.Bottom > maxHeight) y = (int)maxHeight - height;

        return new Rectangle(x, y, width, height);
    }
}

