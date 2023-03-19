
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RectSelector
{

    public class RectangleSelector 
    {
        private bool _isDrawing;
        private bool _isResizing;
        private bool _isMoving;
        private Point _startPoint;
        private readonly PictureBox _pictureBox;
        private readonly Label _label;
        private readonly Button _button;
        private int _selectedHandle;

        private ResizableRectangle _resizableRect;
        private List<ResizableRectangle> _resizableRectangles;
        private ResizableRectangle _selectedResizableRect;

        public RectangleSelector(PictureBox pictureBox, Label label, Button button)
        {
            _pictureBox = pictureBox;
            _label = label;
            _button = button;
            InitializeEventHandlers();
            ResetState();

            _resizableRect = new ResizableRectangle();
            _resizableRectangles = new List<ResizableRectangle>();
            
        }

        public bool IsAnyProcess()
        {
            return _isMoving || _isResizing || _isDrawing;
        }

        private void InitializeEventHandlers()
        {
            _pictureBox.MouseDoubleClick += PictureBox_MouseDoubleClick;
            _pictureBox.MouseMove += PictureBox_MouseMove;
            _pictureBox.MouseDown += PictureBox_MouseDown;
            _pictureBox.MouseUp += PictureBox_MouseUp;
            _pictureBox.Paint += PictureBox_Paint;
            _button.Click += Button_Click;
        }

        private void ResetState()
        {
            _isDrawing = false;
            _isResizing = false;
            _selectedHandle = -1;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            _isDrawing = true;
            _resizableRect = new ResizableRectangle();
            _resizableRectangles.Add(_resizableRect);
            _button.Enabled = false;
        }

        private void PictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = _resizableRectangles.Count - 1; i >= 0; i--)
            {
                if (_resizableRectangles[i].IsMouseInsideRectangle(e.Location))
                {
                    _resizableRectangles.RemoveAt(i);
                    _pictureBox.Invalidate();
                    break;
                }
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            HandleMouseDown(e);
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            _label.Text = $"{e.Location}";
            Point scaledLocation = new Point((int)(e.Location.X), (int)(e.Location.Y));

            if (_isDrawing && e.Button == MouseButtons.Left)
            {
                UpdateRectangleSize(scaledLocation);
            }
            else if (_isResizing && e.Button == MouseButtons.Left)
            {
                _selectedResizableRect.ResizeRectangle(_selectedHandle, _startPoint, scaledLocation);
                _startPoint = scaledLocation;
                _pictureBox.Invalidate();
            }
            else if (_isMoving && e.Button == MouseButtons.Left)
            {
                _selectedResizableRect.MoveRectangle(_startPoint, scaledLocation);
                _startPoint = scaledLocation;
                _pictureBox.Invalidate();
            }
            else
            {
                UpdateCursor(scaledLocation);
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDrawing)
            {
                StopDrawing();
            }

            if (_isResizing)
            {
                StopResizing();
            }

            if (_isMoving)
            {
                StopMoving();
            }

            UpdateLabel();
        }

        private void UpdateCursor(Point location)
        {
            _pictureBox.Cursor = GetCursorForLocation(location, out _selectedResizableRect);
        }

        private Cursor GetCursorForLocation(Point location, out ResizableRectangle selectedRect)
        {
            selectedRect = null;
            Point scaledLocation = new Point((int)(location.X ), (int)(location.Y));

            foreach (var rect in _resizableRectangles)
            {
                int handleIndex = rect.GetSelectedHandle(scaledLocation);
                if (handleIndex > -1)
                {
                    selectedRect = rect;
                    return rect.GetCursorForHandle(handleIndex);
                }
                else if (rect.IsMouseInsideRectangle(scaledLocation))
                {
                    selectedRect = rect;
                }
            }

            return selectedRect != null ? Cursors.SizeAll : Cursors.Default;
        }


        public bool IsMouseInsideRectangle(Point location)
        {
            return _resizableRect.IsMouseInsideRectangle(location);
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            foreach (var rect in _resizableRectangles)
            {
                rect.DrawRectangleAndHandles(e.Graphics);
            }
        }

        private void HandleMouseDown(MouseEventArgs e)
        {
            if (!_isDrawing && !_isResizing)
            {
                _selectedResizableRect = null;
                Point scaledLocation = new Point((int)(e.Location.X), (int)(e.Location.Y ));

                foreach (var rect in _resizableRectangles)
                {
                    int handleIndex = rect.GetSelectedHandle(scaledLocation);
                    if (handleIndex != -1)
                    {
                        _selectedResizableRect = rect;
                        _selectedHandle = handleIndex;
                        StartResizing(scaledLocation);
                        break;
                    }
                    else if (rect.IsMouseInsideRectangle(scaledLocation))
                    {
                        _selectedResizableRect = rect;
                    }
                }

                if (_selectedResizableRect != null && _selectedHandle == -1)
                {
                    StartMoving(scaledLocation);
                }
            }

            if (_isDrawing)
            {
                Point scaledLocation = new Point((int)(e.Location.X), (int)(e.Location.Y));
                StartDrawing(scaledLocation);
            }
        }


        private void StartDrawing(Point startPoint)
        {
            _startPoint = startPoint;
            _resizableRect.SetLocation(startPoint);
            _resizableRect.SetSize(new Size());
        }

        private void StopDrawing()
        {
            _isDrawing = false;
            _button.Enabled = true;
        }

        private void StartResizing(Point startPoint)
        {
            _isResizing = true;
            _startPoint = startPoint;
        }

        private void StopResizing()
        {
            _isResizing = false;
            _selectedHandle = -1;
        }

        private void StartMoving(Point startPoint)
        {
            _isMoving = true;
            _startPoint = startPoint;
        }

        private void StopMoving()
        {
            _isMoving = false;
        }

        private void UpdateLabel()
        {
            Rectangle rect = _resizableRect.GetRectangle();
            _label.Text = $"X: {rect.X}, Y: {rect.Y}, Ширина: {rect.Width}, Высота: {rect.Height}";
        }

        private void UpdateRectangleSize(Point endPoint)
        {
            int x = Math.Min(_startPoint.X, endPoint.X);
            int y = Math.Min(_startPoint.Y, endPoint.Y);
            int width = Math.Abs(_startPoint.X - endPoint.X);
            int height = Math.Abs(_startPoint.Y - endPoint.Y);

            _resizableRect.SetLocation(new Point(x, y));
            _resizableRect.SetSize(new Size(width, height));

            _pictureBox.Invalidate();
        }
        
    }
}
