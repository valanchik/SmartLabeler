using ProcScan;
using RectSelector;
using System;
using System.Drawing;
using System.Windows.Forms;

public class ZoomablePictureBox
{
    private PictureBox _pictureBox;
    private float _zoomFactor;
    private const float ZoomIncrement = 0.1f;

    private RectangleSelector _rectangleSelector;
    private PictureBoxMover _pictureBoxMover;

    public ZoomablePictureBox(PictureBox pictureBox, RectangleSelector rectangleSelector)
    {
        _pictureBox = pictureBox;
        _rectangleSelector = rectangleSelector;
        _zoomFactor = 1.0f;

        _pictureBox.MouseWheel += PictureBox_MouseWheel;
        _pictureBoxMover = new PictureBoxMover(_pictureBox, _rectangleSelector);
    }

    private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
    {
        float oldZoomFactor = _zoomFactor;

        if (e.Delta > 0)
        {
            _zoomFactor += ZoomIncrement;
        }
        else
        {
            _zoomFactor -= ZoomIncrement;
            _zoomFactor = Math.Max(_zoomFactor, 1.0f);
        }

        float zoomRatio = _zoomFactor / oldZoomFactor;

        Point zoomCenter = _pictureBox.PointToClient(Cursor.Position);

        ZoomImage();
        _pictureBox.Left -= (int)((zoomRatio - 1) * zoomCenter.X);
        _pictureBox.Top -= (int)((zoomRatio - 1) * zoomCenter.Y);
        _rectangleSelector.SetScaleFactor(_zoomFactor);

    }

    private void ZoomImage()
    {
        if (_pictureBox.Image != null)
        {
            int newWidth = (int)(_pictureBox.Image.Width * _zoomFactor);
            int newHeight = (int)(_pictureBox.Image.Height * _zoomFactor);

            if (newWidth > 0 && newHeight > 0)
            {
                _pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                _pictureBox.ClientSize = new Size(newWidth, newHeight);
            }
        }
    }
}
