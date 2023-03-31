using ProcScan;
using RectSelector;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

public class ZoomablePictureBox
{
    private readonly PictureBox _pictureBox;
    private double _zoomFactor;
    private const double ZoomIncrement = 0.05f;
    private const double MinZoomFactor = 0.1f;
    private const double MaxZoomFactor = 10.0f;

    private readonly RectangleSelector _rectangleSelector;
    private readonly PictureBoxMover _pictureBoxMover;

    public ZoomablePictureBox(RectangleSelector rectangleSelector)
    {
        _pictureBox = rectangleSelector.GetPictureBox();
        _rectangleSelector = rectangleSelector;
        _zoomFactor = 1.0f;

        _pictureBox.MouseWheel += PictureBox_MouseWheel;
        _pictureBoxMover = new PictureBoxMover(_pictureBox, _rectangleSelector);
    }

    private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
    {
        double oldZoomFactor = _zoomFactor;

        if (e.Delta > 0)
        {
            double newZoomFactor = _zoomFactor + ZoomIncrement * _zoomFactor;
            if (newZoomFactor <= MaxZoomFactor) // Проверьте, не превышает ли новый масштаб максимальное значение
            {
                _zoomFactor = newZoomFactor;
            }
        }
        else
        {
            _zoomFactor -= ZoomIncrement * _zoomFactor;
            _zoomFactor = Math.Max(_zoomFactor, MinZoomFactor);
        }

        double zoomRatio = _zoomFactor / oldZoomFactor;


        ZoomImage();
        _rectangleSelector.SetScaleFactor(_zoomFactor);
        SetPositionImage(zoomRatio);
    }

    private void SetPositionImage(double zoomRatio)
    {
        Point zoomCenter = _pictureBox.PointToClient(Cursor.Position);
        _pictureBox.Left -= (int)((zoomRatio - 1) * zoomCenter.X);
        _pictureBox.Top -= (int)((zoomRatio - 1) * zoomCenter.Y);
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
