using RectSelector;
using System.Drawing;
using System.Windows.Forms;

namespace ProcScan
{
    public class PictureBoxMover
    {
        private PictureBox _pictureBox;
        private RectangleSelector _rectangleSelector;

        private bool _isMoving;
        private Point _lastMousePosition;

        public PictureBoxMover(PictureBox pictureBox, RectangleSelector rectangleSelector)
        {
            _pictureBox = pictureBox;
            _rectangleSelector = rectangleSelector;

            _pictureBox.MouseDown += PictureBox_MouseDown;
            _pictureBox.MouseMove += PictureBox_MouseMove;
            _pictureBox.MouseUp += PictureBox_MouseUp;
        }


        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if ( false && e.Button == MouseButtons.Left && !_rectangleSelector.IsMouseInsideRectangle(e.Location) && ! _rectangleSelector.IsAnyProcess())
            {
                _isMoving = true;
                _lastMousePosition = e.Location;
                _pictureBox.Cursor = Cursors.SizeAll;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMoving)
            {
                Point delta = new Point(e.X - _lastMousePosition.X, e.Y - _lastMousePosition.Y);
                _pictureBox.Left += delta.X;
                _pictureBox.Top += delta.Y;
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isMoving)
            {
                _isMoving = false;
                _pictureBox.Cursor = Cursors.Default;
            }
        }
    }
}
